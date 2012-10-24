<%@ Page Title="" Language="C#" MasterPageFile="~/V2/MasterPageV2.master" AutoEventWireup="true" CodeFile="ProductItem.aspx.cs" Inherits="V2_ProductItem" %>
<%@ MasterType VirtualPath="~/V2/MasterPageV2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="itemblock">
<asp:UpdatePanel ID="uppnlMain" runat="server">
            <ContentTemplate>
<asp:Panel runat="server" ID="pnlProductItem" Visible="true">
    <div id="ProductItemArea">
            <div id="MainProductItemArea">
    
            <h2 style="text-align:left"><asp:Label ID="lblProductName"  runat="server" Visible="false"/></h2>
            
            <asp:Image ID="imgLarge" runat="server" ImageUrl='<%#Eval("ImageURL","http://echo-h.ru/Images/Products/Large/{0}") %>' CssClass="MainImage" BorderWidth="1" /><br />
            <p class="productName"> <asp:Label ID="lblPlatformDescription" runat="server" /></p>
            <p class="productName"><asp:Label ID="lblProductDescription" runat="server" /></p>
            <span class="productName">Цена: <asp:Label ID="lblPrice" runat="server" /></span>
            
            <p></p>
            <p></p>
            <span class="productName">Цвет:
            <asp:DropDownList ID="ddlColors" runat="server" DataTextField="ColorNameRus" 
            DataValueField="ColorID" AutoPostBack="true" 
                    onselectedindexchanged="ddlColors_SelectedIndexChanged" />
            </span><br />

            <p></p>
            <span class="productName">Размер:
            <asp:DropDownList ID="ddlProdSizes" runat="server"  DataTextField="SizeNameRus" 
            DataValueField="ProdSizeID" AutoPostBack="True" 
                    onselectedindexchanged="ddlProdSizes_SelectedIndexChanged"/>
            </span><br /><br />
            <asp:Label ID="lblLargeSizeInfo" runat="server" Visible="false" Text=" При заказе обуви 42,43,44 размеров берется надбавка 600 руб." />
            
             <p><asp:Label ID="lblNotAvailable" runat="server" Visible="false"
             Text="Данная продукция отсутствует" /></p>
            <p>
              <asp:Button ID="btnAdd" runat="server" Text="Добавить в корзину" 
                    CssClass="loginSubStyle" onclick="btnAdd_Click" />
              <asp:Button ID="btnCancel" runat="server" Text="Отмена" CssClass="loginSubStyle" 
                    onclick="btnCancel_Click" />  
            </p>            
            </div>
            <div id="ExtraProductItemArea">
                <h3>Цветовой ряд:</h3><br />
                <asp:Repeater ID="rptImages" runat="server" 
                    onitemcommand="rptImages_ItemCommand">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%#Eval("ImageURL","http://echo-h.ru/Images/Products/Large/{0}") %>' 
                         CommandArgument='<%#Eval("ColorID") %>' CssClass="ExtraImage" BorderWidth="1" /> 
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            </div>
       
    <asp:Label ID="lblTest" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlProductItemAdd" runat="server" Visible="false">
    <h2 class="productName">Вы добавили:</h2>
    <asp:Label ID="lblProduct" runat="server" CssClass="productName"/><br />
    <asp:Label ID="lblColor" runat="server" CssClass="productName"/><br />
    <asp:Label ID="lblSize" runat="server" CssClass="productName"/><br />
    <p>
    <asp:Button ID="btnOrder" runat="server" Text="Оформить заказ" 
            CssClass="loginSubStyle" onclick="btnOrder_Click"/>
    &nbsp; &nbsp;
    <asp:Button ID="btnReturn"
        runat="server" Text="Вернуться" CssClass="loginSubStyle" 
            onclick="btnReturn_Click"/>
    </p>
</asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>
</div>
</asp:Content>

