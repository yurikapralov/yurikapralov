using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class Platinum_ProductItem : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindProductItems();
            BindProductColorItems();
            this.Title = lblProductName.Text;
            if (Request.UrlReferrer != null)
                PreviousPage = Request.UrlReferrer.OriginalString;
            else
                PreviousPage = "http://www.Platinumshoes.ru";
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
                Response.Redirect("http://www.Platinumshoes.ru");
            List<Color> colors = lRepository.GetColorsByProduct(ProductId);
            ddlColors.DataSource = colors;
            ddlColors.DataBind();
            if (colors.Count == 0)
                Response.Redirect("http://www.Platinumshoes.ru");

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
            lblProductName.Text = productColor.Product.ProductNameRus;
            imgLarge.ImageUrl = string.Format("http://echo-h.ru/Images/Products/Large/{0}", productColor.ImageURL);
            if (productColor.Product.ProdTypeID == 1)
            {
                using (PlatformRepository lPlatformRepository = new PlatformRepository())
                {
                    Platform platform = lPlatformRepository.GetPlatformByProduct(ProductId);
                    if (platform != null)
                        lblPlatformDescription.Text = platform.PlatformDescriptionRus;
                }
            }
            lblProductDescription.Text = productColor.Product.ProductDescriptionRus;
            lblPrice.Text = oldSalePrice(string.Format("{0:C}", productColor.Product.SalePrice), true) +
                            string.Format("{0:C}", productColor.Product.OrigPrice);
            using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
            {
                List<ProductSize> productSizes =
                    lProductSizeRepository.GetProductSizeByProductColor(productColor.ProdColorID);
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
        Response.Redirect("http://www.platinumshoes.ru/Shopping.aspx");
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
}
