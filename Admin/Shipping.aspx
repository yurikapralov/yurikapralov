<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.master" AutoEventWireup="true"
    CodeFile="Shipping.aspx.cs" Inherits="Admin_Shipping" %>

<%@ MasterType VirtualPath="~/Admin/Admin2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlShipping" runat="server">
        <ContentTemplate>
            Способы доставки:
            <asp:DropDownList ID="ddlDeliveryMethods" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeliveryMethods_SelectedIndexChanged"
                CssClass="span4">
                <asp:ListItem Text="EMS Гарантпост" Value="1" />
                <asp:ListItem Text="Доставка по Москве" Value="2" />
            </asp:DropDownList>
            <h3>
                <asp:Label ID="lblDeliveyTitle" runat="server" /></h3>
            <asp:Panel ID="pnlEMS" runat="server" Visible="false">
                <div class="row-fluid">
                    <div class="span6">
                        <fieldset>
                            <div class="control-group">
                                <label class="control-label" for="ddlZone">
                                    Тарифная зона:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"
                                        CssClass="span6" ClientIDMode="Static">
                                        <asp:ListItem Text="Зона - 0" Value="0" />
                                        <asp:ListItem Text="Зона - 1" Value="1" />
                                        <asp:ListItem Text="Зона - 2" Value="2" />
                                        <asp:ListItem Text="Зона - 3" Value="3" />
                                        <asp:ListItem Text="Зона - 4" Value="4" />
                                        <asp:ListItem Text="Зона - 5" Value="5" />
                                        <asp:ListItem Text="Зона - 6" Value="6" />
                                        <asp:ListItem Text="Зона - 7" Value="7" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="lbCiities">
                                    Города в выбранной зоне:</label>
                                <div class="controls">
                                    <asp:ListBox ID="lbCiities" runat="server" DataTextField="City_RUS" DataValueField="CityID"
                                        Rows="20" SelectionMode="Multiple" CssClass="span6" ClientIDMode="Static" />
                                    <asp:Button ID="btnUpdateZone" runat="server" Text="Обновить" CssClass="btn btn-primary"
                                        OnClick="btnUpdateZone_Click" />
                                </div>
                            </div>
                        </fieldset>
                        <h3>
                            Добавить город:</h3>
                        <fieldset>
                            <div class="control-group">
                                <label class="control-label" for="txtRusName">
                                    Название рус*.:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtRusName" runat="server" CssClass="span6" ClientIDMode="Static" />
                                    <asp:RequiredFieldValidator ID="valReqRusName" runat="server" ControlToValidate="txtRusName"
                                        Display="Dynamic" Text="*" ValidationGroup="AddCity" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="txtEngName">
                                    Название англ*.:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtEngName" runat="server" CssClass="span6" ClientIDMode="Static" />
                                    <asp:RequiredFieldValidator ID="valReqEngName" runat="server" ControlToValidate="txtEngName"
                                        Display="Dynamic" Text="*" ValidationGroup="AddCity" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="txtDescription">
                                    Описание:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="span6" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnCityAdd" runat="server" Text="Добавить" ValidationGroup="AddCity"
                                    OnClick="btnCityAdd_Click" CssClass="btn btn-primary" />
                            </div>
                            <asp:ValidationSummary ID="valSum" ValidationGroup="AddCity" runat="server" ShowMessageBox="false"
                                ShowSummary="true" CssClass="alert alert-error span3" />
                        </fieldset>
                    </div>
                    <div class="span6">
                        <asp:Panel ID="pnlPrices" runat="server" Visible="false">
                            <h3>
                                Тарифная сетка</h3>
                            <asp:ListView ID="lvZonePrice" runat="server" DataKeyNames="Id">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered table-condensed span6" border="0"
                                        cellpadding="0" cellspacing="0">
                                        <tr>
                                            <th class="center">
                                                Количество
                                            </th>
                                            <th class="center">
                                                Областной центр
                                            </th>
                                            <th class="center">
                                                Территория области
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceHolder" runat="server" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("Qty") %>
                                        </td>
                                        <td>
                                            <%#Eval("CenterPrice", "{0:C}")%>
                                        </td>
                                        <td>
                                            <%#Eval("RegionPrice", "{0:C}")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="span6">
                                <asp:Button ID="btnEditEMSPrice" runat="server" OnClick="btnEditEMSPrice_Click" Text="Редактировать"
                                    CssClass="btn btn-primary" />
                            </div>
                            <asp:ListView ID="lvZonePriceEdit" runat="server" DataKeyNames="Id">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered table-condensed span6" border="0"
                                        cellpadding="0" cellspacing="0">
                                        <tr class="MainAdminTableHeader">
                                            <th>
                                                Количество
                                            </th>
                                            <th class="center">
                                                Областной центр
                                            </th>
                                            <th class="center">
                                                Территория области
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceHolder" runat="server" class="MainAdminTableItem" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("Qty") %>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCenterPrice" runat="server" Text='<%#Bind("CenterPrice","{0:F2}")%>'
                                                CssClass="span10" />
                                            <asp:RequiredFieldValidator ID="valCenterPrice" runat="server" ControlToValidate="txtCenterPrice"
                                                Display="Dynamic" ValidationGroup="EditPrice">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmpCenterPrice" runat="server" ControlToValidate="txtCenterPrice"
                                                Operator="GreaterThanEqual" Type="Double" Display="Dynamic" ValidationGroup="EditPrice"
                                                ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRegionPrice" runat="server" Text='<%#Bind("RegionPrice","{0:F2}")%>'
                                                CssClass="span10" />
                                            <asp:RequiredFieldValidator ID="valRegionPrice" runat="server" ControlToValidate="txtRegionPrice"
                                                Display="Dynamic" ValidationGroup="EditPrice">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmpRegionPrice" runat="server" ControlToValidate="txtRegionPrice"
                                                Operator="GreaterThanEqual" Type="Double" Display="Dynamic" ValidationGroup="EditPrice"
                                                ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="span6">
                                <asp:Button ID="btnUpdateEMSPrice" runat="server" Visible="false" Text="Обновить"
                                    OnClick="btnUpdateEMSPrice_Click" CssClass="btn btn-primary" ValidationGroup="EditPrice" />
                                <asp:Button ID="btnCancelEMSPrice" runat="server" Visible="false" Text="Отмена" CssClass="btn"
                                    OnClick="btnCancelEMSPrice_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMoscow" runat="server" Visible="false">
                <asp:Panel ID="pnlMoscowPriceInfo" runat="server">
                    <div class="control-group">
                            Стоимость доставки по Москве:
                        <asp:Literal ID="lblMoscowPrice" runat="server" />
                    </div>
                    <div>
                        <asp:Button ID="btnMoscowPriceEdit" runat="server" CssClass="btn btn-primary" Text="Изменить"
                            OnClick="btnMoscowPriceEdit_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlMoscowPriceEdit" runat="server" Visible="false">
                    <div class="control-group">
                        <label class="control-label" style="width: 200px;" for="txtMoscowPrice">
                            Стоимость доставки по Москве:</label>
                        <div class="controls">
                            <asp:TextBox ID="txtMoscowPrice" runat="server" CssClass="span1" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="valMoscowPrice" runat="server" ControlToValidate="txtMoscowPrice"
                                Display="Dynamic" ValidationGroup="EditMoscowPrice">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmpRegionPrice" runat="server" ControlToValidate="txtMoscowPrice"
                                Operator="GreaterThanEqual" Type="Double" Display="Dynamic" ValidationGroup="EditMoscowPrice"
                                ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                        </div>
                    </div>
                    <div>
                        <asp:Button ID="btnMoscowPriceUpdate" runat="server" CssClass="btn btn-primary" Text="Обновить"
                            ValidationGroup="EditMoscowPrice" OnClick="btnMoscowPriceUpdate_Click" />
                        <asp:Button ID="btnMoscowPriceCancel" runat="server" CssClass="btn" Text="Отмена"
                            OnClick="btnMoscowPriceCancel_Click" />
                    </div>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="EmptySpace">
    </div>
</asp:Content>
