<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.master" AutoEventWireup="true"
    CodeFile="Platforms.aspx.cs" Inherits="Admin_Platforms" ValidateRequest="false" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ MasterType VirtualPath="~/Admin/Admin2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul id="platformtab" class="nav nav-tabs">
        <li class="active"><a href="#platforms_tab" data-toggle="tab">Платформы</a></li>
        <li><a href="#size_tab" data-toggle="tab">Размеры</a></li>
        <li><a href="#color_tab" data-toggle="tab">Цвета</a></li>
        <li><a href="#brands_tab" data-toggle="tab">Бренды</a></li>
    </ul>
    <div class="tab-content">
        <div class="tab-pane active" id="platforms_tab" style="height: 600px">
            <asp:UpdatePanel ID="uppnlPlatforms" runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top; padding-right: 30px;">
                                <h3>
                                    Список доступных платформ:</h3>
                                <asp:ListView ID="lvPlatforms" runat="server" DataKeyNames="PlatformID" OnPagePropertiesChanged="lvPlatforms_PagePropertiesChanged"
                                    OnSelectedIndexChanging="lvPlatforms_SelectedIndexChanging" OnItemDeleting="lvPlatforms_ItemDeleting">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered table-condensed" border="0" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <th class="center">
                                                    <asp:LinkButton ID="lbtNameSorted" runat="server" Text="Русское название" CommandArgument="Name"
                                                        OnClick="lblSorted_Click" />
                                                </th>
                                                <th class="center">
                                                    Размерный ряд
                                                </th>
                                                <th class="center">
                                                    <asp:LinkButton ID="lbtDateCreatedSorted" runat="server" Text="Дата создания" CommandArgument="DateCreated"
                                                        OnClick="lblSorted_Click" />
                                                </th>
                                                <th>
                                                    &nbsp;
                                                </th>
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
                                                <asp:LinkButton ID="lbtnDelete" runat="server" ForeColor="Red" Text="Удалить" CommandName="Delete"
                                                    CommandArgument='<%#Eval("PlatformID") %>' OnClientClick="return confirm('Вы действительно хотите удалить эту платформу?');" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr>
                                            <td class="row_selected">
                                                <%#Eval("PlatformNameRus") %>
                                            </td>
                                            <td class="row_selected">
                                                <%#Eval("SizeInfo") %>
                                            </td>
                                            <td class="row_selected">
                                                <%#Eval("DateCreated") %>
                                            </td>
                                            <td class="row_selected">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:Panel ID="pnlPager" runat="server" HorizontalAlign="Center" Visible="false">
                                    Перейти:
                                    <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvPlatforms">
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
                            </td>
                            <td style="vertical-align: top; padding-left: 30px;">
                                <h3>
                                    <asp:Label ID="lblEditHeader" runat="server" Text="Добавление новой платформы" /></h3>
                                <fieldset>
                                    <div class="control-group">
                                        <label class="control-label" for="txtPlatformNameRus">
                                            Русское имя:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtPlatformNameRus" runat="server" CssClass="span4" ClientIDMode="Static" />
                                            <asp:RequiredFieldValidator ID="valRequireNameRus" ControlToValidate="txtPlatformNameRus"
                                                runat="server" ErrorMessage="Требуется вести русское имя" Display="Dynamic" ValidationGroup="EditPlatform">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label" for="txtDescriptionRus">
                                            Описание на русском:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtDescriptionRus" runat="server" TextMode="MultiLine" Rows="2"
                                                CssClass="span4" Text="Высота платформы см<br/>Высота каблука см." />
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label" for="txtDescriptionEng">
                                            Описание на английском:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtDescriptionEng" runat="server" TextMode="MultiLine" Rows="2"
                                                CssClass="span4" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label" for="txtSizeInfo">
                                            Размерный ряд:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSizeInfo" runat="server" CssClass="span4" ClientIDMode="Static" />
                                            <asp:RequiredFieldValidator ID="valRequireSizeInfo" ControlToValidate="txtSizeInfo"
                                                runat="server" ErrorMessage="Требуется вести размерный ряд" Display="Dynamic"
                                                ValidationGroup="EditPlatform">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <asp:Button ID="btnAddPlatform" runat="server" Text="Добавить" ValidationGroup="EditPlatform"
                                            CssClass="btn btn-primary" OnClick="btnAddPlatform_Click" />
                                        <asp:Button ID="btnCancelPlatform" runat="server" Text="Отмена" CssClass="btn" OnClick="btnCancelPlatform_Click" />
                                    </div>
                                    <asp:ValidationSummary ID="valSum" ValidationGroup="EditPlatform" runat="server"
                                        ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-error span4" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="tab-pane" id="size_tab">
            <asp:UpdatePanel runat="server" ID="uppnlSizes">
                <ContentTemplate>
                    <h3>
                        Редактирование размеров</h3>
                    <div class="control-group">
                        <label class="control-label" for="ddlProductTypes">
                            Выберите тип продукции:</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlProductTypes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductTypes_SelectedIndexChanged"
                                ClientIDMode="Static" CssClass="span5" />
                        </div>
                    </div>
                    <h3>
                        Список доступных размеров данного типа:</h3>
                    <fieldset>
                        <div class="control-group">
                            <asp:ListBox ID="lbSizes" runat="server" CssClass="span5" Rows="15" DataValueField="SizeID"
                                DataTextField="FullSizeName" AutoPostBack="true" OnSelectedIndexChanged="lbSizes_SelectedIndexChanged" />
                        </div>
                    </fieldset>
                    <h3>
                        Добавить размер:</h3>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" for="txtRusSize">
                                Русский:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtRusSize" runat="server" ClientIDMode="Static" CssClass="span3" />
                                <asp:RequiredFieldValidator ID="valRusSize" runat="server" ControlToValidate="txtRusSize"
                                    ErrorMessage="Требуется ввести русское имя:" Display="Dynamic" ValidationGroup="EditSize">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtEngSize">
                                Английский:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtEngSize" runat="server" ClientIDMode="Static" CssClass="span3" />
                                <asp:RequiredFieldValidator ID="valEngSize" runat="server" ControlToValidate="txtEngSize"
                                    ErrorMessage="Требуется ввести английское имя:" Display="Dynamic" ValidationGroup="EditSize">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="ddlProductTypeEdit">
                                Тип продукции:</label>
                            <div class="controls">
                                <asp:DropDownList ID="ddlProductTypeEdit" runat="server" ClientIDMode="Static" CssClass="span3" />
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnAddSize" runat="server" Text="Добавить" ValidationGroup="EditSize"
                                CssClass="btn btn-primary" OnClick="btnAddSize_Click" />
                            <asp:Button ID="btnDeleteSize" runat="server" Text="Удалить" Visible="false" OnClientClick="return confirm('Вы действительно хотите удалить этот размер?');"
                                CssClass="btn btn-danger" OnClick="btnDeleteSize_Click" />
                            <asp:Button ID="btnCancelSize" runat="server" Text="Отмена" CssClass="btn" OnClick="btnCancelSize_Click" />
                        </div>
                        <asp:ValidationSummary ID="valSumSize" runat="server" ShowMessageBox="false" ShowSummary="true"
                            ValidationGroup="EditSize" CssClass="alert alert-error span5" />
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="tab-pane" id="color_tab">
            <asp:UpdatePanel ID="uppnlColors" runat="server">
                <ContentTemplate>
                    <h3>
                        Список доступных цветов:</h3>
                    <fieldset>
                        <div class="control-group">
                            <asp:ListBox ID="lbColors" runat="server" CssClass="span6" Rows="15" DataValueField="ColorID"
                                DataTextField="FullColorName" AutoPostBack="True" OnSelectedIndexChanged="lbColors_SelectedIndexChanged" />
                        </div>
                    </fieldset>
                    <h3>
                        Добавить цвет:</h3>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" for="txtRusColor">
                                Русский:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtRusColor" runat="server" CssClass="span5" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="valRusColor" runat="server" ControlToValidate="txtRusColor"
                                    ErrorMessage="Требуется ввести русское имя:" Display="Dynamic" ValidationGroup="EditColor">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtRusColor" cssclass="span5" clientidmode="Static">
                                Английский:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtEngColor" runat="server" />
                                <asp:RequiredFieldValidator ID="valEngColor" runat="server" ControlToValidate="txtEngColor"
                                    ErrorMessage="Требуется ввести английское имя:" Display="Dynamic" ValidationGroup="EditColor">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <div class="controls">
                                <label class="checkbox">
                                    <asp:CheckBox ID="cbxIsMain" runat="server" Text="Основной" />
                                </label>
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnAddColor" runat="server" Text="Добавить" OnClick="btnAddColor_Click"
                                ValidationGroup="EditColor" CssClass="btn btn-primary" />
                            <asp:Button ID="btnDeleteColor" runat="server" Text="Удалить" Visible="false" OnClick="btnDeleteColor_Click"
                                OnClientClick="return confirm('Вы действительно хотите удалить этот цвет?');"
                                CssClass="btn btn-danger" />
                            <asp:Button ID="btnCancelColor" runat="server" Text="Отмена" OnClick="btnCancelColor_Click"
                                CssClass="btn" />
                        </div>
                        <asp:ValidationSummary ID="valSumColor" runat="server" ShowMessageBox="false" ShowSummary="true"
                            ValidationGroup="EditColor" CssClass="alert alert-error span5" />
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="tab-pane" id="brands_tab">
            <asp:UpdatePanel ID="uppnlBrands" runat="server">
                <ContentTemplate>
                    <h3>
                        Список брендов</h3>
                    <fieldset>
                        <div class="control-group">
                            <asp:ListBox runat="server" ID="lbBrands" Width="300" Rows="5" DataValueField="BrandId"
                                DataTextField="BrandName" AutoPostBack="True" OnSelectedIndexChanged="lbBrands_SelectedIndexChanged" />
                        </div>
                    </fieldset>
                    <h3>Добавить бренд</h3>
                    <fieldset>
                        <div class="control-group">
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtBrandName" CssClass="span5"/>
                                <asp:RequiredFieldValidator runat="server" ID="valBrandName" ControlToValidate="txtBrandName"
                                    ErrorMessage="Введите имя бренда" Display="Dynamic" ValidationGroup="EditBrand">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button runat="server" ID="btnAddBrand" ValidationGroup="EditBrand" Text="Добавить"
                                CssClass="btn btn-primary" OnClick="btnAddBrand_Click" />
                            <asp:Button ID="btnCancelBrand" runat="server" Text="Отмена" CssClass="btn"
                                OnClick="btnCancelBrand_Click" />
                        </div>
                        <asp:ValidationSummary runat="server" ID="valSumBrand" ShowMessageBox="False" ShowSummary="True"
                            ValidationGroup="EditBrand" CssClass="alert alert-error span5"/>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="upnlEdit" runat="server">
        <ContentTemplate>
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
</asp:Content>
