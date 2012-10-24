<%@ Page Title="" Language="C#" MasterPageFile="~/Platinum/Platinum.master" AutoEventWireup="true" CodeFile="Shopping.aspx.cs" Inherits="Platinum_Shopping" %>
<%@ MasterType VirtualPath="~/Platinum/Platinum.master" %>
<%@ Register src="~/Controls/Order.ascx" tagname="Order" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:Panel ID="pnlShoppingCart" runat="server">
 <p></p>
        <p class="productName">
            Вы можете просмотреть и отредактировать Ваши покупки перед окончательным оформлением
            заказа.
        </p>
        <p class="productName">Внимание: Ознакомтесь с <a href="javascript:window.open('deliverpopup.html','Deliver','top=0,left=0,width=800,height=550');void(0);" class="mainlink">условиями</a> доставки товара!</p>  
        <asp:ObjectDataSource ID="objShoppingCart" runat="server" SelectMethod="GetItems"
            TypeName="echo.BLL.Orders.CurrentUserShoppingCart"></asp:ObjectDataSource>
        <asp:GridView ID="gvwOrderItems" runat="server" AutoGenerateColumns="false" DataSourceID="objShoppingCart"
            DataKeyNames="ProdSizeId" OnRowCreated="gvwOrderItems_RowCreated" OnRowDeleting="gvwOrderItems_RowDeleting" HeaderStyle-CssClass="CartTableHead" RowStyle-CssClass="CartTableBody">
            <Columns>
                <asp:HyperLinkField DataTextField="Title" DataNavigateUrlFormatString="~/ProductItem.aspx?prodID={0}"
                    DataNavigateUrlFields="ProdID" HeaderText="Артикул" />
                <asp:TemplateField HeaderText="Цена">
                    <ItemTemplate>
                        <%#string.Format("{0:C}",Eval("PriceForSale"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Количество">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Bind("Qty") %>' />
                        <asp:RequiredFieldValidator ID="valRequiredQuantity" runat="server" ControlToValidate="txtQuantity"
                            SetFocusOnError="true" ValidationGroup="ShippingAddress" Text="Заполните поле количество"
                            Display="Dynamic" />
                        <asp:CompareValidator ID="valQuantityType" runat="server" Operator="DataTypeCheck"
                            Type="Integer" ControlToValidate="txtQuantity" Display="Dynamic" Text="Должно быть число" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="Удалить" />
            </Columns>
            <EmptyDataTemplate>
                <p>
                    Ваша корзина пуста</p>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Panel ID="pnlTotals" runat="server">
            <p class="productName">
                Итого:<asp:Label ID="lblSubtotal" runat="server" />
            </p>
            <p>
                <asp:Button ID="btnUpdate" runat="server" Text="Обновить" CssClass="loginSubStyle"
                    OnClick="btnUpdate_Click" />
                <asp:Button ID="btnOrder" runat="server" Text="Оформить заказ" CssClass="loginSubStyle"
                    OnClick="btnOrder_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Отменить заказ" CssClass="loginSubStyle"
                    OnClientClick="if(confirm('Очистить вашу корзину?')==false)return false;" OnClick="btnReset_Click" />
            </p>
        </asp:Panel>
    </asp:Panel>
    
    <asp:Panel ID="pnlShipping" runat="server" Visible="false" CssClass="shippingpannel" >
    <p>&nbsp;</p>
        <p class="productName">
            Пожалуйста, заполните следующую контактную информацию</p>
        <table class="alltable" width="550" cellpadding="0" cellspacing="0">
            <tr class="tablebody">
                <td class="questionstext">
                    Ф.И.О.*
                </td>
                <td class="answertext">
                    <asp:TextBox ID="txtFIO" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqireFIO" ControlToValidate="txtFIO" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует ФИО" />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Способ доставки*
                </td>
                <td class="answertext">
                    <asp:DropDownList ID="ddlDeliverType" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlDeliverType_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Выберите способ доставки--</asp:ListItem>
                    <asp:ListItem Value="1">ЕМС Гарантпост</asp:ListItem>
                    <asp:ListItem Value="2">Наложенный платеж</asp:ListItem>
                    <asp:ListItem Value="3">Курьером по Москве</asp:ListItem>
                    <asp:ListItem Value="4">За пределы РФ</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valReqDeliverMethod" ControlToValidate="ddlDeliverType" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo"  InitialValue="0" runat="server" ErrorMessage="Не выбран способ доставки"  />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Страна*
                </td>
                <td class="answertext">
                    <asp:DropDownList ID="ddlCountry" runat="server" DataTextField="CountryNameRU" DataValueField="CountryID">
                    </asp:DropDownList>
                </td>
            </tr>

                    <asp:Panel ID="pnlRusAdress" runat="server" Visible="true" Width="100%">
                        <tr class="tablebody">
                            <td class="questionstext"> 
                                Выберите город или регион:
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
                                    <asp:ListItem Selected="True" Value="1" Text="Региональный центр" />
                                    <asp:ListItem Value="2" Text="Территория области" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlRegionCity" runat="server" Visible="false">
                            <tr class="tablebody">
                                <td class="questionstext">
                                    Город:
                                </td>
                                <td class="answertext">
                                    <asp:TextBox ID="txtCity2" runat="server" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr class="tablebody">
                        <td class="questionstext">
                            Адрес*
                        </td>
                        <td class="answertext">
                            Индекс:
                            <asp:TextBox ID="txtIndex" runat="server" Width="50px" />
                            Улица:<asp:TextBox ID="txtStreet" runat="server" Width="200px" /><br />
                            <asp:RequiredFieldValidator ID="valReqStreet" ControlToValidate="txtStreet" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует улица" />
                            Дом:<asp:TextBox ID="txtHouse" runat="server" Width="30px" />
                            <asp:RequiredFieldValidator ID="valReqHouse" ControlToValidate="txtHouse" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует номер дома" />
                            Корпус:<asp:TextBox
                                ID="txtKorpus" runat="server" Width="30px" />Квартира/Офис:<asp:TextBox ID="txtUnit"
                                    runat="server" Width="30px" />
                        </td>
            </tr>
    </asp:Panel>
    <asp:Panel ID="pnlOutsideAdress" runat="server" Visible="false" Width="100%">
            <tr class="tablebody">
                <td class="questionstext">
                    Адрес*
                </td>
                <td class="answertext">
                    <asp:TextBox ID="txtAdress" runat="server" TextMode="MultiLine" Width="300px" />
                    <asp:RequiredFieldValidator ID="valReqAddress" ControlToValidate="txtAdress" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует адрес" />
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
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует e-mail" />
             <asp:RegularExpressionValidator ID="valRegEmail" runat="server" 
                ControlToValidate="txtEmail" Display="Dynamic"   Text="*" ErrorMessage="Не правильный формат E-mail"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                ValidationGroup="OrderInfo"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Телефон*
        </td>
        <td class="answertext">
            <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valReqPhone" ControlToValidate="txtPhone" Display="Dynamic"
                     Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует номер телефона" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Время
        </td>
        <td class="answertext">
            с
            <asp:TextBox ID="txtTime1" runat="server" Width="30px" />по<asp:TextBox ID="txtTime2"
                runat="server" Width="30px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Комментарий
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
                        <asp:Button ID="btnMore" runat="server" Text="Продолжить" 
                            CssClass="loginSubStyle" ValidationGroup="OrderInfo" onclick="btnMore_Click" />
                    </td>
                    <td width="20%">
                    <asp:ValidationSummary runat="server" ID="valSum" ValidationGroup="OrderInfo" ShowMessageBox="true" ShowSummary="false"  />
                    </td>
                    <td width="40%" align="left">
                        <asp:Button ID="btnReturn" runat="server" Text="Вернуться в корзину" 
                            CssClass="loginSubStyle" onclick="btnReturn_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table> 
    </asp:Panel>
    <asp:Panel ID="pnlShippingConfirmation" runat="server" Visible="false" CssClass="shippingpannel">
    <p>&nbsp;</p>
    <table class="alltable" width="550" cellpadding="0" cellspacing="0">
    <tr class="tablebody">
        <td class="questionstext">Сумма заказа:</td>
        <td class="answertext">
            <asp:Label ID="lblShippingSum" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Стоимость доставки:</td>
        <td class="answertext">
            <asp:Label ID="lblDeliverSum" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Общая сумма:</td>
        <td class="answertext">
            <asp:Label ID="lblTotalSum" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Ф.И.О.</td>
        <td class="answertext">
            <asp:Label ID="lblFIO" runat="server"></asp:Label>
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Способ доставки</td>
        <td class="answertext">
            <asp:Label ID="lblDeliver" runat="server"></asp:Label>
        </td>
    </tr>
    
    <tr class="tablebody">
        <td class="questionstext">Адрес</td>
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
        <td class="questionstext">Телефон</td>
        <td class="answertext">
            <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label> 
            </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Время </td>
        <td class="answertext">
            с <asp:Label ID="lblTime1" runat="server" />по <asp:Label ID="lblTime2" runat="server" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">Комментарий </td>
        <td class="answertext">
            <asp:Label ID="lblNote" runat="server" ></asp:Label> 
        </td>
    </tr>
    <tr class="tablebody">
    <td><asp:Button ID="btnAdmit" runat="server" Text="Подтвердить" 
            CssClass="loginSubStyle" onclick="btnAdmit_Click"/></td>
    <td><asp:Button ID="btnChange" runat="server" Text="Изменить" 
            CssClass="loginSubStyle" onclick="btnChange_Click"/></td>
    </tr>
</table>
    </asp:Panel>
    <asp:Panel ID="pnlFinal" runat="server"  Visible="false" CssClass="shippingpannel">
    <p>&nbsp;</p>
        <asp:Label ID="LblOrderNumber" runat="server" CssClass="productName"/>
        <p class="productName">В ближайшее время с Вами свяжется наш сотрудник.</p>
        <p align="center">
        <asp:Button ID="btnDefault" runat="server" Text="На главную страницу" 
                CssClass="loginSubStyle" onclick="btnDefault_Click" />
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlForEmail" runat="server" Visible="false">
        <uc1:Order ID="OrderCtrl" runat="server" ViewOrderStatus="false" />
    
    </asp:Panel>
</asp:Content>

