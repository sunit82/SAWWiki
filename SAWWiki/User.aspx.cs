using System;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SAWWiki
{
    public partial class User : WikiBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Set up the user roles if they do not currently exist
            if (!Roles.RoleExists("Administrator")) Roles.CreateRole("Administrator");
            if (!Roles.RoleExists("Editor")) Roles.CreateRole("Editor");
            if (!Roles.RoleExists("Reader")) Roles.CreateRole("Reader");

            if (Request.IsAuthenticated)
            {
                ChangePassword1.Visible = true;
                CreateUserWizard1.Visible = false;
                PasswordRecovery1.Visible = false;                
            }
            else
            {
                ChangePassword1.Visible = false;
                if (Request.QueryString["mode"] == "forgot")
                {
                    CreateUserWizard1.Visible = false;
                    PasswordRecovery1.Visible = true;
                }
                else
                {
                    CreateUserWizard1.Visible = true;
                    PasswordRecovery1.Visible = false;
                }
            }
        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            try
            {
                string[] roles = ConfigurationManager.AppSettings["defaultUserRoles"].Split(';');
                string username = ((CreateUserWizard)sender).UserName;
                foreach (string role in roles)
                {
                    Roles.AddUserToRole(username, role);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }
}
