<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order1.ascx.cs" Inherits="Controls_Order1" %>
<h2>
                        <asp:Label ID="lblOrderNumber" runat="server" /></h2>
                    <div class="row">
                    <div class="span12">
                    <table border="1" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-condensed">
                        <tr>
                            <td>
                                Дата заказа:
                            </td>
                            <td style="width:70%;">
                                <asp:Label ID="lblAddedDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Login покупателя:
                            </td>
                            <td>
                                <asp:Label ID="lblUser" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ФИО покупателя:
                            </td>
                            <td>
                                <asp:Label ID="lblFIO" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Адрес:
                            </td>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Телефон:
                            </td>
                            <td>
                                <asp:Label ID="lblPhone" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                E-mail:
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Время для связи:
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Замечания:
                            </td>
                            <td>
                                <asp:Label ID="lblNote" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Заказ
                            </td>
                            <td>
                            <table style="width:100%;" cellpadding="0" cellpadding="0" border="1">
                            <tr style="background-color:Gray; color:White; font-weight:bold;">
                            <td>Артикул</td>
                            <td>Кол-во</td>
                            <td>Цена</td>
                            <td>Сумма</td>
                            </tr>
                            
                                <asp:Repeater ID="rptOrderItems" runat="server">
                                    <ItemTemplate>
                                     <tr>
                                    <td>
                                        <asp:HyperLink ID="lnkProduct" runat="server" Text='<%#Eval("DinamicTitle") %>' NavigateUrl='<%#GetNavigateURL((int)Eval("ProdSizeID")) %>' />
                                    </td>
                                    <td>
                                        <%#Eval("Qty") %>
                                    </td>
                                    <td>
                                        <%#Eval("PriceForSale","{0:c}") %>
                                    </td>
                                     <td>
                                        <%#Eval("Summ", "{0:c}")%>
                                    </td>
                                    </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Сумма заказа
                            </td>
                            <td>
                                <asp:Label ID="lblOrderSum" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Способ доставки
                            </td>
                            <td>
                                <asp:Label ID="lblDeliver" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Общая сумма
                            </td>
                            <td>
                                <asp:Label ID="lblTotalSum" runat="server" />
                            </td>
                        </tr>
                        <asp:Panel ID="pnlViewStatus" runat="server">
                        <tr>
                            <td>
                                Статус заказа
                            </td>
                            
                            <td>
                                <asp:DropDownList ID="ddlStatusEdit" runat="server" DataTextField="OrderStaus" DataValueField="OrderStatusID" />
                            </td>
                        </tr>
                        </asp:Panel>
                    </table>
                    </div>
                    </div>