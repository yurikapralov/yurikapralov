<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true" CodeFile="OrdersHistory.aspx.cs" Inherits="OrdersHistory" Theme="Theme2" %>
<%@ MasterType VirtualPath="~/Theme2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<asp:LoginView ID="LoginView1" runat="server">
    <AnonymousTemplate>
    <h2>Вы должны авторизоваться, чтобы увидеть историю Ваших заказов.</h2>
    </AnonymousTemplate>
    <LoggedInTemplate>
    <h2>История Ваших заказов</h2>
     <asp:Label ID="lblEmpty" runat="server" Visible="false">
    <h3>Вы не делали заказов на нашем сайте</h3>
    </asp:Label>
    <asp:DataList ID="dlOrders" runat="server" CssClass="history">
    <ItemTemplate>
    <h3>Заказ № <%#Eval("OrderNumber")%> - <%#Eval("DateCreated", "{0:g}")%></h3>
    <b>

    Сумма = <%#Eval("TotalSum","{0:C}") %><br />
    Статус заказа:<%#Eval("OrderStatus.OrderStaus") %><br />
    </b>
    <h4>Детали заказа:</h4>
     <asp:Repeater runat="server" ID="rptOrderItems" DataSource='<%#Eval("OrdersItems") %>'>
                <ItemTemplate>
                <a href='<%#GetNavigateURL((int)Eval("ProdSizeID")) %>'><%#Eval("DinamicTitle") %></a>
                                                -
                                                <%#Eval("Qty") %><br />
                </ItemTemplate>
                </asp:Repeater>
      <br />
      Сумма заказа=<%#Eval("OrderSum","{0:c}") %><br />
      Способ доставки   <%#GetDeliverMethod(Eval("DeliverTypeId"),Eval("DeliverSum")) %>
      <br /><br /><br /><br />
    </ItemTemplate>
    
    </asp:DataList>
    </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

