<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderEngl.ascx.cs" Inherits="Controls_OrderEngl" %>
<h2>
                        <asp:Label ID="lblOrderNumber" runat="server" /></h2>
                    <table border="1" cellpadding="0" cellspacing="0" class="MainAdminTable" width="600px">
                        <tr>
                            <td class="MainAdminTableHeader">
                                Order Date:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblAddedDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Customer login:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblUser" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                First And Last Names:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblFIO" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Address:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblAddress" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Phone number:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblPhone" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                E-mail:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblEmail" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Contact time:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblTime" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Note:
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblNote" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Order
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Repeater ID="rptOrderItems" runat="server">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkProduct" runat="server" Text='<%#Eval("DinamicTitleEng") %>' NavigateUrl='<%#GetNavigateURL((int)Eval("ProdSizeID")) %>' />
                                        -
                                        <%#Eval("Qty") %><br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Cost of order
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblOrderSum" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Deliver method
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblDeliver" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MainAdminTableHeader">
                                Sum total
                            </td>
                            <td class="MainAdminTableItem">
                                <asp:Label ID="lblTotalSum" runat="server" />
                            </td>
                        </tr>
                        <asp:Panel ID="pnlViewStatus" runat="server">
                        <tr>
                            <td class="MainAdminTableHeader">
                                Order Status
                            </td>
                            
                            <td class="MainAdminTableItem">
                                <asp:DropDownList ID="ddlStatusEdit" runat="server" DataTextField="OrderStaus" DataValueField="OrderStatusID" />
                            </td>
                        </tr>
                        </asp:Panel>
                    </table>