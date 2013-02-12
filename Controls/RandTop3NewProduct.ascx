<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RandTop3NewProduct.ascx.cs" Inherits="Controls_RandTop3NewProduct" %>
<asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdId" GroupItemCount="3">
<LayoutTemplate>
<table width="100%">
<tr id="groupPlaceholder" runat="server" />
</table>
</LayoutTemplate>
<GroupTemplate>
<tr runat="server">
<td id="itemPlaceholder" runat="server"/>
</tr>
</GroupTemplate>
<ItemTemplate>
<td class="preview_block">
<div class="pre_info"> 
                        <%#GetCategories((int)Eval("ProdId")) %>
                        <a href='<%#"../ProductItem.aspx?ProdId="+Eval("ProdId") %>'><span class="prodthumb_name"><%#Eval("ProductNameRus")%></span></a>
                        </div>
                         <a href='<%#"../ProductItem.aspx?ProdId="+Eval("ProdId") %>'>
                         <img src='<%# Eval("ThumbURL", "Images/Products/Thumb/{0}") %>' class="thumb_image" alt='<%#string.IsNullOrEmpty((string)Eval("Alt")) ? Eval("ProductNameRus"):Eval("Alt") %>' /></a>                   
                    <div class="post_info">
                        <%#oldSalePrice(Eval("SalePrice", "{0:C}"))%>
                        <span class="prodthumb_price"><%#Eval("OrigPrice","{0:C}")%></span>
                        </div>
                        <a href='<%#"../ProductItem.aspx?ProdId="+Eval("ProdId") %>' class="flex_link">Подробнее</a>
                </td>
</ItemTemplate>
</asp:ListView>
