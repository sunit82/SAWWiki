using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace SAWWiki
{
    public partial class History : WikiBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = GetPageName();

            lblHeader.Text = "<H1>History: " + pageName + "</H1>";
            if (!IsPostBack)
            {
                BindHistoryGrid();

            }

            lblDiffError.Visible = false;
        }

        void BindHistoryGrid()
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["sawWikiConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("w_GetHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = pageName;
                SqlDataReader reader;

                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    grdHistory.DataSource = reader;
                    grdHistory.DataBind();
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            }
        }
        protected void grdHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPageText.Visible = true;


            //TODO:  someone can currently promote the current page - needed if promoting a deleted page
            // but otherwise will create a redundant version.
            if (User.IsInRole("Editor") || User.IsInRole("Administrator"))
                btnPromote.Visible = true;
            else
                btnPromote.Visible = false;



            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["sawWikiConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("w_GetPageByVersion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = pageName;
                int version = int.Parse(((GridView)sender).SelectedRow.Cells[0].Text);
                cmd.Parameters.Add("@Version", SqlDbType.Int).Value = version;
                SqlDataReader reader;

                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    lblVersionHeader.Text = string.Format("<br><H2>Page History for {0}, v{1}</H2><br>", pageName, version);
                    litPageHistoryView.Text = WikiParser.GenerateWikiText(reader["PageText"].ToString());
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlPageText.Visible = false;
        }
        protected void btnPromote_Click(object sender, EventArgs e)
        {
            //Make the selected entry the current one.
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["sawWikiConnection"].ConnectionString))
            {
                int version = int.Parse(grdHistory.SelectedRow.Cells[0].Text);

                //get the selected version information
                SqlCommand cmdVersion = new SqlCommand("w_GetPageByVersion", conn);
                cmdVersion.CommandType = CommandType.StoredProcedure;
                cmdVersion.Parameters.Add("@PageName", SqlDbType.VarChar).Value = pageName;
                cmdVersion.Parameters.Add("@Version", SqlDbType.Int).Value = version;

                //check that the page is not deleted
                SqlCommand cmdGetPage = new SqlCommand("w_GetPage", conn);
                cmdGetPage.CommandType = CommandType.StoredProcedure;
                cmdGetPage.Parameters.Add("@PageName", SqlDbType.VarChar).Value = pageName;
                cmdGetPage.Parameters.Add("@IncrementHitCount", SqlDbType.Bit).Value = 0;

                //make it the current version
                SqlCommand cmdUpdate = new SqlCommand("w_UpdatePage", conn);
                cmdUpdate.CommandType = CommandType.StoredProcedure;
                cmdUpdate.Parameters.Add("@PageName", SqlDbType.VarChar).Value = pageName;
                cmdUpdate.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = User.Identity.Name;

                //if the page was deleted we will need to insert instead of update
                SqlCommand cmdInsert = new SqlCommand("w_InsertPage", conn);
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.Parameters.Add("@PageName", SqlDbType.VarChar).Value = pageName;
                cmdInsert.Parameters.Add("@User", SqlDbType.NVarChar).Value = Context.User.Identity.Name;

                SqlDataReader versionReader, getReader;

                try
                {
                    conn.Open();
                    versionReader = cmdVersion.ExecuteReader();
                    versionReader.Read();
                    string pageText = versionReader["PageText"].ToString();
                    versionReader.Close();

                    getReader = cmdGetPage.ExecuteReader();
                    bool isCurrentPage = getReader.Read();
                    getReader.Close();
                    if (isCurrentPage) //if the page exists, update it
                    {
                        cmdUpdate.Parameters.Add("@PageText", SqlDbType.Text).Value = pageText;
                        cmdUpdate.ExecuteNonQuery();
                    }
                    else  //the page was deleted, insert it
                    {
                        cmdInsert.Parameters.Add("@PageText", SqlDbType.Text).Value = pageText;
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            }

            pnlPageText.Visible = false;
            grdHistory.SelectedIndex = -1;
            BindHistoryGrid();

        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?page=" + HttpUtility.UrlEncode(pageName));
        }

        protected void btnDiff_Click(object sender, EventArgs e)
        {
            if (Request.Form["diff1"] != null && Request.Form["diff1"] != null)
                Response.Redirect("diff.aspx?page=" + HttpUtility.UrlEncode(pageName) + "&v1=" + Request.Form["diff1"] + "&v2=" + Request.Form["diff2"]);
            else
                lblDiffError.Visible = true;
        }

    }
}
