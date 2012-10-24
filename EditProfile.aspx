<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" Theme="Theme2" %>
<%@ MasterType VirtualPath="~/Theme2.master" %>
<%@ Register Src="Controls/UserProfile.ascx" TagName="UserProfile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
 <h2>
        Смена пароля</h2>
    <asp:ChangePassword ID="ChangePassword1" runat="server" OnSendingMail="ChangePassword1_SendingMail">
        <ChangePasswordTemplate>
            <table class="tablePanel" width="550" cellpadding="0" cellspacing="0">
                <tr class="tablebody">
                    <td class="questionstext">
                        Текущий пароль:
                    </td>
                    <td class="answertext">
                        <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="valRequireCurrentPassword" runat="server" ErrorMessage="Необходимо ввести пароль"
                            SetFocusOnError="true" Display="Dynamic" ControlToValidate="CurrentPassword"
                            ValidationGroup="ChangePassword" Text="*" />
                    </td>
                </tr>
                <tr class="tablebody">
                    <td class="questionstext">
                        Новый пароль:
                    </td>
                    <td class="answertext">
                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" ErrorMessage="Необходимо ввести пароль"
                            SetFocusOnError="true" Display="Dynamic" ControlToValidate="NewPassword" ValidationGroup="ChangePassword"
                            Text="*" />
                        <asp:RegularExpressionValidator ID="valPasswordLenght" runat="server" ErrorMessage="Пароль должен быть не меньше четырех символов"
                            Text="*" ControlToValidate="NewPassword" SetFocusOnError="true" Display="Dynamic"
                            ValidationExpression="\w{4,}" ValidationGroup="ChangePassword" />
                    </td>
                </tr>
                <tr class="tablebody">
                    <td class="questionstext">
                        Подтвердите пароль:
                    </td>
                    <td class="answertext">
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="valRequireConfirmPassword" runat="server" ErrorMessage="Необходимо ввести подтверждение пароля"
                            SetFocusOnError="true" Display="Dynamic" ControlToValidate="ConfirmNewPassword"
                            ValidationGroup="ChangePassword" Text="*" />
                        <asp:CompareValidator ID="valComparePasword" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" ErrorMessage="Пароль и подтверждение пароля должны совпадать"
                            SetFocusOnError="true" Display="Dynamic" ValidationGroup="ChangePassword" Text="*" />
                    </td>
                </tr>
                <tr class="tablebody">
                    <td colspan="2">
                        <asp:Label ID="FailureText" runat="server" />
                        <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                            Text="Изменить пароль" ValidationGroup="ChangePassword" CssClass="subform" Width="150px"/>
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ChangePassword" />
                    </td>
                </tr>
            </table>
        </ChangePasswordTemplate>
        <SuccessTemplate>
            <asp:Label runat="server" ID="lblSuccess" Text="Ваш пароль успешно изменен" />
        </SuccessTemplate>
        <MailDefinition From="info@echo-h.ru" Subject="Изменение пароля в интернет-магазине Echo Of Hollywood"
            IsBodyHtml="true">
        </MailDefinition>
    </asp:ChangePassword>
    <p>&nbsp;</p>
    <h2 style="text-align:left;">
        Изменение личных данных</h2>
    <p>
        Эти данные помогут Вам быстрее оформить заказ в нашем магазине</p>
    <p>
        <uc1:UserProfile ID="UserProfile1" runat="server" />
    </p>
    <asp:Label runat="server" ID="lblChangeOK" Visible="false" Text="Данные успешно изменены" /><br />
    <asp:Button runat="server" ID="btnUpdate" ValidationGroup="EditProfile" Text="Обновить профиль."
        OnClick="btnUpdate_Click" CssClass="subform"  Width="150px"/>
</asp:Content>

