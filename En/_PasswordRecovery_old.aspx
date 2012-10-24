<%@ Page Title="" Language="C#" MasterPageFile="~/En/MasterPageEng.master" AutoEventWireup="true" CodeFile="_PasswordRecovery_old.aspx.cs" Inherits="En_PasswordRecovery_old" Theme="Default" %>
<%@ MasterType VirtualPath="~/En/MasterPageEng.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CurrencySelectContentHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceMainHolder" Runat="Server">
<h2>Password Recovery</h2>
<p>If you forgot your password, you can use this form to have us send it
your e-mail</p>
    <p>
    Enter your name: <asp:TextBox ID="txtUser" runat="server"/>
    <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" 
    ErrorMessage="You must enter a name" SetFocusOnError="true"
    Display="Dynamic" ControlToValidate="txtUser" 
    ValidationGroup="CreateUserWizard1" Text="*"/>
    </p>
   <p>
   Enter your e-mail: <asp:TextBox ID="txtEmail" runat="server"/>
   <asp:RequiredFieldValidator ID="valRequireEmail" runat="server" 
    ErrorMessage="You must enter a e-mail" SetFocusOnError="true"
    Display="Dynamic" ControlToValidate="txtEmail" 
    ValidationGroup="CreateUserWizard1" Text="*"/>
   <asp:RegularExpressionValidator runat="server" ID="valEmailPattern"  
   Display="Dynamic" SetFocusOnError="true" ValidationGroup="CreateUserWizard1"
   ControlToValidate="txtEmail" 
   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
   ErrorMessage="Is not the correct format e-mail" Text="*"/>
   </p>
    <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" 
    ValidationGroup="CreateUserWizard1" CssClass="orderbutton"/> 
    <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="CreateUserWizard1" />
   <p>
       <asp:Label ID="lblResult" runat="server" Visible="false"></asp:Label>
   </p>
   <p>
   <asp:Button ID="btnReturn" runat="server" Text="Go to homepage" 
            CssClass="orderbutton" onclick="btnReturn_Click" />
   </p>
</asp:Content>

