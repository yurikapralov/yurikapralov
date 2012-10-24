<%@ Page Title="" Language="C#" MasterPageFile="~/En/MasterPageEng.master" AutoEventWireup="true" CodeFile="_OrdersHistory_old.aspx.cs" Inherits="En_OrdersHistory_old" Theme="Default" %>
<%@ MasterType VirtualPath="~/En/MasterPageEng.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CurrencySelectContentHolder" Runat="Server">
select the display currency:
            <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true" 
                    DataValueField="Id" DataTextField="Currency" 
                    onselectedindexchanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceMainHolder" Runat="Server">
<asp:LoginView ID="LoginView1" runat="server">
    <AnonymousTemplate>
    <h2>You must log in to see the history of your orders.</h2>
    </AnonymousTemplate>
    <LoggedInTemplate>
    <h2>The history of your orders</h2>
     <asp:Label ID="lblEmpty" runat="server" Visible="false">
    <h3>You do not make an order on our site</h3>
    </asp:Label>
    <asp:DataList ID="dlOrders" runat="server">
    <ItemTemplate>
    <h3>Order # <%#Eval("OrderNumber")%> - <%#Eval("DateCreated", "{0:g}")%></h3>
    <b>

    Sum total = <%#ConvertPrice(Eval("TotalSum", "{0:F2}"))%><br />

    </b>
    <h4>Order Details:</h4>
     <asp:Repeater runat="server" ID="rptOrderItems" DataSource='<%#Eval("OrdersItems") %>'>
                <ItemTemplate>
                <asp:HyperLink ID="lnkProduct" runat="server" Text='<%#Eval("DinamicTitleEng") %>' NavigateUrl='<%#GetNavigateURL((int)Eval("ProdSizeID")) %>' />
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

