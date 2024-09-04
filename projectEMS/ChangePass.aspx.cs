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
    public partial class ChangePass : System.Web.UI.Page
    {
        connection dbConn = new connection();
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=dbEMS;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            string userEmail = Session["LoggedUserEMail"].ToString();
            string storedPass = string.Empty;
            string oldPass = txtOldPassword.Text;
            string newPass = txtNewPassword.Text;
            string confirmPass = txtConfirmPassword.Text;


            using (SqlConnection con = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Password FROM tblUsers WHERE eMail = @email", con))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@email", userEmail);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            storedPass = reader["Password"].ToString();
                        }
                    }


                    if (storedPass != null && oldPass == storedPass)
                    {
                        if (newPass == confirmPass)
                        {
                            if (newPass != oldPass)
                            {
                                using (SqlCommand updateCommand = new SqlCommand("UPDATE tblUsers SET Password = @NewPassword WHERE eMail = @email", con))
                                {                                   
                                    updateCommand.Parameters.Clear();                                    
                                    updateCommand.Parameters.AddWithValue("@NewPassword", newPass);
                                    updateCommand.Parameters.AddWithValue("@email", userEmail);
                                    updateCommand.ExecuteNonQuery();
                                    con.Close();
                                }

                                Session["LoggedUserPass"] = newPass;

                                lblMessage.Text = "Password updated";

                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAnchor", "setTimeout(visibleAnchor, 2000);", true);
                            }
                            else
                            {
                                lblMessage.Text = "New password can not be the same as old password.";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "New passwords do not match.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Old password is incorrect.";
                    }

                    
                }

            }

        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}