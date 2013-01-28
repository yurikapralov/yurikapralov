using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class Admin_Products : AdminPage
{
    private string _uploadThumbPath;
    private string _uploadLargePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.Title = "Управление продукцией";
            Master.HeaderText = "Управление продукцией";
            BindCategories(ddlCategories, true);
            BindCategories();
            Helpers.EnumToListBox(typeof(ProductType), ddlProductType);
            BindPlatforms();
            BindBrands();
        }
        _uploadThumbPath = Server.MapPath("~/Images/Products/Thumb/");
        _uploadLargePath = Server.MapPath("~/Images/Products/Large/");

    }



    public ProductSorted CurProductSorted
    {
        get
        {
            if (ViewState["ProductSorted"] != null)
                return (ProductSorted)ViewState["ProductSorted"];
            return ProductSorted.DateAsc;
        }
        set
        {
            ViewState["ProductSorted"] = value;
        }
    }


    protected void BindCategories(DropDownList control, bool added)
    {
        using (CathegoryRepository lRepository = new CathegoryRepository())
        {
            List<Cathegory> cathegories = lRepository.GetCathegories(CathegorySortField.GroupOrder, CathegorySortType.Asc);
            control.DataSource = cathegories;
            control.DataBind();
            if (added)
            {
                control.Items.Insert(0, new ListItem("--Выберите категорию--", "-1"));
                control.Items.Insert(1, new ListItem("--Все категории--", "0"));
                control.Items.Insert(2, new ListItem("--Обувь--", "-2"));
                control.Items.Insert(3, new ListItem("--Одежда--", "-3"));
            }
        }
    }

    protected void BindCategories()
    {
        using (CathegoryRepository lRepository = new CathegoryRepository())
        {
            List<Cathegory> cathegories = lRepository.GetCathegories(CathegorySortField.GroupOrder, CathegorySortType.Asc);
            lbCategories.DataSource = cathegories;
            lbCategories.DataBind();
        }
    }

    protected void BindPlatforms()
    {
        using (PlatformRepository lRepository = new PlatformRepository())
        {
            List<Platform> platforms = lRepository.GetPlatforms();
            ddlPlatform.DataSource = platforms;
            ddlPlatform.DataBind();
        }
    }

    protected void BindBrands()
    {
        using (BrandRepository rep = new BrandRepository())
        {
            List<Brand> brands = rep.GetBrands();
            ddlBrand.DataSource = brands;
            ddlBrand.DataBind();
            ddlBrand.SelectedValue = "1";
        }
    }



    protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        BindProducts();
        ListItem finded = ddlCategories.Items.FindByValue("-1");
        if (finded != null)
            ddlCategories.Items.Remove(finded);
        lblsubHeader.Text = ddlCategories.SelectedItem.Text;
    }

    protected void BindProducts()
    {
        using (ProductRepository lRepository = new ProductRepository())
        {
            List<Product> products = null;
            if (txtSearch.Text != "")
            {
                products = lRepository.GetSearchProducts(txtSearch.Text, ProductSorted.DateAsc, 0);
                cbxActive.Checked = false;
                ddlCategories.SelectedIndex = -1;
            }
            else
            {
                int catId = int.Parse(ddlCategories.SelectedValue);
                if (catId == -1)
                    return;
                if (catId > 0)
                {
                    if (cbxActive.Checked)
                        products = lRepository.GetActiveProductsByCategory(catId, CurProductSorted);
                    else
                        products = lRepository.GetAllProductsByCategory(catId, CurProductSorted);
                }
                if (catId == 0)
                {
                    if (cbxActive.Checked)
                        products = lRepository.GetActiveProducts(CurProductSorted);
                    else
                        products = lRepository.GetAllProducts(CurProductSorted);
                }
                if (catId == -2)
                {
                    if (cbxActive.Checked)
                        products = lRepository.GetActiveProductsByProductType(1, CurProductSorted);
                    else
                        products = lRepository.GetAllProductsByProductType(1, CurProductSorted);
                }
                if (catId == -3)
                {
                    if (cbxActive.Checked)
                        products = lRepository.GetActiveProductsByProductType(2, CurProductSorted);
                    else
                        products = lRepository.GetAllProductsByProductType(2, CurProductSorted);
                }
            }
            lvProducts.DataSource = products;
            lvProducts.DataBind();
            if (products.Count > 0)
                lblProductCount.Text = string.Format("Всего позиций: {0}", products.Count);
            else
                lblProductCount.Text = "";
            if (products.Count < pagerBottom.PageSize)
                pnlPager.Visible = false;
            else
                pnlPager.Visible = true;
        }
    }
    protected void lvProducts_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindProducts();
    }

    protected void lbtnNameSorted_Click(object sender, EventArgs e)
    {
        if (CurProductSorted == ProductSorted.NameAsc)
            CurProductSorted = ProductSorted.NameDesc;
        else
            CurProductSorted = ProductSorted.NameAsc;
        BindProducts();
    }

    protected void lbtnPriceSorted_Click(object sender, EventArgs e)
    {
        if (CurProductSorted == ProductSorted.PriceAsc)
            CurProductSorted = ProductSorted.PriceDesc;
        else
            CurProductSorted = ProductSorted.PriceAsc;
        BindProducts();
    }

    protected void lbtnCreateSorted_Click(object sender, EventArgs e)
    {
        if (CurProductSorted == ProductSorted.DateAsc)
            CurProductSorted = ProductSorted.DateDesc;
        else
            CurProductSorted = ProductSorted.DateAsc;
        BindProducts();
    }

    protected void lbtnUpdateSorted_Click(object sender, EventArgs e)
    {
        if (CurProductSorted == ProductSorted.UpdateAsc)
            CurProductSorted = ProductSorted.UpdateDesc;
        else
            CurProductSorted = ProductSorted.UpdateAsc;
        BindProducts();
    }
    protected void cbxActive_CheckedChanged(object sender, EventArgs e)
    {
        BindProducts();
    }

    protected void cbxIsMainColors_CheckedChanged(object sender, EventArgs e)
    {
        BindColors();
    }

    protected void lvProducts_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Image imgPreview = (Image)e.Item.FindControl("imgPreview");
            imgPreview.Visible = cbxPreview.Checked;
            Label txtDateUpdated = (Label)e.Item.FindControl("txtDateUpdated");
            if (txtDateUpdated != null && txtDateUpdated.Text != "")
            {
                DateTime dateUpdated = DateTime.Parse(txtDateUpdated.Text);
                if (dateUpdated.DayOfYear == DateTime.Now.DayOfYear && dateUpdated.Year == DateTime.Now.Year)
                    txtDateUpdated.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
    protected void cbxPreview_CheckedChanged(object sender, EventArgs e)
    {
        BindProducts();
    }
    protected void lvProducts_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvProducts.EditIndex = e.NewEditIndex;
        BindProducts();
    }
    protected void lvProducts_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvProducts.EditIndex = -1;
        BindProducts();
    }

    protected void QuickUpdateProduct(int productId, string sortedName, string productNameRus, decimal origPrice, decimal? salePrice, bool avaliable, bool onSale)
    {
        using (ProductRepository repository = new ProductRepository())
        {
            Product product = repository.GetProductById(productId);
            product.DateUpdated = DateTime.Now;
            product.SortedName = sortedName;
            product.ProductNameRus = productNameRus;
            product.OrigPrice = origPrice;
            product.SalePrice = salePrice;
            product.Available = avaliable;
            product.OnSale = onSale;
            product = repository.AddProduct(product);
        }
    }
    protected void lvProducts_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        int productId = int.Parse(lvProducts.DataKeys[e.ItemIndex].Value.ToString());
        string sortedName = ((TextBox)lvProducts.Items[e.ItemIndex].FindControl("txtSortedName")).Text;
        string productNameRus = ((TextBox)lvProducts.Items[e.ItemIndex].FindControl("txtProductNameRus")).Text;
        decimal origPrice =
            decimal.Parse(((TextBox)lvProducts.Items[e.ItemIndex].FindControl("txtOrigPrice")).Text);
        string strSale = ((TextBox)lvProducts.Items[e.ItemIndex].FindControl("txtSalePrice")).Text;
        decimal? salePrice = null;
        if (strSale != "")
            salePrice = decimal.Parse(strSale);
        bool available = ((CheckBox)lvProducts.Items[e.ItemIndex].FindControl("cbActive")).Checked;
        bool onSale = ((CheckBox)lvProducts.Items[e.ItemIndex].FindControl("cbOnSale")).Checked;

        QuickUpdateProduct(productId, sortedName, productNameRus, origPrice, salePrice, available, onSale);
        lvProducts.EditIndex = -1;
        BindProducts();
    }
    protected void lvProducts_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        lvProducts.SelectedIndex = e.NewSelectedIndex;
        if (!cbxVisible.Checked)
            pnlProductList.Visible = false;
        BindProducts();
    }

    protected void lbtnSelect_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        int productId = int.Parse(lbtn.CommandArgument);
        BindProduct(productId);
    }

    protected void BindProduct(int productId)
    {
        using (ProductRepository lProductRepository = new ProductRepository())
        {
            ProductId = productId;
            Product product = lProductRepository.GetProductById(productId);
            lblEditProductHeader.Text = string.Format("Редактирование {0}, ID: {1}", product.ProductNameRus,
                                                      product.ProdID);
            List<int> selectedCategories = lProductRepository.GetCategoryIDListByProduct(productId);
            foreach (int selCat in selectedCategories)
            {
                foreach (ListItem item in lbCategories.Items)
                {
                    if (int.Parse(item.Value) == selCat)
                        item.Selected = true;
                }
            }
            ddlProductType.SelectedValue = product.ProdTypeID.ToString();
            txtSorted.Text = product.SortedName;
            txtEngName.Text = product.ProductNameEng;
            txtRusName.Text = product.ProductNameRus;
            if (product.ProdTypeID == 1)
            {
                pnlPlatform.Visible = true;
                ddlPlatform.SelectedValue = product.PlatformId.ToString();
            }
            else
            {
                pnlPlatform.Visible = false;
            }
            ddlBrand.SelectedValue = product.BrandId.ToString();
            txtEngDescr.Text = product.ProductDescriptionEng;
            txtRusDescr.Text = product.ProductDescriptionRus;
            txtThumb.Text = product.ThumbURL;
            txtAlt.Text = product.Alt;
            txtOrigPrice.Text = string.Format("{0:F2}", product.OrigPrice);
            txtSalePrice.Text = string.Format("{0:F2}", product.SalePrice);
            cbProdAvailable.Checked = product.Available;
            cbOnSale.Checked = product.OnSale;
            cbFromUsa.Checked = product.FromUsa;
            btnMore.Visible = false;
            btnUpdateProduct.Visible = true;
            btnExtraInfo.Visible = true;
        }
    }
    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductType.SelectedValue == "1")
            pnlPlatform.Visible = true;
        else
            pnlPlatform.Visible = false;
    }
    protected void btnUpdateProduct_Click(object sender, EventArgs e)
    {
        //проверка на наличие изменений в превью фотографии, если загрузка новой фотографии успешна
        //запускается механизм обновления продукции.
        if (fuThumb.HasFile)
            if (!Uploadfile())
                return;
        UpdateProduct();
        ClearProduct();
    }

    protected void UpdateProduct()
    {
        using (ProductRepository lRepository = new ProductRepository())
        {
            if (ProductId > 0)
            {
                Product product = lRepository.GetProductById(ProductId);
                GetProductData(product);
                product.DateUpdated = DateTime.Now;
                product = lRepository.AddProduct(product);
                using (CollectionRepository collectionRepository = new CollectionRepository())
                {
                    collectionRepository.DeleteCollectionByProd(product.ProdID);
                    foreach (ListItem item in lbCategories.Items)
                    {
                        if (item.Selected)
                        {
                            Collection coll = new Collection();
                            coll.ProductId = product.ProdID;
                            coll.CathegoryId = int.Parse(item.Value);
                            collectionRepository.AddCollection(coll);
                        }
                    }
                }
            }
        }
    }

    protected void ClearProduct()
    {
        lvProducts.SelectedIndex = -1;
        ProductId = 0;
        lblEditProductHeader.Text = "Добавление нового продукта";
        btnMore.Visible = true;
        btnUpdateProduct.Visible = false;
        btnExtraInfo.Visible = false;
        lbCategories.SelectedIndex = -1;
        ddlProductType.SelectedIndex = -1;
        pnlPlatform.Visible = true;
        txtSorted.Text = "";
        txtEngName.Text = "";
        txtRusName.Text = "";
        txtRusDescr.Text = "";
        txtEngDescr.Text = "";
        txtThumb.Text = "";
        txtOrigPrice.Text = "";
        txtSalePrice.Text = "";
        cbProdAvailable.Checked = true;
        cbOnSale.Checked = false;
        cbFromUsa.Checked = false;
        BindProducts();
        BindProductColors();
        ddlBrand.SelectedValue = "1";
        txtAlt.Text = "";
    }

    protected void GetProductData(Product product)
    {


        product.ProdTypeID = int.Parse(ddlProductType.SelectedValue);
        // todo: проверить добавление одежды
        if (product.ProdTypeID == 1)
            product.PlatformId = int.Parse(ddlPlatform.SelectedValue);
        product.BrandId = int.Parse(ddlBrand.SelectedValue);
        product.SortedName = txtSorted.Text;
        product.ProductNameRus = txtRusName.Text;
        product.ProductNameEng = txtEngName.Text;
        product.ProductDescriptionRus = txtRusDescr.Text;
        product.ProductDescriptionEng = txtEngDescr.Text;
        product.ThumbURL = txtThumb.Text;
        product.OrigPrice = decimal.Parse(txtOrigPrice.Text);
        if (txtSalePrice.Text != "")
            product.SalePrice = decimal.Parse(txtSalePrice.Text);
        else
            product.SalePrice = null;
        product.Available = cbProdAvailable.Checked;
        product.OnSale = cbOnSale.Checked;
        product.FromUsa = cbFromUsa.Checked;
        product.Alt = txtAlt.Text;
    }

    protected bool Uploadfile()
    {
        string prefix = Helpers.GetRandomPrefix();
        if (fuThumb.HasFile)
        {
            try
            {
                fuThumb.SaveAs(_uploadThumbPath + prefix + fuThumb.FileName);
                txtThumb.Text = prefix + fuThumb.FileName;
                lblThumUploadErr.Visible = false;
                return true;
            }
            catch (Exception er)
            {
                lblThumUploadErr.Visible = true;
                lblThumUploadErr.Text = "Ошибка загрузки: " + er.Message;
                return false;
            }
        }
        else
        {
            lblThumUploadErr.Visible = true;
            lblThumUploadErr.Text = "Укажите имя файла";
            return false;
        }
    }
    protected void btnCancelUpThumb_Click(object sender, EventArgs e)
    {
        //Очищает fileUploader простой перезагрузкой страницы
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearProductColor();
        ClearProduct();
        pnlProductList.Visible = true;
    }
    protected void btnSortedCopy_Click(object sender, EventArgs e)
    {
        txtEngName.Text = "Art. " + txtSorted.Text;
    }

    protected void BindProductColors()
    {
        if (ProductId > 0)
        {
            pnlProdColorEdit.Visible = true;
            using (ProductColorRepository lRepository = new ProductColorRepository())
            {
                List<ProductColor> productColors = lRepository.GetProductColorsByProduct(ProductId);
                lvProdColors.DataSource = productColors;
                lvProdColors.DataBind();
            }


        }
        else
        {
            lvProdColors.DataSource = null;
            lvProdColors.DataBind();
            ddlColors.DataSource = null;
            ddlColors.DataBind();
            lblSizeInfo.Text = "";
            lbSizes.DataSource = null;
            lbSizes.DataBind();
            pnlProdColorEdit.Visible = false;

        }
    }

    protected void BindColors()
    {
        using (ColorRepository lRepository = new ColorRepository())
        {
            bool isall = !cbxIsMainColors.Checked;
            List<Color> colors = lRepository.GetColors(isall);
            ddlColors.DataSource = colors;
            ddlColors.DataBind();
        }
    }

    protected void BindSizes()
    {
        using (ProductRepository lProductRepository = new ProductRepository())
        {
            using (SizeRepository lSizeRepository = new SizeRepository())
            {
                int prodtype;
                string platformInfo = "";
                if (ProductId > 0)
                {
                    Product product = lProductRepository.GetProductById(ProductId);
                    prodtype = product.ProdTypeID;
                    platformInfo = product.Platform.SizeInfo;
                }
                else
                {
                    prodtype = int.Parse(ddlProductType.SelectedValue);
                    if (prodtype == 1)
                    {
                        using (PlatformRepository lPlatformRepository = new PlatformRepository())
                        {
                            Platform platform = lPlatformRepository.GetPlatformById(int.Parse(ddlPlatform.SelectedValue));
                            platformInfo = platform.SizeInfo;
                        }
                    }
                }
                if (prodtype == 1)
                {
                    lblSizeInfo.Text = platformInfo;
                    pnlSizeInfo.Visible = true;
                }
                else
                {
                    lblSizeInfo.Text = "";
                    pnlSizeInfo.Visible = false;
                }

                List<Size> sizes = lSizeRepository.GetSizes(prodtype);
                lbSizes.DataSource = sizes;
                lbSizes.DataBind();
            }
        }
    }

    protected void lbtnSelectColor_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        int productColorId = int.Parse(lbtn.CommandArgument);
        BindProductColor(productColorId);
    }
    protected void btnExtraInfo_Click(object sender, EventArgs e)
    {
        BindProductColors();
        BindColors();
        BindSizes();
    }
    protected void btnColorAdd_Click(object sender, EventArgs e)
    {
        string colorNameRus = txtColorRusAdd.Text;
        string colorNameEng = txtColorEngAdd.Text;
        using (ColorRepository lRepository = new ColorRepository())
        {
            if (lRepository.IsUsedColorName(colorNameRus, colorNameEng))
            {
                int colorId = lRepository.GetColorIdByColorNameRusAndColorNameEng(colorNameRus, colorNameEng);
                if (colorId > 0)
                    ddlColors.SelectedValue = colorId.ToString();
            }
            else
            {
                Color color = new Color();
                color.ColorNameRus = colorNameRus;
                color.ColorNameEng = colorNameEng;
                color.IsMain = true;
                color = lRepository.AddColor(color);
                BindColors();
            }
            txtColorRusAdd.Text = "";
            txtColorEngAdd.Text = "";
        }
    }
    protected void btnSizeAdd_Click(object sender, EventArgs e)
    {
        using (ProductRepository lProductRepository = new ProductRepository())
        {
            using (SizeRepository lSizeRepository = new SizeRepository())
            {
                Product product = lProductRepository.GetProductById(ProductId);
                Size size = new Size();
                size.ProdTypeID = product.ProdTypeID;
                size.SizeNameRus = txtSizeRusAdd.Text;
                size.SizeNameEng = txtSizeEngAdd.Text;
                size = lSizeRepository.AddSize(size);

                List<Size> sizes = lSizeRepository.GetSizes(product.ProdTypeID);
                lbSizes.DataSource = sizes;
                lbSizes.DataBind();
            }
        }
    }

    protected void BindProductColor(int productColorId)
    {
        using (ProductColorRepository lRepository = new ProductColorRepository())
        {
            ProdColorId = productColorId;
            ProductColor productColor = lRepository.GetProductColorById(productColorId);
            lblEditProductColorHeader.Text = string.Format("Редактировние модели: {0}, цвета {1}",
                                                           productColor.Product.SortedName,
                                                           productColor.Color.ColorNameRus);
            cbxIsMainColors.Checked = false;
            BindColors();
            ddlColors.SelectedValue = productColor.ColorId.ToString();
            using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
            {
                List<ProductSize> selectedSizes = lProductSizeRepository.GetProductSizeByProductColor(productColorId);

                foreach (ProductSize selectedSize in selectedSizes)
                {
                    foreach (ListItem item in lbSizes.Items)
                    {
                        if (int.Parse(item.Value) == selectedSize.SizeId)
                            item.Selected = true;
                    }
                }
            }
            txtImageURL.Text = productColor.ImageURL;
            btnAddProdColor.Text = "Изменить";

        }
    }

    protected void ClearProductColor()
    {
        lvProdColors.SelectedIndex = -1;
        ProdColorId = 0;
        lblEditProductColorHeader.Text = "Добавление нового цвета";
        btnAddProdColor.Text = "Добавить";
        ddlColors.SelectedIndex = -1;
        lbSizes.SelectedIndex = -1;
        txtImageURL.Text = "";
        BindProductColors();
    }

    protected void lvProdColors_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        lvProdColors.SelectedIndex = e.NewSelectedIndex;
        BindProductColors();
    }
    protected void btnCancelCategory_Click(object sender, EventArgs e)
    {
        ClearProductColor();
    }
    protected void btnAddProdColor_Click(object sender, EventArgs e)
    {
        lvProdColors.Visible = true;
        if (txtImageURL.Text == "")
            UploadLargeFile();
        else if (fuLarge.HasFile)
            UploadLargeFile();
        UpdateProductColor();
    }

    protected void UpdateProductColor()
    {
        if (ProductId == 0)
        {
            using (ProductRepository lProductRepository = new ProductRepository())
            {
                Product product = new Product();
                GetProductData(product);
                product.DateCreated = DateTime.Now;
                product = lProductRepository.AddProduct(product);
                ProductId = product.ProdID;
                using (CollectionRepository collectionRepository = new CollectionRepository())
                {
                    foreach (ListItem item in lbCategories.Items)
                    {
                        if (item.Selected)
                        {
                            Collection coll = new Collection();
                            coll.ProductId = product.ProdID;
                            coll.CathegoryId = int.Parse(item.Value);
                            collectionRepository.AddCollection(coll);
                        }
                    }
                }
            }
        }
        if (ProductId > 0)
        {

            using (ProductColorRepository lProductColorRepository = new ProductColorRepository())
            {
                using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
                {
                    ProductColor productColor;
                    if (ProdColorId > 0)
                    {
                        productColor = lProductColorRepository.GetProductColorById(ProdColorId);
                        productColor.DateUpdated = DateTime.Now;
                        lProductSizeRepository.DeleteProdSizeByProdColorId(ProdColorId);
                    }
                    else
                    {
                        productColor = new ProductColor();
                        productColor.DateCreated = DateTime.Now;
                    }
                    productColor.ImageURL = txtImageURL.Text;
                    productColor.ProductId = ProductId;
                    productColor.ColorId = int.Parse(ddlColors.SelectedValue);
                    productColor = lProductColorRepository.AddProductColor(productColor);
                    foreach (ListItem item in lbSizes.Items)
                    {
                        if (item.Selected)
                        {
                            ProductSize productSize = new ProductSize();
                            productSize.SizeId = int.Parse(item.Value);
                            productSize.ProductColorId = productColor.ProdColorID;
                            lProductSizeRepository.AddProductSize(productSize);
                        }
                    }
                }

            }
        }
        ClearProductColor();
        BindProducts();
    }

    protected bool UploadLargeFile()
    {
        string prefix = Helpers.GetRandomPrefix();
        if (fuLarge.HasFile)
        {
            try
            {
                fuLarge.SaveAs(_uploadLargePath + prefix + fuLarge.FileName);
                txtImageURL.Text = prefix + fuLarge.FileName;
                lblfuLargeErr.Visible = false;
                return true;
            }
            catch (Exception er)
            {
                lblfuLargeErr.Visible = true;
                lblfuLargeErr.Text = "Ошибка загрузки: " + er.Message;
                return false;
            }
        }
        else
        {
            lblfuLargeErr.Visible = true;
            lblfuLargeErr.Text = "Укажите имя файла";
            return false;
        }
    }
    protected void lvProdColors_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int prodColorId = int.Parse(lvProdColors.DataKeys[e.ItemIndex].Value.ToString());
        using (ProductColorRepository lRepository = new ProductColorRepository())
        {
            lRepository.DeleteProductColor(prodColorId);
            ClearProductColor();
        }
    }
    protected void btnMore_Click(object sender, EventArgs e)
    {
        Uploadfile();
        BindColors();
        BindSizes();
        lvProdColors.Visible = false;
        //BindProductColors();
        pnlProdColorEdit.Visible = true;

    }
    protected void lvProducts_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int prodId = int.Parse(lvProducts.DataKeys[e.ItemIndex].Value.ToString());
        using (ProductRepository lRepository = new ProductRepository())
        {
            lRepository.DeleteProduct(prodId);
            BindProducts();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindProducts();
        lblsubHeader.Text = "Поиск по имени: " + txtSearch.Text;
    }

    protected string GetUsingCathegory(int productId)
    {
        using (CathegoryRepository lRepository = new CathegoryRepository())
        {
            string output = "";
            List<Cathegory> cathegories = lRepository.GetCathegoriesByProduct(productId);
            foreach (Cathegory cathegory in cathegories)
            {
                output += string.Format("{0}<br/>", cathegory.CatNameRus);
            }
            return output;
        }
    }
}