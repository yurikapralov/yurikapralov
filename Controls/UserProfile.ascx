<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="Controls_UserProfile" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablePanel">
    <tr class="tablebody">
        <td width="40%" class="questionstext">
            Фамилия
        </td>
        <td width="60%" class="answertext"  align="left">
            <asp:TextBox ID="txtLastName" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Имя:
        </td>
        <td class="answertext"  align="left">
            <asp:TextBox ID="txtFirstName" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Отчество:
        </td>
        <td class="answertext"  align="left">
            <asp:TextBox ID="txtMiddleName" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td colspan="2" align="center" class="questionstext">
            Адресные данные
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Страна:
        </td>
        <td class="answertext"  align="left">
            <asp:DropDownList ID="ddlCountries" runat="server" DataTextField="CountryNameRU"
                DataValueField="CountryID" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged"
                AutoPostBack="true" />
        </td>
    </tr>
    <asp:Panel ID="pnlRusCountry" runat="server">
        <tr class="tablebody">
            <td class="questionstext">
                Выберите город или регион:
            </td>
            <td class="answertext"  align="left">
                <asp:DropDownList ID="ddlCities" runat="server" DataTextField="City_RUS" DataValueField="CityID"
                    AutoPostBack="true" />
            </td>
        </tr>
        <tr class="tablebody">
            <td colspan="2" class="answertext">
                <asp:RadioButtonList ID="rblRegionType" runat="server" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rblRegionType_SelectedIndexChanged" AutoPostBack="true">
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
                <td class="answertext" align="left">
                    <asp:TextBox ID="txtCity2" runat="server" Width="250px" />
                </td>
            </tr>
        </asp:Panel>
        <tr class="tablebody">
            <td class="questionstext">
                Почтовый индекс:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtCityIndex" runat="server" />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                Улица:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtStreet" runat="server" Width="250px"  />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                Дом:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtHome" runat="server" />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                Корпус/Строение:
            </td>
            <td class="answertext  align="left">
                <asp:TextBox ID="txtKorpus" runat="server" />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                Квартира/Офис:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtUnit" runat="server" />
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnlNotRusCountry" runat="server" Visible="false">
        <tr class="tablebody">
            <td class="questionstext">
                Адрес:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="5" runat="server" />
            </td>
        </tr>
    </asp:Panel>
    <tr class="tablebody">
        <td colspan="2" class="questionstext">
            Контактные данные
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Номер телефона:
        </td>
        <td class="answertext"  align="left">
            <asp:TextBox ID="txtPhone" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Предпочтителное время контакта:
        </td>
        <td class="answertext"  align="left">
            C &nbsp;<asp:TextBox ID="txtTime1" runat="server" Width="30px" />&nbsp; По &nbsp;<asp:TextBox
                ID="txtTime2" runat="server" Width="30px" />
        </td>
    </tr>
</table>
