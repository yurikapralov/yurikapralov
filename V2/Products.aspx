<%@ Page Title="" Language="C#" MasterPageFile="~/V2/MasterPageV2.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="V2_Products" %>
<%@ MasterType VirtualPath="~/V2/MasterPageV2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="productListDescription">
        <asp:Label ID="lblDescription" runat="server"/>
        </div>
        <table width="100%" align="center">
<tr>
<td colspan="2">
<asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdId" 
        GroupItemCount="2" onpagepropertieschanged="lvProducts_PagePropertiesChanged">
<LayoutTemplate>
<table id="tblProducts" runat="server" style="width:100%;">
<tr id="groupPlaceholder" runat="server" />
</table>
</LayoutTemplate>
<GroupTemplate>
<tr id="ProductRow" runat="server">
<td id="itemPlaceholder" runat="server" />
</tr>
</GroupTemplate>
<ItemTemplate>
<td class="itemdescription" cellSpacing="0" cellPadding="0" valign="top" >                   
                    <asp:ImageButton ID="ImageButton1" runat="server"  
                    ImageUrl='<%# Eval("ThumbURL", "http://www.echo-h.ru/Images/Products/Thumb/{0}") %>'
                    CssClass="itemImage" PostBackUrl='<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>'  
                    AlternateText='<%#Eval("ProductNameRus") + " - " +Eval("ProductDescriptionRus") %>'/>
                   
                    <div class="productName"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>'><%#Eval("ProductNameRus")%></asp:HyperLink></div>
                    <div class="productPrice">Цена:<%#oldSalePrice(Eval("SalePrice", "{0:C}"),true)%><%#Eval("OrigPrice","{0:C}")%></div>
                    <%#DescRestrict(Eval("ProductDescriptionRus"))%>
                </td>
</ItemTemplate>
</asp:ListView>
</td>
</tr>
<asp:Panel ID="pnlNavigation" runat="server">
<tr>
<td width="50%">Предложений на странице: 
    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" 
        onselectedindexchanged="ddlPageSize_SelectedIndexChanged">
    <asp:ListItem Value="8">8</asp:ListItem>
    <asp:ListItem Value="16">16</asp:ListItem>
    <asp:ListItem Value="24">24</asp:ListItem>
    <asp:ListItem Value="32">32</asp:ListItem>
    <asp:ListItem Value="0">Все</asp:ListItem>
    </asp:DropDownList></td>
<td width="50%">Сортировать по:<br />
Названию: (<asp:LinkButton Text="возр." runat="server" ID="NameASC" 
        CssClass="mainlink" onclick="NameASC_Click"/> | 
    <asp:LinkButton Text="убыв." runat="server" ID="NameDESC" CssClass="mainlink" 
        onclick="NameDESC_Click"/>)<br />
Цене: (<asp:LinkButton Text="возр." runat="server" ID="PriceASC" 
        CssClass="mainlink" onclick="PriceASC_Click"/> | 
    <asp:LinkButton Text="убыв." runat="server" ID="PriceDESC" CssClass="mainlink" 
        onclick="PriceDESC_Click"/>)<br />
Новизне: (<asp:LinkButton Text="возр." runat="server" ID="DateASC" 
        CssClass="mainlink" onclick="DateASC_Click"/> | 
    <asp:LinkButton Text="убыв." runat="server" ID="DateDESC" CssClass="mainlink" 
        onclick="DateDESC_Click"/>)
</td>
</tr>
<tr class="navigationbar">
<td width="50%" align="left">
            <asp:Label ID="lblCounts" runat="server" />
        </td>
        <td width="50%" align="left">
        <asp:Panel ID="pnlPager" runat="server">
        Перейти:
        <asp:DataPager ID="pagerBottom" runat="server" PageSize="16" PagedControlID="lvProducts">
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
        </td>
</tr>
</asp:Panel>
</table>
</asp:Content>

