using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedschemeMemberUpdate
{
    public partial class memberpage : System.Web.UI.Page
    {

        private IDbConnection db = new MySqlConnection(ConfigurationManager.AppSettings["MemberProfile.ConnectionString"]);
        private object memberprofile;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IdNumber = Session["IDNumber"] as string;
                if(!string.IsNullOrEmpty(IdNumber))
                {
                    BindDataToGridView();
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
        }

        private void BindDataToGridView()
        {
            string IdNumber = Session["IDNumber"] as string;
            string CompanyLogo = Session["CompanyLogo"] as string;

            DataTable dataTable = new DataTable();

            var membernumber = db.Query<MemberDetail>("SELECT MemberNumber FROM MedschemeMemberProfiles.MemberProfile WHERE IDNumber = '" + IdNumber + "' AND DepCode = '00'");

            foreach (var member in membernumber)
            {
                memberprofile = db.Query<MemberDetail>("SELECT * FROM MedschemeMemberProfiles.MemberProfile WHERE MemberNumber = '" + member.MemberNumber + "'");
            }

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


            try
            {
                GridView1.DataSource = memberprofile;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the DataKey values for the row being edited
            DataKey dataKey = GridView1.DataKeys[e.NewEditIndex];

            Session["DependantMemberNumber"] = dataKey["MemberNumber"].ToString();
            Session["DependantIDNumber"] = dataKey["IDNumber"].ToString();
  

            Response.Redirect("DepUpdate.aspx");
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the primary keys of the row being edited
            string MemberNumber = GridView1.DataKeys[e.RowIndex].Values["MemberNumber"].ToString();
            string IDNumber = GridView1.DataKeys[e.RowIndex].Values["IDNumber"].ToString();

            // Get all values from the GridViewRow
            GridViewRow row = GridView1.Rows[e.RowIndex];
            var values = GetValuesFromRow(row);

            // Example of how to use the values (assuming Column1 and Column2 are strings)
            string Name = values["Name"];
            string Surname = values["Surname"];
            string NickName = values["NickName"];
            string CellNumber= values["CellNumber"];
            string EmailAddress = values["EmailAddress"];
            string TaxRefNumber = values["TaxRefNumber"];

            try
            {
                db.Execute("UPDATE MedschemeMemberProfiles.MemberProfile SET Name = @Name, Surname = @Surname, NickName = @NickName, IDNumber = @IDNumber, CellNumber =@CellNumber, EmailAddress = @EmailAddress, TaxRefNumber = @TaxRefNumber, Updated = 'Y', UpdateDate = NOW() WHERE IDNumber = @IDNumber AND MemberNumber = @MemberNumber",
                    new { Name = Name, Surname = Surname, NickName = NickName, CellNumber = CellNumber, EmailAddress = EmailAddress, TaxRefNumber = TaxRefNumber, IDNumber = IDNumber, MemberNumber = MemberNumber });

                lblMessage.Text = "Member Updated Succesfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "We were unable to update the entry at this time please try again.";
                Response.Write("Error: " + ex.Message);
            }
            

            GridView1.EditIndex = -1;
            BindDataToGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindDataToGridView();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                var row = e.Row;

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Controls.Count > 0 && row.Cells[i].Controls[0] is TextBox textBox)
                    {
                        var columnValue = textBox.Text;

                        // Condition to make the cell read-only
                        if (!string.IsNullOrEmpty(textBox.Text))
                        {
                            textBox.ReadOnly = true;
                        }
                        else
                        {
                            textBox.ReadOnly = false;
                        }
                    }
                }
            }
        }

        private Dictionary<string, string> GetValuesFromRow(GridViewRow row)
        {
            var values = new Dictionary<string, string>();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                // Assuming all cells contain a TextBox control when in edit mode
                if (row.Cells[i].Controls.Count > 0 && row.Cells[i].Controls[0] is TextBox)
                {
                    var textBox = (TextBox)row.Cells[i].Controls[0];
                    var columnName = GridView1.Columns[i].HeaderText;
                    values[columnName] = textBox.Text;
                }
            }

            return values;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}