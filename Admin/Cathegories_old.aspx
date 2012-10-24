<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Cathegories_old.aspx.cs" Inherits="Admin_Cathegories_old" Theme="Admin"  ValidateRequest="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content_total">
        <h2>
            Список категорий по группам</h2>
        <br />
        Выберите группу:
        <asp:DropDownList ID="ddlGroup" runat="server" DataValueField="GroupId" DataTextField="GroupNameRus"
            AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" />
        <br />
        <br />
        <asp:ListView ID="lvCategories" runat="server" DataKeyNames="CatId" OnPagePropertiesChanged="lvCategories_PagePropertiesChanged"
            OnSelectedIndexChanging="lvCategories_SelectedIndexChanging" OnItemDeleting="lvCategories_ItemDeleting">
            <LayoutTemplate>
                <table class="MainAdminTable" border="1" cellspacing="0" cellpadding="0">
                    <tr class="MainAdminTableHeader">
                        <td>
                            Английский заголовок
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtNameSorted" runat="server" Text="Русский заголовок" OnClick="lbtSorted_Click"
                                CommandArgument="Name" />
                        </td>
                        <td>
                            Статус
                        </td>
                        <td>
                            Файл Английского заголовка
                        </td>
                        <td>
                            Файл Русского заголовка
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtDateCreatedSorted" runat="server" Text="Дата создания" OnClick="lbtSorted_Click"
                                CommandArgument="DateCreated" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtDateUpdatedSorted" runat="server" Text="Дата изменения" OnClick="lbtSorted_Click"
                                CommandArgument="DateUpdated" />
                        </td>
                        <td>
                            Используемый шаблон
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="itemPlaceholder" runat="server" class="MainAdminTableItem">
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("CatNameEng")%>
                    </td>
                    <td>
                        <%#Eval("CatNameRus") %>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("ActiveStatus") %>' Enabled="false" />
                    </td>
                    <td>
                        <%#Eval("HeaderImageEngURL")%>
                    </td>
                    <td>
                        <%#Eval("HeaderImageRusURL")%>
                    </td>
                    <td>
                        <%#Eval("DateCreated")%>
                    </td>
                    <td>
                        <%#Eval("DateUpdated")%>
                    </td>
                    <td>
                        <%--                        <%#Eval("TempleName")%>--%>
                        <%#Eval("Template.TempleName") %>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnSelect" runat="server" Text="Выбрать" CommandName="Select"
                            CommandArgument='<%#Eval("CatId") %>' OnClick="lbtnSelect_Click" />
                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Удалить" CommandName="Delete"
                            CommandArgument='<%#Eval("CatId") %>' OnClientClick="return confirm('Вы действительно хотите удалить эту категорию?');" />
                    </td>
                </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
                <tr style="color: red;">
                    <td>
                        <%#Eval("CatNameEng")%>
                    </td>
                    <td>
                        <%#Eval("CatNameRus") %>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("ActiveStatus") %>' Enabled="false" />
                    </td>
                    <td>
                        <%#Eval("HeaderImageEngURL")%>
                    </td>
                    <td>
                        <%#Eval("HeaderImageRusURL")%>
                    </td>
                    <td>
                        <%#Eval("DateCreated")%>
                    </td>
                    <td>
                        <%#Eval("DateUpdated")%>
                    </td>
                    <td>
                        <%--                        <%#Eval("TempleName")%>--%>
                        <%#Eval("Template.TempleName")%>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </SelectedItemTemplate>
            <EmptyDataTemplate>
                <p>
                    Ни одной категории в данной группе нет.</p>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:Panel ID="pnlPager" runat="server" HorizontalAlign="Center" Visible="false">
            Перейти:
            <asp:DataPager ID="pagerBottom" runat="server" PageSize="20" PagedControlID="lvCategories">
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
        <p>
        </p>
        <p>
        </p>
        <asp:Panel ID="pnlEditCathegory" runat="server">
            <h2>
                <asp:Label ID="lblEditHeader" runat="server" Text="Добавление новой категории" /></h2>
            <br />
            <table class="MainAdminTable">
                <tr>
                    <td class="MainAdminTableHeader" width="200">
                        Русское имя:
                    </td>
                    <td class="MainAdminTableItem" width="300" align="left">
                        <asp:TextBox ID="txtCatNameRus" runat="server" Width="280" />
                        <asp:RequiredFieldValidator ID="valRequireNameRus" ControlToValidate="txtCatNameRus"
                            runat="server" ErrorMessage="Требуется вести русское имя" Display="Dynamic" ValidationGroup="EditCat">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Английское имя:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:TextBox ID="txtCatNameEng" runat="server" Width="280" />
                        <asp:RequiredFieldValidator ID="valRequireNameEng" ControlToValidate="txtCatNameEng"
                            runat="server" ErrorMessage="Требуется вести английское имя" Display="Dynamic"
                            ValidationGroup="EditCat">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Группа:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:DropDownList ID="ddlGroupForEdit" runat="server" DataTextField="GroupNameRus"
                            DataValueField="GroupID" />
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Шаблон:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:DropDownList ID="ddlTemplate" runat="server" DataTextField="TempleName" DataValueField="TempleID" />
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Описание на русском (вверху):
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:TextBox ID="txtDescriptionRus" runat="server" TextMode="MultiLine" Rows="5"
                            Width="280" />
                    </td>
                </tr>
                 <tr>
                    <td class="MainAdminTableHeader">
                        Описание на русском (внизу):
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:TextBox ID="txtDescriptionRus2" runat="server" TextMode="MultiLine" Rows="5"
                            Width="280" />
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
                    <td class="MainAdminTableHeader">
                        Активна:
                    </td>
                    <td class="MainAdminTableItem">
                        <asp:CheckBox ID="cbkActive" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Выделена:
                    </td>
                    <td class="MainAdminTableItem">
                        <asp:CheckBox ID="cbkMarked" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Порядок сортировки:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                    <asp:TextBox ID="txtCatOrder" runat="server" /> 
                        <asp:NumericUpDownExtender ID="txtCatOrder_NumericUpDownExtender" 
                            runat="server" Maximum="30" Minimum="1" Width="60" TargetControlID="txtCatOrder">
                        </asp:NumericUpDownExtender>
                     <asp:RequiredFieldValidator ID="reqValCatOrder" ControlToValidate="txtCatOrder"
                            runat="server" ErrorMessage="Требуется вести порядок сортировки" Display="Dynamic" ValidationGroup="EditCat">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Title:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:TextBox ID="txtMetaTitle" runat="server" TextMode="MultiLine" Rows="5"
                            Width="280" />
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Meta Description:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:TextBox ID="txtMetaDescription" runat="server" TextMode="MultiLine" Rows="5"
                            Width="280" />
                    </td>
                </tr>
                <tr>
                    <td class="MainAdminTableHeader">
                        Meta Keywords:
                    </td>
                    <td class="MainAdminTableItem" align="left">
                        <asp:TextBox ID="txtMetaKeywords" runat="server" TextMode="MultiLine" Rows="5"
                            Width="280" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnAddCathegory" runat="server" Text="Добавить" OnClick="btnAddCathegory_Click"
                            ValidationGroup="EditCat" CssClass="TableButton" />
                        <asp:Button ID="btnCancelCathegory" runat="server" Text="Отмена" OnClick="btnCancelCathegory_Click"
                            CssClass="TableButton" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:ValidationSummary ID="valSum" ValidationGroup="EditCat" runat="server" ShowMessageBox="false"
                            ShowSummary="true" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button ID="btnGenerateMenu" runat="server" Text="Генерировать меню" 
            CssClass="TableButton" onclick="btnGenerateMenu_Click" />
        <div id="EmptySpace"></div>
    </div>
</asp:Content>
