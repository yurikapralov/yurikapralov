<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true"
    CodeFile="Shopping.aspx.cs" Inherits="Shopping" Theme="Theme2" %>

<%@ MasterType VirtualPath="~/Theme2.master" %>
<%@ Register Src="Controls/Order1.ascx" TagName="Order" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://www.kupivkredit.ru/widget/vkredit.js"></script>
    <asp:Literal ID="lblCreditScript" runat="server" /> 
    <script type="text/javascript">
        var GoCredit = function () {
            vkredit.openWidget();
            $('#btngocred').hide();
            $('#btnBuyCash').val('Завершить покупку');
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="Server">
    <asp:Panel ID="pnlShoppingCart" runat="server">
        <p>
            Вы можете просмотреть и отредактировать Ваши покупки перед окончательным оформлением
            заказа.
        </p>
        <p class="conditions">
            Внимание: Ознакомтесь с <a href="javascript:window.open('deliverpopup.html','Deliver','top=0,left=0,width=800,height=780');void(0);"
                class="mainlink">условиями</a> доставки товара!</p>
        <asp:ObjectDataSource ID="objShoppingCart" runat="server" SelectMethod="GetItems"
            TypeName="echo.BLL.Orders.CurrentUserShoppingCart"></asp:ObjectDataSource>
        <asp:GridView ID="gvwOrderItems" runat="server" AutoGenerateColumns="false" DataSourceID="objShoppingCart"
            DataKeyNames="ProdSizeId" OnRowCreated="gvwOrderItems_RowCreated" OnRowDeleting="gvwOrderItems_RowDeleting"
            CssClass="ShopTable">
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
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Bind("Qty") %>' Width="20" />
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
            <p>
                <asp:Label ID="lblSubtotal" runat="server" CssClass="subtotal" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblUsa" Visible="False">Заказ содержит товары, доставляемые напрямую из США</asp:Label></p>
            <p>
                <asp:Button ID="btnUpdate" runat="server" Text="Обновить" CssClass="btn" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnOrder" runat="server" Text="Оформить заказ" CssClass="btn btn-primary"
                    OnClick="btnOrder_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Отменить заказ" CssClass="btn"
                     OnClientClick="if(confirm('Очистить вашу корзину?')==false)return false;"
                    OnClick="btnReset_Click" />
            </p>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlAuntification" runat="server" Visible="false" ClientIDMode="Static">
        <p>
            Вы можете войти или зарегистрироваться, для более удобного оформления заказа</p>
        <p>
            Регистрация избавляет Вас от необходимости вводить каждый раз Ваши контактные данные,
            а также дает возможность отслеживать историю Ваших заказов.</p>
        <p>
            Регистрация на нашем сайте НЕ ЯВЛЯЕТСЯ обязательной.</p>
        <asp:Login ID="ctrLogin" FailureText="Неправильное имя или пароль" runat="server">
            <LayoutTemplate>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Literal runat="server" ID="FailureText" /><span class="logTitle">Логин:</span>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textform" Width="100px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valRequireLogin" runat="server" SetFocusOnError="true"
                                Text="*" Display="Dynamic" ControlToValidate="UserName" ValidationGroup="Login1" />
                            <span class="logTitle">Пароль:</span>
                            <asp:TextBox ID="Password" runat="server" CssClass="textform" TextMode="Password"
                                ToolTip="Пароль"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" SetFocusOnError="true"
                                Text="*" Display="Dynamic" ControlToValidate="Password" ValidationGroup="Login1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Register.aspx?Shop=OK">Зарегистрироваться</asp:HyperLink>
                            <br />
                            <asp:HyperLink ID="lnkPaswordRecovery" NavigateUrl="~/PasswordRecovery.aspx" runat="server">Забыли пароль</asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <p>
                </p>
                <asp:Button ID="btnLogin" runat="server" Text="Войти" CssClass="btn btn-primary" ValidationGroup="Login1"
                    CommandName="Login" />
                <asp:Button ID="btnEsc" runat="server" Text="Пропустить регистрацию" OnClick="btnEsc_Click"
                    CssClass="btn"  />
            </LayoutTemplate>
        </asp:Login>
    </asp:Panel>
    <asp:Panel ID="pnlShipping" runat="server" Visible="false" ClientIDMode="Static"
        CssClass="tablePanel">
        <h2>
            Пожалуйста, заполните следующую контактную информацию</h2>
        <table class="alltable" width="550" cellpadding="0" cellspacing="0">
            <tr class="tablebody">
                <td class="questionstext">
                    &nbsp;
                </td>
                <td class="answertext">
                    <asp:CheckBox runat="server" Text="Покупка в кредит" Font-Bold="True" ID="cbxCreditPayment"
                        OnCheckedChanged="cbxCreditPayment_CheckedChanged" AutoPostBack="True" />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Фамилия
                </td>
                <td class="answertext">
                    <asp:TextBox ID="txtLastName" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqireLastName" ControlToValidate="txtLastName"
                        Display="Dynamic" Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует Фамилия" />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Имя
                </td>
                <td class="answertext">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqireFirstName" ControlToValidate="txtFirstName"
                        Display="Dynamic" Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует Имя" />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Отчество
                </td>
                <td class="answertext">
                    <asp:TextBox ID="txtMiddleName" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Способ доставки*
                </td>
                <td class="answertext">
                    <asp:DropDownList ID="ddlDeliverType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeliverType_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Выберите способ доставки--</asp:ListItem>
                        <asp:ListItem Value="1">ЕМС Гарантпост</asp:ListItem>
                        <asp:ListItem Value="2">Наложенный платеж</asp:ListItem>
                        <asp:ListItem Value="3">Курьером по Москве</asp:ListItem>
                        <asp:ListItem Value="4">За пределы РФ</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valReqDeliverMethod" ControlToValidate="ddlDeliverType"
                        Display="Dynamic" Text="*" ValidationGroup="OrderInfo" InitialValue="0" runat="server"
                        ErrorMessage="Не выбран способ доставки" />
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
                            AutoPostBack="true" OnSelectedIndexChanged="rblRegionType_SelectedIndexChanged">
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
                        Улица:<asp:TextBox ID="txtStreet" runat="server" Width="180px" /><br />
                        <asp:RequiredFieldValidator ID="valReqStreet" ControlToValidate="txtStreet" Display="Dynamic"
                            Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует улица" />
                        Дом:<asp:TextBox ID="txtHouse" runat="server" Width="30px" />
                        <asp:RequiredFieldValidator ID="valReqHouse" ControlToValidate="txtHouse" Display="Dynamic"
                            Text="*" ValidationGroup="OrderInfo" runat="server" ErrorMessage="Отсутствует номер дома" />
                        Корпус:<asp:TextBox ID="txtKorpus" runat="server" Width="30px" />Квартира/Офис:<asp:TextBox
                            ID="txtUnit" runat="server" Width="30px" />
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
                    <asp:RegularExpressionValidator ID="valRegEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" Text="*" ErrorMessage="Не правильный формат E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
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
                            <td width="40%" align="right" class="cont_table">
                                <asp:Button ID="btnMore" runat="server" Text="Продолжить" CssClass="btn btn-primary" ValidationGroup="OrderInfo"
                                    OnClick="btnMore_Click" />
                            </td>
                            <td width="20%">
                                <asp:ValidationSummary runat="server" ID="valSum" ValidationGroup="OrderInfo" ShowMessageBox="true"
                                    ShowSummary="false" />
                            </td>
                            <td width="40%" align="left">
                                <asp:Button ID="btnReturn" runat="server" Text="Вернуться в корзину" CssClass="btn"
                                    OnClick="btnReturn_Click"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlShippingConfirmation" runat="server" Visible="false" ClientIDMode="Static"
        CssClass="tablePanel">
        <table class="alltable" width="100%" cellpadding="0" cellspacing="0">
            <tr class="tablebody">
                <td class="questionstext">
                    Сумма заказа:
                </td>
                <td class="answertext">
                    <asp:Label ID="lblShippingSum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Стоимость доставки:
                </td>
                <td class="answertext">
                    <asp:Label ID="lblDeliverSum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Общая сумма:
                </td>
                <td class="answertext">
                    <asp:Label ID="lblTotalSum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Ф.И.О.
                </td>
                <td class="answertext">
                    <asp:Label ID="lblFIO" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Способ доставки
                </td>
                <td class="answertext">
                    <asp:Label ID="lblDeliver" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Адрес
                </td>
                <td class="answertext">
                    <asp:Label ID="lblAdress" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    E-mail
                </td>
                <td class="answertext">
                    <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Телефон
                </td>
                <td class="answertext">
                    <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Время
                </td>
                <td class="answertext">
                    с
                    <asp:Label ID="lblTime1" runat="server" />по
                    <asp:Label ID="lblTime2" runat="server" />
                </td>
            </tr>
            <tr class="tablebody">
                <td class="questionstext">
                    Комментарий
                </td>
                <td class="answertext">
                    <asp:Label ID="lblNote" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tablebody">
                <td class="cont_table">
                    <asp:Button ID="btnAdmit" runat="server" Text="Подтвердить" CssClass="btn btn-primary" OnClick="btnAdmit_Click"
                        OnClientClick="yaCounter11473630.reachGoal('ORDER'); return true;" />
                </td>
                <td>
                    <asp:Button ID="btnChange" runat="server" Text="Изменить" CssClass="btn" OnClick="btnChange_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlCredit" Visible="False">
        <asp:Button CssClass="btn" runat="server" ID="btnBuyCash" 
            Text="Купить за наличные" onclick="btnBuyCash_Click" ClientIDMode="Static"/>
        <input type="button" value="Получить кредит" id="btngocred" class="btn btn-primary" onclick="GoCredit();"/>
    </asp:Panel>
    <asp:Panel ID="pnlFinal" runat="server" Visible="false">
        <h2>
            <asp:Label ID="LblOrderNumber" runat="server" /></h2>
        <p class="ProductItemDescription">
            В ближайшее время с Вами свяжется наш сотрудник.</p>
        <p align="center">
            <asp:Button ID="btnDefault" runat="server" Text="На главную страницу" CssClass="btn btn-primary"
                OnClick="btnDefault_Click"  />
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlForEmail" runat="server" Visible="false">
        <uc1:Order ID="OrderCtrl" runat="server" ViewOrderStatus="false" />
    </asp:Panel>
</asp:Content>
