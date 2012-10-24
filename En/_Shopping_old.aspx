<%@ Page Title="" Language="C#" MasterPageFile="~/En/MasterPageEng.master" AutoEventWireup="true" CodeFile="_Shopping_old.aspx.cs" Inherits="En_Shopping_old" Theme="Default" %>
<%@ MasterType VirtualPath="~/En/MasterPageEng.master" %>
<%@ Register src="~/Controls/OrderEngl.ascx" tagname="Order" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CurrencySelectContentHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceMainHolder" Runat="Server">
<asp:Panel ID="pnlShoppingCart" runat="server">
        <p>
            You can view and edit your final purchase before placing your order.
        </p>
        <asp:ObjectDataSource ID="objShoppingCart" runat="server" SelectMethod="GetItems"
            TypeName="echo.BLL.Orders.CurrentUserShoppingCart"></asp:ObjectDataSource>
        <asp:GridView ID="gvwOrderItems" runat="server" AutoGenerateColumns="false" DataSourceID="objShoppingCart"
            DataKeyNames="ProdSizeId" OnRowCreated="gvwOrderItems_RowCreated" OnRowDeleting="gvwOrderItems_RowDeleting">
            <Columns>
                <asp:HyperLinkField DataTextField="Title" DataNavigateUrlFormatString="~/ProductItem.aspx?prodID={0}"
                    DataNavigateUrlFields="ProdID" HeaderText="Art." />
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <%#ConvertPrice(Eval("PriceForSale","{0:F2}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Bind("Qty") %>' />
                        <asp:RequiredFieldValidator ID="valRequiredQuantity" runat="server" ControlToValidate="txtQuantity"
                            SetFocusOnError="true" ValidationGroup="ShippingAddress" Text="Заполните поле количество"
                            Display="Dynamic" />
                        <asp:CompareValidator ID="valQuantityType" runat="server" Operator="DataTypeCheck"
                            Type="Integer" ControlToValidate="txtQuantity" Display="Dynamic" Text="Должно быть число" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
            </Columns>
            <EmptyDataTemplate>
                <p>
                    Your cart is empty</p>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Panel ID="pnlTotals" runat="server">
            <p>
                Total:<asp:Label ID="lblSubtotal" runat="server" />
            </p>
            <p>
                <asp:Button ID="btnUpdate" runat="server" Text="Recount" CssClass="orderbutton"
                    OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnOrder" runat="server" Text="Checkout" CssClass="orderbutton"
                    OnClick="btnOrder_Click" />&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="Cancel Order" CssClass="orderbutton"
                    OnClientClick="if(confirm('Refine your shopping cart?')==false)return false;" OnClick="btnReset_Click" />
            </p>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlAuntification" runat="server" Visible="false">
        <p>
            You can log-in or register for easier ordering</p>
        <asp:Login ID="ctrLogin" runat="server">
            <LayoutTemplate>
                <table width="100%">
                    <tr>
                        <td  align="right">
                            <asp:Literal runat="server" ID="FailureText" />
                            Name:
                            <asp:TextBox ID="UserName" runat="server" CssClass="logpas" Width="100px" />
                            <asp:RequiredFieldValidator ID="valRequireLogin" runat="server" SetFocusOnError="true"
                                Text="*" Display="Dynamic" ControlToValidate="UserName" ValidationGroup="Login1" />

                            Password:
                            <asp:TextBox ID="Password" runat="server" CssClass="logpas" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" SetFocusOnError="true"
                                Text="*" Display="Dynamic" ControlToValidate="Password" ValidationGroup="Login1" />
                                 <asp:Button ID="btnLogin" runat="server" Text="Войти" CssClass="orderbutton" ValidationGroup="Login1"
                                CommandName="Login" />
                        </td>

                       
                    </tr>
                    <tr>
                     <td >
                            <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Register.aspx">Register</asp:HyperLink>
                            <br />
                            <asp:HyperLink ID="lnkPaswordRecovery" NavigateUrl="~/PasswordRecovery.aspx" runat="server">Recover Password</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
        <p></p>
        <asp:Button ID="btnEsc" runat="server" Text="Skip registration" OnClick="btnEsc_Click" CssClass="orderbutton" />
    </asp:Panel>
    <asp:Panel ID="pnlShipping" runat="server" Visible="false">
        <p>
            Fill this form to make an order</p>
            <p>Attention: Connect with our managers to fix a delivery terms</p>
        <table class="alltable" width="550" cellpadding="0" cellspacing="0">
            <tr class="tablebody">
                <td class="questionstext">
                    First and Last Names* 
                </td>
                <td class="answertext">
                    <asp:TextBox ID="txtFIO" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqireFIO" ControlToValidate="txtFIO" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="First and Last Names required" />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Deliver method*
                </td>
                <td class="answertext">
                    <asp:DropDownList ID="ddlDeliverType" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlDeliverType_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select a delivery method--</asp:ListItem>
                    <asp:ListItem Value="1">EMS GarantPost (Only in Russia)</asp:ListItem>
                    <asp:ListItem Value="2">By Post (Only in Russia)</asp:ListItem>
                    <asp:ListItem Value="3">Courier to Moscow</asp:ListItem>
                    <asp:ListItem Value="4">Outside the Russian Federation</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valReqDeliverMethod" ControlToValidate="ddlDeliverType" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo"  InitialValue="0" runat="server" ErrorMessage="Not selected delivery method"  />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Counrty*
                </td>
                <td class="answertext">
                    <asp:DropDownList ID="ddlCountry" runat="server" DataTextField="CountryNameEN" DataValueField="CountryID">
                    </asp:DropDownList>
                </td>
            </tr>

                    <asp:Panel ID="pnlRusAdress" runat="server" Visible="true" Width="100%">
                        <tr class="tablebody">
                            <td class="questionstext"> 
                                Select a city or region:
                            </td>
                            <td class="answertext">
                                <asp:DropDownList ID="ddlCities" runat="server" DataTextField="City_RUS" DataValueField="CityID"
                                    AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr class="tablebody">
                            <td colspan="2" class="answertext">
                                <asp:RadioButtonList ID="rblRegionType" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" onselectedindexchanged="rblRegionType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="1" Text="Regional Center" />
                                    <asp:ListItem Value="2" Text="Territory area" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlRegionCity" runat="server" Visible="false">
                            <tr class="tablebody">
                                <td class="questionstext">
                                    City:
                                </td>
                                <td class="answertext">
                                    <asp:TextBox ID="txtCity2" runat="server" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr class="tablebody">
                        <td class="questionstext">
                            Address*
                        </td>
                        <td class="answertext">
                            Postcode:
                            <asp:TextBox ID="txtIndex" runat="server" Width="50px" />
                            Street:<asp:TextBox ID="txtStreet" runat="server" Width="200px" /><br />
                            <asp:RequiredFieldValidator ID="valReqStreet" ControlToValidate="txtStreet" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="The Street Is Required" />
                            House:<asp:TextBox ID="txtHouse" runat="server" Width="30px" />
                            <asp:RequiredFieldValidator ID="valReqHouse" ControlToValidate="txtHouse" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="The house number is required" />
                            Building:<asp:TextBox
                                ID="txtKorpus" runat="server" Width="30px" />Apartment/Office:<asp:TextBox ID="txtUnit"
                                    runat="server" Width="30px" />
                        </td>
            </tr>
    </asp:Panel>
    <asp:Panel ID="pnlOutsideAdress" runat="server" Visible="false" Width="100%">
            <tr class="tablebody">
                <td class="questionstext">
                    Address*
                </td>
                <td class="answertext">
                    <asp:TextBox ID="txtAdress" runat="server" TextMode="MultiLine" Width="300px" />
                    <asp:RequiredFieldValidator ID="valReqAddress" ControlToValidate="txtAdress" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="The Address Is Required" />
                </td>
            </tr>
    </asp:Panel>
    <tr class="tablebody">
        <td class="questionstext">
            E-mail*
        </td>
        <td class="answertext">
            <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valReqEmail" ControlToValidate="txtEmail" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="E-mail is required" />
             <asp:RegularExpressionValidator ID="valRegEmail" runat="server" 
                ControlToValidate="txtEmail" Display="Dynamic"   Text="*" ErrorMessage="E-mail is not well formed"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                ValidationGroup="OrderInfo"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Phone number*
        </td>
        <td class="answertext">
            <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valReqPhone" ControlToValidate="txtPhone" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Phone number is Required" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Time
        </td>
        <td class="answertext">
            from
            <asp:TextBox ID="txtTime1" runat="server" Width="30px" />till<asp:TextBox ID="txtTime2"
                runat="server" Width="30px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Note
        </td>
        <td class="answertext">
            <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
        </td>
    </tr>
    <tr class="tablebody">
        <td colspan="2">
            <table>
                <tr>
                    <td width="40%" align="right">
                        <asp:Button ID="btnMore" runat="server" Text="Continue" 
                            CssClass="orderbutton" ValidationGroup="OrderInfo" onclick="btnMore_Click" />
                    </td>
                    <td width="20%">
                    <asp:ValidationSummary runat="server" ID="valSum" ValidationGroup="OrderInfo" ShowMessageBox="true" ShowSummary="false"  />
                    </td>
                    <td width="40%" align="left">
                        <asp:Button ID="btnReturn" runat="server" Text="Return To Cart" 
                            CssClass="orderbutton" onclick="btnReturn_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table> 
    </asp:Panel>
    <asp:Panel ID="pnlShippingConfirmation" runat="server" Visible="false">
    <table class="alltable" width="550" cellpadding="0" cellspacing="0">
    <tr class="tablebody">
        <td class="questionstext">Cost of order:</td>
        <td class="answertext">
            <asp:Label ID="lblShippingSum" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Cost of delivery:</td>
        <td class="answertext">
            <asp:Label ID="lblDeliverSum" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Sum total:</td>
        <td class="answertext">
            <asp:Label ID="lblTotalSum" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">First and Last Names</td>
        <td class="answertext">
            <asp:Label ID="lblFIO" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Deliver Method</td>
        <td class="answertext">
            <asp:Label ID="lblDeliver" runat="server"></asp:Label>
        </td>
    </tr>
    
    <tr class="tablebody">
        <td class="questionstext">Address</td>
        <td class="answertext">
            <asp:Label ID="lblAdress" runat="server" Text="Label"></asp:Label>
        </td>
    </tr> 
    <tr class="tablebody">
        <td class="questionstext">E-mail </td>
        <td class="answertext">
            <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">PhoneNumber</td>
        <td class="answertext">
            <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label> 
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Time </td>
        <td class="answertext">
            from <asp:Label ID="lblTime1" runat="server" />till <asp:Label ID="lblTime2" runat="server" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Note </td>
        <td class="answertext">
            <asp:Label ID="lblNote" runat="server" ></asp:Label> 
        </td>
    </tr>
    <tr class="tablebody">
    <td><asp:Button ID="btnAdmit" runat="server" Text="Confirm" 
            CssClass="orderbutton" onclick="btnAdmit_Click"/></td>
    <td><asp:Button ID="btnChange" runat="server" Text="Change" 
            CssClass="orderbutton" onclick="btnChange_Click"/></td>
    </tr>
</table>
    </asp:Panel>
    <asp:Panel ID="pnlFinal" runat="server"  Visible="false">
        <asp:Label ID="LblOrderNumber" runat="server" CssClass="OrderNumber"/>
        <p class="OrderNumber">In the near future you will contact our staff.</p>
        <p align="center">
        <asp:Button ID="btnDefault" runat="server" Text="Go to the main page" 
                CssClass="orderbutton" onclick="btnDefault_Click" />
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlForEmail" runat="server" Visible="false">
        <uc1:Order ID="OrderCtrl" runat="server" ViewOrderStatus="false" />
    
    </asp:Panel>
</asp:Content>

