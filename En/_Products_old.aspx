<%@ Page Title="" Language="C#" MasterPageFile="~/En/MasterPageEng.master" AutoEventWireup="true" CodeFile="_Products_old.aspx.cs" Inherits="En_Products_old" %>
<%@ MasterType VirtualPath="~/En/MasterPageEng.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="CurrencyContent" ContentPlaceHolderID="CurrencySelectContentHolder" runat="server">
 select the display currency:
            <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true" 
                    DataValueField="Id" DataTextField="Currency" 
                    onselectedindexchanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceMainHolder" Runat="Server">
<h2><asp:Label ID="lblDescription" runat="server" /></h2>
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
                    <div style="position:relative;">                  
                    <asp:ImageButton ID="ImageButton1" runat="server"  
                    ImageUrl='<%# Eval("ThumbURL", "~/Images/Products/Thumb/{0}") %>'
                    CssClass="itemImage" PostBackUrl='<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>'  
                    AlternateText='<%#Eval("ProductNameEng") + " - " +Eval("ProductDescriptionEng") %>' 
                    onmouseover='<% # formatFunction(Eval("ProdId"),true) %>' onmouseout='<%# formatFunction(Eval("ProdId"),false) %>'/>
                    <div id="img_block_<%#Eval("ProdId") %>" style="z-index:1000; height:100px; left:0px; top:0px; display:none; position:absolute;"></div>
                    </div>
                    <h4><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"ProductItem.aspx?ProdId="+Eval("ProdId") %>'
                    onmouseover='<% # formatFunction(Eval("ProdId"),true) %>' onmouseout='<%# formatFunction(Eval("ProdId"),false) %>'><%#Eval("ProductNameEng")%></asp:HyperLink>
                    <br />Price:<%#oldSalePrice(Eval("SalePrice", "{0:F2}"),false)%><%#ConvertPrice(Eval("OrigPrice","{0:F2}"))%></h4><br />
                    <%#DescRestrict(Eval("ProductDescriptionEng"))%>
                </td>
</ItemTemplate>
</asp:ListView>
</td>
</tr>
<asp:Panel ID="pnlNavigation" runat="server">
<tr>
<td width="50%">Goods on the page: 
    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" 
        onselectedindexchanged="ddlPageSize_SelectedIndexChanged">
    <asp:ListItem Value="8">8</asp:ListItem>
    <asp:ListItem Value="16">16</asp:ListItem>
    <asp:ListItem Value="24">24</asp:ListItem>
    <asp:ListItem Value="32">32</asp:ListItem>
    <asp:ListItem Value="0">Все</asp:ListItem>
    </asp:DropDownList></td>
<td width="50%">Sorting By:<br />
Name: (<asp:LinkButton Text="Asc." runat="server" ID="NameASC" 
        CssClass="mainlink" onclick="NameASC_Click"/> | 
    <asp:LinkButton Text="Desc." runat="server" ID="NameDESC" CssClass="mainlink" 
        onclick="NameDESC_Click"/>)<br />
Price: (<asp:LinkButton Text="Asc." runat="server" ID="PriceASC" 
        CssClass="mainlink" onclick="PriceASC_Click"/> | 
    <asp:LinkButton Text="Desc." runat="server" ID="PriceDESC" CssClass="mainlink" 
        onclick="PriceDESC_Click"/>)<br />
Newness: (<asp:LinkButton Text="Asc." runat="server" ID="DateASC" 
        CssClass="mainlink" onclick="DateASC_Click"/> | 
    <asp:LinkButton Text="Desc." runat="server" ID="DateDESC" CssClass="mainlink" 
        onclick="DateDESC_Click"/>)
</td>
</tr>
<tr class="navigationbar">
<td width="50%" align="left">
            <asp:Label ID="lblCounts" runat="server" />
        </td>
        <td width="50%" align="left">
        <asp:Panel ID="pnlPager" runat="server">
        	Go to page:
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

