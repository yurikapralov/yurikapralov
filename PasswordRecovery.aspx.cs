using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class PasswordRecovery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Восстановление пароля";
        Master.MainHeader = "Восстановление пароля";
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        lblResult.Visible = true;
        MembershipUser user = Membership.GetUser(txtUser.Text);
        if (user == null)
            lblResult.Text = "Данный пользователь не зарегистрирован";
        else
        {
            if (Roles.IsUserInRole(user.UserName, "Administrators"))
                lblResult.Text = "На этого пользователя нельзя получить пароль";
            else
            {
                string userName = Membership.GetUserNameByEmail(txtEmail.Text);
                if (userName == null)
                    lblResult.Text = "Данный e-mail не зарегистрирован";
                else
                {
                    if (user.UserName == userName)
                    {
                        lblResult.Text = "Ваше имя: " + userName + " ;Ваш пароль: " + user.GetPassword();
                        SendMail(userName, user.GetPassword(), txtEmail.Text);
                    }
                    else
                        lblResult.Text = "Данный e-mail зарегестрирован не на вас";
                }
            }
        }
    }

    protected void SendMail(string name, string password, string email)
    {
        string message = string.Format("Данные Вашей учетной записи:<br/>имя: {0}<br/>пароль: {1}<br/>www.echo-h.ru", name, password);
        SmtpClient smtp = new SmtpClient();
        string fromAddress = Helpers.Settings.ContactForm.MailFrom;
        try
        {
            MailMessage msg1 = new MailMessage(fromAddress, email);
            msg1.Subject = "Echo Of Hollywood - Восстановление пароля";
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