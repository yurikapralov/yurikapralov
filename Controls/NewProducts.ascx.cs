using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL.Products;

public partial class Controls_NewProducts : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindProducts(_productType);
    }

    private int _productType;

    public int ProductType
    {
        get { return _productType; }
        set { _productType = value; }
    }

    protected void BindProducts(int productType)
    {
        using (ProductRepository repository=new ProductRepository())
        {
            List<Product> products=new List<Product>();
            products = repository.GetNewProducts(productType);
            lvProducts.DataSource = products;
            lvProducts.DataBind();
            if (products.Count > 0)
            {
                hplMore.NavigateUrl = string.Format("../Products.aspx?CatId=0&GroupId=2&ProdType={0}", _productType);
                hplMore.Visible = true;
            }
        }
    }

    protected string DescRestrict(object oDescription)
    {
        if (oDescription == null)
            return "";
        string description = (string)oDescription;
        int numberOfLetters = 150;

        if (description.Length < numberOfLetters)
            return description;

        return description.Substring(0, numberOfLetters) + "...";
    }

    protected string oldSalePrice(string salePrice)
    {
        if (string.IsNullOrEmpty(salePrice))
            return "";
        return "&nbsp;<span class=;salePrice'><s>" + salePrice + "</s></span>&nbsp";
    }
}
