using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAWWiki
{
    public partial class SearchPage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + this.btnSearch.UniqueID + "').click();return false;}} else {return true}; ");

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("search.aspx?query={0}", HttpUtility.UrlEncode(txtSearch.Text)));
        }
    }
}