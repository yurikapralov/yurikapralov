<%@ Page Title="" Language="C#" MasterPageFile="~/Platinum/Platinum.master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="Platinum_Products" %>

<%@ MasterType VirtualPath="~/Platinum/Platinum.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="productListDescription">
        <asp:Label ID="lblDescription" runat="server" />
    </div>
    <table width="100%" align="center">
        <asp:Panel ID="pnlNavigation" runat="server">
            <tr>
                <td colspan="2">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="productNavigation">
                        <tr>
                            <td align="left">
                                Предложений на странице:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="0">Все</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblCounts" runat="server" />
                            </td>
                            <td align="center">
                                <asp:Panel ID="pnlPager" runat="server">
                                    Перейти:
                                    <asp:DataPager ID="pagerBottom" runat="server" PageSize="16" PagedControlID="lvProducts">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="currentpage"
                                                NextPreviousButtonCssClass="command" />
                                            <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </asp:Panel>
                            </td>
                            <td align="right">
                                Сортировать по: <asp:PlaceHolder ID="phlSorting" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td colspan="2">
                <asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdId" GroupItemCount="3"
                    OnPagePropertiesChanged="lvProducts_PagePropertiesChanged">
                    <LayoutTemplate>
                        <table id="tblProducts" runat="server" style="width: 100%;">
                            <tr id="groupPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr id="ProductRow" runat="server">
                            <td id="itemPlaceholder" runat="server" />
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td class="itemdescription" cellspacing="0" cellpadding="0" valign="top" align="center">
                            <div class="itemImage">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("ThumbURL", "http://www.echo-h.ru/Images/Products/Thumb/{0}") %>'
                                    PostBackUrl='<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>' AlternateText='<%#Eval("ProductNameRus")  %>' onmouseover='<% # formatFunction(Eval("ProdId"),true) %>' onmouseout='<%# formatFunction(Eval("ProdId"),false) %>'/>
                            <div id="img_block_<%#Eval("ProdId") %>" style="z-index:1000; height:100px; left:0px; top:0px; display:none; position:absolute;"></div>
                             </div>
                            </div>
                            <div id="container">
                                <b class="xb1"></b><b class="xb2"></b><b class="xb3"></b><b class="xb4"></b><b class="xb5">
                                </b><b class="xb6"></b><b class="xb7"></b>
                                <div class="xboxcontent">
                                    <div class="productName">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>'><%#Eval("ProductNameRus")%></asp:HyperLink></div>
                                    <div class="productPrice">
                                        Цена:<%#oldSalePrice(Eval("SalePrice", "{0:C}"),true)%><%#Eval("OrigPrice","{0:C}")%></div>
                                    <%#DescRestrict(Eval("ProductDescriptionRus"))%>
                                </div>
                                <b class="xb7"></b><b class="xb6"></b><b class="xb5"></b><b class="xb4"></b><b class="xb3">
                                </b><b class="xb2"></b><b class="xb1"></b>
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
    </table>
</asp:Content>
