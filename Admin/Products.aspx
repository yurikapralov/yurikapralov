<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="Admin_Products" %>

<%@ MasterType VirtualPath="~/Admin/Admin2.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        <asp:Label ID="lblsubHeader" runat="server" /></h3>
    <ul id="prodtab" class="nav nav-tabs">
        <li class="active"><a href="#prodcat" data-toggle="tab">Выбор по категории</a></li>
        <li><a href="#prodsearch" data-toggle="tab">Поиск</a></li>
    </ul>
    <div class="tab-content">
        <div class="tab-pane active" id="prodcat">
            <div class="control-group">
                <asp:DropDownList ID="ddlCategories" runat="server" DataValueField="CatID" DataTextField="FullRusName"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged"
                    ClientIDMode="Static" CssClass="span8">
                </asp:DropDownList>
            </div>
        </div>
        <div class="tab-pane" id="prodsearch">
            <div class="control-group">
                <label class="control-label" for="txtSearch">
                    Поиск по названию:</label>
                <div class="controls">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="span5" ClientIDMode="Static" />
                    <asp:Button ID="btnSearch" runat="server" Text="Найти" OnClick="btnSearch_Click"
                        CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <div class="btn-group pull-right">
        <div class="control-group">
            <label class="control-label" for="optionsCheckboxList">
                Опции показа:</label>
            <div class="controls">
                <label class="checkbox">
                    <asp:CheckBox ID="cbxPreview" runat="server" AutoPostBack="true" OnCheckedChanged="cbxPreview_CheckedChanged" />Показывать
                    Preview
                </label>
                <label class="checkbox">
                    <asp:CheckBox ID="cbxActive" runat="server" AutoPostBack="true" OnCheckedChanged="cbxActive_CheckedChanged" />Только
                    Активные
                </label>
                <label class="checkbox">
                    <asp:CheckBox ID="cbxVisible" runat="server" Checked="true" />Показывать список
                    при редактировании
                </label>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlProductList" runat="server">
        <div class="row">
            <div class="span15">
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <a class="accordion-toggle" href="#" data-toggle="collapse" onclick="$('#collapseProdList').collapse('toggle')">
                            Список </a>
                    </div>
                    <div id="collapseProdList" class="accordion-body in collapse" style="height: auto;">
                        <asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdID" OnPagePropertiesChanged="lvProducts_PagePropertiesChanged"
                            OnItemDataBound="lvProducts_ItemDataBound" OnItemEditing="lvProducts_ItemEditing"
                            OnItemCanceling="lvProducts_ItemCanceling" OnItemUpdating="lvProducts_ItemUpdating"
                            OnSelectedIndexChanging="lvProducts_SelectedIndexChanging" OnItemDeleting="lvProducts_ItemDeleting">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered table-condensed" border="0" cellpadding="0"
                                    cellspacing="0">
                                    <tr class="MainAdminTableHeader">
                                        <th class="center">
                                        </th>
                                        <th class="center">
                                            <asp:LinkButton ID="lbtnNameSorted" runat="server" Text="Имя для сортировки" OnClick="lbtnNameSorted_Click" />
                                        </th>
                                        <th class="center">
                                            Русское имя
                                        </th>
                                        <th class="center">
                                            <asp:LinkButton ID="lbtnPriceSorted" runat="server" Text="Цена" OnClick="lbtnPriceSorted_Click" />
                                        </th>
                                        <th class="center">
                                            Расп. цена
                                        </th>
                                        <th class="center">
                                            <asp:LinkButton ID="lbtnCreateSorted" runat="server" Text="Дата создания" OnClick="lbtnCreateSorted_Click" />
                                        </th>
                                        <th class="center">
                                            <asp:LinkButton ID="lbtnUpdateSorted" runat="server" Text="Дата изменения" OnClick="lbtnUpdateSorted_Click" />
                                        </th>
                                        <th class="center">
                                            Категория
                                        </th>
                                        <th class="center">
                                            Активна
                                        </th>
                                        <th class="center">
                                            Акция
                                        </th>
                                        <th class="center">
                                            &nbsp;
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" class="MainAdminTableItem" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class='<%#(bool)Eval("Available") ? "" : "disabled"%>'>
                                    <td>
                                        <asp:Image ID="imgPreview" runat="server" Height="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Thumb\{0}",Eval("ThumbURL")) %>' />
                                    </td>
                                    <td>
                                        <%#Eval("SortedName") %>
                                    </td>
                                    <td>
                                        <%#Eval("ProductNameRus") %>
                                    </td>
                                    <td>
                                        <%#Eval("OrigPrice","{0:c}") %>
                                    </td>
                                    <td>
                                        <font color='red'><s>
                                            <%#Eval("SalePrice","{0:c}") %></s></font>
                                    </td>
                                    <td>
                                        <%#Eval("DateCreated") %>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtDateUpdated" runat="server" Text='<%#Eval("DateUpdated") %>' />
                                    </td>
                                    <td style="font-size: x-small; font-style: italic;">
                                        <%#GetUsingCathegory((int)Eval("ProdID")) %>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("Available") %>' Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbOnSale" runat="server" Checked='<%#Eval("OnSale") %>' Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="Редактировать" CommandName="Edit"
                                            CommandArgument='<%#Eval("ProdID") %>' ForeColor="Green" />
                                        <asp:LinkButton ID="lbtnSelect" runat="server" Text="Выбрать" CommandName="Select"
                                            CommandArgument='<%#Eval("ProdID") %>' OnClick="lbtnSelect_Click" />
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Удалить" CommandName="Delete"
                                            CommandArgument='<%#Eval("ProdID") %>' OnClientClick="return confirm('Вы действительно хотите удалить этот продукт?');"
                                            ForeColor="Red" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <tr>
                                    <td class="row_selected">
                                        <asp:Image ID="imgPreview" runat="server" Height="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Thumb\{0}",Eval("ThumbURL")) %>' />
                                    </td>
                                    <td class="row_selected">
                                        <%#Eval("SortedName") %>
                                    </td>
                                    <td class="row_selected">
                                        <%#Eval("ProductNameRus") %>
                                    </td>
                                    <td class="row_selected">
                                        <%#Eval("OrigPrice","{0:c}") %>
                                    </td>
                                    <td class="row_selected">
                                        <font color='red'><s>
                                            <%#Eval("SalePrice","{0:c}") %></s></font>
                                    </td>
                                    <td class="row_selected">
                                        <%#Eval("DateCreated") %>
                                    </td>
                                    <td class="row_selected">
                                        <%#Eval("DateUpdated") %>
                                    </td>
                                    <td class="row_selected" style="font-size: x-small; font-style: italic;">
                                        <%#GetUsingCathegory((int)Eval("ProdID")) %>
                                    </td>
                                    <td class="row_selected">
                                        <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("Available") %>' Enabled="false" />
                                    </td>
                                    <td class="row_selected">
                                        <asp:CheckBox ID="cbOnSale" runat="server" Checked='<%#Eval("OnSale") %>' Enabled="false" />
                                    </td>
                                    <td class="row_selected">
                                        &nbsp;
                                    </td>
                                </tr>
                            </SelectedItemTemplate>
                            <EditItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Image ID="imgPreview" runat="server" Height="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Thumb\{0}",Eval("ThumbURL")) %>' />
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtSortedName" runat="server" Text='<%#Bind("SortedName") %>' Font-Size="X-Small"
                                            CssClass="span1" />
                                        <asp:RequiredFieldValidator ID="valReqSortName" runat="server" Display="Dynamic"
                                            ControlToValidate="txtSortedName" ValidationGroup="qUpdate">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtProductNameRus" runat="server" Text='<%#Bind("ProductNameRus") %>'
                                            Font-Size="X-Small" CssClass="span2" />
                                        <asp:RequiredFieldValidator ID="valReqRusName" runat="server" Display="Dynamic" ControlToValidate="txtProductNameRus"
                                            ValidationGroup="qUpdate">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtOrigPrice" runat="server" Text='<%#Bind("OrigPrice","{0:F2}") %>'
                                            Font-Size="X-Small" CssClass="span1" />
                                        <asp:RequiredFieldValidator ID="valReqOrigPrice" runat="server" Display="Dynamic"
                                            ControlToValidate="txtOrigPrice" ValidationGroup="qUpdate">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cmpOrigPrice" runat="server" ControlToValidate="txtOrigPrice"
                                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="qUpdate" ValueToCompare="0">**</asp:CompareValidator>
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="txtSalePrice" runat="server" Text='<%#Bind("SalePrice","{0:F2}") %>'
                                            Font-Size="X-Small" CssClass="span1" />
                                        <asp:CompareValidator ID="valReqSalePrice" runat="server" ControlToValidate="txtSalePrice"
                                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="qUpdate" ValueToCompare="0">**</asp:CompareValidator>
                                    </td>
                                    <td>
                                        <%#Eval("DateCreated") %>
                                    </td>
                                    <td>
                                        <%#Eval("DateUpdated") %>
                                    </td>
                                    <td style="font-size: x-small; font-style: italic;">
                                        <%#GetUsingCathegory((int)Eval("ProdID")) %>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Bind("Available") %>' />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbOnSale" runat="server" Checked='<%#Bind("OnSale") %>' />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Text="Обновить" CommandName="Update"
                                            CommandArgument='<%#Eval("ProdID") %>' ValidationGroup="qUpdate" ForeColor="Green" />
                                        <asp:LinkButton ID="lbtnCancel" runat="server" Text="Отмена" CommandName="Cancel"
                                            CommandArgument='<%#Eval("ProdID") %>' />
                                    </td>
                                </tr>
                            </EditItemTemplate>
                            <EmptyDataTemplate>
                                <p>
                                    В данной категории продукции нет.</p>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center;" class="well-small well">
                                    <asp:Panel ID="pnlPager" runat="server" HorizontalAlign="Center" Visible="false">
                                        <asp:DataPager ID="pagerBottom" runat="server" PageSize="20" PagedControlID="lvProducts">
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
                                    <asp:Label ID="lblProductCount" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlEdit" runat="server">
        <div class="row">
            <div class="span8">
                <asp:Panel ID="pnlEditProduct" runat="server">
                    <h3>
                        <asp:Label ID="lblEditProductHeader" runat="server" Text="Добавление нового продукта" /></h3>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" for="lbCategories">
                                Будет на страницах:</label>
                            <div class="controls">
                                <asp:ListBox ID="lbCategories" Rows="10" runat="server" DataTextField="FullRusName"
                                    DataValueField="CatID" SelectionMode="Multiple" CssClass="span6" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="valReqCategories" runat="server" ControlToValidate="lbCategories"
                                    Display="Dynamic" ValidationGroup="EditProduct" ErrorMessage="Вы дожны выбрать хотя бы одну категорию">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="ddlProductType">
                                Тип продукции:</label>
                            <div class="controls">
                                <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"
                                    CssClass="span4" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtSorted">
                                Имя для сортировки*:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtSorted" runat="server" ClientIDMode="Static" CssClass="span4" />
                                <asp:RequiredFieldValidator ID="valReqSortName" runat="server" ControlToValidate="txtSorted"
                                    Display="Dynamic" ValidationGroup="EditProduct" ErrorMessage="Введите имя для сортировки">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtEngName">
                                Английское имя*:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtEngName" runat="server" ClientIDMode="Static" CssClass="span4" />
                                <asp:Button ID="btnSortedCopy" runat="server" Text="Копировать Sorted" CssClass="btn"
                                    OnClientClick="return CopyEnglishName();" />
                                <asp:RequiredFieldValidator ID="valReqEngName" runat="server" ControlToValidate="txtEngName"
                                    Display="Dynamic" ValidationGroup="EditProduct" ErrorMessage="Введите английское имя">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="rusnametemplate">
                                Шаблон имени:</label>
                            <div class="controls">
                                <select id="rusnametemplate" onchange="SetRusTemplate();" class="span4">
                                    <option>Пустой</option>
                                    <option>Босоножки модель:</option>
                                    <option>Сапоги модель:</option>
                                    <option>Туфли модель:</option>
                                </select>
                                <input type="hidden" id="previousrusval" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtRusName">
                                Русское имя*:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtRusName" runat="server" CssClass="span4" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="valReqRusName" runat="server" ControlToValidate="txtRusName"
                                    Display="Dynamic" ValidationGroup="EditProduct" ErrorMessage="Введите русское имя">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <asp:Panel ID="pnlPlatform" runat="server">
                            <div class="control-group">
                                <label class="control-label" for="ddlPlatform">
                                    Платформа:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlPlatform" runat="server" DataTextField="PlatformNameRus"
                                        DataValueField="PlatformID" CssClass="span5" ClientIDMode="Static" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="control-group">
                            <label class="control-label" for="ddlBrand">
                                Бренд:</label>
                            <div class="controls">
                                <asp:DropDownList ID="ddlBrand" runat="server" DataTextField="BrandName" DataValueField="BrandId"
                                    CssClass="span4" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="accordion-group">
                            <div class="accordion-heading">
                                <a class="accordion-toggle" href="#" data-toggle="collapse" onclick="$('#collapseProdDesc').collapse('toggle')">
                                    Описания </a>
                            </div>
                            <div id="collapseProdDesc" class="accordion-body in collapse" style="height: auto;">
                                <div class="control-group">
                                    <label class="control-label" for="txtEngDescr">
                                        Английское описание:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtEngDescr" runat="server" TextMode="MultiLine" Rows="2" CssClass="span4"
                                            ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for="txtRusDescr">
                                        Русское описание:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtRusDescr" runat="server" TextMode="MultiLine" Rows="2" CssClass="span4"
                                            ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Preview*:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtThumb" runat="server" Enabled="false" ReadOnly="true" CssClass="span4" />
                                <br />
                                <asp:FileUpload ID="fuThumb" runat="server" CssClass="btn" />
                                <asp:Button ID="btnCancelUpThumb" runat="server" CssClass="btn" OnClick="btnCancelUpThumb_Click"
                                    Text="Отмена" />
                                <asp:Label ID="lblThumUploadErr" runat="server" ForeColor="Red" Visible="false" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtOrigPrice">
                                Цена*:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtOrigPrice" runat="server" ClientIDMode="Static" CssClass="span1" />
                                <asp:RequiredFieldValidator ID="valReqOrigPrice" runat="server" ControlToValidate="txtOrigPrice"
                                    Display="Dynamic" ValidationGroup="EditProduct" ErrorMessage="Введите цену">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cmpOrigPrice" runat="server" ControlToValidate="txtOrigPrice"
                                    Operator="GreaterThanEqual" Type="Double" ValidationGroup="EditProduct" ValueToCompare="0"
                                    ErrorMessage="Не правильно указана цена.">*</asp:CompareValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtSalePrice">
                                Дораспродажная цена:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtSalePrice" runat="server" ClientIDMode="Static" CssClass="span1" />
                                <asp:CompareValidator ID="cmpSalePrice" runat="server" ControlToValidate="txtSalePrice"
                                    Operator="GreaterThanEqual" Type="Double" ValidationGroup="EditProduct" ValueToCompare="0"
                                    ErrorMessage="Не правильно указана цена.">*</asp:CompareValidator>
                            </div>
                        </div>
                        <div class="control-group">
                            <div class="controls">
                                <label class="checkbox">
                                    <asp:CheckBox ID="cbProdAvailable" runat="server" Checked="true" />&nbsp;В наличии
                                </label>
                                <label class="checkbox">
                                    <asp:CheckBox ID="cbOnSale" runat="server" Checked="false" />&nbsp;Участвует в акции
                                </label>
                                <label class="checkbox">
                                    <asp:CheckBox ID="cbFromUsa" runat="server" Checked="false" />&nbsp;Из США
                                </label>
                                <label class="checkbox">
                                    <asp:CheckBox ID="cbVip" runat="server" Checked="false" />&nbsp;Vip
                                </label>
                                 <label class="checkbox">
                                    <asp:CheckBox ID="cbOnSale2" runat="server" Checked="false" />&nbsp;Распродажа
                                </label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtAlt">
                                Alt:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtAlt" runat="server" CssClass="span4" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnMore" runat="server" CssClass="btn btn-primary" Text="Далее" ValidationGroup="EditProduct"
                                OnClick="btnMore_Click" />
                            <asp:Button ID="btnExtraInfo" runat="server" CssClass="btn btn-primary" Text="Цвета и размеры"
                                Visible="false" OnClick="btnExtraInfo_Click" />
                            <asp:Button ID="btnUpdateProduct" runat="server" CssClass="btn-success btn" OnClick="btnUpdateProduct_Click"
                                Text="Обновить" Visible="false" ValidationGroup="EditProduct" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="отмена" OnClick="btnCancel_Click" />
                        </div>
                        <asp:ValidationSummary ID="valSum" ValidationGroup="EditProduct" runat="server" ShowMessageBox="false"
                            ShowSummary="true" CssClass="alert alert-error span3" />
                    </fieldset>
                </asp:Panel>
            </div>
            <div class="span7">
                <div style="padding-top: 50px">
                </div>
                <asp:Panel ID="pnlProdColorEdit" runat="server" Visible="false">
                    <div class="btn-group pull-right">
                        <asp:Button ID="btnXClear" runat="server" CssClass="btn" Text="x" OnClick="btnCancel_Click" />
                    </div>
                    <asp:ListView ID="lvProdColors" runat="server" DataKeyNames="ProdColorID" OnSelectedIndexChanging="lvProdColors_SelectedIndexChanging"
                        OnItemDeleting="lvProdColors_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-condensed span6">
                                <tr>
                                    <th class="center">
                                        Цвет
                                    </th>
                                    <th class="center">
                                        Файл картинки
                                    </th>
                                    <th>
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" class="MainAdminTableItem" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#(bool)Eval("IsAvilable") ? "" : "<font color='red'><b><s>"%>
                                    <%#Eval("ColorNameRus") %>
                                    <%#(bool)Eval("IsAvilable")? "":"</s></b></font>" %>
                                </td>
                                <td>
                                    <%#Eval("ImageURL") %>
                                </td>
                                <td style="width: 50px;">
                                    <asp:Image ID="imgURL" runat="server" Width="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Large\{0}",Eval("ImageURL")) %>' />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbtnSelectColor" runat="server" Text="Выбрать" CommandName="Select"
                                        CommandArgument='<%#Eval("ProdColorID") %>' OnClick="lbtnSelectColor_Click" />
                                    <asp:LinkButton ID="lbtnDeleteColor" runat="server" Text="Удалить" CommandName="Delete"
                                        ForeColor="Red" CommandArgument='<%#Eval("ProdColorID") %>' OnClientClick="return confirm('Вы действительно хотите удалить этот цвет?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <td class="row_selected">
                                    <%#Eval("ColorNameRus") %>
                                </td>
                                <td class="row_selected">
                                    <%#Eval("ImageURL") %>
                                </td>
                                <td class="row_selected">
                                    <asp:Image ID="imgURL" runat="server" Width="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Large\{0}",Eval("ImageURL")) %>' />
                                </td>
                                <td class="row_selected">
                                    &nbsp;
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <h3>
                        <asp:Label ID="lblEditProductColorHeader" runat="server" Text="Добавление нового цвета" /></h3>
                    <p>
                    </p>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" for="ddlColors">
                                Цвет:</label>
                            <div class="controls">
                                <asp:DropDownList ID="ddlColors" runat="server" DataTextField="ColorNameRus" DataValueField="ColorId"
                                    CssClass="span4" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="accordion-group">
                            <div class="accordion-heading">
                                <a class="accordion-toggle" href="#" data-toggle="collapse" onclick="$('#collapsColor').collapse('toggle')">
                                    Опции цвета </a>
                            </div>
                            <div id="collapsColor" class="accordion-body in collapse" style="height: auto;">
                                <div class="control-group">
                                    <label class="control-label" for="txtColorRusAdd">
                                        Рус:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtColorRusAdd" runat="server" CssClass="span3" ClientIDMode="Static" />
                                        <asp:RequiredFieldValidator ID="valColorRusAdd" runat="server" ControlToValidate="txtColorRusAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddColor" ErrorMessage="Необходимо русское название цвета" />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for="txtColorEngAdd">
                                        Анг:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtColorEngAdd" runat="server" CssClass="span3" ClientIDMode="Static" />
                                        <asp:RequiredFieldValidator ID="valColorEngAdd" runat="server" ControlToValidate="txtColorEngAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddColor" ErrorMessage="Необходимо английское название цвета" />
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <asp:Button ID="btnColorAdd" runat="server" Text="Добавить цвет" CssClass="btn btn-primary"
                                        ValidationGroup="AddColor" OnClick="btnColorAdd_Click" />
                                </div>
                                <asp:ValidationSummary ID="valsumColor" ValidationGroup="AddColor" runat="server"
                                    ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-error span3" />
                                <div class="control-group">
                                    <div class="controls">
                                        <label class="checkbox">
                                            <asp:CheckBox ID="cbxIsMainColors" runat="server" Text="Показывать только основные цвета"
                                                OnCheckedChanged="cbxIsMainColors_CheckedChanged" AutoPostBack="true" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Изображение*:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtImageURL" runat="server" Enabled="false" ReadOnly="true" CssClass="span4" />
                                <br />
                                <asp:FileUpload ID="fuLarge" runat="server" CssClass="btn" />
                                <asp:Button ID="btnCancelUpLare" runat="server" CssClass="btn" Text="Отмена" />
                                <asp:Label ID="lblfuLargeErr" runat="server" ForeColor="Red" Visible="false" />
                            </div>
                        </div>
                        <asp:Panel ID="pnlSizeInfo" runat="server">
                            <div class="control-group">
                                <div class="controls">
                                    Информация о размерном ряде:
                                    <asp:Label ID="lblSizeInfo" runat="server" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="control-group">
                            <label class="control-label" for="lbSizes">
                                Размерный ряд:</label>
                            <div class="controls">
                                <asp:ListBox ID="lbSizes" runat="server" Rows="10" SelectionMode="Multiple" DataTextField="SizeNameRus"
                                    DataValueField="SizeID" CssClass="span2" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="valReqSize" runat="server" ValidationGroup="EditProdColor"
                                    Display="Dynamic" ControlToValidate="lbSizes" ErrorMessage="Вы должны выбрать хотя бы один размер">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="accordion-group">
                            <div class="accordion-heading">
                                <a class="accordion-toggle" href="#" data-toggle="collapse" onclick="$('#collapsSize').collapse('toggle')">
                                    Опции размера </a>
                            </div>
                            <div id="collapsSize" class="accordion-body in collapse" style="height: auto;">
                                <div class="control-group">
                                    <label class="control-label" for="txtSizeRusAdd">
                                        Рус:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtSizeRusAdd" runat="server" CssClass="span3" ClientIDMode="Static" />
                                        <asp:RequiredFieldValidator ID="valAddRusSize" runat="server" ControlToValidate="txtSizeRusAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddSize" ErrorMessage="Необходимо русское название размера" />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for="txtSizeEngAdd">
                                        Анг:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtSizeEngAdd" runat="server" CssClass="span3" ClientIDMode="Static" />
                                        <asp:RequiredFieldValidator ID="valAddEngSize" runat="server" ControlToValidate="txtSizeEngAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddSize" ErrorMessage="Необходимо английское название размера" />
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <asp:Button ID="btnSizeAdd" runat="server" Text="Добавить размер" CssClass="btn btn-primary"
                                        ValidationGroup="AddSize" OnClick="btnSizeAdd_Click" />
                                </div>
                                <asp:ValidationSummary ID="valsumSize" ValidationGroup="AddSize" runat="server" ShowMessageBox="false"
                                    ShowSummary="true" CssClass="alert alert-error span3" />
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnAddProdColor" runat="server" Text="Добавить" ValidationGroup="EditProdColor"
                                CssClass="btn btn-primary" OnClick="btnAddProdColor_Click" />
                            <asp:Button ID="btnCancelCategory" runat="server" Text="Отмена" CssClass="btn" OnClick="btnCancelCategory_Click" />
                        </div>
                        <asp:ValidationSummary ID="valsumProdSize" ValidationGroup="EditProdColor" runat="server"
                            ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-error span3" />
                    </fieldset>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
