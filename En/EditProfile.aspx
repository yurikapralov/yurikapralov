<%@ Page Title="" Language="C#" MasterPageFile="~/En/Theme2En.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="En_EditProfile" Theme="Theme2" %>
<%@ Register TagPrefix="uc1" TagName="UserProfile" Src="~/Controls/UserProfileEng.ascx" %>
<%@ MasterType VirtualPath="Theme2En.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="extraNavHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    <h2>
        Change Password</h2>
    <asp:ChangePassword ID="ChangePassword1" runat="server" OnSendingMail="ChangePassword1_SendingMail">
        <ChangePasswordTemplate>
            <table class="tablePanel" width="550" cellpadding="0" cellspacing="0">
                <tr class="tablebody">
                    <td class="questionstext">
                        Current Password:
                    </td>
                    <td class="answertext">
                        <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="valRequireCurrentPassword" runat="server" ErrorMessage="You must enter a password"
                            SetFocusOnError="true" Display="Dynamic" ControlToValidate="CurrentPassword"
                            ValidationGroup="ChangePassword" Text="*" />
                    </td>
                </tr>
                <tr class="tablebody">
                    <td class="questionstext">
                        New Password:
                    </td>
                    <td class="answertext">
                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" ErrorMessage="You must enter a password"
                            SetFocusOnError="true" Display="Dynamic" ControlToValidate="NewPassword" ValidationGroup="ChangePassword"
                            Text="*" />
                        <asp:RegularExpressionValidator ID="valPasswordLenght" runat="server" ErrorMessage="The password must be at least four characters"
                            Text="*" ControlToValidate="NewPassword" SetFocusOnError="true" Display="Dynamic"
                            ValidationExpression="\w{4,}" ValidationGroup="ChangePassword" />
                    </td>
                </tr>
                <tr class="tablebody">
                    <td class="questionstext">
                        Confirm Password:
                    </td>
                    <td class="answertext">
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="valRequireConfirmPassword" runat="server" ErrorMessage="You must enter a password confirmation"
                            SetFocusOnError="true" Display="Dynamic" ControlToValidate="ConfirmNewPassword"
                            ValidationGroup="ChangePassword" Text="*" />
                        <asp:CompareValidator ID="valComparePasword" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" ErrorMessage="Password and confirm password must match"
                            SetFocusOnError="true" Display="Dynamic" ValidationGroup="ChangePassword" Text="*" />
                    </td>
                </tr>
                <tr class="tablebody">
                    <td colspan="2">
                        <asp:Label ID="FailureText" runat="server" />
                        <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                            Text="Change Password" ValidationGroup="ChangePassword" CssClass="subform" Width="150px" />
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ChangePassword" />
                    </td>
                </tr>
            </table>
        </ChangePasswordTemplate>
        <SuccessTemplate>
            <asp:Label runat="server" ID="lblSuccess" Text="Your password was successfully changed" />
        </SuccessTemplate>
        <MailDefinition From="info@echo-h.ru" Subject="Changing your password in e-shop Echo Of Hollywood"
            IsBodyHtml="true">
        </MailDefinition>
    </asp:ChangePassword>
    <p>&nbsp;</p>
    <h2 style="text-align:left;">
        Change Personal Data</h2>
    <p>
        These data will help you to quickly place your order at our store</p>
    <p>
        <uc1:UserProfile ID="UserProfile1" runat="server" />
    </p>
    <asp:Label runat="server" ID="lblChangeOK" Visible="false" Text="Details were changed" /><br />
    <asp:Button runat="server" ID="btnUpdate" ValidationGroup="EditProfile" Text="Update Your Profile."
        OnClick="btnUpdate_Click" CssClass="subform"  Width="150px"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="socialPlaceHolder" Runat="Server">
</asp:Content>

