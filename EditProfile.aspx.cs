using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class EditProfile : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Редактирование учетной записи";
        Master.MainHeader = "Редактирование учетной записи";
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UserProfile1.SaveProfile();
        lblChangeOK.Visible = true;
    }

    protected void ChangePassword1_SendingMail(object sender, MailMessageEventArgs e)
    {
        string message;
        message = "Вы изменили пароль./n Ваш новый пароль: " + ChangePassword1.NewPassword;
        e.Message.Body = message;
    }
}