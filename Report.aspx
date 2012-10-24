<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="title1" runat="server">Отчет по продукции</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <div style="width: 100%; background-color: #cccccc;">
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <asp:TabPanel runat="server" HeaderText="По категориям" ID="TabPanel1">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlSelectedCategory" runat="server" DataTextField="CatNameRus"
                            DataValueField="CatId" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="По группам" ID="TabPanel2">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlSelectedGroups" runat="server" DataTextField="GroupNameRus"
                            DataValueField="GroupId" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <asp:RadioButtonList ID="rblReportMode" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0" Text="Общий" />
                <asp:ListItem Value="1" Text="Полный" />
            </asp:RadioButtonList>
            <asp:CheckBox ID="cbxShowPrice" runat="server" Checked="true" Text="Показать цену" />
            <asp:CheckBox ID="cbxActive" runat="server" Text="Только активные" />
            <asp:Button ID="btnReport" runat="server" Text="Показать" OnClick="btnReport_Click" />
        </div>
   
    <asp:Panel ID="pnlGenetal" Visible="false" runat="server">
     <h1>
        <asp:Label ID="lblTitleGeneral" runat="server" /></h1>
        <asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdId" GroupItemCount="4"
            OnItemDataBound="lvProducts_ItemDataBound">
            <LayoutTemplate>
                <table id="tblProducts" runat="server" style="width: 100%;">
                    <tr id="groupPlaceholder" runat="server" />
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr id="ProductRow" runat="server">
                    <td id="itemPlaceholder" runat="server" />
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td cellspacing="0" cellpadding="0" valign="top" width="200">
                    <asp:Image ID="ThumbImage" runat="server" ImageUrl='<%# Eval("ThumbURL", "~/Images/Products/Thumb/{0}") %>' />
                    <h4>
                        <%#Eval("ProductNameRus")%>
                        <br />
                      -  <asp:Panel ID="pnlGeneralPrice" runat="server">
                            Цена:<%#oldSalePrice(Eval("SalePrice", "{0:C}"),true)%><%#Eval("OrigPrice","{0:C}")%></asp:Panel>
                    </h4>
                </td>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
    <asp:Panel ID="pnlTotal" Visible="false" runat="server">
    <h1>
        <asp:Label ID="lblTitleTotal" runat="server" /></h1>
         <asp:Label ID="Label1" runat="server" /></h1>
        <asp:ListView ID="lvProductColors" runat="server" DataKeyNames="ProdColorId" GroupItemCount="4">
            <LayoutTemplate>
                <table id="tblProducts" runat="server" style="width: 100%;">
                    <tr id="groupPlaceholder" runat="server" />
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr id="ProductRow" runat="server">
                    <td id="itemPlaceholder" runat="server" />
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td  cellspacing="0" cellpadding="0" valign="top" width="200">
                    <asp:Image ID="LargeImage" runat="server" Width="150px" ImageUrl='<%# Eval("ImageURL", "~/Images/Products/Large/{0}") %>' />
                    <h4><%-- <%#(bool)Eval("IsAvilable") ? "" : "<font color='red'><b><s>"%>--%>
                        <%#Eval("Product.ProductNameRus")%>
                       <%-- <%#(bool)Eval("IsAvilable")? "":"</s></b></font>" %>--%>
                        <br />
                        <asp:Panel ID="pnlGeneralPrice" runat="server">
                            Цена:<%#oldSalePrice(Eval("Product.SalePrice", "{0:C}"),true)%><%#Eval("Product.OrigPrice","{0:C}")%></asp:Panel>
                        <%#Eval("ColorNameRus") %>
                    </h4>
                </td>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
    <asp:Label ID="lblCount" runat="server" /><br />
    </form>
</body>
</html>
