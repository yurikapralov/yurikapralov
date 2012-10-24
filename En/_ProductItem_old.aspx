<%@ Page Title="" Language="C#" MasterPageFile="~/En/MasterPageEng.master" AutoEventWireup="true" CodeFile="_ProductItem_old.aspx.cs" Inherits="En_ProductItem_old" %>
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
 <asp:UpdatePanel ID="uppnlMain" runat="server">
            <ContentTemplate>
<asp:Panel runat="server" ID="pnlProductItem" Visible="true">
    <div id="ProductItemArea">
            <div id="MainProductItemArea">
    
            <h2 style="text-align:left"><asp:Label ID="lblProductName"  runat="server"/></h2>
            
            <asp:Image ID="imgLarge" runat="server" ImageUrl='<%#Eval("ImageURL","~/Images/Products/Large/{0}") %>' CssClass="MainImage" BorderWidth="1" /><br />
            <p class="ProductItemDescription"> <asp:Label ID="lblPlatformDescription" runat="server" /></p>
            <p class="ProductItemDescription"><asp:Label ID="lblProductDescription" runat="server" /></p>
            <span class="ProductItemPrice">Price: <asp:Label ID="lblPrice" runat="server" /></span>
            
            <p></p>
            <p></p>
            <span class="ProductItemPrice">Color:
            <asp:DropDownList ID="ddlColors" runat="server" DataTextField="ColorNameEng" 
            DataValueField="ColorID" AutoPostBack="true" 
                    onselectedindexchanged="ddlColors_SelectedIndexChanged" />
            </span><br />

            <p></p>
            <span class="ProductItemPrice">Size:
            <asp:DropDownList ID="ddlProdSizes" runat="server"  DataTextField="SizeNameEng" 
            DataValueField="ProdSizeID" AutoPostBack="True" 
                    onselectedindexchanged="ddlProdSizes_SelectedIndexChanged"/>
            </span><br /><br />
            <asp:Label ID="lblLargeSizeInfo" runat="server" />
            
            <p><asp:Label ID="lblNotAvailable" runat="server" Visible="false" CssClass="ErrorNote"
             Text="This goods are not available" /></p>
            <p>
              <asp:Button ID="btnAdd" runat="server" Text="Add To Cart" 
                    CssClass="orderbutton" onclick="btnAdd_Click" />
              <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="orderbutton" 
                    onclick="btnCancel_Click" />  
            </p>            
            </div>
            <div id="ExtraProductItemArea">
                <h3>Color range:</h3><br />
                <asp:Repeater ID="rptImages" runat="server" 
                    onitemcommand="rptImages_ItemCommand">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%#Eval("ImageURL","~/Images/Products/Large/{0}") %>' 
                         CommandArgument='<%#Eval("ColorID") %>' CssClass="ExtraImage" BorderWidth="1" /> 
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            </div>
       
    <asp:Label ID="lblTest" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlProductItemAdd" runat="server" Visible="false">
    <h2>You added:</h2>
    <asp:Label ID="lblProduct" runat="server" CssClass="ProductItemPrice"/><br />
    <asp:Label ID="lblColor" runat="server" CssClass="ProductItemPrice"/><br />
    <asp:Label ID="lblSize" runat="server" CssClass="ProductItemPrice"/><br />
    <p>
    <asp:Button ID="btnOrder" runat="server" Text="Go to your cart" 
            CssClass="orderbutton" onclick="btnOrder_Click"/>
    &nbsp; &nbsp;
    <asp:Button ID="btnReturn"
        runat="server" Text="Continue shopping" CssClass="orderbutton" 
            onclick="btnReturn_Click"/>
    </p>
</asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

