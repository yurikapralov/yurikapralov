<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="Admin_ManageUsers"  Theme="Admin"%>
<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<%@ Register Src="../Controls/UserProfile.ascx" TagName="UserProfile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="content_total">
  <h2><asp:Label ID="lblLeftHeader" runat="server" /></h2><br />
<p><b>Всего зарегестрированно пользователей:
<asp:Literal ID="lblTotalUsers" runat="server" /><br />
Из них on-line:<asp:Literal ID="lblOnlineUsers" runat="server"/></b></p>
<asp:Panel ID="pnlUsers" runat="server" Visible="true">
<p>
Выберите роль:
<asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" />
</p>
<p>
Поиск по:<asp:DropDownList runat="server" ID="ddlSearchTypes">
    <asp:ListItem Text="Имя пользователя" Selected="True" />
    <asp:ListItem Text="E-mail" />
</asp:DropDownList>
Содержит:
<asp:TextBox runat="server" ID="txtSearchText" />
<asp:Button runat="server" ID="btnSearch" Text="Найти" OnClick="btnSearch_Click" />
</p>
<asp:GridView ID="gvwUsers" runat="server" AutoGenerateColumns="False" 
DataKeyNames="UserName" OnRowCreated="gvwUsers_RowCreated" OnRowDeleting="gvwUsers_RowDeleting" 
OnSelectedIndexChanged="gvwUsers_SelectedIndexChanged"  EnableViewState="false">
    <Columns>
        <asp:BoundField HeaderText="Имя Пользователя" DataField="UserName" />
        <asp:HyperLinkField HeaderText="E-mail" DataTextField="Email" 
         DataNavigateUrlFormatString="maito:{0}" DataNavigateUrlFields="Email" />
       <asp:BoundField HeaderText="Создан" DataField="CreationDate" />
       <asp:BoundField HeaderText="Был" DataField="LastActivityDate"  />
        <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" DeleteText="Удалить" SelectText="Редактировать" />
    </Columns>
    <EmptyDataTemplate>
    Нет ни одного пользователя по выбранному критерию.
    </EmptyDataTemplate>
