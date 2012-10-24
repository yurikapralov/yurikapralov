<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewProducts.ascx.cs" Inherits="Controls_NewProducts" %>
<asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdId" 
        GroupItemCount="4">
<LayoutTemplate>
<table id="tblProducts" runat="server" style="width:536px;">
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
                    ImageUrl='<%# Eval("ThumbURL", "~/Images/Products/Thumb/{0}") %>'
                    CssClass="thumbImage" PostBackUrl='<%#"../ProductItem.aspx?ProdId="+Eval("ProdId") %>'  
                    AlternateText='<%#Eval("ProductNameRus") + " - " +Eval("ProductDescriptionRus") %>' /><br />
                    <h4><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"../ProductItem.aspx?ProdId="+Eval("ProdId") %>'><%#Eval("ProductNameRus")%></asp:HyperLink><br />
<%--                    <br />Цена:<%#oldSalePrice(Eval("SalePrice", "{0:C}"))%><%#Eval("OrigPrice","{0:C}")%></h4><br />--%>
                    <%#DescRestrict(Eval("ProductDescriptionRus"))%>
                </td>
</ItemTemplate>
</asp:ListView>
<p class="LookingMore"><asp:HyperLink ID="hplMore" runat="server" Visible="false">Смотреть все новинки</asp:HyperLink></p>