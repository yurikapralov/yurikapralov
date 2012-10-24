<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" Theme="Theme2"%>
<%@ Register Src="Controls/UserProfile.ascx" TagName="UserProfile" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Theme2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
 <asp:CreateUserWizard ID="CreateUserWizard1" runat="server"
     AutoGeneratePassword="false" ContinueDestinationPageUrl="~/Default.aspx"
      FinishDestinationPageUrl="~/Default.aspx" OnFinishButtonClick="CreateUserWizard1_FinishButtonClick" 
      DuplicateUserNameErrorMessage="Данное имя уже зарегестрированно. Пожалуйста, введите другое имя" 
      DuplicateEmailErrorMessage="Данный адрес уже зарегестрирован. Пожалуйста, введите другой e-mail."
      Onsendingmail="Createuserwizard1_SendingMail" 
    FinishCompleteButtonText="Готово" CreateUserButtonText="Зарегистрироваться">
        <FinishCompleteButtonStyle CssClass="subform" Width="150px" />
        <CreateUserButtonStyle CssClass="subform" Width="150px" />
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <h2>Регистрация на сайте:</h2>
                    <table width="550" border="0" cellpadding="2" cellspacing="2" class="tablePanel">
                        <tr class="tablebody">
                            <td width="30%" class="questionstext">Имя:</td>
                            <td width="30%" class="answertext"><asp:TextBox ID="UserName" Width="200" runat="server"/></td>
                            <td class="answertext">
                                <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" 
                                ErrorMessage="Необходимо ввести имя" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="UserName" 
                                  ValidationGroup="CreateUserWizard1" Text="Необходимо ввести имя"/>
                            </td>
                       </tr>
                       <tr class="tablebody">
                            <td class="questionstext">Пароль: <small>(минимум 4 символа)</small></td>
                            <td class="answertext"><asp:TextBox ID="Password" TextMode="Password" Width="200" runat="server"/></td>
                            <td class="answertext">
                                <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" 
                                ErrorMessage="Необходимо ввести пароль" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="Password" 
                                  ValidationGroup="CreateUserWizard1" Text="Необходимо ввести пароль"/>
                                <asp:RegularExpressionValidator ID="valPasswordLenght" runat="server" 
                                ErrorMessage="Пароль должен быть не меньше четырех символов" Text="Пароль должен быть не меньше четырех символов"
                                 ControlToValidate="Password" SetFocusOnError="true" Display="Dynamic"
                                  ValidationExpression="\w{4,}"  ValidationGroup="CreateUserWizard1"/>
                            </td>
                       </tr>
                       <tr class="tablebody">
                            <td class="questionstext">Подтвердите пароль:</td>
                            <td  class="answertext"><asp:TextBox ID="ConfirmPassword" TextMode="Password" Width="200" runat="server"/></td>
                            <td  class="answertext">
                                <asp:RequiredFieldValidator ID="valRequireConfirmPassword" runat="server" 
                                ErrorMessage="Необходимо ввести подтверждение пароля" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="ConfirmPassword" 
                                  ValidationGroup="CreateUserWizard1" Text="Необходимо ввести подтверждение пароля"/>
                                <asp:CompareValidator ID="valComparePasword" runat="server"
                                 ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                 ErrorMessage="Пароль и подтверждение пароля должны совпадать" SetFocusOnError="true"
                                 Display="Dynamic" ValidationGroup="CreateUserWizard1" Text="Пароль и подтверждение пароля должны совпадать"/>
                            </td>
                       </tr>
                       <tr class="tablebody"> 
                            <td width="30%" class="questionstext">E-mail:</td>
                            <td width="30%"  class="answertext"><asp:TextBox ID="Email" Width="200" runat="server"/></td>
                            <td  class="answertext">
                                <asp:RequiredFieldValidator ID="valRequireEmail" runat="server" 
                                ErrorMessage="Необходимо ввести e-mail" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="Email" 
                                  ValidationGroup="CreateUserWizard1" Text="Необходимо ввести e-mail"/>
                                  <asp:RegularExpressionValidator runat="server" ID="valEmailPattern"  
                                  Display="Dynamic" SetFocusOnError="true" ValidationGroup="CreateUserWizard1"
                        ControlToValidate="Email" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ErrorMessage="Не правильный формат e-mail" Text="Не правильный формат e-mail"/>
                            </td>
                       </tr>
                       <tr class="tablebody">
                        <td colspan="3" align="right"  class="answertext">
                            <asp:Literal EnableViewState="False" ID="ErrorMessage" runat="server" />
                        </td> 
                       </tr>
                       </table>
                      <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="CreateUserWizard1" />
                    
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Личные данные">
                <h2>Заполните Ваши личные данные</h2>
                <p>Эти данные помогут Вам быстрее оформить заказ в нашем магазине</p>
                <uc1:UserProfile ID="UserProfile1"  runat="server" />             
            </asp:WizardStep>            
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
        <MailDefinition               
                From="info@echo-h.ru"
                Subject="Регистрация в интернет-магазине Echo Of Hollywood" IsBodyHtml="true">
             </MailDefinition> 
    </asp:CreateUserWizard>
</asp:Content>

