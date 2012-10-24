<%@ Page Title="" Language="C#" MasterPageFile="~/En/Theme2En.master" AutoEventWireup="true" CodeFile="ProductItem.aspx.cs" Inherits="En_ProductItem"  Theme="Theme2"%>
<%@ MasterType VirtualPath="Theme2En.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="extraNavHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
     <asp:UpdatePanel ID="uppnlMain" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlProductItem" Visible="true">
                <div id="ProductItemArea">
                    <div id="MainProductItemArea">
                        <h2 style="text-align: left">
                            <asp:Label ID="lblProductName" runat="server" /></h2>
                            <div style="position: relative;">
                        <asp:Image ID="imgLarge" runat="server" ImageUrl='<%#Eval("ImageURL","~/Images/Products/Large/{0}") %>'
                            CssClass="MainImage" />              
                            <asp:Literal runat="server"  ID="ltlsale" />
                            </div>
                            <br />
                             <script type="text/javascript" src="//yandex.st/share/share.js" charset="utf-8"></script>
                             <div style="text-align:right;" class="yashare-auto-init" data-yashareL10n="ru" data-yashareType="icon" data-yashareQuickServices="yaru,vkontakte,facebook,twitter,odnoklassniki,moimir,lj,moikrug,gplus"></div> 
                        <div class="productItemInfo">
                            <p class="ProductItemDescription">
                                <asp:Label ID="lblPlatformDescription" runat="server" /></p>
                            <p class="ProductItemDescription">
                                <asp:Label ID="lblProductDescription" runat="server" /></p>
                            <asp:Label ID="lblPrice" runat="server" />
                            <p>
                            </p>
                            <p>
                            </p>
                            <span class="ProductItemOpt">Color:
                                <asp:DropDownList ID="ddlColors" runat="server" DataTextField="ColorNameEng" DataValueField="ColorID"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlColors_SelectedIndexChanged" />
                            </span>
                            <br />
                            <p>
                            </p>
                            <span class="ProductItemOpt">Size:
                                <asp:DropDownList ID="ddlProdSizes" runat="server" DataTextField="SizeNameEng" DataValueField="ProdSizeID" />
                            </span>
                            <br />
                            <br />
                            <p>
                                <asp:Label ID="lblNotAvailable" runat="server" Visible="false" CssClass="ErrorNote"
                                    Text="Not Available" /></p>
                            <p>
                                <asp:Button ID="btnAdd" runat="server" Text="Add To Cart" CssClass="subform"
                                    OnClick="btnAdd_Click"  Width="150px"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="subform" OnClick="btnCancel_Click" />
                            </p>
                        </div>
                    </div>
                    <div id="ExtraProductItemArea">
                        <h3>
                            Color range:</h3>
                        <br />
                        <asp:Repeater ID="rptImages" runat="server" OnItemCommand="rptImages_ItemCommand">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%#Eval("ImageURL","~/Images/Products/Large/{0}") %>'
                                    CommandArgument='<%#Eval("ColorID") %>' CssClass="ExtraImage" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <asp:Label ID="lblTest" runat="server" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlProductItemAdd" runat="server" Visible="false">
        <h2>
            You added:</h2>
        <p class="ProductItemDescription">
            <asp:Label ID="lblProduct" runat="server" /><br />
            <asp:Label ID="lblColor" runat="server"/><br />
            <asp:Label ID="lblSize" runat="server"/><br />
        </p>
        <p>
            <asp:Button ID="btnOrder" runat="server" Text="Go to your cart" CssClass="subform"
                OnClick="btnOrder_Click" Width="150px"/>
            &nbsp; &nbsp;
            <asp:Button ID="btnReturn" runat="server" Text="Continue shopping" CssClass="subform"
                OnClick="btnReturn_Click" Width="150px" />
        </p>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="socialPlaceHolder" Runat="Server">
</asp:Content>

