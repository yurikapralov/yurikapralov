<%@ Page Title="" Language="C#" MasterPageFile="~/En/Theme2En.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="En_Search" Theme="Theme2"%>
<%@ MasterType VirtualPath="Theme2En.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="extraNavHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    <table width="100%">
        <asp:Panel ID="pnlNavigationTop" runat="server">
            <tr>
                <td colspan="2" class="pager_count">
                    Goods on the page:
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="32">32</asp:ListItem>
                        <asp:ListItem Value="0">All</asp:ListItem>
                    </asp:DropDownList>
                    of
                    <asp:Label ID="lblCounts" runat="server" />
                </td>
                <td colspan="2" class="pager" align="right">
                    <asp:Panel ID="pnlTopPager" runat="server">
                        Page:
                        <asp:DataPager ID="pagerTop" runat="server" PagedControlID="lvProducts">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="curPage"
                                    NextPreviousButtonCssClass="command" />
                                <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                            </Fields>
                        </asp:DataPager>
                    </asp:Panel>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td colspan="4">
                <asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdId" GroupItemCount="4"
                    OnPagePropertiesChanged="lvProducts_PagePropertiesChanged">
                    <LayoutTemplate>
                        <table style="width: 100%;">
                            <tr id="groupPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <td id="itemPlaceholder" runat="server" />
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td class="preview_product">
                            <div class="pre_info_small_eng">
                                <a href="<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>" onmouseover="<%# formatFunction(Eval("ProdId"),true) %>"
                                    onmouseout="<%# formatFunction(Eval("ProdId"),false) %>"><span class="prodthumb_name">
                                        <%#Eval("ProductNameEng")%></span></a>
                            </div>
                            <div style="position: relative;">
                                <a href="<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>">
                                    <img src="<%# Eval("ThumbURL", "../Images/Products/Thumb/{0}") %>" class="thumb_image"
                                        alt="<%#Eval("ProductNameRus") %>" onmouseover="<%# formatFunction(Eval("ProdId"),true) %>"
                                        onmouseout="<%# formatFunction(Eval("ProdId"),false) %>" />
                                </a>
                                <div id="img_block_<%#Eval("ProdId") %>" style="z-index: 1000; height: 100px; left: 0px;
                                    top: 0px; display: none; position: absolute;">
                                </div>
                                <%#(bool)Eval("onSale")? "<div id=\"sale_"+Eval("ProdId")+"\" style=\"z-index: 2; height: 60px; left: 85px; width:60px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/sale.png\" /></div>":"" %>
                                
                            </div>
                            <div class="post_info_eng">
                                <%#oldSalePrice(Eval("SalePrice", "{0:F2}"),false)%><span class="prodthumb_price"><%#ConvertPrice(Eval("OrigPrice", "{0:F2}"))%></span>
                            </div>
                            <a href="<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>" class="flex_link">More Info</a>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
        <asp:Panel ID="pnlNavigationBottom" runat="server">
            <tr>
                <td colspan="2" class="pager" align="left">
                    <asp:Panel ID="pnlPager" runat="server">
                        Page:
                        <asp:DataPager ID="pagerBottom" runat="server" PagedControlID="lvProducts">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="curPage"
                                    NextPreviousButtonCssClass="command" />
                                <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                            </Fields>
                        </asp:DataPager>
                    </asp:Panel>
                </td>
                <td colspan="2" class="pager_sort">
                    Sort by:<br />
                    Name: (<asp:LinkButton Text="Asc." runat="server" ID="NameASC" OnClick="NameASC_Click" />
                    |
                    <asp:LinkButton Text="Desc." runat="server" ID="NameDESC" CssClass="mainlink" OnClick="NameDESC_Click" />)<br />
                    Price: (<asp:LinkButton Text="Asc." runat="server" ID="PriceASC" OnClick="PriceASC_Click" />
                    |
                    <asp:LinkButton Text="Desc." runat="server" ID="PriceDESC" CssClass="mainlink" OnClick="PriceDESC_Click" />)<br />
                    Newness: (<asp:LinkButton Text="Asc." runat="server" ID="DateASC" OnClick="DateASC_Click" />
                    |
                    <asp:LinkButton Text="Desc." runat="server" ID="DateDESC" OnClick="DateDESC_Click" />)
                </td>
            </tr>
        </asp:Panel>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="socialPlaceHolder" Runat="Server">
</asp:Content>