</asp:GridView>
<asp:Label ID="lblControlsTest" runat="server" /> 
</asp:Panel>
<asp:Panel ID="pnlUserPrefernces" runat="server" Visible="false">
    <uc1:UserProfile ID="UserProfile1" runat="server" />
    <asp:Button ID="btnSaveProfile" runat="server" Text="Принять изменения" OnClick="btnSaveProfile_Click" />
    <asp:Button ID="btnCancelProfile" runat="server" Text="Отмена" OnClick="btnCancelProfile_Click" /></asp:Panel> 
 <p></p><p></p> 
        <h2><asp:Label ID="lblRightHeader" runat="server" /></h2><br />
    <asp:Panel ID="pnlInsertMode" runat="server" Visible="true">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server"
     AutoGeneratePassword="false" OnCreatedUser="CreateUserWizard1_CreatedUser"
      DuplicateUserNameErrorMessage="Данное имя уже зарегестрированно. Пожалуйста, введите другое имя" 
      DuplicateEmailErrorMessage="Данный адрес уже зарегестрирован. Пожалуйста, введите другой e-mail." LoginCreatedUser="false" OnContinueButtonClick="CreateUserWizard1_ContinueButtonClick">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table width="90%" border="0" cellpadding="2" cellspacing="2">
                        <tr>
                            <td>Состоит в группе:</td>
                            <td><asp:DropDownList ID="ddlRoleAdd" runat="server" /></td>
                        </tr>
                        <tr>
                            <td width="30%">Имя:</td>
                            <td width="30%"><asp:TextBox ID="UserName" Width="200" runat="server" EnableViewState="false" /></td>
                            <td>
                                <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" 
                                ErrorMessage="Необходимо ввести имя" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="UserName" 
                                  ValidationGroup="CreateUserWizard1" Text="*"/>
                            </td>
                       </tr>
                       <tr>
                            <td>Пароль:</td>
                            <td><asp:TextBox ID="Password" TextMode="Password" Width="200" runat="server"/></td>
                            <td>
                                <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" 
                                ErrorMessage="Необходимо ввести пароль" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="Password" 
                                  ValidationGroup="CreateUserWizard1" Text="*"/>
                                <asp:RegularExpressionValidator ID="valPasswordLenght" runat="server" 
                                ErrorMessage="Пароль должен быть не меньше четырех симоволов" Text="*"
                                 ControlToValidate="Password" SetFocusOnError="true" Display="Dynamic"
                                  ValidationExpression="\w{4,}"  ValidationGroup="CreateUserWizard1"/>
                            </td>
                       </tr>
                       <tr>
                            <td>Подтвердите пароль:</td>
                            <td><asp:TextBox ID="ConfirmPassword" TextMode="Password" Width="200" runat="server"/></td>
                            <td>
                                <asp:RequiredFieldValidator ID="valRequireConfirmPassword" runat="server" 
                                ErrorMessage="Необходимо ввести подтверждение пароля" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="ConfirmPassword" 
                                  ValidationGroup="CreateUserWizard1" Text="*"/>
                                <asp:CompareValidator ID="valComparePasword" runat="server"
                                 ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                 ErrorMessage="Пароль и подтверждение пароля должны совпадать" SetFocusOnError="true"
                                 Display="Dynamic" ValidationGroup="CreateUserWizard1" Text="*"/>
                            </td>
                       </tr>
                       <tr> 
                            <td width="30%">E-mail:</td>
                            <td width="30%"><asp:TextBox ID="Email" Width="200" runat="server" EnableViewState="false" /></td>
                            <td>
                                <asp:RequiredFieldValidator ID="valRequireEmail" runat="server" 
                                ErrorMessage="Необходимо ввести e-mail" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="Email" 
                                  ValidationGroup="CreateUserWizard1" Text="*"/>
                                  <asp:RegularExpressionValidator runat="server" ID="valEmailPattern"  
                                  Display="Dynamic" SetFocusOnError="true" ValidationGroup="CreateUserWizard1"
                        ControlToValidate="Email" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ErrorMessage="Не правильный формат e-mail" Text="*"/>
                            </td>
                       </tr>
                       <tr>
                        <td colspan="3" align="right">
                            <asp:Literal EnableViewState="False" ID="ErrorMessage" runat="server" />
                        </td> 
                       </tr>
                       </table>
                      <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="CreateUserWizard1" />
                    
                </ContentTemplate>
            </asp:CreateUserWizardStep>
        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
        <ContentTemplate>
        Запись успешно создана.
        <asp:Button ID="btnContinue" CommandName="Continue" runat="server" Text="Продолжить"/>
        </ContentTemplate>     
            </asp:CompleteWizardStep>

        </WizardSteps>
       </asp:CreateUserWizard>
  </asp:Panel>
  <asp:Panel ID="pnlEditMode" runat="server" Visible="false">
    <table width="90%">
    <tr>
    <td width="40%">Имя пользователя:</td>
    <td width="60%"><asp:Label ID="lblUserName" runat="server"/></td>
    </tr>
    <tr>
    <td>E-mail:</td>
    <td>
        <asp:TextBox ID="txtEmail" runat="server"/>
        <asp:RequiredFieldValidator ID="valRequireEmailChange" runat="server" 
        ErrorMessage="Необходимо ввести e-mail" SetFocusOnError="true"
        Display="Dynamic" ControlToValidate="txtEmail" 
         ValidationGroup="ChangeData" Text="Необходимо ввести e-mail"/>
         <asp:RegularExpressionValidator runat="server" ID="valEmailPatternChange"  
         Display="Dynamic" SetFocusOnError="true" ValidationGroup="ChangeData"
          ControlToValidate="txtEmail" 
         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
          ErrorMessage="Не правильный формат e-mail" Text="Не правильный формат e-mail"/>
    </td>
    </tr>
    <tr>
        <td>Состоит в группе:</td>
        <td><asp:DropDownList ID="ddlRoleUpdate" runat="server" /></td>
    </tr>
    <tr>
    <td>Новый пароль:</td>
    <td>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/>
        <asp:RequiredFieldValidator ID="valRequirePasswordChange" runat="server" 
        ErrorMessage="Необходимо ввести пароль" SetFocusOnError="true"
        Display="Dynamic" ControlToValidate="txtPassword" 
        ValidationGroup="CreateUserWizard1" Text="Необходимо ввести пароль"/>
        <asp:RegularExpressionValidator ID="valPasswordLenghtChange" runat="server" 
        ErrorMessage="Пароль должен быть не меньше четырех симоволов" 
        Text="Пароль должен быть не меньше четырех симоволов"
        ControlToValidate="txtPassword" SetFocusOnError="true" Display="Dynamic"
        ValidationExpression="\w{4,}"  ValidationGroup="ChangePassword"/>
    </td>
    </tr>
    <tr>
    <td>Подтвердить новый пароль:</td>
    <td>
        <asp:TextBox ID="txtPasswordConfirmChange" runat="server" TextMode="Password"/>
        <asp:RequiredFieldValidator ID="valRequireConfirmPassword" runat="server" 
        ErrorMessage="Необходимо ввести подтверждение пароля" SetFocusOnError="true"
       Display="Dynamic" ControlToValidate="txtPasswordConfirmChange" 
      ValidationGroup="ChangePassword1" Text="Необходимо ввести подтверждение пароля"/>
      <asp:CompareValidator ID="valComparePaswordChange" runat="server"
       ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirmChange" 
       ErrorMessage="Пароль и подтверждение пароля должны совпадать" SetFocusOnError="true"
       Display="Dynamic" ValidationGroup="ChangePassword" Text="Пароль и подтверждение пароля должны совпадать"/>
    </td>
    </tr>
    <tr>
    <td colspan="2">
        <asp:CheckBox ID="cbxPassChange" runat="server" Text="Изменить пароль" ValidationGroup="ChangePassword" />
    </td>
    </tr>
    <tr>
       <td>Зарегестрирован:</td>
       <td><asp:Label ID="lblRegistrd" runat="server"/></td>
    </tr>
    <tr>
       <td>Заходил на сайт:</td>
       <td><asp:Label ID="lblLastLogin" runat="server"/></td>
    </tr>
    <tr>
       <td>Последнее действие:</td>
       <td><asp:Label ID="lblLastActivity" runat="server"/></td>
    </tr>
    </table>
     <asp:Button ID="btnChange" runat="server" Text="Изменить" OnClick="btnChange_Click" ValidationGroup="ChangeData"/>
    <asp:Button ID="btnCancel" runat="server" Text="Отмена" OnClick="btnCancel_Click" />
      <asp:Button ID="btnEditPreferences" runat="server" Text="Личные данные" OnClick="btnEditPreferences_Click" />
      <asp:Label ID="lblTest" runat="server"></asp:Label>
    </asp:Panel>
 </div>   
</asp:Content>

