using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
// Страничка для прямого добавления ролей на сайт не выкладывать
public partial class testroles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
        
    }

    private void BindData()
    {
        lstRoles.DataSource = Roles.GetAllRoles();
        lstRoles.DataBind();
        lstUsers.DataSource = Membership.GetAllUsers();
        lstUsers.DataBind();
    }

    protected void btnRolesAdd_Click(object sender, EventArgs e)
    {
        Roles.CreateRole(txtRolesAdd.Text);
        BindData();
    }
    protected void btnAdminAdd_Click(object sender, EventArgs e)
    {
        Membership.CreateUser(txtAdminAdd.Text, "echoholl", "info@echo-h.ru");
        Roles.AddUserToRole(txtAdminAdd.Text,"Administrators");
    }
    protected void btnDeleteTestUser_Click(object sender, EventArgs e)
    {
        Membership.DeleteUser("test");
    }
}
