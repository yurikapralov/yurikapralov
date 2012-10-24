<%@ Page Title="" Language="C#" MasterPageFile="~/En/Theme2En.master" AutoEventWireup="true" CodeFile="Shopping.aspx.cs" Inherits="En_Shopping" Theme="Theme2" %>
<%@ Register TagPrefix="uc1" TagName="Order" Src="~/Controls/OrderEngl.ascx" %>
<%@ MasterType VirtualPath="Theme2En.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="extraNavHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    <asp:Panel ID="pnlShoppingCart" runat="server">
        <p>
            You can view and edit your final purchase before placing your order.
        </p>
        <asp:ObjectDataSource ID="objShoppingCart" runat="server" SelectMethod="GetItems"
            TypeName="echo.BLL.Orders.CurrentUserShoppingCart"></asp:ObjectDataSource>
        <asp:GridView ID="gvwOrderItems" runat="server" AutoGenerateColumns="false" DataSourceID="objShoppingCart"
            DataKeyNames="ProdSizeId" OnRowCreated="gvwOrderItems_RowCreated" OnRowDeleting="gvwOrderItems_RowDeleting" CssClass="ShopTable">
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
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Bind("Qty") %>' Width="20" />
                        <asp:RequiredFieldValidator ID="valRequiredQuantity" runat="server" ControlToValidate="txtQuantity"
                            SetFocusOnError="true" ValidationGroup="ShippingAddress" Text="Required"
                            Display="Dynamic" />
                        <asp:CompareValidator ID="valQuantityType" runat="server" Operator="DataTypeCheck"
                            Type="Integer" ControlToValidate="txtQuantity" Display="Dynamic" Text="Must be digit" />
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
                <asp:Label ID="lblSubtotal" runat="server"  CssClass="subtotal"/>
            </p>
            <p><asp:Label runat="server" ID="lblUsa" Visible="False">Ths order contains articles shiped from The USA.</asp:Label></p>
            <p>
                <asp:Button ID="btnUpdate" runat="server" Text="Recount" CssClass="subform"
                    OnClick="btnUpdate_Click" />
                <asp:Button ID="btnOrder" runat="server" Text="Checkout" CssClass="subform" Width="150px"
                    OnClick="btnOrder_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Cancel Order" CssClass="subform" Width="150px"
                    OnClientClick="if(confirm('Refine your shopping cart?')==false)return false;" OnClick="btnReset_Click" />
            </p>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlAuntification" runat="server" Visible="false" ClientIDMode="Static">
        <p>
             You can log-in or register for easier ordering.</p>
        <asp:Login ID="ctrLogin" FailureText="Login or Password is wrong" runat="server">
            <LayoutTemplate>
                <table width="100%">
                    <tr>
                        <td  align="right">
                            <asp:Literal runat="server" ID="FailureText" /><span class="logTitle">Login:</span>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textform" Width="100px" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valRequireLogin" runat="server" SetFocusOnError="true"
                                Text="*" Display="Dynamic" ControlToValidate="UserName" ValidationGroup="Login1" />
                                 <span class="logTitle">Password:</span>
                            <asp:TextBox ID="Password" runat="server" CssClass="textform" TextMode="Password" ToolTip="Password" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" SetFocusOnError="true"
                                Text="*" Display="Dynamic" ControlToValidate="Password" ValidationGroup="Login1" />
                                
                        </td>

                       
                    </tr>
                    <tr>
                     <td >
                            <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Register.aspx?Shop=OK">Register</asp:HyperLink>
                            <br />
                            <asp:HyperLink ID="lnkPaswordRecovery" NavigateUrl="~/PasswordRecovery.aspx" runat="server">Recover Password</asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <p></p>
                 <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="subform" ValidationGroup="Login1"
                                CommandName="Login"/> 
            <asp:Button ID="btnEsc" runat="server" Text="Skip registration" OnClick="btnEsc_Click" CssClass="subform" Width="180px" />
            </LayoutTemplate>
        </asp:Login>
    </asp:Panel>
    <asp:Panel ID="pnlShipping" runat="server" Visible="false" ClientIDMode="Static" CssClass="tablePanel">
        <h2>
            Fill this form to make an order</h2>
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
                            Street:<asp:TextBox ID="txtStreet" runat="server" Width="180px" /><br />
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
                    <td width="40%" align="right" class="cont_table">
                        <asp:Button ID="btnMore" runat="server" Text="Continue" 
                            CssClass="subform" ValidationGroup="OrderInfo" onclick="btnMore_Click"/>
                    </td>
                    <td width="20%">
                    <asp:ValidationSummary runat="server" ID="valSum" ValidationGroup="OrderInfo" ShowMessageBox="true" ShowSummary="false"  />
                    </td>
                    <td width="40%" align="left">
                        <asp:Button ID="btnReturn" runat="server" Text="Return To Cart" 
                            CssClass="subform" onclick="btnReturn_Click"  Width="150px"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table> 
    </asp:Panel>
    <asp:Panel ID="pnlShippingConfirmation" runat="server" Visible="false" ClientIDMode="Static" CssClass="tablePanel">
    <table class="alltable" width="100%" cellpadding="0" cellspacing="0">
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
        <td class="questionstext">E-mail</td>
        <td class="answertext">
            <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Phone Number</td>
        <td class="answertext">
            <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label> 
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Time</td>
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
    <td class="cont_table"><asp:Button ID="btnAdmit" runat="server" Text="Confirm" 
            CssClass="subform" onclick="btnAdmit_Click" /></td>
    <td><asp:Button ID="btnChange" runat="server" Text="Change" 
            CssClass="subform" onclick="btnChange_Click"/></td>
    </tr>
</table>
    </asp:Panel>
    <asp:Panel ID="pnlFinal" runat="server"  Visible="false">
        <h2><asp:Label ID="LblOrderNumber" runat="server"/></h2>
        <p class="ProductItemDescription">In the near future you will contact our staff.</p>
        <p align="center">
        <asp:Button ID="btnDefault" runat="server" Text="Go to the main page" 
                CssClass="subform" onclick="btnDefault_Click" Width="200px" />
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlForEmail" runat="server" Visible="false">
        <uc1:Order ID="OrderCtrl" runat="server" ViewOrderStatus="false" />
    
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="socialPlaceHolder" Runat="Server">
</asp:Content>

