<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.master" AutoEventWireup="true"
    CodeFile="Orders.aspx.cs" Inherits="Admin_Orders" %>

<%@ MasterType VirtualPath="~/Admin/Admin2.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/Order.ascx" TagName="Order" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppnlTotal" runat="server">
        <ContentTemplate>
            <ul id="order_search_tab" class="nav nav-tabs nonprint">
                <li class="active"><a href="#stat_search" data-toggle="tab">Поиск по статусу</a></li>
                <li><a href="#name_search" data-toggle="tab">Поиск по Фамилии</a></li>
                <li><a href="#user_search" data-toggle="tab">Поиск по зарегистрированному пользователю</a></li>
                <li><a href="#email_search" data-toggle="tab">Поиск по e-mail</a></li>
                <li><a href="#nmbr_search" data-toggle="tab">Поиск по номеру заказа</a></li>
                <li><a href="#city_search" data-toggle="tab">Поиск по городу</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="stat_search">
                    <asp:DropDownList ID="ddlOrderStatus" runat="server" DataValueField="OrderStatusId"
                        DataTextField="OrderStaus" CssClass="span2" />
                    в период с
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="span2" />
                    <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                        Format="dd.MM.yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqValFrom" runat="server" ErrorMessage="Поле начала периода не должно быть пустым"
                        ControlToValidate="txtFromDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="valStat">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compValFrom" runat="server" ErrorMessage="Поле начала периода имеет неверный формат"
                        ControlToValidate="txtFromDate" Display="Dynamic" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Date" ValidationGroup="valStat">*</asp:CompareValidator>
                    по
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="span2" />
                    <asp:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                        Format="dd.MM.yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqValTo" runat="server" ErrorMessage="Поле конца периода не должно быть пустым"
                        ControlToValidate="txtToDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="valStat">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compValTo" runat="server" ErrorMessage="Поле конца периода имеет неправильный формат "
                        ControlToValidate="txtToDate" Display="Dynamic" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Date">*</asp:CompareValidator>
                    <asp:Button ID="btnShowByStatus" runat="server" Text="Показать" ValidationGroup="valStat"
                        OnClick="btnShowByStatus_Click" CssClass="btn btn-success" /><br />
                    <asp:ValidationSummary runat="server" ID="valSumStat" ValidationGroup="valStat" ShowMessageBox="false"
                        ShowSummary="true" CssClass="alert alert-error span5" />
                </div>
                <div class="tab-pane" id="name_search">
                    Введите частично или полностью фамилию заказчика:
                    <asp:TextBox ID="txtFIO" runat="server" CssClass="span3" />
                    <asp:AutoCompleteExtender ID="txtFIO_AutoCompleteExtender" runat="server" TargetControlID="txtFIO"
                        ServiceMethod="GetFIOs" MinimumPrefixLength="1">
                    </asp:AutoCompleteExtender>
                    <asp:RequiredFieldValidator ID="valReqFIO" runat="server" ControlToValidate="txtFIO"
                        Text="Введите фамилию" Display="Dynamic" ValidationGroup="valFIO" />
                    <asp:Button ID="btnShowByFIO" runat="server" Text="Показать" ValidationGroup="valFIO"
                        OnClick="btnShowByFIO_Click" CssClass="btn btn-success" />
                    <br />
                    <asp:ValidationSummary runat="server" ID="valSumFio" ValidationGroup="valFIO" ShowMessageBox="false"
                        ShowSummary="true" CssClass="alert alert-error span5" />
                </div>
                <div class="tab-pane" id="user_search">
                    Введите частично или полностью логин заказчика:
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="span3" />
                    <asp:AutoCompleteExtender ID="txtLogin_AutoCompleteExtender" runat="server" TargetControlID="txtLogin"
                        ServiceMethod="GetUsers" MinimumPrefixLength="1">
                    </asp:AutoCompleteExtender>
                    <asp:RequiredFieldValidator ID="valReqUser" runat="server" ControlToValidate="txtLogin"
                        Text="Введите логин" Display="Dynamic" ValidationGroup="valLogin" />
                    <asp:Button ID="btnShowByUser" runat="server" Text="Показать" ValidationGroup="valLogin"
                        OnClick="btnShowByUser_Click" CssClass="btn btn-success" /><br />
                    <asp:ValidationSummary runat="server" ID="valSumLogin" ValidationGroup="valLogin"
                        ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-error span5" />
                </div>
                <div class="tab-pane" id="email_search">
                    Введите частично или полностью e-mail заказчика:
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="span3" />
                    <asp:AutoCompleteExtender ID="txtEmail_AutoCompleteExtender" runat="server" TargetControlID="txtEmail"
                        ServiceMethod="GetEmails" MinimumPrefixLength="1">
                    </asp:AutoCompleteExtender>
                    <asp:RequiredFieldValidator ID="valReqEmail" runat="server" ControlToValidate="txtEmail"
                        Text="Введите e-mail" Display="Dynamic" ValidationGroup="valEmail" />
                    <asp:Button ID="btnShowByEmail" runat="server" Text="Показать" ValidationGroup="valEmail"
                        OnClick="btnShowByEmail_Click" CssClass="btn btn-success" /><br />
                    <asp:ValidationSummary runat="server" ID="valSumEmail" ValidationGroup="valEmail"
                        ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-error span5" />
                </div>
                <div class="tab-pane" id="nmbr_search">
                    Введите частично или полностью номер заказа:
                    <asp:TextBox ID="txtNumber" runat="server" CssClass="span3"/>
                    <asp:RequiredFieldValidator ID="valReqNumber" runat="server" ControlToValidate="txtNumber"
                        Text="Введите номер" Display="Dynamic" ValidationGroup="valNumber" />
                    <asp:Button ID="btnNumber" runat="server" Text="Показать" ValidationGroup="valNumber"
                        OnClick="btnNumber_Click" CssClass="btn btn-success" /><br />
                    <asp:ValidationSummary runat="server" ID="valSumNumber" ValidationGroup="valNumber"
                        ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-error span5" />
                </div>
                <div class="tab-pane" id="city_search">
                   Выберите город:
                    <asp:DropDownList ID="ddlCities" DataTextField="City_RUS" DataValueField="CityID"
                        runat="server" CssClass="span3"/>
                    <asp:Button ID="btnCity" runat="server" Text="Показать" OnClick="btnCity_Click" CssClass="btn btn-success"/> 
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppnlTotal">
                <ProgressTemplate>
                    <img src="bootstrap/img/loading25.gif" style="display: block; margin: 0 auto;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <h3 style="margin 20px 0px 10px 0px;"><asp:Literal ID="ltlHeader" runat="server"/></h3>
            <asp:Panel ID="pnlOrdersList" runat="server">
                <asp:ListView ID="lvOrders" runat="server" DataKeyNames="OrderID" OnPagePropertiesChanged="lvOrders_PagePropertiesChanged"
                    OnSelectedIndexChanging="lvOrders_SelectedIndexChanging" OnItemDeleting="lvOrders_ItemDeleting">
                    <LayoutTemplate>
                        <table class="table table-striped table-bordered table-condensed" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <th class="center">
                                    № Заказа
                                </th>
                                <th class="center">
                                    Дата
                                </th>
                                <th class="center">
                                    User
                                </th>
                                <th class="center">
                                    ФИО
                                </th>
                                <th class="center">
                                    E-mail
                                </th>
                                <th class="center">
                                    Город
                                </th>
                                <th class="center">
                                    Датали заказа
                                </th>
                                <th class="center">
                                    Статус заказа
                                </th>
                                <th class="center">
                                    В кредит
                                </th>
                                <th>
                                    &nbsp;
                                </th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server" class="MainAdminTableItem" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("OrderNumber") %>
                            </td>
                            <td>
                                <%#Eval("DateCreated") %>
                            </td>
                            <td>
                                <%#Eval("AddedBy") %>
                            </td>
                            <td>
                                <%#Eval("FIO") %>
                            </td>
                            <td>
                                <%#Eval("Email") %>
                            </td>
                            <td>
                                <%#Eval("City.City_RUS") %>
                            </td>
                            <td style="font-size:smaller;">
                                    <asp:Repeater ID="rptOrderItems" runat="server" DataSource='<%#Eval("OrdersItems") %>'>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkProduct" runat="server" Text='<%#Eval("DinamicTitle") %>' NavigateUrl='<%#GetNavigateURL((int)Eval("ProdSizeID")) %>' />
                                            -
                                            <%#Eval("Qty") %><br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </td>
                            <td>
                                <%#Eval("OrderStatus.OrderStaus") %>
                            </td>
                            <td>
                                <input type="checkbox" disabled="disabled"  <%#(bool)Eval("InCredit") ? "checked":"" %>/>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnSelect" runat="server" Text="Выбрать" CommandName="Select"
                                    CommandArgument='<%#Eval("OrderID") %>' OnClick="lbtnSelect_Click" />
                                <asp:LinkButton ID="lbtnDelete" runat="server" Text="Удалить" CommandName="Delete" ForeColor="Red"
                                    CommandArgument='<%#Eval("OrderID") %>' OnClientClick="return confirm('Вы действительно хотите удалить этот заказ?');" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <p>
                            Не одного заказа по заданным параметрам нет.</p>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:Panel ID="pnlPager" runat="server" HorizontalAlign="Center" Visible="false">
                    Перейти:
                    <asp:DataPager ID="pagerBottom" runat="server" PageSize="20" PagedControlID="lvOrders">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                            <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                NextPreviousButtonCssClass="command" />
                            <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                        </Fields>
                    </asp:DataPager>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlOrder" runat="server" Visible="false">
                <uc1:Order ID="OrderCtrl" runat="server" ViewOrderStatus="True" />
                <div>
                <asp:Button ID="btnUpdate" runat="server" Text="Обновить" CssClass="btn btn-primary"
                                OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Отмена" CssClass="btn" OnClick="btnCancel_Click" />
                            <asp:HyperLink ID="lnkPrint" runat="server" CssClass="btn btn-inverse" Target="_blank">Печать</asp:HyperLink>
                </div>
            </asp:Panel>
            <div id="EmptySpace">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
