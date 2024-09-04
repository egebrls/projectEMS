using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projectEMS
{
    public partial class Login : System.Web.UI.Page
    {
        connection dbConn = new connection();

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spLogin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (userCount > 0)
                    {
                        Session["LoggedUserEmail"] = txtEmail.Text;
                        Session["LoggedUserRole"] = getRole(txtEmail.Text);
                        Session["LoggedUserPass"] = txtPassword.Text;
                        Session["LoggedEmpID"] = getID(txtEmail.Text);
                        Session["LoggedEmpDep"] = getDep(txtEmail.Text);

                        if (txtPassword.Text == "00000000")
                        {
                            Response.Redirect("ChangePass.aspx");
                        }
                        else
                        {
                            Response.Redirect("Profile.aspx");
                        }
                    }
                    else
                    {
                        lblError.Text = "Invalid e-mail or password";
                    }
                    con.Close();
                }
            }
        }

        private string getRole(string userEmail)
        {
            string userRole = string.Empty;
            string query = "SELECT Role from tblUsers WHERE eMail = @email";

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@email", userEmail);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userRole = reader["Role"].ToString();
                        }
                    }
                }
            }
            return userRole;
        }

        private int getID(string userEmail)
        {
            int employeeID;
            string query = "SELECT EmployeeID from tblEmployees WHERE eMail = @email";
            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@email", userEmail);

                    var result = cmd.ExecuteScalar();

                    int.TryParse(result.ToString(), out employeeID );
                }
            }
            return employeeID;
        }

        private string getDep(string userEmail)
        {
            string empDepartment;
            string query = "SELECT DepartmentName from tblEmployees WHERE eMail = @email";
            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@email", userEmail);

                    var result = cmd.ExecuteScalar();
                    empDepartment = result.ToString();
                }
            }
            return empDepartment;
        }
    }
}