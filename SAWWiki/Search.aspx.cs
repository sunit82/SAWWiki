using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace SAWWiki
{
    public partial class Search : WikiBasePage
    {
        string query = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["query"] != null)
                query = HttpUtility.UrlDecode(Request.Params["query"].ToString());
            else
                query = "";


            if (!IsPostBack)
                BindResultsGrid();
        }
        public void BindResultsGrid()
        {
            grdSearch.DataSource = GetResults();
            grdSearch.DataBind();
        }

        public DataSet GetResults()
        {
            query = "%" + query + "%";

            using (
                SqlDataAdapter da = new SqlDataAdapter(
                    "w_SearchSimpleTerm",
                    ConfigurationManager.ConnectionStrings["sawWikiConnection"].ConnectionString
                ))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (query != "") da.SelectCommand.Parameters.Add("@parameter", SqlDbType.VarChar).Value = query;
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "SearchResults");
                    //show only the first part of the page text
                    string pageText = "";

                    foreach (DataRow row in ds.Tables["SearchResults"].Rows)
                    {
                        pageText = "\n" + row["PageText"].ToString();
                        row["PageText"] = (pageText.Length > 100) ? pageText.Substring(0, 100) : pageText.Substring(0, pageText.Length);
                    }
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                    return null;
                }
                return ds;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("search.aspx?query={0}", HttpUtility.UrlEncode(txtSearch.Text)));
        }
        protected void grdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?page=" + HttpUtility.UrlEncode((((GridView)sender).SelectedValue).ToString()));
        }
        protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSearch.DataSource = GetResults();
            grdSearch.PageIndex = e.NewPageIndex;
            grdSearch.DataBind();
        }
    }
}