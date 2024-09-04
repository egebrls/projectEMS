using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace projectEMS
{
    public partial class projects : System.Web.UI.Page
    {
        connection dbConn = new connection();
        DataTable getdb = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string userRole = Session["LoggedUserRole"] as string;
                if (userRole == "General Manager")
                {
                    employeeDropdownAdd();
                }
                if (userRole == "Manager")
                {
                    string managerDep = Session["LoggedEmpDep"] as string;
                    employeeDropdownAdd(managerDep);
                }

                getdb = getProjects();
                repeaterProjects.DataSource = getdb;
                repeaterProjects.DataBind();
                if (userRole == "General Manager")
                {
                    imgbtnAddPrj.Visible = true;
                }
            }
        }

        protected void btnPrjDetails_Click(object sender, EventArgs e)
        {
            txtNewAsgName.Text = string.Empty;
            txtNewAsgStart.Text = string.Empty;
            txtNewAsgEnd.Text = string.Empty;
            ddlNewAsgEmp.SelectedIndex = 0;
            lblErrorMsg.Visible = false;
            lblAddAsgMsg.Visible = false;
            pnlAddAsg.Visible = false;
            btnOpenAddAsg.Visible = false;
            Button clickedButton = (Button)sender;
            int projectID = Convert.ToInt32(clickedButton.CommandArgument);
            Session["SelectedPrjID"] = projectID;
            DataTable dt = getProjectDetails(projectID);
            string prjStatus = getPrjStatus(projectID);
            string projectName = getPrjName(projectID);
            lblAsgPrjName.Text = projectName;
            repeaterAsg.DataSource = dt;
            repeaterAsg.DataBind();

            if (Session["LoggedUserRole"].ToString() == "General Manager" || Session["LoggedUserRole"].ToString() == "Manager")
            {
                if (prjStatus != "COMPLETED")
                {
                    btnOpenAddAsg.Visible = true;
                }

            }
            pnlPrjDetails.Visible = true;
        }

        protected void imgbtnCloseDetails_Click(object sender, EventArgs e)
        {
            pnlPrjDetails.Visible = false;
            pnlAddAsg.Visible = false;
        }

        protected void imgbtnCloseAsg_Click(object sender, EventArgs e)
        {
            txtNewAsgName.Text = string.Empty;
            txtNewAsgStart.Text = string.Empty;
            txtNewAsgEnd.Text = string.Empty;
            ddlNewAsgEmp.SelectedIndex = 0;
            lblErrorMsg.Visible = false;
            lblAddAsgMsg.Visible = false;
            pnlAddAsg.Visible = false;
            pnlPrjDetails.Visible = true;
            
        }

        protected void btnOpenAddAsg_Click(object sender, EventArgs e)
        {
            pnlPrjDetails.Visible = false;
            pnlAddAsg.Visible = true;
        }

        protected void btnAddNewAsg_Click(Object sender, EventArgs e)
        {

            string newAsgEmpID = ddlNewAsgEmp.SelectedValue;

            if (!string.IsNullOrEmpty(newAsgEmpID))
            {
                if (int.TryParse(newAsgEmpID, out int selectedEmpID))
                {
                    string newAsgName = txtNewAsgName.Text;
                    string newAsgStart = txtNewAsgStart.Text;
                    string newAsgEnd = txtNewAsgEnd.Text;

                    if (string.IsNullOrEmpty(newAsgName) || string.IsNullOrEmpty(newAsgStart) || string.IsNullOrEmpty(newAsgEnd))
                    {
                        lblErrorMsg.Text = "Please fill all the required fields.";
                        lblErrorMsg.Visible = true;
                    }
                    else
                    {
                        addAssignment(newAsgName, Session["SelectedPrjID"].ToString(), selectedEmpID.ToString(), newAsgStart, newAsgEnd);
                        txtNewAsgName.Text = string.Empty;
                        ddlNewAsgEmp.SelectedIndex = 0;
                        txtNewAsgStart.Text = string.Empty;
                        txtNewAsgEnd.Text = string.Empty;
                        lblErrorMsg.Visible = false;
                        lblAddAsgMsg.Visible = false;
                        lblAddAsgMsg.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "HideAddAsgMsg", "setTimeout(hideAsgAddMessage, 3000);", true);
                    }
                }
                else
                {
                    lblErrorMsg.Text = "Pick an employee";
                    lblErrorMsg.Visible = true;
                }
            }
            else
            {
                lblErrorMsg.Text = "Pick an employee";
                lblErrorMsg.Visible = true;
            }

        }

        public DataTable getProjects()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tblProjects";

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);

                        sda.Dispose();
                    }
                    cmd.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable getProjectDetails(int projectID)
        {
            DataTable dataTable = new DataTable();
            

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spProjects", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "getPrjDetails");
                    cmd.Parameters.AddWithValue("@projectID", projectID);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dataTable);
                        sda.Dispose();
                    }
                    using (SqlCommand projectNameCmd = new SqlCommand("SELECT ProjectName FROM tblProjects WHERE ProjectID = @projectID", conn))
                    {
                        projectNameCmd.Parameters.AddWithValue("@projectID", projectID);
                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dataTable;
        }

        private string getPrjName(int projectID)
        {
            string projectName = string.Empty;
            string query = "SELECT ProjectName from tblProjects WHERE ProjectID = @projectid";

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@projectid", projectID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            projectName = reader["ProjectName"].ToString();
                        }
                    }
                }
            }

            return projectName;
        }

        private string getPrjStatus(int projectID)
        {
            string projectStatus = string.Empty;
            string query = "SELECT PrjStatus from tblProjects WHERE ProjectID = @projectid";

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@projectid", projectID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            projectStatus = reader["PrjStatus"].ToString();
                        }
                    }
                }
            }
            return projectStatus;
        }

        private void employeeDropdownAdd(string managerDep = null)
        {
            ddlNewAsgEmp.Items.Clear();
            ddlNewAsgEmp.Items.Add(new ListItem("", ""));
            using (SqlConnection conn = dbConn.dbBag())
            {
                string query = "SELECT EmployeeID, FirstName, LastName FROM tblEmployees";
                if (managerDep != null)
                {
                    query += " WHERE DepartmentName = @depName";
                }
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Clear();
                    if (!string.IsNullOrEmpty(managerDep))
                    {
                        cmd.Parameters.AddWithValue("@depName", managerDep);

                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullName =  reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                            string employeeID = reader["EmployeeID"].ToString();
                            ddlNewAsgEmp.Items.Add(new ListItem(fullName, employeeID));
                        }
                    }
                }
            }
        }

        private void addAssignment(string asgName, string projectID, string empID, string asgStart, string asgEnd)
        {
            try {
                using (SqlConnection conn = dbConn.dbBag())
                {
                    using (SqlCommand cmd = new SqlCommand("spProjects", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@action", "addAsg");
                        cmd.Parameters.AddWithValue("@asgName", asgName);
                        cmd.Parameters.AddWithValue("@projectID", projectID);
                        cmd.Parameters.AddWithValue("@empID", empID);
                        cmd.Parameters.AddWithValue("@asgStart", Convert.ToDateTime(asgStart));
                        cmd.Parameters.AddWithValue("@asgEnd", Convert.ToDateTime(asgEnd));
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

            catch(SqlException ex)
            {
                if(ex.Number == 51000)
                {
                    lblSqlEx.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "HideSqlMsg", "setTimeout(hideAsgErrorMessage, 3000);", true);
                }
                else if(ex.Number == 52000)
                {
                    lblSqlEx.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "HideSqlMsg", "setTimeout(hideAsgErrorMessage, 3000);", true);
                }
            }
            
        }
    }
}