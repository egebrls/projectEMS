using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projectEMS
{
    public partial class master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUserEmail"] == null || Session["LoggedEmpID"] == null || Session["LoggedUserRole"].ToString() == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }

        protected void btnLogout_Click (object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}