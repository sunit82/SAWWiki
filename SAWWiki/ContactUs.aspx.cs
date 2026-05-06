using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace SAWWiki
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;

            MailMessage objMail = new MailMessage(txtEmail.Text.Trim(), "admin@sawinfotech.com",
                "SAWWiki-comment by " + txtName.Text.Trim(), txtComment.Text.Trim());
            SmtpClient objSMTP = new SmtpClient("mail.sawinfotech.com");
            objSMTP.Credentials = new System.Net.NetworkCredential("admin@sawinfotech.com", "admmailtech");
            objSMTP.Send(objMail);
        }
    }
}
