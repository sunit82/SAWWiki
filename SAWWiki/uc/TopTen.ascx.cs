using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SAWWiki
{
    public partial class TopTen : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new
                SqlConnection(ConfigurationManager.ConnectionStrings["sawWikiConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "Select top 10 PageName from wiki order by hitcount desc",
                    conn);
                SqlDataReader reader;
                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    rptTopTen.DataSource = reader;
                    rptTopTen.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not get Top Ten List", ex);
                }
            }
        }
    }
}
