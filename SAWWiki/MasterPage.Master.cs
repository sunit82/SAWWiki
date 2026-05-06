using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAWWiki
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public int MenuIndex
        {
            get
            {
                if (Session["menuIndex"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Session["menuIndex"]);
            }
            set
            {
                Session["menuIndex"] = value;
            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
              
            }
            //handle the page header, etc.
            Page.Title = ConfigurationManager.AppSettings["pageTitle"].ToString();
            //lblHeader.Text = ConfigurationManager.AppSettings["pageTitle"].ToString();

            //hide the extra web part zones if not needed.
            if (!mgrMaster.Personalization.CanEnterSharedScope)
            {
                DisplayMenu1.Visible = false;
                if (wpzLeftTop.WebParts.Count < 1) wpzLeftTop.Visible = false;
                if (wpzRightTop.WebParts.Count < 1) wpzRightTop.Visible = false;
                if (wpzCenterBottom.WebParts.Count < 1) wpzCenterBottom.Visible = false;
                if (wpzLeftBottom.WebParts.Count < 1) wpzLeftBottom.Visible = false;
                if (wpzRightBottom.WebParts.Count < 1) wpzRightBottom.Visible = false;
            }
            else
            {
                DisplayMenu1.Visible = true;
                wpzLeftTop.Visible = true;
                wpzRightTop.Visible = true;
                wpzCenterBottom.Visible = true;
                wpzLeftBottom.Visible = true;
                wpzRightBottom.Visible = true;
            }
            if (ConfigurationManager.AppSettings["betaFlag"] == "ON")
            {
                lblBeta.Visible = true;
                lblBetaWarning.Visible = true;
            }
            else
            {
                lblBeta.Visible = false;
                lblBetaWarning.Visible = false;
            }
            litFooter.Text = "<span class=menutextindent>" + ConfigurationManager.AppSettings["pageFooter"].ToString()
                + "</span>";
            
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            string errorMessage = "Error occurred" + Server.GetLastError();

            Server.ClearError();
            string LogName = "SAWWikiLog";
            string SourceName = "SAWWikiWebPage";
            if (!EventLog.SourceExists(SourceName))
            {

                EventLog.CreateEventSource(SourceName, LogName);

            }
            EventLog MyLog = new EventLog();
            MyLog.Source = SourceName;
            MyLog.WriteEntry(errorMessage, EventLogEntryType.Error);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            mainMenu.Items[MenuIndex].Attributes["id"] = "current";
            base.Render(writer);
        }
        protected void mainMenu_Click(object sender, BulletedListEventArgs e)
        {
            MenuIndex = e.Index;
            Response.Redirect(mainMenu.Items[MenuIndex].Value);
        }
    }
}
