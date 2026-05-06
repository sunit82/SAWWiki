using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAWWiki
{
    public partial class WikiLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
            }
            //Panel1.DefaultButton = LoginView1.FindControl("login1").FindControl("LoginButton").ClientID;

        }
    }
}
