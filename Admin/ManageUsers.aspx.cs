using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class Admin_ManageUsers : AdminPage
{
    private MembershipUserCollection allUsers = Membership.GetAllUsers();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            lblTotalUsers.Text = allUsers.Count.ToString();
            lblOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
            BindRoles();
            BindUsers(false, false);
            lblLeftHeader.Text = "Список пользователей сайта";
            lblRightHeader.Text = "Добавление нового пользователя";
            Master.Title = "Управление пользователями";
            Master.HeaderText = "Управление пользователями";
        }
    }
    private void BindRoles()
    {
        ddlRoles.DataSource = Roles.GetAllRoles();
        ddlRoles.DataBind();
        DropDownList ddlRoleAdd = (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlRoleAdd");
        ddlRoleAdd.DataSource = Roles.GetAllRoles();
        ddlRoleAdd.DataBind();
    }
    private void BindUsers(bool reloadAllUsers, bool usingSearch)
    {
        if (reloadAllUsers)
            allUsers = Membership.GetAllUsers();
        MembershipUserCollection users = new MembershipUserCollection();
        if (usingSearch)
        {
            if (ddlSearchTypes.SelectedValue == "E-mail")
                users = Membership.FindUsersByEmail("%" + txtSearchText.Text + "%");
            else
                users = Membership.FindUsersByName("%" + txtSearchText.Text + "%");
        }
        else
        {
            string selectedRole = ddlRoles.SelectedValue.ToString();
            string[] strUsers = Roles.GetUsersInRole(selectedRole);
            foreach (string strUser in strUsers)
            {
                MembershipUser user = Membership.GetUser(strUser);
                users.Add(user);
            }

        }
        gvwUsers.DataSource = users;
        gvwUsers.DataBind();


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindUsers(false, true);
    }
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUsers(false, false);
    }
    protected void gvwUsers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btn = e.Row.Cells[4].Controls[0] as LinkButton;
            btn.OnClientClick = "if(confirm('Вы уверены что хотите удалить этого пользователя?')==false) return false;";
            if (e.Row.DataItem != null)
            {
                if (e.Row.DataItem.ToString() == User.Identity.Name)
                {
                    e.Row.Cells[4].Controls[0].Visible = false;
                }
            }

        }

    }
    protected void gvwUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userName = gvwUsers.DataKeys[e.RowIndex].Value.ToString();
        ProfileManager.DeleteProfile(userName);
        Membership.DeleteUser(userName);
        BindUsers(true, false);
        lblTotalUsers.Text = allUsers.Count.ToString();
    }



    protected void gvwUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlInsertMode.Visible = false;
        pnlEditMode.Visible = true;
        string userName = gvwUsers.SelectedValue.ToString();
        lblRightHeader.Text = "Редактирование пользователя " + userName;
        MembershipUser user = Membership.GetUser(userName);
        lblUserName.Text = user.UserName;
        txtEmail.Text = user.Email;
        txtPassword.Text = "";
        txtPasswordConfirmChange.Text = "";
        lblRegistrd.Text = user.CreationDate.ToString();
        lblLastActivity.Text = user.LastActivityDate.ToString();
        lblLastLogin.Text = user.LastLoginDate.ToString();
        ddlRoleUpdate.DataSource = Roles.GetAllRoles();
        ddlRoleUpdate.DataBind();
        ddlRoleUpdate.SelectedValue = Roles.GetRolesForUser(userName)[0];


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvwUsers.SelectedIndex = -1;
        BindUsers(false, false);
        pnlEditMode.Visible = false;
        pnlInsertMode.Visible = true;
        lblRightHeader.Text = "Добавление нового пользователя";
    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        DropDownList ddlRoleAdd = (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlRoleAdd");
        Roles.AddUserToRole(CreateUserWizard1.UserName, ddlRoleAdd.SelectedValue.ToString());
        BindUsers(true, false);
        lblTotalUsers.Text = allUsers.Count.ToString();
    }
    protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
    {
        pnlInsertMode.Visible = true;
        CreateUserWizard1.ActiveStepIndex = 0;
        //CreateUserWizard1.StepPreviousButtonStyle.Width = 0;
        CreateUserWizard1.StepPreviousButtonStyle.CssClass = "hidden";
        ClearControls(CreateUserWizardStep1);       
        BindUsers(false, false);
    }
    //Почему-то метод не работает
    protected void ClearControls(Control parentControl)
    {
        foreach (Control control in parentControl.Controls)
        {
            if(control.ID=="UserName" || control.ID=="Email")
            {
                ((TextBox) control).Text = "";
            }
            if(control.HasControls())
            {
                ClearControls(control);
            }
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (cbxPassChange.Checked)
        {
            valRequirePasswordChange.Validate();
            valRequireConfirmPassword.Validate();
            valPasswordLenghtChange.Validate();
            valComparePaswordChange.Validate();
            Page.Validate("ChangePassword");
            if (IsValid)
            {
                UpdateUserData();
            }
        }
        else
        {
            UpdateUserData();
        }

    }

    private void UpdateUserData()
    {
        string userName = lblUserName.Text;
        MembershipUser user = Membership.GetUser(userName);
        if (cbxPassChange.Checked)
        {
            string strOldPassword = user.GetPassword();
            string strNewPassword = txtPassword.Text;
            user.ChangePassword(strOldPassword, strNewPassword);
        }
        user.Email = txtEmail.Text;
        string[] strUserInRoles = Roles.GetRolesForUser(userName);
        Roles.RemoveUserFromRoles(userName, strUserInRoles);
        Roles.AddUserToRole(userName, ddlRoleUpdate.SelectedValue.ToString());
        pnlEditMode.Visible = false;
        pnlInsertMode.Visible = true;
        Membership.UpdateUser(user);
        BindUsers(true, false);
        lblRightHeader.Text = "Добавление нового пользователя";

    }

    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {
        lblTest.Text = txtPassword.Text;
    }
    protected void btnEditPreferences_Click(object sender, EventArgs e)
    {
        lblLeftHeader.Text = "Настройки пользователя " + lblUserName.Text;
        pnlUsers.Visible = false;
        pnlUserPrefernces.Visible = true;
        UserProfile1.UserName = lblUserName.Text;
        UserProfile1.LoadProfile();
    }
    protected void btnSaveProfile_Click(object sender, EventArgs e)
    {
        UserProfile1.SaveProfile();
        pnlUserPrefernces.Visible = false;
        pnlUsers.Visible = true;
        lblLeftHeader.Text = "Список пользователей сайта";
    }
    protected void btnCancelProfile_Click(object sender, EventArgs e)
    {
        pnlUserPrefernces.Visible = false;
        pnlUsers.Visible = true;
        lblLeftHeader.Text = "Список пользователей сайта";
    }
}
