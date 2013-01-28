<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.master" AutoEventWireup="true"
    CodeFile="Cathegories.aspx.cs" Inherits="Admin_Cathegories" ValidateRequest="false" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ MasterType VirtualPath="~/Admin/Admin2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="btn-group pull-right">
        <asp:Button ID="btnGenerateMenu" runat="server" Text="Генерировать меню" CssClass="btn btn-success btn-large"
            OnClick="btnGenerateMenu_Click" />
    </div>
    <h3>
        Список категорий по группам</h3>
    <div class="control-group">
        <label class="control-label" for="ddlGroup">
            Выберите группу:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlGroup" runat="server" DataValueField="GroupId" DataTextField="GroupNameRus"
                AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" ClientIDMode="Static"
                CssClass="span5" />
        </div>
    </div>
    <div class="row">
        <div class="span15">
            <asp:ListView ID="lvCategories" runat="server" DataKeyNames="CatId" OnPagePropertiesChanged="lvCategories_PagePropertiesChanged"
                OnSelectedIndexChanging="lvCategories_SelectedIndexChanging" OnItemDeleting="lvCategories_ItemDeleting">
                <LayoutTemplate>
                    <table class="table table-striped table-bordered table-condensed" border="0" cellspacing="0"
                        cellpadding="0">
                        <tr>
                            <th class="center">
                                Порядок сортировки
                            </th>
                            <th class="center">
                                Английский заголовок
                            </th>
                            <th class="center">
                                <asp:LinkButton ID="lbtNameSorted" runat="server" Text="Русский заголовок" OnClick="lbtSorted_Click"
                                    CommandArgument="Name" />
                            </th>
                            <th class="center">
                                Статус
                            </th>
                            <th class="center">
                                <asp:LinkButton ID="lbtDateCreatedSorted" runat="server" Text="Дата создания" OnClick="lbtSorted_Click"
                                    CommandArgument="DateCreated" />
                            </th>
                            <th class="center">
                                <asp:LinkButton ID="lbtDateUpdatedSorted" runat="server" Text="Дата изменения" OnClick="lbtSorted_Click"
                                    CommandArgument="DateUpdated" />
                            </th>
                            <th class="center">
                                Используемый шаблон
                            </th>
                            <th class="center">
                                Участвует в групповой ссылке
                            </th>
                            <td class="center">
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
                            <%#Eval("CatOrder")%>
                        </td>
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
                            <asp:CheckBox ID="cbInGeneral" runat="server" Checked='<%#Eval("InGeneralLink") %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnSelect" runat="server" Text="Выбрать" CommandName="Select"
                                CommandArgument='<%#Eval("CatId") %>' OnClick="lbtnSelect_Click" />&nbsp;
                            <asp:LinkButton ID="lbtnDelete" runat="server" Text="Удалить" CommandName="Delete"
                                ForeColor="Red" CommandArgument='<%#Eval("CatId") %>' OnClientClick="return confirm('Вы действительно хотите удалить эту категорию?');" />
                        </td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr>
                        <td class="row_selected">
                            <%#Eval("CatOrder")%>
                        </td>
                        <td class="row_selected">
                            <%#Eval("CatNameEng")%>
                        </td>
                        <td class="row_selected">
                            <%#Eval("CatNameRus") %>
                        </td>
                        <td class="row_selected">
                            <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("ActiveStatus") %>' Enabled="false" />
                        </td>
                        <td class="row_selected">
                            <%#Eval("DateCreated")%>
                        </td>
                        <td class="row_selected">
                            <%#Eval("DateUpdated")%>
                        </td>
                        <td class="row_selected">
                            <%--                        <%#Eval("TempleName")%>--%>
                            <%#Eval("Template.TempleName")%>
                        </td>
                        <td class="row_selected">
                            <asp:CheckBox ID="cbInGeneral" runat="server" Checked='<%#Eval("InGeneralLink") %>' Enabled="false" />
                        </td>
                        <td class="row_selected">
                            &nbsp;
                        </td>
                    </tr>
                </SelectedItemTemplate>
                <EmptyDataTemplate>
                    <p>
                        Ни одной категории в данной группе нет.</p>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:Panel ID="pnlPager" runat="server" HorizontalAlign="Center" Visible="false"
                CssClass="well-small well">
                <asp:DataPager ID="pagerBottom" runat="server" PageSize="20" PagedControlID="lvCategories">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonCssClass="pager_link" FirstPageText="«" PreviousPageText="‹"
                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                            ShowLastPageButton="false" ShowNextPageButton="false" />
                        <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="pager_link" CurrentPageLabelCssClass="pager_cur"
                            NextPreviousButtonCssClass="pager_link" />
                        <asp:NextPreviousPagerField ButtonCssClass="pager_link" LastPageText="»" NextPageText="›"
                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                            ShowLastPageButton="true" ShowNextPageButton="true" />
                    </Fields>
                </asp:DataPager>
            </asp:Panel>
        </div>
    </div>
    <p>
    </p>
    <p>
    </p>
    <asp:Panel ID="pnlEditCathegory" runat="server">
        <h3>
            <asp:Label ID="lblEditHeader" runat="server" Text="Добавление новой категории" /></h3>
        <div class="row">
            <div class="span12">
                <fieldset>
                    <div class="control-group">
                        <label class="control-label" for="txtCatNameRus">
                            Русское имя:</label>
                        <div class="controls">
                            <asp:TextBox ID="txtCatNameRus" runat="server" CssClass="span5" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="valRequireNameRus" ControlToValidate="txtCatNameRus"
                                runat="server" ErrorMessage="Требуется вести русское имя" Display="Dynamic" ValidationGroup="EditCat">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="txtCatNameEng">
                            Английское имя:</label>
                        <div class="controls">
                            <asp:TextBox ID="txtCatNameEng" runat="server" CssClass="span5" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="valRequireNameEng" ControlToValidate="txtCatNameEng"
                                runat="server" ErrorMessage="Требуется вести английское имя" Display="Dynamic"
                                ValidationGroup="EditCat">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="ddlGroupForEdit">
                            Группа:</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlGroupForEdit" runat="server" DataTextField="GroupNameRus"
                                DataValueField="GroupID" CssClass="span5" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="ddlTemplate">
                            Шаблон:</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlTemplate" runat="server" DataTextField="TempleName" DataValueField="TempleID"
                                CssClass="span5" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="cbxInGroupLink">
                            Учавствует в групповй ссылке:</label>
                        <div class="controls">
                           <asp:CheckBox runat="server" ID="cbxInGroupLink" ClientIDMode="Static"/>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                           <%-- <a class="accordion-toggle" href="#collapseOne" data-toggle="collapse">Описания
                            </a>--%>
                            <a class="accordion-toggle" href="#" data-toggle="collapse" onclick="$('#collapseOne').collapse('toggle')">Описания
                            </a>
                        </div>
                        <div id="collapseOne" class="accordion-body in collapse" style="height: auto;">
                            <div class="control-group">
                                <label class="control-label" for="txtDescriptionRus">
                                    Описание на русском (вверху):</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtDescriptionRus" runat="server" TextMode="MultiLine" Rows="2"
                                        CssClass="span10" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="txtDescriptionRus2">
                                    Описание на русском (внизу):</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtDescriptionRus2" runat="server" TextMode="MultiLine" Rows="2"
                                        CssClass="span10" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="txtDescriptionEng">
                                    Описание на английском:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtDescriptionEng" runat="server" TextMode="MultiLine" Rows="2"
                                        CssClass="span10" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="optionsCheckboxList">Опции показа:</label>
                        <div class="controls">
                            <label class="checkbox">
                                <asp:CheckBox ID="cbkActive" runat="server" Checked="true" name="optionsCheckboxList1" />&nbsp;Активна
                            </label>
                            <label class="checkbox">
                               <asp:CheckBox ID="cbkMarked" runat="server" name="optionsCheckboxList2" />&nbsp;Выделена
                            </label>
                           </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="txtCatOrder">
                            Порядок сортировки:</label>
                        <div class="controls">
                             <asp:TextBox ID="txtCatOrder" runat="server" CssClass="span1" ClientIDMode="Static"/>
                        <asp:NumericUpDownExtender ID="txtCatOrder_NumericUpDownExtender" runat="server"
                            Maximum="30" Minimum="1" Width="60" TargetControlID="txtCatOrder">
                        </asp:NumericUpDownExtender>
                        <asp:RequiredFieldValidator ID="reqValCatOrder" ControlToValidate="txtCatOrder" runat="server"
                            ErrorMessage="Требуется вести порядок сортировки" Display="Dynamic" ValidationGroup="EditCat">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" href="#" data-toggle="collapse" onclick="$('#collapseTwo').collapse('toggle')">Meta
                            </a>
                        </div>
                        <div id="collapseTwo" class="accordion-body in collapse" style="height: auto;">
                            <div class="control-group">
                                <label class="control-label" for="txtMetaTitle">
                                    Title:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtMetaTitle" runat="server" TextMode="MultiLine" Rows="2" CssClass="span10" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="xtMetaDescription">
                                     Meta Description:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtMetaDescription" runat="server" TextMode="MultiLine" Rows="2"
                                    CssClass="span10" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="txtMetaKeywords">
                                      Meta Keywords:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtMetaKeywords" runat="server" TextMode="MultiLine" Rows="2"
                                    CssClass="span10" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btnAddCathegory" runat="server" Text="Добавить" OnClick="btnAddCathegory_Click"
                        ValidationGroup="EditCat" CssClass="btn btn-primary" />
                    <asp:Button ID="btnCancelCathegory" runat="server" Text="Отмена" OnClick="btnCancelCathegory_Click"
                        CssClass="btn" />
                    </div>
                    <asp:ValidationSummary ID="valSum" ValidationGroup="EditCat" runat="server" ShowMessageBox="false"
                        ShowSummary="true" CssClass="alert alert-error span3"/>
                </fieldset>
            </div>
        </div>
       
    </asp:Panel>
    <div id="EmptySpace">
    </div>
</asp:Content>
