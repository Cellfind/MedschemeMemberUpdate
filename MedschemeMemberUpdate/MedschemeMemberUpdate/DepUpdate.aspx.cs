using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedschemeMemberUpdate
{
    public partial class DepUpdate : System.Web.UI.Page
    {
        private IDbConnection db = new MySqlConnection(ConfigurationManager.AppSettings["MemberProfile.ConnectionString"]);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string IdNumber;
        public string membernumber;

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

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdNumber = Session["DependantIDNumber"] as string;
                membernumber = Session["DependantMemberNumber"] as string;

                if (!string.IsNullOrEmpty(IdNumber))
                {
                    BindDataToGridView();
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
            else
            { 
            
            
            }    
        }

        private void BindDataToGridView()
        {
            string IdNumber = Session["DependantIDNumber"] as string;
            string CompanyLogo = Session["CompanyLogo"] as string;

            var memberprofile = db.Query<MemberDetail>("SELECT * FROM MedschemeMemberProfiles.MemberProfile WHERE IDNumber = '" + IdNumber + "' AND MemberNumber = '" + membernumber + "'");

            try
            {
                string CompanyURL = Convert.ToString(db.ExecuteScalar($"SELECT CompanyLogo FROM " +
                    $"`MedschemeMemberProfiles`.`MemberProfile`" +
                    $" INNER JOIN `MedschemeMemberProfiles`.`Company`" +
                    $" ON (`MemberProfile`.`SchemeCode` = `Company`.`SchemeCode`) " +
                    $" WHERE MemberProfile.`IDNumber` = '" + IdNumber + "'"));

                Image1.ImageUrl = CompanyURL;
            }
            catch
            {
                Image1.ImageUrl = "~/images/Medscheme-Logo.jpg";
            }

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("FieldName");
            dataTable.Columns.Add("FieldType");

            dataTable.Rows.Add("MemberNumber", "TextBox");
            dataTable.Rows.Add("Name", "TextBox");
            dataTable.Rows.Add("Surname", "TextBox");
            dataTable.Rows.Add("NickName", "TextBox");
            dataTable.Rows.Add("IDNumber", "TextBox");
            dataTable.Rows.Add("CellNumber", "TextBox");
            dataTable.Rows.Add("EmailAddress", "TextBox");
            dataTable.Rows.Add("TaxRefNumber", "TextBox");

            dataSet.Tables.Add(dataTable);

            foreach (var value in memberprofile)
            {
                // Iterate through each row in the dataset and create controls
                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {
                        string fieldName = row["FieldName"].ToString();

                        if (fieldName == "MemberNumber")
                        {
                            MemberNumbertxt.Text = value.MemberNumber;

                            if (!string.IsNullOrEmpty(value.MemberNumber))
                            {
                                MemberNumbertxt.Enabled = false;
                            }

                        }
                        else if (fieldName == "Name")
                        {
                            Nametxt.Text = value.Name;

                            if (!string.IsNullOrEmpty(value.Name))
                            {
                                Nametxt.Enabled = false;
                            }
                        }
                        else if (fieldName == "Surname")
                        {
                            Surnametxt.Text = value.Surname;

                            if (!string.IsNullOrEmpty(value.Surname))
                            {
                                Surnametxt.Enabled = false;
                            }
                        }
                        else if (fieldName == "NickName")
                        {
                            NickNametxt.Text = value.NickName;

                            if (!string.IsNullOrEmpty(value.NickName))
                            {
                                NickNametxt.Enabled = false;
                            }
                        }
                        else if (fieldName == "IDNumber")
                        {
                            IDNumbertxt.Text = value.IDNumber;

                            if (!string.IsNullOrEmpty(value.IDNumber))
                            {
                                IDNumbertxt.Enabled = false;
                            }
                        }
                        else if (fieldName == "CellNumber")
                        {
                            CellNumbertxt.Text = value.CellNumber;

                            if (!string.IsNullOrEmpty(value.CellNumber))
                            {
                                CellNumbertxt.Enabled = false;
                            }
                        }
                        else if (fieldName == "EmailAddress")
                        {
                            EmailAddresstxt.Text = value.EmailAddress;

                            if (!string.IsNullOrEmpty(value.EmailAddress))
                            {
                                EmailAddresstxt.Enabled = false;
                            }
                        }
                        else if (fieldName == "TaxRefNumber")
                        {
                            TaxRefNumbertxt.Text = value.TaxRefNumber;

                            if (!string.IsNullOrEmpty(value.TaxRefNumber))
                            {
                                TaxRefNumbertxt.Enabled = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                }
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Example of how to use the values (assuming Column1 and Column2 are strings)
            string Name = Nametxt.Text;
            string Surname = Surnametxt.Text;
            string NickName = NickNametxt.Text;
            string CellNumber = CellNumbertxt.Text;
            string EmailAddress = EmailAddresstxt.Text;
            string TaxRefNumber = TaxRefNumbertxt.Text;

            try
            {
                db.Execute("UPDATE MedschemeMemberProfiles.MemberProfile SET Name = @Name, Surname = @Surname, NickName = @NickName, IDNumber = @IDNumber, CellNumber =@CellNumber, EmailAddress = @EmailAddress, TaxRefNumber = @TaxRefNumber, Updated = 'Y', UpdateDate = NOW() WHERE IDNumber = @IDNumber AND MemberNumber = @MemberNumber",
                    new { Name = Name, Surname = Surname, NickName = NickName, CellNumber = CellNumber, EmailAddress = EmailAddress, TaxRefNumber = TaxRefNumber, IDNumber = IDNumbertxt.Text, MemberNumber = MemberNumbertxt.Text });

                lblMessage.Text = "Member Updated Succesfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "We were unable to update the entry at this time please try again.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberUpdate.aspx");
        }
    }
}