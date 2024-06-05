using Dapper;
using log4net;
using MySql.Data.MySqlClient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedschemeMemberUpdateSMSService
{
    public class Sender : IJob
    {
        private static ILog log = LogManager.GetLogger(typeof(Program));
        private IDbConnection db = new MySqlConnection(ConfigurationManager.AppSettings["MemberProfile.ConnectionString"]);

        public int starthour = Convert.ToInt32(ConfigurationManager.AppSettings["starthour"]);
        public int endhour = Convert.ToInt32(ConfigurationManager.AppSettings["endhour"]);

        public int batchlimit = Convert.ToInt32(ConfigurationManager.AppSettings["batchlimit"]);

        public class MemberDetail
        {
            public int ID { get; set; }
            public string SchemeCode { get; set; }
            public string MemberNumber { get; set; }
            public string DepCode { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string NickName { get; set; }
            public string IDNumber { get; set; }
            public string CellNumber { get; set; }
            public string EmailAddress { get; set; }
            public string TaxRefNumber { get; set; }
            public string Updated { get; set; }

        }

        public class CompanyDetail
        {
            public int ID { get; set; }
            public string SchemeCode { get; set; }
            public string CompanyLogo { get; set; }
            public string SMSMessage { get; set; }
        }


        public class SMSDetail
        {
            public int ID { get; set; }
            public int CompanyID { get; set; }
            public string SMSUser { get; set; }
            public string SMSPassword { get; set; }
        }

        public void Execute(IJobExecutionContext context)
        {
            log.Info("Starting Checks");

            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Set the start and end times for the range (5am and 6am)
            DateTime startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, starthour, 0, 0);
            DateTime endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, endhour, 0, 0);

            // Check if the current time is between the start and end times
            bool isBetweengiventime = (currentTime >= startTime && currentTime < endTime);

            if (isBetweengiventime)
            {
                var mainmemeberdetail = db.Query<MemberDetail>($"SELECT * FROM MemberProfile WHERE DepCode = '00' AND SMSSent = 0 LIMIT {batchlimit}");

                if (mainmemeberdetail.Count() > 0)
                {
                    foreach (var member in mainmemeberdetail)
                    {
                        var companyDetail = db.Query<CompanyDetail>($"SELECT * FROM Company WHERE SchemeCode = '{member.SchemeCode}'");

                        if (companyDetail.Count() > 0)
                        {
                            foreach (var company in companyDetail)
                            {
                                var smsdetails = db.Query<SMSDetail>($"SELECT * FROM SMSConfig WHERE CompanyID = '{company.ID}'");

                                if (smsdetails.Count() > 0)
                                {
                                    foreach (var smsconfig in smsdetails)
                                    {
                                        cfgateway.Service sms = new cfgateway.Service();

                                        try
                                        {
                                            string mobilenumber = member.CellNumber.Substring(member.CellNumber.Length - 9);
                                            int messageid = sms.SendSMSMessageSingleRefUP(smsconfig.SMSUser, smsconfig.SMSPassword, "27"+ mobilenumber, company.SMSMessage, member.SchemeCode + member.MemberNumber);

                                            if (messageid != 0)
                                            {
                                                db.Execute("UPDATE MedschemeMemberProfiles.MemberProfile SET SMSSent = 1 WHERE IDNumber = @IDNumber AND MemberNumber = @MemberNumber AND ID = @ID",
                                                                        new { IDNumber = member.IDNumber, MemberNumber = member.MemberNumber, ID = member.ID });

                                            }
                                            else
                                            {
                                                log.Info(@"SMS Could not be sent to" + member.CellNumber + "Member Profile ID " + member.ID);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            log.Error(@"SMS Could not be sent to " + ex.Message);
                                        }

                                    }
                                }
                                else
                                {
                                    log.Info(@"No SMS Config Details Found for " + member.SchemeCode);
                                }
                            }
                        }
                        else
                        {
                            log.Info(@"No Company Details Found for " + member.SchemeCode);
                        }
                    }
                }
                else
                {
                    log.Info(@"No New sms messages to send");
                }

            }
            else
            {
                log.Info(@"No New sms messages to send, time outside of allowed hours.");
            }
        }
    }
}
