<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Platforms_old.aspx.cs" Inherits="Admin_Platforms_old" Theme="Admin" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content_total">
        <asp:UpdatePanel ID="upnlEdit" runat="server">
            <ContentTemplate>
                <h2>
                    Редактировать
                    <asp:DropDownList ID="ddlEditMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEditMode_SelectedIndexChanged">
                        <asp:ListItem Text="Выберите предмет редактирования" Value="0" />
                        <asp:ListItem Text="Платформы" Value="1" />
                        <asp:ListItem Text="Размеры" Value="2" />
                        <asp:ListItem Text="Цвета" Value="3" />
                        <asp:ListItem Text="Бренды" Value="4"/>
                    </asp:DropDownList>
                </h2>
                <p>
                </p>
                <asp:Panel ID="pnlPlatforms" runat="server" Visible="false">
                    <table width="80%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="70%" valign="top">
                                <h2>
                                    Список доступных платформ:</h2>
                                <asp:ListView ID="lvPlatforms" runat="server" DataKeyNames="PlatformID" OnPagePropertiesChanged="lvPlatforms_PagePropertiesChanged"
                                    OnSelectedIndexChanging="lvPlatforms_SelectedIndexChanging" OnItemDeleting="lvPlatforms_ItemDeleting">
                                    <LayoutTemplate>
                                        <table class="MainAdminTable" border="1" cellpadding="0" cellspacing="0">
                                            <tr class="MainAdminTableHeader">
                                                <td>
                                                    <asp:LinkButton ID="lbtNameSorted" runat="server" Text="Русское название" CommandArgument="Name"
                                                        OnClick="lblSorted_Click" />
                                                </td>
                                                <td>
                                                    Размерный ряд
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtDateCreatedSorted" runat="server" Text="Дата создания" CommandArgument="DateCreated"
                                                        OnClick="lblSorted_Click" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" class="MainAdminTableItem" />
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%#Eval("PlatformNameRus") %>
                                            </td>
                                            <td>
                                                <%#Eval("SizeInfo") %>
                                            </td>
                                            <td>
                                                <%#Eval("DateCreated") %>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text="Выбрать" CommandName="Select"
                                                    CommandArgument='<%#Eval("PlatformID") %>' OnClick="lbtnSelect_Click" />
                                                <asp:LinkButton ID="lbtnDelete" runat="server" Text="Удалить" CommandName="Delete"
                                                    CommandArgument='<%#Eval("PlatformID") %>' OnClientClick="return confirm('Вы действительно хотите удалить эту платформу?');" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="color: Red;">
                                            <td>
                                                <%#Eval("PlatformNameRus") %>
                                            </td>
                                            <td>
                                                <%#Eval("SizeInfo") %>
                                            </td>
                                            <td>
                                                <%#Eval("DateCreated") %>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:Panel ID="pnlPager" runat="server" HorizontalAlign="Center" Visible="false">
                                    Перейти:
                                    <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvPlatforms">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                                NextPreviousButtonCssClass="command" />
                                            <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </asp:Panel>
                            </td>
                            <td width="10%">
                            </td>
                            <td valign="top">
                                <h2>
                                    <asp:Label ID="lblEditHeader" runat="server" Text="Добавление новой платформы" /></h2>
                                <br />
                                <table class="MainAdminTable">
                                    <tr>
                                        <td class="MainAdminTableHeader" width="200">
                                            Русское имя:
                                        </td>
                                        <td class="MainAdminTableItem" width="300" align="left">
                                            <asp:TextBox ID="txtPlatformNameRus" runat="server" Width="280" />
                                            <asp:RequiredFieldValidator ID="valRequireNameRus" ControlToValidate="txtPlatformNameRus"
                                                runat="server" ErrorMessage="Требуется вести русское имя" Display="Dynamic" ValidationGroup="EditPlatform">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Описание на русском:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:TextBox ID="txtDescriptionRus" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="280" Text="Высота платформы см<br/>Высота каблука см." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Описание на английском:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:TextBox ID="txtDescriptionEng" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="280" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader" width="200">
                                            Размерный ряд:
                                        </td>
                                        <td class="MainAdminTableItem" width="300" align="left">
                                            <asp:TextBox ID="txtSizeInfo" runat="server" Width="280" />
                                            <asp:RequiredFieldValidator ID="valRequireSizeInfo" ControlToValidate="txtSizeInfo"
                                                runat="server" ErrorMessage="Требуется вести размерный ряд" Display="Dynamic"
                                                ValidationGroup="EditPlatform">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnAddPlatform" runat="server" Text="Добавить" ValidationGroup="EditPlatform"
                                                CssClass="TableButton" OnClick="btnAddPlatform_Click" />
                                            <asp:Button ID="btnCancelPlatform" runat="server" Text="Отмена" CssClass="TableButton"
                                                OnClick="btnCancelPlatform_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:ValidationSummary ID="valSum" ValidationGroup="EditPlatform" runat="server"
                                                ShowMessageBox="false" ShowSummary="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlSizes" runat="server" Visible="false">
                    <p>
                        &nbsp;</p>
                    <h2>
                        Редактирование размеров</h2>
                    <p>
                    </p>
                    <p>
                        Выберите тип продукции:
                        <asp:DropDownList ID="ddlProductTypes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductTypes_SelectedIndexChanged" />
                    </p>
                    <p>
                    </p>
                    <h2>
                        Список доступных размеров данного типа:</h2>
                    <p>
                    </p>
                    <asp:ListBox ID="lbSizes" runat="server" Width="200" Rows="15" DataValueField="SizeID"
                        DataTextField="FullSizeName" AutoPostBack="true" OnSelectedIndexChanged="lbSizes_SelectedIndexChanged" />
                    <p>
                        &nbsp;</p>
                    <h2>
                        Добавить размер:</h2>
                    <p>
                    </p>
                    Русский:
                    <asp:TextBox ID="txtRusSize" runat="server" />
                    <asp:RequiredFieldValidator ID="valRusSize" runat="server" ControlToValidate="txtRusSize"
                        ErrorMessage="Требуется ввести русское имя:" Display="Dynamic" ValidationGroup="EditSize">*</asp:RequiredFieldValidator>
                    Английский:
                    <asp:TextBox ID="txtEngSize" runat="server" />
                    <asp:RequiredFieldValidator ID="valEngSize" runat="server" ControlToValidate="txtEngSize"
                        ErrorMessage="Требуется ввести английское имя:" Display="Dynamic" ValidationGroup="EditSize">*</asp:RequiredFieldValidator>
                    Тип продукции:
                    <asp:DropDownList ID="ddlProductTypeEdit" runat="server" />
                    <br />
                    <asp:Button ID="btnAddSize" runat="server" Text="Добавить" ValidationGroup="EditSize"
                        CssClass="TableButton" OnClick="btnAddSize_Click" />
                    <asp:Button ID="btnDeleteSize" runat="server" Text="Удалить" Visible="false" OnClientClick="return confirm('Вы действительно хотите удалить этот размер?');"
                        CssClass="TableButton" OnClick="btnDeleteSize_Click" />
                    <asp:Button ID="btnCancelSize" runat="server" Text="Отмена" CssClass="TableButton"
                        OnClick="btnCancelSize_Click" />
                    <br />
                    <asp:ValidationSummary ID="valSumSize" runat="server" ShowMessageBox="false" ShowSummary="true"
                        ValidationGroup="EditSize" />
                </asp:Panel>
                <asp:Panel ID="pnlColors" runat="server" Visible="false">
                    <h2>
                        Список доступных цветов:</h2>
                    <asp:ListBox ID="lbColors" runat="server" Width="500" Rows="15" DataValueField="ColorID"
                        DataTextField="FullColorName" AutoPostBack="True" OnSelectedIndexChanged="lbColors_SelectedIndexChanged" />
                    <p>
                        &nbsp;</p>
                    <h2>
                        Добавить цвет:</h2>
                    <p>
                    </p>
                    Русский:
                    <asp:TextBox ID="txtRusColor" runat="server" />
                    <asp:RequiredFieldValidator ID="valRusColor" runat="server" ControlToValidate="txtRusColor"
                        ErrorMessage="Требуется ввести русское имя:" Display="Dynamic" ValidationGroup="EditColor">*</asp:RequiredFieldValidator>
                    Английский:
                    <asp:TextBox ID="txtEngColor" runat="server" />
                    <asp:RequiredFieldValidator ID="valEngColor" runat="server" ControlToValidate="txtEngColor"
                        ErrorMessage="Требуется ввести английское имя:" Display="Dynamic" ValidationGroup="EditColor">*</asp:RequiredFieldValidator>
                    <asp:CheckBox ID="cbxIsMain" runat="server" Text="Основной"/>&nbsp;
                    <asp:Button ID="btnAddColor" runat="server" Text="Добавить" OnClick="btnAddColor_Click"
                        ValidationGroup="EditColor" CssClass="TableButton" />
                    <asp:Button ID="btnDeleteColor" runat="server" Text="Удалить" Visible="false" OnClick="btnDeleteColor_Click"
                        OnClientClick="return confirm('Вы действительно хотите удалить этот цвет?');"
                        CssClass="TableButton" />
                    <asp:Button ID="btnCancelColor" runat="server" Text="Отмена" OnClick="btnCancelColor_Click"
                        CssClass="TableButton" />
                    <br />
                    <asp:ValidationSummary ID="valSumColor" runat="server" ShowMessageBox="false" ShowSummary="true"
                        ValidationGroup="EditColor" />
                </asp:Panel>
                <asp:Panel ID="pnlBrands" runat="server" Visible="false">
                <h2>Список брендов</h2>
                    <asp:ListBox runat="server" ID="lbBrands" Width="300" Rows="5" 
                        DataValueField="BrandId" DataTextField="BrandName" AutoPostBack="True" 
                        onselectedindexchanged="lbBrands_SelectedIndexChanged"/>
                    <p>&nbsp;</p>
                    <h2>Добавить бренд</h2>
                    <p>&nbsp;</p>
                    <asp:TextBox runat="server" ID="txtBrandName"/>
                    <asp:RequiredFieldValidator runat="server" ID="valBrandName" ControlToValidate="txtBrandName" ErrorMessage="Введите имя бренда" Display="Dynamic"
                    ValidationGroup="EditBrand">*</asp:RequiredFieldValidator>
                    <asp:Button runat="server" ID="btnAddBrand" ValidationGroup="EditBrand" Text="Добавить"
                        CssClass="TableButton" onclick="btnAddBrand_Click"/>
                        <asp:Button ID="btnCancelBrand" runat="server" Text="Отмена" 
                        CssClass="TableButton" onclick="btnCancelBrand_Click" />
                        <br/>
                    <asp:ValidationSummary runat="server" ID="valSumBrand" ShowMessageBox="False" ShowSummary="True" ValidationGroup="EditBrand"/>
                </asp:Panel>
                <asp:Panel ID="pnlErorr" runat="server" Visible="false">
                    <table width="80%" border="0" class="ErrorPanel">
                        <tr>
                            <td width="80%">
                                <asp:Label ID="lblErrorMessage" runat="server" />
                            </td>
                            <td align="right" valign="top">
                                <asp:LinkButton ID="lbtnCloseErrorMessage" runat="server" OnClick="lbtnCloseErrorMessage_Click"
                                    Text="Закрыть" ForeColor="Red" />
                            </td>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="EmptySpace">
        </div>
    </div>
</asp:Content>
