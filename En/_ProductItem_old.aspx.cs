using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Info;
using echo.BLL.Products;

public partial class En_ProductItem_old : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCurrency(Currency);
            BindProductItems();
            BindProductColorItems();
            Master.Title = lblProductName.Text;
            Master.MainHeader = lblProductName.Text;
            Master.ContentStyle = "Content2";
            if (Request.UrlReferrer != null)
                PreviousPage = Request.UrlReferrer.OriginalString;
            else
                PreviousPage = "Default.aspx";
        }


    }

    protected void BindCurrency(string value)
    {
        using (RateRepository lRepository = new RateRepository())
        {
            List<Rate> rates = lRepository.GetRates();
            ddlCurrency.DataSource = rates;
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, new ListItem("Rub", "0"));
            ddlCurrency.SelectedValue = value;
        }
    }

    public string PreviousPage
    {
        get { return (string)ViewState["PreviousPage"]; }

        set { ViewState["PreviousPage"] = value; }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Theme = CurrentTheme;
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
            lblProductName.Text = productColor.Product.ProductNameEng;
            imgLarge.ImageUrl = string.Format("~/Images/Products/Large/{0}", productColor.ImageURL);
            if (productColor.Product.ProdTypeID == 1)
            {
                using (PlatformRepository lPlatformRepository = new PlatformRepository())
                {
                    Platform platform = lPlatformRepository.GetPlatformByProduct(ProductId);
                    if (platform != null)
                        lblPlatformDescription.Text = platform.PlatformDescriptionEng;
                }
            }
            lblProductDescription.Text = productColor.Product.ProductDescriptionEng;
            lblPrice.Text = oldSalePrice(string.Format("{0:F2}", productColor.Product.SalePrice),false) +
                           ConvertPrice(string.Format("{0:F2}", productColor.Product.OrigPrice));
           
            using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
            {
                List<ProductSize> productSizes =
                    lProductSizeRepository.GetProductSizeByProductColor(productColor.ProdColorID);
                if ((productSizes.Count == 1 && productSizes[0].SizeId == 24) || (!productColor.Product.Available))  // идентификатор размера нет в наличии
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
        int colorId = int.Parse(ddlColors.SelectedValue);
        using (ProductColorRepository lProductColorRepository = new ProductColorRepository())
        {
            ProductColor productColor = lProductColorRepository.GetProductColorByProductAndColor(ProductId, colorId);
            lblProduct.Text = productColor.Product.ProductNameEng;
            lblColor.Text = string.Format("Color: {0}", productColor.Color.ColorNameEng);
            lblSize.Text = string.Format("Size: {0}", ddlProdSizes.SelectedItem.Text);
            string s_title = string.Format("{0}, color: {1}, size: {2}", productColor.Product.ProductNameEng,
                                           productColor.Color.ColorNameEng, ddlProdSizes.SelectedItem.Text);
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
                lblLargeSizeInfo.Text = "if you ordered shoes by sizes: 12,13,14, the prise is increasing by " + ConvertPrice("600");
            else
                lblLargeSizeInfo.Text="";
        }
    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Currency"] = ddlCurrency.SelectedValue;
        BindProductColorItems();
    }
}
