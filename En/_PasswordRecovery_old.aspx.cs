using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class En_PasswordRecovery_old : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Password Recovery";
        Master.MainHeader = "Password Recovery";
        Master.ContentStyle = "Content2";
        Master.HeaderImage = "Header.jpg";
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        lblResult.Visible = true;
        MembershipUser user = Membership.GetUser(txtUser.Text);
        if (user == null)
            lblResult.Text = "This user is not registered";
        else
        {
            if (Roles.IsUserInRole(user.UserName, "Administrators"))
                lblResult.Text = "This user can not retrieve your password";
            else
            {
                string userName = Membership.GetUserNameByEmail(txtEmail.Text);
                if (userName == null)
                    lblResult.Text = "This e-mail is not registered";
                else
                {
                    if (user.UserName == userName)
                    {
                        lblResult.Text = "Your name: " + userName + " ;Your password: " + user.GetPassword();
                        SendMail(userName, user.GetPassword(), txtEmail.Text);
                    }
                    else
                        lblResult.Text = "This e-mail is not registered to you";
                }
            }
        }
    }

    protected void SendMail(string name, string password, string email)
    {
        string message = string.Format("Your account information:<br/>name: {0}<br/>password: {1}<br/>www.echo-h.ru", name, password);
        SmtpClient smtp = new SmtpClient();
        string fromAddress = Helpers.Settings.ContactForm.MailFrom;
        try
        {
            MailMessage msg1 = new MailMessage(fromAddress, email);
            msg1.Subject = "Echo Of Hollywood - Password recovery";
            msg1.Body = message;
            msg1.IsBodyHtml = true;
            smtp.Send(msg1);
        }
        catch (Exception)
        {


        }

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
