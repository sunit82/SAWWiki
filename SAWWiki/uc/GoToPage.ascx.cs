using System;
using System.Web;

namespace SAWWiki
{
    public partial class GoToPage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtGoTo.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + this.txtGoTo.UniqueID + "').click();return false;}} else {return true}; ");
        }
        protected void btnGoTo_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?page=" + HttpUtility.UrlEncode(txtGoTo.Text));
        }
    }
}
