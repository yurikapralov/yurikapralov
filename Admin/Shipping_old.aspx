<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Shipping_old.aspx.cs" Inherits="Admin_Shipping_old" Theme="Admin" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content_total">
        <asp:UpdatePanel ID="upnlShipping" runat="server">
            <ContentTemplate>
                <h2>
                    Способы доставки:
                    <asp:DropDownList ID="ddlDeliveryMethods" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeliveryMethods_SelectedIndexChanged">
                        <asp:ListItem Text="EMS Гарантпост" Value="1" />
                        <asp:ListItem Text="Доставка по Москве" Value="2" />
                    </asp:DropDownList>
                </h2>
                <p>
                </p>
                <h2>
                    <asp:Label ID="lblDeliveyTitle" runat="server" /></h2>
                <asp:Panel ID="pnlEMS" runat="server" Visible="false">
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td valign="top">
                                Выберите тарифную зону:
                                <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                    <asp:ListItem Text="Зона - 0" Value="0" />
                                    <asp:ListItem Text="Зона - 1" Value="1" />
                                    <asp:ListItem Text="Зона - 2" Value="2" />
                                    <asp:ListItem Text="Зона - 3" Value="3" />
                                    <asp:ListItem Text="Зона - 4" Value="4" />
                                    <asp:ListItem Text="Зона - 5" Value="5" />
                                    <asp:ListItem Text="Зона - 6" Value="6" />
                                    <asp:ListItem Text="Зона - 7" Value="7" />
                                </asp:DropDownList>
                                <br />
                                Города в выбранной зоне:<br />
                                <asp:ListBox ID="lbCiities" runat="server" DataTextField="City_RUS" DataValueField="CityID"
                                    Rows="20" SelectionMode="Multiple" Width="150px" />
                                <asp:Button ID="btnUpdateZone" runat="server" Text="Обновить" CssClass="TableButton"
                                    OnClick="btnUpdateZone_Click" />
                                <p>
                                </p>
                                Добавить город:<br />
                                Название рус*.:
                                <asp:TextBox ID="txtRusName" runat="server" />
                                <asp:RequiredFieldValidator ID="valReqRusName" runat="server" ControlToValidate="txtRusName"
                                    Display="Dynamic" Text="*" ValidationGroup="AddCity" />
                                Название англ*.:<asp:TextBox ID="txtEngName" runat="server" />
                                <asp:RequiredFieldValidator ID="valReqEngName" runat="server" ControlToValidate="txtEngName"
                                    Display="Dynamic" Text="*" ValidationGroup="AddCity" /><br />
                                Описание:
                                <asp:TextBox ID="txtDescription" runat="server" />
                                <asp:Button ID="btnCityAdd" runat="server" Text="Добавить" ValidationGroup="AddCity"
                                    OnClick="btnCityAdd_Click" CssClass="TableButton" />
                            </td>
                            <td valign="top">
                                <asp:Panel ID="pnlPrices" runat="server" Visible="false">
                                    <h2>
                                        Тарифная сетка</h2>
                                    <asp:ListView ID="lvZonePrice" runat="server" DataKeyNames="Id">
                                        <LayoutTemplate>
                                            <table class="MainAdminTable" border="1" cellpadding="0" cellspacing="0">
                                                <tr class="MainAdminTableHeader">
                                                    <td>
                                                        Количество
                                                    </td>
                                                    <td>
                                                        Областной центр
                                                    </td>
                                                    <td>
                                                        Территория области
                                                    </td>
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
                                                    <%#Eval("CenterPrice", "{0:C}")%>
                                                </td>
                                                <td>
                                                    <%#Eval("RegionPrice", "{0:C}")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <asp:Button ID="btnEditEMSPrice" runat="server" OnClick="btnEditEMSPrice_Click" Text="Редактировать"
                                        CssClass="TableButton" />
                                    <asp:ListView ID="lvZonePriceEdit" runat="server" DataKeyNames="Id">
                                        <LayoutTemplate>
                                            <table class="MainAdminTable" border="1" cellpadding="0" cellspacing="0">
                                                <tr class="MainAdminTableHeader">
                                                    <td>
                                                        Количество
                                                    </td>
                                                    <td>
                                                        Областной центр
                                                    </td>
                                                    <td>
                                                        Территория области
                                                    </td>
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
                                                        Font-Size="X-Small" />
                                                    <asp:RequiredFieldValidator ID="valCenterPrice" runat="server" ControlToValidate="txtCenterPrice"
                                                        Display="Dynamic" ValidationGroup="EditPrice">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cmpCenterPrice" runat="server" ControlToValidate="txtCenterPrice"
                                                        Operator="GreaterThanEqual" Type="Double" Display="Dynamic" ValidationGroup="EditPrice"
                                                        ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRegionPrice" runat="server" Text='<%#Bind("RegionPrice","{0:F2}")%>'
                                                        Font-Size="X-Small" />
                                                    <asp:RequiredFieldValidator ID="valRegionPrice" runat="server" ControlToValidate="txtRegionPrice"
                                                        Display="Dynamic" ValidationGroup="EditPrice">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cmpRegionPrice" runat="server" ControlToValidate="txtRegionPrice"
                                                        Operator="GreaterThanEqual" Type="Double" Display="Dynamic" ValidationGroup="EditPrice"
                                                        ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <asp:Button ID="btnUpdateEMSPrice" runat="server" Visible="false" Text="Обновить"
                                        OnClick="btnUpdateEMSPrice_Click" CssClass="TableButton" ValidationGroup="EditPrice" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlMoscow" runat="server" Visible="false">
                    <asp:Panel ID="pnlMoscowPriceInfo" runat="server">
                        Стоимость доставки по Москве:
                        <asp:Label ID="lblMoscowPrice" runat="server" />
                        <asp:Button ID="btnMoscowPriceEdit" runat="server" CssClass="TableButton" Text="Изменить"
                            OnClick="btnMoscowPriceEdit_Click" />
                    </asp:Panel>
                    <asp:Panel ID="pnlMoscowPriceEdit" runat="server" Visible="false">
                        Стоимость доставки по Москве:
                        <asp:TextBox ID="txtMoscowPrice" runat="server" />
                        <asp:RequiredFieldValidator ID="valMoscowPrice" runat="server" ControlToValidate="txtMoscowPrice"
                            Display="Dynamic" ValidationGroup="EditMoscowPrice">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cmpRegionPrice" runat="server" ControlToValidate="txtMoscowPrice"
                            Operator="GreaterThanEqual" Type="Double" Display="Dynamic" ValidationGroup="EditMoscowPrice"
                            ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                        <asp:Button ID="btnMoscowPriceUpdate" runat="server" CssClass="TableButton" Text="Обновить"
                            ValidationGroup="EditMoscowPrice" OnClick="btnMoscowPriceUpdate_Click" />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="EmptySpace">
        </div>
    </div>
</asp:Content>
