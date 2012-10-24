using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class En_EditProfile_old : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.Title =
                "Edit Account";
            Master.MainHeader = "Edit Account";
            Master.ContentStyle = "Content2";
            Master.HeaderImage = "Header.jpg";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UserProfile1.SaveProfile();
        lblChangeOK.Visible = true;
    }

    protected void ChangePassword1_SendingMail(object sender, MailMessageEventArgs e)
    {
        string message;
        message = "You have changed your password./n Your new password: " + ChangePassword1.NewPassword;
        e.Message.Body = message;
    }
}
