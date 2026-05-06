using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace SAWWiki
{

    /// <summary>
    /// Summary description for WikiBasePage
    /// </summary>
    public class WikiBasePage : System.Web.UI.Page
    {
        protected string pageName;        

        protected string GetPageName()
        {
            if (Request.Params["page"] == null || Request.Params["page"] == "")
                return ConfigurationManager.AppSettings["defaultPage"];
            else
                return HttpUtility.UrlDecode(Request.Params["page"].ToString());
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            HandleError(Server.GetLastError());
            Server.ClearError();
        }

        protected void HandleError(Exception ex)
        {               
            string errorMessage = "Page: " + Request.Url.ToString() +
                    "\nUser: " +
                    ((Request.IsAuthenticated) ? User.Identity.Name : Request.ServerVariables["REMOTE_HOST"].ToString()) + "\n" +
                    ex.Message + "\n" + ex.StackTrace;
            Response.Write(@"<table width=100% CellPadding=0 CellSpacing=0><tr><td class=""errormessage"">An error occurred in the system, and last operation may not have completed: <em>" +
                ex.Message + "</em></td></tr></table>");

            if (ConfigurationManager.AppSettings["loggingEnabled"].ToLower() == "true")
            {
                try
                {
                    switch (ConfigurationManager.AppSettings["logFile"].ToLower())
                    {
                        case "eventlog":
                            //Create the event log if not present
                            string logName = "SAWWikiLog";
                            string sourceName = "SAWWikiWebPage";
                            if (!EventLog.SourceExists(sourceName))
                            {
                                EventLog.CreateEventSource(sourceName, logName);
                            }
                            EventLog log = new EventLog();
                            log.Source = sourceName;
                            log.WriteEntry(errorMessage, EventLogEntryType.Error);
                            break;
                        default: //if not "eventlog", save it to the specified file
                            FileInfo f = new FileInfo(ConfigurationManager.AppSettings["logFile"]);
                            using (FileStream s = f.Open(FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
                            {
                                errorMessage = "**Error at " + DateTime.Now.ToString() + " : " + errorMessage + "\r\n\r\n";
                                s.Write(System.Text.ASCIIEncoding.UTF8.GetBytes(errorMessage), 0, errorMessage.Length);
                                s.Flush();
                            }
                            break;
                    }
                }
                catch { } //do nothing if logging fails
                        

            }                            

        }

    }

}
