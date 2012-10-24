<%@ Page Title="" Language="C#" MasterPageFile="~/En/Theme2En.master" AutoEventWireup="true" CodeFile="OrdersHistory.aspx.cs" Inherits="En_OrdersHistory" Theme="Theme2"%>
<%@ MasterType VirtualPath="Theme2En.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="extraNavHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    <asp:LoginView ID="LoginView1" runat="server">
    <AnonymousTemplate>
    <h2>You must log in to see the history of your orders.</h2>
    </AnonymousTemplate>
    <LoggedInTemplate>
    <h2>The history of your orders</h2>
     <asp:Label ID="lblEmpty" runat="server" Visible="false">
    <h3>You do not make an order on our site</h3>
    </asp:Label>
    <asp:DataList ID="dlOrders" runat="server" CssClass="history">
    <ItemTemplate>
    <h3>Order # <%#Eval("OrderNumber")%> - <%#Eval("DateCreated", "{0:g}")%></h3>
    <b>

    Sum total = <%#ConvertPrice(Eval("TotalSum", "{0:F2}"))%><br />

    </b>
    <h4>Order Details:</h4>
     <asp:Repeater runat="server" ID="rptOrderItems" DataSource='<%#Eval("OrdersItems") %>'>
                <ItemTemplate>
                <a href='<%#GetNavigateURL((int)Eval("ProdSizeID")) %>'><%#Eval("DinamicTitle") %></a>
                                                -
                                                <%#Eval("Qty") %><br />
                </ItemTemplate>
                </asp:Repeater>
      <br />
      Sum total=<%#ConvertPrice(Eval("OrderSum", "{0:F2}"))%><br />
      Deliver Method  <%#GetDeliverMethod(Eval("DeliverTypeId"), ConvertPrice(Eval("DeliverSum","{0:F2}")))%>
      <br /><br /><br /><br />
    </ItemTemplate>
    
    </asp:DataList>
    </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="socialPlaceHolder" Runat="Server">
</asp:Content>

