<%@ Page Title="" Language="C#" MasterPageFile="~/En/MasterPageEng.master" AutoEventWireup="true" CodeFile="_Register_old.aspx.cs" Inherits="En_Register_old" Theme="Default" %>
<%@ MasterType VirtualPath="~/En/MasterPageEng.master" %>
<%@ Register Src="~/Controls/UserProfileEng.ascx" TagName="UserProfile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CurrencySelectContentHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceMainHolder" Runat="Server">
<asp:CreateUserWizard ID="CreateUserWizard1" runat="server"
     AutoGeneratePassword="false" ContinueDestinationPageUrl="~/Default.aspx"
      FinishDestinationPageUrl="~/Default.aspx" OnFinishButtonClick="CreateUserWizard1_FinishButtonClick"
      Onsendingmail="Createuserwizard1_SendingMail">
        <FinishCompleteButtonStyle CssClass="orderbutton" />
        <CreateUserButtonStyle CssClass="orderbutton" />
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <h2>Registration:</h2>
                    <table width="550" border="0" cellpadding="2" cellspacing="2" class="alltable">
                        <tr class="tablebody">
                            <td width="30%" class="questionstext">Name:</td>
                            <td width="30%" class="answertext"><asp:TextBox ID="UserName" Width="200" runat="server"/></td>
                            <td class="answertext">
                                <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" 
                                ErrorMessage="You must enter a name" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="UserName" 
                                  ValidationGroup="CreateUserWizard1" Text="You must enter a name"/>
                            </td>
                       </tr>
                       <tr class="tablebody">
                            <td class="questionstext">Password: <small>(at least 4 characters)</small></td>
                            <td class="answertext"><asp:TextBox ID="Password" TextMode="Password" Width="200" runat="server"/></td>
                            <td class="answertext">
                                <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" 
                                ErrorMessage="You must enter a password" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="Password" 
                                  ValidationGroup="CreateUserWizard1" Text="You must enter a password"/>
                                <asp:RegularExpressionValidator ID="valPasswordLenght" runat="server" 
                                ErrorMessage="The password must be at least four characters" Text="The password must be at least four characters"
                                 ControlToValidate="Password" SetFocusOnError="true" Display="Dynamic"
                                  ValidationExpression="\w{4,}"  ValidationGroup="CreateUserWizard1"/>
                            </td>
                       </tr>
                       <tr class="tablebody">
                            <td class="questionstext">Confirm Password:</td>
                            <td  class="answertext"><asp:TextBox ID="ConfirmPassword" TextMode="Password" Width="200" runat="server"/></td>
                            <td  class="answertext">
                                <asp:RequiredFieldValidator ID="valRequireConfirmPassword" runat="server" 
                                ErrorMessage="You must enter a password confirmation" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="ConfirmPassword" 
                                  ValidationGroup="CreateUserWizard1" Text="You must enter a password confirmation"/>
                                <asp:CompareValidator ID="valComparePasword" runat="server"
                                 ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                 ErrorMessage="Password and confirm password must match" SetFocusOnError="true"
                                 Display="Dynamic" ValidationGroup="CreateUserWizard1" Text="Password and confirm password must match"/>
                            </td>
                       </tr>
                       <tr class="tablebody"> 
                            <td width="30%" class="questionstext">E-mail:</td>
                            <td width="30%"  class="answertext"><asp:TextBox ID="Email" Width="200" runat="server"/></td>
                            <td  class="answertext">
                                <asp:RequiredFieldValidator ID="valRequireEmail" runat="server" 
                                ErrorMessage="You must enter the e-mail" SetFocusOnError="true"
                                 Display="Dynamic" ControlToValidate="Email" 
                                  ValidationGroup="CreateUserWizard1" Text="You must enter the e-mail"/>
                                  <asp:RegularExpressionValidator runat="server" ID="valEmailPattern"  
                                  Display="Dynamic" SetFocusOnError="true" ValidationGroup="CreateUserWizard1"
                        ControlToValidate="Email" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ErrorMessage="Is not the correct format e-mail" Text="Is not the correct format e-mail"/>
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
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Personal data">
                <h2>Fill out your personal data</h2>
                <p>These data will help you to quickly place your order at our store</p>
                <uc1:UserProfile ID="UserProfile1"  runat="server" />             
            </asp:WizardStep>            
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
        <MailDefinition               
                From="info@echo-h.ru"
                Subject="Echo Of Hollywood - Registration" IsBodyHtml="true">
             </MailDefinition> 
    </asp:CreateUserWizard>
</asp:Content>

