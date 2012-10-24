<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" Theme="Theme2"%>
<%@ MasterType VirtualPath="~/Theme2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<h2>Восстановление пароля</h2>
<p>Если вы забыли пароль то вы можете использовать эту форму для того, чтобы мы выслали его
на Ваш e-mail</p>
    <p>
    Введите ваше имя: <asp:TextBox ID="txtUser" runat="server"/>
    <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" 
    ErrorMessage="Необходимо ввести имя" SetFocusOnError="true"
    Display="Dynamic" ControlToValidate="txtUser" 
    ValidationGroup="CreateUserWizard1" Text="*"/>
    </p>
   <p>
   Введите ваш e-mail: <asp:TextBox ID="txtEmail" runat="server"/>
   <asp:RequiredFieldValidator ID="valRequireEmail" runat="server" 
    ErrorMessage="Необходимо ввести e-mail" SetFocusOnError="true"
    Display="Dynamic" ControlToValidate="txtEmail" 
    ValidationGroup="CreateUserWizard1" Text="*"/>
   <asp:RegularExpressionValidator runat="server" ID="valEmailPattern"  
   Display="Dynamic" SetFocusOnError="true" ValidationGroup="CreateUserWizard1"
   ControlToValidate="txtEmail" 
   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
   ErrorMessage="Не правильный формат e-mail" Text="*"/>
   </p>
    <asp:Button ID="btnSend" runat="server" Text="Отправить" OnClick="btnSend_Click" 
    ValidationGroup="CreateUserWizard1" CssClass="subform"/> 
    <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="CreateUserWizard1" />
   <p>
   <asp:Button ID="btnReturn" runat="server" Text="На главную страницу" 
            CssClass="subform" onclick="btnReturn_Click" Width="200px"/>
   </p>
    <p>
       <asp:Label ID="lblResult" runat="server" Visible="false" ForeColor="#ff0000"></asp:Label>
   </p>
</asp:Content>

