using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class Register : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
              "Регистрация на сайте";
        Master.MainHeader = "Регистрация на сайте";
    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        Roles.AddUserToRole(CreateUserWizard1.UserName, "Users");
        UserProfile1.SaveProfile();
        if (Request.QueryString["Shop"] == "OK")
            Response.Redirect("Shopping.aspx");
    }
    protected void Createuserwizard1_SendingMail(object sender, MailMessageEventArgs e)
    {
        e.Message.Subject = "Echo Of Hollywood - Регистрация";
        string message;
        message = "Спасибо за регистрацию в нашем магазине<br/>" +
         "Ваши:<br/>Имя: " + CreateUserWizard1.UserName + "<br/>Пароль: " + CreateUserWizard1.Password +
         "<br/> www.echo-h.ru";
        message +=
            @"<p>Регистрация на нашем сайте избавляет Вас от необходимости вводить каждый раз Ваши контактные данные, а также дает
            возможность отслеживать историю Ваших заказов.</p>";
        e.Message.Body = message;
    }
}