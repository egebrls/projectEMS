using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace projectEMS
{
    public partial class HomePage : System.Web.UI.Page
    {
        connection dbConn = new connection();
        DataTable getdb = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getEmployeeDetails();

                getdb = getEmployeeProjects();
                gvEmpProjects.DataSource = getdb;
                gvEmpProjects.DataBind();
            }
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePass.aspx");
        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {

            pnlEditInfo.Visible = !pnlEditInfo.Visible;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == Session["LoggedUserPass"].ToString())
            {

                string fName = txtEditFirstName.Text;
                string lName = txtEditLastName.Text;
                string phone = txtEditPhone.Text;
                string email = txtEditEmail.Text;

                updateDetails(fName, lName, phone, email);
                getEmployeeDetails();

                lblSaveMsg.Visible = false;
                lblSaveMsg.Text = "Information updated !";
                lblSaveMsg.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "HideSaveMsg", "setTimeout(hideMessage, 3000);", true);

                txtEditFirstName.Text = string.Empty;
                txtEditLastName.Text = string.Empty;
                txtEditPhone.Text = string.Empty;
                txtEditEmail.Text = string.Empty;
            }
            else
            {
                lblSaveMsg.Visible = false;
                lblSaveMsg.Text = "Wrong Password !";
                lblSaveMsg.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "HideSaveMsg", "setTimeout(hideMessage, 3000);", true);
            }
        }

        private void getEmployeeDetails()
        {
            if (Session["LoggedEmpID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int.TryParse(Session["LoggedEmpID"].ToString(), out int empIDint);

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spProfile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "getDetails");
                    cmd.Parameters.AddWithValue("@empID", empIDint);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string imgPath = reader["ProfilePicURL"].ToString();
                            string empID = reader["EmployeeID"].ToString();
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();
                            string phone = reader["Phone"].ToString();
                            string email = reader["eMail"].ToString();
                            string depName = reader["DepartmentName"].ToString();
                            string position = reader["Position"].ToString();
                            string salary = reader["Salary"].ToString();
                            DateTime hireDate = (DateTime)reader["HireDate"];

                            imgProfile.ImageUrl = imgPath;
                            lblEmployeeID.Text = "<strong>Employee ID: </strong>" + empID;
                            lblFirstName.Text = "<strong>First Name: </strong>" + firstName;
                            lblLastName.Text = "<strong>Last Name: </strong>" + lastName;
                            lblPhone.Text = "<strong>Phone: </strong>" + phone;
                            lblEmail.Text = "<strong>E-Mail: </strong>" + email;
                            lblDepartment.Text = "<strong>Department: </strong>" + depName;
                            lblPosition.Text = "<strong>Position: </strong>" + position;
                            lblSalary.Text = "<strong>Salary: </strong>" + salary.ToString();
                            lblHireDate.Text = "<strong>Hire Date: </strong>" + hireDate.ToString("dd-MM-yyyy");
                        }
                        conn.Close();
                    }
                }
            }
        }

        private void updateDetails(string fName = null, string lName = null, string phone = null, string email = null)
        {
            int.TryParse(Session["LoggedEmpID"].ToString(), out int empID);

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spProfile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "updateProfile");
                    cmd.Parameters.AddWithValue("@empID", empID);
                    if (!string.IsNullOrEmpty(fName))
                    {
                        cmd.Parameters.AddWithValue("@firstName", fName);
                    }
                    if (!string.IsNullOrEmpty(lName))
                    {
                        cmd.Parameters.AddWithValue("@lastName", lName);
                    }
                    if (!string.IsNullOrEmpty(phone))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                    }
                    if (!string.IsNullOrEmpty(email))
                    {
                        cmd.Parameters.AddWithValue("@newEmail", email);
                    }
             
                    cmd.ExecuteNonQuery();
                    conn.Close();
                 
                }
            }         
        }

        public DataTable getEmployeeProjects()
        {
            DataTable dt = new DataTable();

            int.TryParse(Session["LoggedEmpID"].ToString(), out int empIDint);

            using(SqlConnection conn = dbConn.dbBag())
            {
                using(SqlCommand cmd = new SqlCommand("spProjects", conn))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "getEmpAsg");
                    cmd.Parameters.AddWithValue("@empID", empIDint);
                    cmd.Connection = conn;
                    using(SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                        sda.Dispose();
                    }
                    cmd.Dispose();

                }
                conn.Close ();
                conn.Dispose();               
            }
            return dt;
        }
    }
}