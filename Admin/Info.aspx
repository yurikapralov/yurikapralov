<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.master" AutoEventWireup="true"
    CodeFile="Info.aspx.cs" Inherits="Admin_Info" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Admin/Admin2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul id="infotab" class="nav nav-tabs">
        <li class="active"><a href="#ord_status_tab" data-toggle="tab">Статус заказа</a></li>
        <li><a href="#news_tab" data-toggle="tab">Новости сайта</a></li>
        <li><a href="#rate_tab" data-toggle="tab">Курс валют</a></li>
    </ul>
    <div class="tab-content">
        <div class="tab-pane active" id="ord_status_tab">
            <div class="row">
                <div class="span7">
                    <asp:UpdatePanel ID="uppnlOrderStatus" runat="server">
                        <ContentTemplate>
                            <h3>
                                Управление статусом заказа</h3>
                            <asp:GridView ID="gvwOrderStatus" runat="server" AutoGenerateColumns="false" DataKeyNames="OrderStatusId"
                                OnRowEditing="gvwOrderStatus_RowEditing" OnRowCancelingEdit="gvwOrderStatus_RowCancelingEdit"
                                OnRowCreated="gvwOrderStatus_RowCreated" OnRowDeleting="gvwOrderStatus_RowDeleting"
                                OnRowUpdating="gvwOrderStatus_RowUpdating" CssClass="table table-striped table-bordered table-condensed"
                                GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="OrderStatusId" HeaderText="&nbsp;Id&nbsp;" ReadOnly="True"
                                        HeaderStyle-CssClass="center" />
                                    <asp:BoundField DataField="OrderStaus" HeaderText="Имя" HeaderStyle-CssClass="center" />
                                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" EditText="Изменить"
                                        DeleteText="Удалить" />
                                </Columns>
                            </asp:GridView>
                            <p>
                                <asp:Label ID="lblErrorDelStat" runat="server" ForeColor="Red" Text="Данный статус содержит заказы. Его удаление не возможно."
                                    Visible="false" />
                            </p>
                            <h3>
                                Добавить новый статус:</h3>
                            <fieldset>
                                <div class="control-group">
                                    <label class="control-label" for="txtStatusAdd">
                                        Имя:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtStatusAdd" runat="server" CssClass="span5" ClientIDMode="Static" />
                                        <asp:RequiredFieldValidator ID="valReqStatus" ControlToValidate="txtStatusAdd" Text="*"
                                            Display="Dynamic" runat="server" ValidationGroup="AddStatus" />
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <asp:Button ID="btnStatusAdd" runat="server" Text="Добавить" CssClass="btn btn-primary"
                                        ValidationGroup="AddStatus" OnClick="btnStatusAdd_Click" />
                                </div>
                                <asp:ValidationSummary ID="valSumStat" ValidationGroup="AddStatus" runat="server"
                                    ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-error span3" />
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
        </div>
    </div>
    <div class="tab-pane" id="news_tab">
        <div class="row">
            <div class="span12">
                <asp:UpdatePanel ID="uppnlNews2" runat="server">
                    <ContentTemplate>
                        <h3>
                            Список новостей</h3>
                        <asp:GridView ID="gvwNews" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                            OnRowCreated="gvwNews_RowCreated" OnRowDeleting="gvwNews_RowDeleting" 
                            OnSelectedIndexChanging="gvwNews_SelectedIndexChanging" CssClass="table table-striped table-bordered table-condensed"
                                GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="NewsDate" HeaderText="Дата Новости" DataFormatString="{0:d}" HeaderStyle-CssClass="center"/>
                                <asp:BoundField DataField="Header" HeaderText="Заголовок" HeaderStyle-CssClass="center"/>
                                <asp:CheckBoxField DataField="avail" HeaderText="Показывать" HeaderStyle-CssClass="center"/>
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
                        <fieldset>
                                <div class="control-group">
                                    <label class="control-label" for="txtDate">
                                        Дата:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="span3"  ClientIDMode="Static"/>
                                    <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate"
                                        Format="dd.MM.yyyy">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqValDate" runat="server" ErrorMessage="Поле даты не должно быть пустым"
                                        ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="valNews">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="compValDate" runat="server" ErrorMessage="Поле даты имеет неверный формат"
                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Date" ValidationGroup="valNews">*</asp:CompareValidator>
                                    </div>
                                </div>
                                 <div class="control-group">
                                     <label class="control-label" for="txtHeader">Заголовок</label>
                                     <div class="controls">
                                         <asp:TextBox ID="txtHeader" runat="server" TextMode="MultiLine" Rows="5" CssClass="span5" ClientIDMode="Static"/>
                                         </div>
                                 </div>
                                 <div class="control-group">
                                     <label class="control-label" for="txtBody">Текст</label>
                                     <div class="controls">
                                         <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="5" CssClass="span5" ClientIDMode="Static"/>
                                         </div>
                                 </div>
                                 <div class="control-group">
                                     <label class="control-label" for="cbxAvail">Показывать</label>
                                     <div class="controls">
                                         <asp:CheckBox runat="server" ID="cbxAvail" ClientIDMode="Static"/>
                                         </div>
                                 </div>
                                <div class="form-actions">
                                   <asp:Button ID="btnEchoNewsAdd" runat="server" Text="Добавить" ValidationGroup="valNews"
                                        CssClass="btn btn-primary" OnClick="btnEchoNewsAdd_Click" />
                                    <asp:Button runat="server" ID="btnEchoNewsCancel" Text="Отмена" CssClass="btn"
                                        OnClick="btnEchoNewsCancel_Click" />
                                </div>
                                <asp:ValidationSummary ID="valsum" ValidationGroup="valNews" runat="server" ShowMessageBox="false"
                                        ShowSummary="true" CssClass="alert alert-error span3" />
                            </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="tab-pane" id="rate_tab">
         <div class="row">
            <div class="span5">
        <asp:UpdatePanel ID="uppnlRate" runat="server">
            <ContentTemplate>
                <h3>
                    Управление курсом валют</h3>
                <p>
                </p>
                <asp:ListView ID="lvRates" runat="server" DataKeyNames="Id" OnItemCanceling="lvRates_ItemCanceling"
                    OnItemEditing="lvRates_ItemEditing" OnItemUpdating="lvRates_ItemUpdating">
                    <LayoutTemplate>
                        <table class="table table-striped table-bordered table-condensed" border="0" cellpadding="0" cellspacing="0">
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
                                <asp:TextBox ID="txtRate" runat="server" Text='<%#Bind("RateUS","{0:N}") %>' CssClass="span1" />
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
        </div>
        </div>
    </div>
    </div>
    <div id="EmptySpace">
    </div>
</asp:Content>
