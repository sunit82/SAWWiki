using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;
namespace SAWWiki
{
    public partial class Diff : WikiBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string page = "";
            int v1 = 0;
            int v2 = 0;

            try
            {
                page = Request.Params["page"].ToString();
                v1 = int.Parse(Request.Params["v1"].ToString());
                v2 = int.Parse(Request.Params["v2"].ToString());
            }
            catch
            {
                HandleError(new Exception("This page needs a page and version parameters passed to it.  If you are " +
                    "reading this message now, these parameters were not properly supplied.  Please return to " +
                    "the history view for a wiki page and select the 'Show Differences' option from there, or " +
                    "contact your system administrator"));
                Response.End();
                return;
            }
            if (v2 > v1)
            {
                v1 = v1 + v2;
                v2 = v1 - v2;
                v1 = v1 - v2;
            }

            lblVersion1.Text = v1.ToString();
            lblVersion2.Text = v2.ToString();
            lnkPage.Text = page;

            //now get the page text from the wiki

            string page1Text = "";
            string page2Text = "";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sawWikiConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("w_GetPageByVersion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = page;
                cmd.Parameters.Add("@Version", SqlDbType.Int).Value = v1;
                SqlDataReader reader;
                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                        page1Text = reader["PageText"].ToString();
                    reader.Close();
                    cmd.Parameters["@Version"].Value = v2;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                        page2Text = reader["PageText"].ToString();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            }

            //display the results
            string results = HTMLDiff(page1Text, page2Text);
            //HACK:  handle UL and OL elements and headers
            results = Regex.Replace(results, @"(<span class=text(?:added|missing)>)\s?(\++|#+|!+)", "$2$1", RegexOptions.Compiled);

            results = WikiParser.GenerateWikiText(results);
            litVersion1.Text = (results).Replace("&lt;span class=textadded&gt;",
                "<span class=textadded>").Replace("&lt;/span&gt;", "</span>").Replace("&lt;span class=textmissing&gt;",
                "<span class=textmissing>");
        }
        private string HTMLDiff(string text1, string text2)
        {
            text1 = text1.Replace("\r", "");
            text2 = text2.Replace("\r", "");
            char[] crlf = ((string)("\n")).ToCharArray();
            string lineBreak = "\n";

            string[] t1Lines = text1.Split(crlf);
            string[] t2Lines = text2.Split(crlf);

            StringBuilder t1Final = new StringBuilder();

            int lastPos = 0;
            bool foundLine = false;

            for (int count = 0; count < t1Lines.Length; count++)
            {
                for (int count2 = lastPos; count2 < t2Lines.Length; count2++)
                {

                    if ((t1Lines[count] == t2Lines[count2]) && (t1Lines[count] != "" && t2Lines[count2] != ""))
                    {
                        //if there are lines in doc 2 that weren't in doc 1, mark
                        if ((count2 - lastPos) > 0)
                        {
                            for (int i = lastPos; i < count2; i++)
                                if (t2Lines[i] != "") t1Final.Append("<span class=textmissing>" + t2Lines[i] + "</span>" + lineBreak);
                        }

                        t1Final.Append(t1Lines[count] + lineBreak);

                        lastPos = count2 + 1;
                        foundLine = true;
                        break;
                    }
                }

                if (!foundLine) //mark lines in doc1 as additions                            
                    if (t1Lines[count] != "")
                        t1Final.Append("<span class=textadded>" + t1Lines[count] + "</span>" + lineBreak);
                    else
                        t1Final.Append(crlf);

                foundLine = false;
            }

            //catch any remaining lines in the second version
            if (lastPos < t2Lines.Length)
            {
                for (int i = lastPos; i < t2Lines.Length; i++)
                    t1Final.Append("<span class=textmissing>" + t2Lines[i] + "</span>" + lineBreak);
            }


            return t1Final.ToString();

        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("default.aspx?page={0}", HttpUtility.UrlEncode(((LinkButton)sender).Text)));
        }
    }
}
