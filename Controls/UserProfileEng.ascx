<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfileEng.ascx.cs" Inherits="Controls_UserProfileEng" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablePanel">
    <tr class="tablebody">
        <td width="40%" class="questionstext">
            Last Name:
        </td>
        <td width="60%" class="answertext"  align="left">
            <asp:TextBox ID="txtLastName" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            First Name:
        </td>
        <td class="answertext"  align="left">
            <asp:TextBox ID="txtFirstName" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Patronymic :
        </td>
        <td class="answertext"  align="left">
            <asp:TextBox ID="txtMiddleName" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td colspan="2" align="center" class="questionstext">
            Address:
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Country:
        </td>
        <td class="answertext"  align="left">
            <asp:DropDownList ID="ddlCountries" runat="server" DataTextField="CountryNameEn"
                DataValueField="CountryID" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged"
                AutoPostBack="true" />
        </td>
    </tr>
    <asp:Panel ID="pnlRusCountry" runat="server">
        <tr class="tablebody">
            <td class="questionstext">
                Select a city or region:
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
                <td class="answertext" align="left">
                    <asp:TextBox ID="txtCity2" runat="server" Width="250px" />
                </td>
            </tr>
        </asp:Panel>
        <tr class="tablebody">
            <td class="questionstext">
                Postcode:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtCityIndex" runat="server" />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                Street:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtStreet" runat="server" Width="250px"  />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                House:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtHome" runat="server" />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                Building:
            </td>
            <td class="answertext  align="left">
                <asp:TextBox ID="txtKorpus" runat="server" />
            </td>
        </tr>
        <tr class="tablebody">
            <td class="questionstext">
                Apartment/Office:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtUnit" runat="server" />
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnlNotRusCountry" runat="server" Visible="false">
        <tr class="tablebody">
            <td class="questionstext">
                Address:
            </td>
            <td class="answertext"  align="left">
                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="5" runat="server" />
            </td>
        </tr>
    </asp:Panel>
    <tr class="tablebody">
        <td colspan="2" class="questionstext">
           Contact information
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
            Phone number:
        </td>
        <td class="answertext"  align="left">
            <asp:TextBox ID="txtPhone" runat="server" Width="250px" />
        </td>
    </tr>
    <tr class="tablebody">
        <td class="questionstext">
           Preferred contact time:
        </td>
        <td class="answertext"  align="left">
            from &nbsp;<asp:TextBox ID="txtTime1" runat="server" Width="30px" />&nbsp; till &nbsp;<asp:TextBox
                ID="txtTime2" runat="server" Width="30px" />
        </td>
    </tr>
</table>