<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Products_old.aspx.cs" Inherits="Admin_Products_old" Theme="Admin" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content_total">
        <h2>
            <asp:Label ID="lblsubHeader" runat="server" /></h2>
        <br /><br />
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="60%">
            <asp:TabPanel runat="server" HeaderText="Выбор по категории" ID="TabPanel1">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlCategories" runat="server" DataValueField="CatID" DataTextField="FullRusName"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="Поиск" ID="TabPanel2"> 
                <ContentTemplate>
                    Поиск по названию:<asp:TextBox ID="txtSearch" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" Text="Найти" OnClick="btnSearch_Click" />
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
         <p></p>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="33%">
                    <asp:CheckBox ID="cbxPreview" runat="server" Text="Показывать Preview" AutoPostBack="true"
                        OnCheckedChanged="cbxPreview_CheckedChanged" />
                </td>
                <td width="33%">
                    <asp:CheckBox ID="cbxActive" runat="server" Text="Только Активные" AutoPostBack="true"
                        OnCheckedChanged="cbxActive_CheckedChanged" />
                </td>
                <td>
                    <asp:CheckBox ID="cbxVisible" runat="server" Text="Показывать список при редактировании"
                        Checked="true" />
                </td>
            </tr>
        </table>
        <p></p>
        <asp:Panel ID="pnlProductList" runat="server">
            <asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProdID" OnPagePropertiesChanged="lvProducts_PagePropertiesChanged"
                OnItemDataBound="lvProducts_ItemDataBound" OnItemEditing="lvProducts_ItemEditing"
                OnItemCanceling="lvProducts_ItemCanceling" OnItemUpdating="lvProducts_ItemUpdating"
                OnSelectedIndexChanging="lvProducts_SelectedIndexChanging" OnItemDeleting="lvProducts_ItemDeleting">
                <LayoutTemplate>
                    <table class="MainAdminTable" border="1" cellpadding="0" cellspacing="0">
                        <tr class="MainAdminTableHeader">
                            <td>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnNameSorted" runat="server" Text="Имя для сортировки" OnClick="lbtnNameSorted_Click" />
                            </td>
                            <td>
                                Русское имя
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnPriceSorted" runat="server" Text="Цена" OnClick="lbtnPriceSorted_Click" />
                            </td>
                            <td>Расп. цена</td>
                            <td>
                                <asp:LinkButton ID="lbtnCreateSorted" runat="server" Text="Дата создания" OnClick="lbtnCreateSorted_Click" />
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnUpdateSorted" runat="server" Text="Дата изменения" OnClick="lbtnUpdateSorted_Click" />
                            </td>
                            <td>Категория</td>
                            <td>
                                Активна
                            </td>
                            <td>
                                Акция
                            </td>
                            <td>
                                &nbsp;
                            </td>
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
                           <font color='red'><s> <%#Eval("SalePrice","{0:c}") %></s></font>
                        </td>
                        <td>
                            <%#Eval("DateCreated") %>
                        </td>
                        <td>
                            <asp:Label ID="txtDateUpdated" runat="server" Text='<%#Eval("DateUpdated") %>' />
                        </td>
                        <td style="font-size:x-small; font-style:italic;"><%#GetUsingCathegory((int)Eval("ProdID")) %></td>
                        <td>
                            <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("Available") %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbOnSale" runat="server" Checked='<%#Eval("OnSale") %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnEdit" runat="server" Text="Редактировать" CommandName="Edit"
                                CommandArgument='<%#Eval("ProdID") %>' />
                            <asp:LinkButton ID="lbtnSelect" runat="server" Text="Выбрать" CommandName="Select"
                                CommandArgument='<%#Eval("ProdID") %>' OnClick="lbtnSelect_Click" />
                            <asp:LinkButton ID="lbtnDelete" runat="server" Text="Удалить" CommandName="Delete"
                                CommandArgument='<%#Eval("ProdID") %>' OnClientClick="return confirm('Вы действительно хотите удалить этот продукт?');" />
                        </td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr style="font-weight: bold; font-style: italic; background-color: Black; color: White;">
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
                           <font color='red'><s> <%#Eval("SalePrice","{0:c}") %></s></font>
                        </td>
                        <td>
                            <%#Eval("DateCreated") %>
                        </td>
                        <td>
                            <%#Eval("DateUpdated") %>
                        </td>
                        <td style="font-size:x-small; font-style:italic;"><%#GetUsingCathegory((int)Eval("ProdID")) %></td>
                        <td>
                            <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("Available") %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbOnSale" runat="server" Checked='<%#Eval("OnSale") %>' Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </SelectedItemTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <asp:Image ID="imgPreview" runat="server" Height="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Thumb\{0}",Eval("ThumbURL")) %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortedName" runat="server" Text='<%#Bind("SortedName") %>' Font-Size="X-Small" />
                            <asp:RequiredFieldValidator ID="valReqSortName" runat="server" Display="Dynamic"
                                ControlToValidate="txtSortedName" ValidationGroup="qUpdate">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductNameRus" runat="server" Text='<%#Bind("ProductNameRus") %>'
                                Font-Size="X-Small" Width="100%" />
                            <asp:RequiredFieldValidator ID="valReqRusName" runat="server" Display="Dynamic" ControlToValidate="txtProductNameRus"
                                ValidationGroup="qUpdate">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrigPrice" runat="server" Text='<%#Bind("OrigPrice","{0:F2}") %>'
                                Font-Size="X-Small" />
                            <asp:RequiredFieldValidator ID="valReqOrigPrice" runat="server" Display="Dynamic"
                                ControlToValidate="txtOrigPrice" ValidationGroup="qUpdate">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmpOrigPrice" runat="server" ControlToValidate="txtOrigPrice"
                                Operator="GreaterThanEqual" Type="Double" ValidationGroup="qUpdate" ValueToCompare="0">**</asp:CompareValidator>
                        </td>
                         <td>
                            <asp:TextBox ID="txtSalePrice" runat="server" Text='<%#Bind("SalePrice","{0:F2}") %>'
                                Font-Size="X-Small" />
                            <asp:CompareValidator ID="valReqSalePrice" runat="server" ControlToValidate="txtSalePrice"
                                Operator="GreaterThanEqual" Type="Double" ValidationGroup="qUpdate" ValueToCompare="0">**</asp:CompareValidator>
                        </td>
                        <td>
                            <%#Eval("DateCreated") %>
                        </td>
                        <td>
                            <%#Eval("DateUpdated") %>
                        </td>
                         <td style="font-size:x-small; font-style:italic;"><%#GetUsingCathegory((int)Eval("ProdID")) %></td>
                        <td>
                            <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Bind("Available") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbOnSale" runat="server" Checked='<%#Bind("OnSale") %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnUpdate" runat="server" Text="Обновить" CommandName="Update"
                                CommandArgument='<%#Eval("ProdID") %>' ValidationGroup="qUpdate" />
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
            <table>
                <tr>
                    <td width="200">
                        <asp:Label ID="lblProductCount" runat="server" />
                    </td>
                    <td align="center">
                        <asp:Panel ID="pnlPager" runat="server" HorizontalAlign="Center" Visible="false">
                            Перейти:
                            <asp:DataPager ID="pagerBottom" runat="server" PageSize="20" PagedControlID="lvProducts">
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
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEdit" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td width="50%" valign="top">
                        <asp:Panel ID="pnlEditProduct" runat="server">
                            <h2>
                                <asp:Label ID="lblEditProductHeader" runat="server" Text="Добавление нового продукта" /></h2>
                            <br />
                            <table class="MainAdminTable">
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Будет на страницах:
                                    </td>
                                    <td class="MainAdminTableItem">
                                        <asp:ListBox ID="lbCategories" Rows="10" runat="server" DataTextField="FullRusName"
                                            DataValueField="CatID" SelectionMode="Multiple" />
                                        <asp:RequiredFieldValidator ID="valReqCategories" runat="server" ControlToValidate="lbCategories"
                                            Display="Dynamic" ValidationGroup="EditProduct">Вы дожны выбрать хотябы одну категорию</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Тип продукции:
                                    </td>
                                    <td class="MainAdminTableItem" align="left">
                                        <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Имя для сортировки*:
                                    </td>
                                    <td class="MainAdminTableItem" align="left">
                                        <asp:TextBox ID="txtSorted" runat="server" Width="200px" ClientIDMode="Static" />
                                    </td>
                                    <asp:RequiredFieldValidator ID="valReqSortName" runat="server" ControlToValidate="txtSorted"
                                        Display="Dynamic" ValidationGroup="EditProduct">Введите имя для сортировки</asp:RequiredFieldValidator>
                                </tr>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Английское имя*:
                                    </td>
                                    <td class="MainAdminTableItem" align="left">
                                     <asp:TextBox ID="txtEngName" runat="server" Width="200px"  ClientIDMode="Static"/>
                                                <asp:Button ID="btnSortedCopy" runat="server" Text="Копировать Sorted" CssClass="TableButton"
                                                    OnClientClick="return CopyEnglishName();" /> 
                                   </td>
                                    <asp:RequiredFieldValidator ID="valReqEngName" runat="server" ControlToValidate="txtEngName"
                                        Display="Dynamic" ValidationGroup="EditProduct">Введите английское имя</asp:RequiredFieldValidator>
                                </tr>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Шаблон имени:
                                    </td>
                                    <td  class="MainAdminTableItem" align="left">
                                        <select id="rusnametemplate" onchange="SetRusTemplate();">
                                            <option>Пустой</option>
                                            <option>Босоножки модель:</option>
                                            <option>Сапоги модель:</option>
                                            <option>Туфли модель:</option>
                                        </select>
                                        <input type="hidden" id="previousrusval" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Русское имя*:
                                    </td>
                                    <td class="MainAdminTableItem" align="left">
                                        <asp:TextBox ID="txtRusName" runat="server" Width="300px" ClientIDMode="Static" />
                                    </td>
                                    <asp:RequiredFieldValidator ID="valReqRusName" runat="server" ControlToValidate="txtRusName"
                                        Display="Dynamic" ValidationGroup="EditProduct">Введите русское имя</asp:RequiredFieldValidator>
                                </tr>
                                <asp:Panel ID="pnlPlatform" runat="server">
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Платформа:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:DropDownList ID="ddlPlatform" runat="server" DataTextField="PlatformNameRus"
                                                DataValueField="PlatformID" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                            Бренд:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:DropDownList ID="ddlBrand" runat="server" DataTextField="BrandName"
                                                DataValueField="BrandId" />
                                        </td>
                                </tr>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Английское описание:
                                    </td>
                                    <td class="MainAdminTableItem" align="left">
                                        <asp:TextBox ID="txtEngDescr" runat="server" TextMode="MultiLine" Rows="5" Width="300px" />
                                    </td>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Русское описание:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:TextBox ID="txtRusDescr" runat="server" Rows="5" TextMode="MultiLine" Width="300px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Preview*:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:TextBox ID="txtThumb" runat="server" Enabled="false" ReadOnly="true" Width="300px" />
                                            <br />
                                            <asp:FileUpload ID="fuThumb" runat="server" CssClass="TableButton" />
                                            <asp:Button ID="btnCancelUpThumb" runat="server" CssClass="TableButton" OnClick="btnCancelUpThumb_Click"
                                                Text="Отмена" />
                                            <asp:Label ID="lblThumUploadErr" runat="server" ForeColor="Red" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Цена*:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:TextBox ID="txtOrigPrice" runat="server" />
                                            <asp:RequiredFieldValidator ID="valReqOrigPrice" runat="server" ControlToValidate="txtOrigPrice"
                                                Display="Dynamic" ValidationGroup="EditProduct">Введите цену</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmpOrigPrice" runat="server" ControlToValidate="txtOrigPrice"
                                                Operator="GreaterThanEqual" Type="Double" ValidationGroup="EditProduct" ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Дораспродажная цена:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:TextBox ID="txtSalePrice" runat="server" />
                                            <asp:CompareValidator ID="cmpSalePrice" runat="server" ControlToValidate="txtSalePrice"
                                                Operator="GreaterThanEqual" Type="Double" ValidationGroup="EditProduct" ValueToCompare="0">Не правильно указана цена.</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            В наличии:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:CheckBox ID="cbProdAvailable" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Участвует в акции:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:CheckBox ID="cbOnSale" runat="server" Checked="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Из США:
                                        </td>
                                        <td class="MainAdminTableItem" align="left">
                                            <asp:CheckBox ID="cbFromUsa" runat="server" Checked="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnMore" runat="server" CssClass="TableButton" Text="Далее" OnClick="btnMore_Click" />
                                            <asp:Button ID="btnExtraInfo" runat="server" CssClass="TableButton" Text="Цвета и размеры"
                                                Visible="false" OnClick="btnExtraInfo_Click" />
                                            <asp:Button ID="btnUpdateProduct" runat="server" CssClass="TableButton" OnClick="btnUpdateProduct_Click"
                                                Text="Обновить" Visible="false" ValidationGroup="EditProduct" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="TableButton" Text="отмена" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <asp:Panel ID="pnlProdColorEdit" runat="server" Visible="false">
                        <td valign="top">
                            <asp:ListView ID="lvProdColors" runat="server" DataKeyNames="ProdColorID" OnSelectedIndexChanging="lvProdColors_SelectedIndexChanging"
                                OnItemDeleting="lvProdColors_ItemDeleting">
                                <LayoutTemplate>
                                    <table class="MainAdminTable" border="1" cellpadding="0" cellspacing="0">
                                        <tr class="MainAdminTableHeader">
                                            <td>
                                                Цвет
                                            </td>
                                            <td>
                                                Файл картинки
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
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
                                        <td>
                                            <asp:Image ID="imgURL" runat="server" Width="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Large\{0}",Eval("ImageURL")) %>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtnSelectColor" runat="server" Text="Выбрать" CommandName="Select"
                                                CommandArgument='<%#Eval("ProdColorID") %>' OnClick="lbtnSelectColor_Click" />
                                            <asp:LinkButton ID="lbtnDeleteColor" runat="server" Text="Удалить" CommandName="Delete"
                                                CommandArgument='<%#Eval("ProdColorID") %>' OnClientClick="return confirm('Вы действительно хотите удалить этот цвет?');" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <SelectedItemTemplate>
                                    <tr style="font-weight: bold; font-style: italic; background-color: Black; color: White;">
                                        <td>
                                            <%#Eval("ColorNameRus") %>
                                        </td>
                                        <td>
                                            <%#Eval("ImageURL") %>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgURL" runat="server" Width="50px" ImageUrl='<%# string.Format(@"~\Images\Products\Large\{0}",Eval("ImageURL")) %>' />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                            <h2>
                                <asp:Label ID="lblEditProductColorHeader" runat="server" Text="Добавление нового цвета" /></h2>
                            <p>
                            </p>
                            <table class="MainAdminTable">
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Цвет:
                                    </td>
                                    <td class="MainAdminTableItem">
                                        <asp:DropDownList ID="ddlColors" runat="server" DataTextField="ColorNameRus" DataValueField="ColorId" />
                                        <br />
                                        Рус:
                                        <asp:TextBox ID="txtColorRusAdd" runat="server" />
                                        <asp:RequiredFieldValidator ID="valColorRusAdd" runat="server" ControlToValidate="txtColorRusAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddColor" />
                                        Анг:
                                        <asp:TextBox ID="txtColorEngAdd" runat="server" />
                                        <asp:RequiredFieldValidator ID="valColorEngAdd" runat="server" ControlToValidate="txtColorEngAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddColor" />
                                        <asp:Button ID="btnColorAdd" runat="server" Text="Добавить цвет" CssClass="TableButton"
                                            ValidationGroup="AddColor" OnClick="btnColorAdd_Click" /><br />
                                        <asp:CheckBox ID="cbxIsMainColors" runat="server" Text="Только основные цвета" OnCheckedChanged="cbxIsMainColors_CheckedChanged" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Изображение*:
                                    </td>
                                    <td class="MainAdminTableItem">
                                        <asp:TextBox ID="txtImageURL" runat="server" Enabled="false" ReadOnly="true" />
                                        <br />
                                        <asp:FileUpload ID="fuLarge" runat="server" CssClass="TableButton" />
                                        <asp:Button ID="btnCancelUpLare" runat="server" CssClass="TableButton" Text="Отмена" />
                                        <asp:Label ID="lblfuLargeErr" runat="server" ForeColor="Red" Visible="false" />
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlSizeInfo" runat="server">
                                    <tr>
                                        <td class="MainAdminTableHeader">
                                            Информация о Размерном ряде
                                        </td>
                                        <td class="MainAdminTableItem">
                                            <asp:Label ID="lblSizeInfo" runat="server" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td class="MainAdminTableHeader">
                                        Размерный ряд
                                    </td>
                                    <td class="MainAdminTableItem">
                                        <asp:ListBox ID="lbSizes" runat="server" Rows="10" SelectionMode="Multiple" DataTextField="SizeNameRus"
                                            DataValueField="SizeID" />
                                        <asp:RequiredFieldValidator ID="valReqSize" runat="server" ValidationGroup="EditProdColor"
                                            Display="Dynamic" ControlToValidate="lbSizes">Вы должны выбрать хотя бы один размер</asp:RequiredFieldValidator><br />
                                        Рус:
                                        <asp:TextBox ID="txtSizeRusAdd" runat="server" />
                                        <asp:RequiredFieldValidator ID="valAddRusSize" runat="server" ControlToValidate="txtSizeRusAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddSize" />
                                        Анг:
                                        <asp:TextBox ID="txtSizeEngAdd" runat="server" />
                                        <asp:RequiredFieldValidator ID="valAddEngSize" runat="server" ControlToValidate="txtSizeEngAdd"
                                            Display="Dynamic" Text="*" ValidationGroup="AddSize" />
                                        <asp:Button ID="btnSizeAdd" runat="server" Text="Добавить размер" CssClass="TableButton"
                                            ValidationGroup="AddSize" OnClick="btnSizeAdd_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnAddProdColor" runat="server" Text="Добавить" ValidationGroup="EditProdColor"
                                            CssClass="TableButton" OnClick="btnAddProdColor_Click" />
                                        <asp:Button ID="btnCancelCategory" runat="server" Text="Отмена" CssClass="TableButton"
                                            OnClick="btnCancelCategory_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <asp:Button ID="btnXClear" runat="server" CssClass="TableButton" Text="x" OnClick="btnCancel_Click" />
                        </td>
                    </asp:Panel>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
