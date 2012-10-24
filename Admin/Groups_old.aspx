<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Groups_old.aspx.cs" Inherits="Admin_Groups_old" Theme="Admin" %>
<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content_total">
        <h2>           
            Список разделов</h2>
        <br />
        <asp:GridView ID="gvwGroups" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="GroupID" 
            onselectedindexchanging="gvwGroups_SelectedIndexChanging" 
            onrowcreated="gvwGroups_RowCreated" onrowdeleting="gvwGroups_RowDeleting">
        <Columns>
             <asp:BoundField DataField="GroupOrder" HeaderText="Порядок сортировки" />
            <asp:BoundField DataField="GroupID" HeaderText="GroupID"/>
            <asp:BoundField DataField="GroupNameRus" HeaderText="Русское имя"/>
            <asp:BoundField DataField="GroupNameEng" HeaderText="Английское имя"/>
             <asp:CheckBoxField DataField="AvaliableInEngilsh"  HeaderText="Доступно в англ. версии"/>
             <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" DeleteText="Удалить" SelectText="Выбрать" ControlStyle-ForeColor="#33333" />
        </Columns>
    </asp:GridView>

        <p>
        </p>
        <p>
        </p>
        <asp:Panel ID="pnlEditGroup" runat="server">
            <h2>
                <asp:Label ID="lblEditHeader" runat="server" Text="Добавление нового раздела" /></h2>
            <br />
            <table class="MainAdminTable">
                <tr>
                    <td class="MainAdminTableHeader" width="200">
                        Русское имя:
                    </td>
                    <td class="MainAdminTableItem" width="300" align="left">
                        <asp:TextBox ID="txtGroupNameRus" runat="server" Width="280" />
                        <asp:RequiredFieldValidator ID="valRequireNameRus" ControlToValidate="txtGroupNameRus"
                            runat="server" ErrorMessage="Требуется вести русское имя" Display="Dynamic" ValidationGroup="EditGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Английское имя:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:TextBox ID="txtGroupNameEng" runat="server" Width="280" />
                        <asp:RequiredFieldValidator ID="valRequireNameEng" ControlToValidate="txtGroupNameEng"
                            runat="server" ErrorMessage="Требуется вести английское имя" Display="Dynamic" ValidationGroup="EditGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Порядок сортировки:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                    <asp:TextBox ID="txtGroupOrder" runat="server" /> 
                        <asp:NumericUpDownExtender ID="txtGroupOrder_NumericUpDownExtender" 
                            runat="server" Maximum="30" Minimum="1" Width="60" TargetControlID="txtGroupOrder">
                        </asp:NumericUpDownExtender>
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Доступна в английской версии:
                    </td>
                    <td class="MainAdminTableItem">
                        <asp:CheckBox ID="cbxAvaliable" runat="server" Checked="true" />
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnAddGroup" runat="server" Text="Добавить"
                            ValidationGroup="EditCat" CssClass="TableButton" 
                            onclick="btnAddGroup_Click" />
                        <asp:Button ID="btnCancelGroup" runat="server" Text="Отмена"
                            CssClass="TableButton" onclick="btnCancelGroup_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:ValidationSummary ID="valSum" ValidationGroup="EditGroup" runat="server" ShowMessageBox="false"
                            ShowSummary="true" />
                    </td>
                </tr>
                </table>
                </asp:Panel>
                        </div>
</asp:Content>

