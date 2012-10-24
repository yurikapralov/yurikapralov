using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class En_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Registration";
        Master.MainHeader = "Registration";
    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        Roles.AddUserToRole(CreateUserWizard1.UserName, "Users");
        UserProfile1.SaveProfile();
    }
    protected void Createuserwizard1_SendingMail(object sender, MailMessageEventArgs e)
    {
        e.Message.Subject = "Echo Of Hollywood - Registration";
        string message;
        message = "Thank you for registering in our shop<br/>" +
         "Your:<br/>Name: " + CreateUserWizard1.UserName + "<br/>Password: " + CreateUserWizard1.Password + "<% Password %>" +
         "<br/> www.echo-h.ru";
        e.Message.Body = message;
    }
}