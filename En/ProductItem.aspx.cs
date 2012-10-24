using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class En_ProductItem : BasePage
{
    private string _prodName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Подключаемя к обработчик к событию на мастер странице
        Master.DDlCurrency.SelectedIndexChanged += new EventHandler(DDlCurrency_SelectedIndexChanged);
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
            _prodName = productColor.Product.ProductNameEng;
            lblProductName.Text = productColor.Product.ProductNameEng + " " + productColor.Color.ColorNameEng;
            imgLarge.ImageUrl = string.Format("~/Images/Products/Large/{0}", productColor.ImageURL);
            ltlsale.Text = productColor.Product.OnSale ? " <div id=\"sale_" + productColor.ProductId + "\" style=\"z-index: 2; height: 128px; left: 350px; width:128px;top: 0px; position: absolute;\"><img src=\"Images/Decoration/sale_large.png\" /></div>" : "";
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
            lblPrice.Text = oldSalePrice(string.Format("{0:F2}", productColor.Product.SalePrice), false) +
                          "<span class=\"prodthumb_price\">" + ConvertPrice(string.Format("{0:F2}", productColor.Product.OrigPrice))+"</span>";

           
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

    protected void DDlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductColorItems();
    }
}