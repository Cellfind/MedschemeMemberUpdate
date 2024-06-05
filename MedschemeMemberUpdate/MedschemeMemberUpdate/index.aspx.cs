using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedschemeMemberUpdate
{
    public partial class index : System.Web.UI.Page
    {

        private IDbConnection db = new MySqlConnection(ConfigurationManager.AppSettings["MemberProfile.ConnectionString"]);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string idnumber = txtidnumber.Text;

            bool Validaterecord = Convert.ToBoolean(db.ExecuteScalar("SELECT COUNT(1) FROM MedschemeMemberProfiles.MemberProfile WHERE IDNumber = '" + idnumber + "' AND DepCode = 00"));

           if (Validaterecord)
           {
                // Redirect to a secure page after successful login
                Session["IDNumber"] = idnumber;
                Response.Redirect("MemberUpdate.aspx");
            }
            else
            {
                // Display error message
                lblMessage.Text = "Invalid Entry.";
            }
        }
    }
}