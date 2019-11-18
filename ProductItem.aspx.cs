using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class ProductItem : BasePage
{
    private string _prodName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindProductItems();
            BindProductColorItems();
            Master.Title = _prodName;
            Master.MainHeader = _prodName;
            if (Request.UrlReferrer != null)
                PreviousPage = Request.UrlReferrer.OriginalString;
            else
                PreviousPage = "Default.aspx";
        }


    }

    public string PreviousPage
    {
        get { return (string)ViewState["PreviousPage"]; }

        set { ViewState["PreviousPage"] = value; }
    }


    protected void BindProductItems()
    {
        using (ProductColorRepository lRepository = new ProductColorRepository())
        {
            if (ProductId <= 0)
                Response.Redirect("Default.aspx");
            List<Color> colors = lRepository.GetColorsByProduct(ProductId);
            ddlColors.DataSource = colors;
            ddlColors.DataBind();
            if (colors.Count == 0)
                Response.Redirect("Default.aspx");

            List<ProductColor> productColors = lRepository.GetProductColorsByProduct(ProductId);
            rptImages.DataSource = productColors;
            rptImages.DataBind();
        }
    }


    protected void BindProductColorItems()
    {
        using (ProductColorRepository lRepository = new ProductColorRepository())
        {
            int colorId = int.Parse(ddlColors.SelectedValue);
            ProductColor productColor = lRepository.GetProductColorByProductAndColor(ProductId, colorId);
            _prodName = productColor.Product.ProductNameRus;
            lblProductName.Text = productColor.Product.ProductNameRus+" "+productColor.Color.ColorNameRus;
            imgLarge.ImageUrl = string.Format("~/Images/Products/Large/{0}", productColor.ImageURL);
            imgLarge.AlternateText = (string.IsNullOrEmpty(productColor.Product.Alt))
                                         ? productColor.Product.ProductNameRus
                                         : productColor.Product.Alt;
            lblUSA.Visible = productColor.Product.FromUsa;
            btnAddKredit.Visible = false; // productColor.Product.FromUsa == false;
            if(this.Profile.ShoppingCart.HaveUsaProduct()&&(!productColor.Product.FromUsa))
            {
                btnAddKredit.OnClientClick =
                    "alert('В вашем заказе присутствуют вещи с доставкой из США.\\nВ кредит доступна только продукция Echo Of Hollywood.\\nВы можете оформить покупки разными заказами.');return false;";
            }
            if(this.Profile.ShoppingCart.InCredit && productColor.Product.FromUsa)
            {
                btnAdd.OnClientClick = "return confirm('При добавлении продукции с доставкой из США покупка в кредит будет невозможной.\\nДобавить товар в корзину?');";
            }
            ltlsale.Text = productColor.Product.OnSale ? " <div id=\"sale_" + productColor.ProductId + "\" style=\"z-index: 2; height: 128px; left: 350px; width:128px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/sale_large.png\" /></div>" : "";
            ltlVip.Text = productColor.Product.IsVip ? " <div id=\"vip_" + productColor.ProductId + "\" style=\"z-index: 3; height: 128px; left: 350px; width:128px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/vip_large.png\" /></div>" : "";
            ltlsale2.Text = productColor.Product.OnSale2 ? " <div id=\"vip_" + productColor.ProductId + "\" style=\"z-index: 3; height: 128px; left: 350px; width:128px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/sale2_l.png\" /></div>" : "";
            if (productColor.Product.ProdTypeID == 1)
            {
                using (PlatformRepository lPlatformRepository = new PlatformRepository())
                {
                    Platform platform = lPlatformRepository.GetPlatformByProduct(ProductId);
                    if (platform != null)
                        lblPlatformDescription.Text = platform.PlatformDescriptionRus;
                }
            }
            using (BrandRepository b_rep=new BrandRepository())
            {
                Brand brand = b_rep.GetBrandById(productColor.Product.BrandId);
                lblBrand.Text = brand.BrandName;
            }
            lblProductDescription.Text = productColor.Product.ProductDescriptionRus;
            lblPrice.Text = oldSalePrice(string.Format("{0:C}", productColor.Product.SalePrice), true) +
                            string.Format("<span class=\"prodthumb_price\">{0:C}</span>", productColor.Product.OrigPrice);

            using (CathegoryRepository lCathegoryRepository = new CathegoryRepository())
            {
                List<Cathegory> cathegories = lCathegoryRepository.GetCathegoriesByProduct(ProductId);
                string cathegorylinklist = "";
                foreach (Cathegory cathegory in cathegories)
                {
                    cathegorylinklist += string.Format("<a href='Products.aspx?CatID={0}'>{1}</a><br/>",
                                                       cathegory.CatID, cathegory.CatNameRus);
                }
                lblCategoriesLinks.Text = cathegorylinklist;
            }
            using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
            {
                List<ProductSize> productSizes =
                    lProductSizeRepository.GetProductSizeByProductColor(productColor.ProdColorID);
                if ((productSizes.Count == 1 && (productSizes[0].SizeId == 24 || productSizes[0].SizeId == 25)) || (!productColor.Product.Available))  // идентификатор размера нет в наличии
                {
                    btnAdd.Enabled = false;
                    lblNotAvailable.Visible = true;
                }
                else
                {
                    btnAdd.Enabled = true;
                    lblNotAvailable.Visible = false;
                }
                ddlProdSizes.DataSource = productSizes;
                ddlProdSizes.DataBind();
            }
        }
    }

    protected void ddlColors_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductColorItems();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlProductItem.Visible = false;
        pnlProductItemAdd.Visible = true;
        AddProdut();
    }

    protected void AddProdut()
    {
        int colorId = int.Parse(ddlColors.SelectedValue);
        using (ProductColorRepository lProductColorRepository = new ProductColorRepository())
        {
            ProductColor productColor = lProductColorRepository.GetProductColorByProductAndColor(ProductId, colorId);
            lblProduct.Text = productColor.Product.ProductNameRus;
            lblColor.Text = string.Format("Цвет: {0}", productColor.Color.ColorNameRus);
            lblSize.Text = string.Format("Размер: {0}", ddlProdSizes.SelectedItem.Text);
            string s_title = string.Format("{0}, цвет: {1}, размер: {2}", productColor.Product.ProductNameRus,
                                           productColor.Color.ColorNameRus, ddlProdSizes.SelectedItem.Text);
            int s_ProdSizeId = int.Parse(ddlProdSizes.SelectedValue);
            decimal s_salePrice = productColor.Product.OrigPrice;
            using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
            {
                Size size = lProductSizeRepository.GetProductSizeById(int.Parse(ddlProdSizes.SelectedValue)).Size;
                s_salePrice = size.IsLargeShoesSize() ? s_salePrice + 600 : s_salePrice;
                this.Profile.ShoppingCart.InsertItem(s_ProdSizeId, s_salePrice, s_title, ProductId);
            }
        } 
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(PreviousPage);
    }
    protected void btnOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("Shopping.aspx");
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(PreviousPage);
    }
    protected void rptImages_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ddlColors.SelectedValue = e.CommandArgument.ToString();
        BindProductColorItems();
    }
    protected void ddlProdSizes_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
        {
            Size size = lProductSizeRepository.GetProductSizeById(int.Parse(ddlProdSizes.SelectedValue)).Size;
            if (size.IsLargeShoesSize())
                lblLargeSizeInfo.Visible = true;
            else
                lblLargeSizeInfo.Visible = false;
        }
    }

    protected void btnAddKredit_Click(object sender, EventArgs e)
    {
        this.Profile.ShoppingCart.InCredit = true;
        AddProdut();
        Response.Redirect("Shopping.aspx");
    }
}