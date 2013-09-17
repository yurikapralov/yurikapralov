<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="Products" Theme="Theme2" %>

<%@ MasterType VirtualPath="~/Theme2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="Server">
    <asp:Literal ID="lblDescription" runat="server" />
     <script type="text/javascript" src="//yandex.st/share/share.js" charset="utf-8"></script>
     <div style="text-align:right;" class="yashare-auto-init" data-yashareL10n="ru" data-yashareType="icon" data-yashareQuickServices="yaru,vkontakte,facebook,twitter,odnoklassniki,moimir,lj,moikrug,gplus"></div> 
    <table width="100%">
        <asp:Panel ID="pnlNavigationTop" runat="server">
            <tr>
                <td colspan="2" class="pager_count">
                    Предложений на странице:
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="32">32</asp:ListItem>
                        <asp:ListItem Value="0">Все</asp:ListItem>
                    </asp:DropDownList>
                    из
                    <asp:Label ID="lblCounts" runat="server" />
                </td>
                <td colspan="2" class="pager" align="right">
                    <asp:Panel ID="pnlTopPager" runat="server">
                        Страница:
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
                            <div class="pre_info_small">
                                <a href="<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>" onmouseover="<%# formatFunction(Eval("ProdId"),true) %>"
                                    onmouseout="<%# formatFunction(Eval("ProdId"),false) %>"><span class="prodthumb_name">
                                        <%#Eval("ProductNameRus")%></span></a>
                            </div>
                            <div style="position: relative;">
                                <a href="<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>">
                                    <img src="<%# Eval("ThumbURL", "Images/Products/Thumb/{0}") %>" class="thumb_image"
                                        alt="<%#string.IsNullOrEmpty((string)Eval("Alt")) ? Eval("ProductNameRus"):Eval("Alt") %>" onmouseover="<%# formatFunction(Eval("ProdId"),true) %>"
                                        onmouseout="<%# formatFunction(Eval("ProdId"),false) %>" />
                                </a>
                                <div id="img_block_<%#Eval("ProdId") %>" style="z-index: 1000; height: 100px; left: 0px;
                                    top: 0px; display: none; position: absolute;">
                                </div>
                                <%#(bool)Eval("onSale")? "<div id=\"sale_"+Eval("ProdId")+"\" style=\"z-index: 2; height: 60px; left: 85px; width:60px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/sale.png\" /></div>":"" %>
                                <%#(bool)Eval("isVip")? "<div id=\"vip_"+Eval("ProdId")+"\" style=\"z-index: 3; height: 60px; left: 85px; width:60px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/vip.png\" /></div>":"" %>
                                 <%#(bool)Eval("onSale2")? "<div id=\"vip_"+Eval("ProdId")+"\" style=\"z-index: 3; height: 60px; left: 85px; width:60px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/sale2_sm.png\" /></div>":"" %>
                                
                            </div>
                            <div class="post_info">
                                <%#oldSalePrice(Eval("SalePrice", "{0:C}"), true)%><%#Eval("OrigPrice","<span class=\"prodthumb_price\">{0:C}</span>")%>
                                <%#ColorVariation(Eval("ProdId")) %>
                                <span class="prodthumb_description">
                                    <%#DescRestrict(Eval("ProductDescriptionRus"))%></span>
                                    
                            </div>
                            <a href="<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>" class="flex_link">Подробнее</a>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
        <asp:Panel ID="pnlNavigationBottom" runat="server">
            <tr>
                <td colspan="2" class="pager_small" align="left">
                Предложений на странице:
                    <asp:DropDownList ID="ddlPageSize2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize2_SelectedIndexChanged">
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="32">32</asp:ListItem>
                        <asp:ListItem Value="0">Все</asp:ListItem>
                    </asp:DropDownList>
                    из
                    <asp:Label ID="lblCount2" runat="server" />
                    <asp:Panel ID="pnlPager" runat="server">
                        К странице:
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
                    Сортировать по:<br />
                    Названию: (<asp:LinkButton Text="возр." runat="server" ID="NameASC" OnClick="NameASC_Click" />
                    |
                    <asp:LinkButton Text="убыв." runat="server" ID="NameDESC" CssClass="mainlink" OnClick="NameDESC_Click" />)<br />
                    Цене: (<asp:LinkButton Text="возр." runat="server" ID="PriceASC" OnClick="PriceASC_Click" />
                    |
                    <asp:LinkButton Text="убыв." runat="server" ID="PriceDESC" CssClass="mainlink" OnClick="PriceDESC_Click" />)<br />
                    Новизне: (<asp:LinkButton Text="возр." runat="server" ID="DateASC" OnClick="DateASC_Click" />
                    |
                    <asp:LinkButton Text="убыв." runat="server" ID="DateDESC" OnClick="DateDESC_Click" />)
                </td>
            </tr>
        </asp:Panel>
    </table>
    <asp:Literal ID="lblDescription2" runat="server" />
</asp:Content>
