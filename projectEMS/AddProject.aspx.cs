using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projectEMS
{
    public partial class AddProject : System.Web.UI.Page
    {
        connection dbConn = new connection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUserEmail"] == null || Session["LoggedEmpID"] == null || Session["LoggedUserRole"].ToString() == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            string prjName = txtProjectName.Text;
            string prjStart = txtProjectStart.Text;
            string prjEnd = txtProjectEnd.Text;

            if(string.IsNullOrEmpty(prjName) || string.IsNullOrEmpty(prjStart) || string.IsNullOrEmpty(prjEnd))
            {
                lblError.Visible = true;
                return;
            }

            addProject(prjName, prjStart, prjEnd);
            lblError.Visible = false;
            txtProjectName.Text = string.Empty;
            txtProjectStart.Text = string.Empty;
            txtProjectEnd.Text = string.Empty;
            lblPrjAdd.Visible = false;
            lblPrjAdd.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "HidePrjAddMessage", "setTimeout(hideMessage, 3000);", true);

        }

        private void addProject(string projectName, string startDate, string endDate)
        {
            using(SqlConnection conn = dbConn.dbBag())
            {
                using(SqlCommand cmd = new SqlCommand("spProjects", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "addPrj");
                    cmd.Parameters.AddWithValue("@prjName", projectName);
                    cmd.Parameters.AddWithValue("@prjStart", startDate);
                    cmd.Parameters.AddWithValue("@prjEnd", endDate);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}