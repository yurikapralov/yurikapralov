<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Info_old.aspx.cs" Inherits="Admin_Info_old" Theme="Admin" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content_total">
        <div id="secondary_menu">
            <asp:LinkButton ID="lbtnOrderStatus" runat="server" Text="Статус заказа" OnClick="lbtnOrderStatus_Click" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lbtnNews" runat="server" Text="Новости сайта" OnClick="lbtnNews_Click" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lbtnRate" runat="server" Text="Курс валют" OnClick="lbtnRate_Click" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lbtnNews2" runat="server" Text="Новый раздел новостей" OnClick="lbtnNews2_Click" />
        </div>
        <p>
        </p>
        <asp:UpdatePanel ID="uppnlOrderStatus" runat="server" Visible="false">
            <ContentTemplate>
                <h2>
                    Управление статусом заказа</h2>
                <p>
                </p>
                <asp:GridView ID="gvwOrderStatus" runat="server" AutoGenerateColumns="false" DataKeyNames="OrderStatusId"
                    OnRowEditing="gvwOrderStatus_RowEditing" OnRowCancelingEdit="gvwOrderStatus_RowCancelingEdit"
                    OnRowCreated="gvwOrderStatus_RowCreated" OnRowDeleting="gvwOrderStatus_RowDeleting"
                    OnRowUpdating="gvwOrderStatus_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="OrderStatusId" HeaderText="&nbsp;Id&nbsp;" ReadOnly="True" />
                        <asp:BoundField DataField="OrderStaus" HeaderText="Имя" />
                        <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" EditText="Изменить"
                            DeleteText="Удалить" />
                    </Columns>
                </asp:GridView>
                <p>
                    <asp:Label ID="lblErrorDelStat" runat="server" ForeColor="Red" Text="Данный статус содержит заказы. Его удаление не возможно."
                        Visible="false" />
                </p>
                Добавить новый статус:
                <asp:TextBox ID="txtStatusAdd" runat="server" />
                <asp:RequiredFieldValidator ID="valReqStatus" ControlToValidate="txtStatusAdd" Text="*"
                    Display="Dynamic" runat="server" ValidationGroup="AddStatus" />
                <asp:Button ID="btnStatusAdd" runat="server" Text="Добавить" CssClass="TableButton"
                    ValidationGroup="AddStatus" OnClick="btnStatusAdd_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="uppnlRate" runat="server" Visible="false">
            <ContentTemplate>
                <h2>
                    Управление курсом валют</h2>
                <p>
                </p>
                <asp:ListView ID="lvRates" runat="server" DataKeyNames="Id" OnItemCanceling="lvRates_ItemCanceling"
                    OnItemEditing="lvRates_ItemEditing" OnItemUpdating="lvRates_ItemUpdating">
                    <LayoutTemplate>
                        <table class="MainAdminTable" border="1" cellpadding="0" cellspacing="0">
                            <tr class="MainAdminTableHeader">
                                <td>
                                    Валюта
                                </td>
                                <td>
                                    Курс
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="itemPlaceholder" runat="server" class="MainAdminTableItem" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Currency")%>
                            </td>
                            <td>
                                <%#Eval("RateUS") %>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnEdit" runat="server" Text="Редактировать" CommandName="Edit"
                                    CommandArgument='<%#Eval("Id") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Currency")%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRate" runat="server" Text='<%#Bind("RateUS","{0:N}") %>' Font-Size="X-Small" />
                                <asp:RequiredFieldValidator ID="valReqRate" runat="server" Display="Dynamic" ControlToValidate="txtRate"
                                    ValidationGroup="qUpdate">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cmpRate" runat="server" ControlToValidate="txtRate" Operator="GreaterThanEqual"
                                    Type="Double" ValidationGroup="qUpdate" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnUpdate" runat="server" Text="Обновить" CommandName="Update"
                                    CommandArgument='<%#Eval("Id") %>' ValidationGroup="qUpdate" />
                                <asp:LinkButton ID="lbtnCancel" runat="server" Text="Отмена" CommandName="Cancel"
                                    CommandArgument='<%#Eval("Id") %>' />
                            </td>
                        </tr>
                    </EditItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="uppnlNews" runat="server" Visible="false">
            <ContentTemplate>
                <h2>
                    Управление новостями сайта</h2>
                <p>
                </p>
                <p>
                    <asp:Label ID="lblNewsStatus" runat="server" Visible="false" /></p>
                <asp:TextBox ID="txtNews" runat="server" TextMode="MultiLine" Rows="10" Width="400" /><br />
                <asp:Button ID="btnUpdateNews" runat="server" CssClass="TableButton" Text="Обновить"
                    OnClick="btnUpdateNews_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="uppnlNews2" runat="server" Visible="false">
            <ContentTemplate>
                <h2>
                    Список новостей</h2>
                <asp:GridView ID="gvwNews" runat="server" AutoGenerateColumns="false" 
                    DataKeyNames="Id" onrowcreated="gvwNews_RowCreated" 
                    onrowdeleting="gvwNews_RowDeleting" 
                    onselectedindexchanging="gvwNews_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="NewsDate" HeaderText="Дата Новости" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Header" HeaderText="Заголовок" />
                        <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" DeleteText="Удалить"
                            SelectText="Выбрать" />
                    </Columns>
                    <EmptyDataTemplate>
                    Новостей еще нет.
                    </EmptyDataTemplate>
                </asp:GridView>
                <h2>
                    <asp:Label ID="lblNewsEditHeader" runat="server" Text="Добавление новости" />
                </h2>
                <table class="MainAdminTable">
                    <tr>
                        <td class="MainAdminTableHeader">
                            Дата
                        </td>
                        <td class="MainAdminTableItem" width="500px" align="left">
                            <asp:TextBox ID="txtDate" runat="server" Width="100px" />
                             <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate"
                                        Format="dd.MM.yyyy">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqValDate" runat="server" ErrorMessage="Поле даты не должно быть пустым"
                                        ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="valNews">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="compValDate" runat="server" ErrorMessage="Поле даты имеет неверный формат"
                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Date" ValidationGroup="valNews">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="MainAdminTableHeader">
                            Заголовок
                        </td>
                        <td class="MainAdminTableItem">
                            <asp:TextBox ID="txtHeader" runat="server" TextMode="MultiLine" Rows="5" Width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MainAdminTableHeader">
                            Текст
                        </td>
                        <td class="MainAdminTableItem">
                            <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="10" Width="95%" />
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnEchoNewsAdd" runat="server" Text="Добавить" 
                            ValidationGroup="valNews" CssClass="TableButton" 
                            onclick="btnEchoNewsAdd_Click" />
                        <asp:Button runat="server" ID="btnEchoNewsCancel" Text="Отмена" 
                            CssClass="TableButton" onclick="btnEchoNewsCancel_Click"/>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="2" align="center">
                    <asp:ValidationSummary ID="valsum" ValidationGroup="valNews" runat="server" ShowMessageBox="false" ShowSummary="true" />
                    </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="EmptySpace">
        </div>
    </div>
</asp:Content>
