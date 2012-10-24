using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL.Products;

public partial class Controls_RandTop3NewProduct : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindProducts();
    }

    //Режим работы
    //1-Новинки
    //2-Распродажа
    private int _prod_mode;

    public int Prod_mode
    {
        get { return _prod_mode; }
        set { _prod_mode = value; }
    }



    protected string oldSalePrice(string salePrice)
    {
        if (string.IsNullOrEmpty(salePrice))
            return "";
        return "<span class='prodthumb_oldprice'>" + salePrice + "</span>";
    }

    protected void BindProducts()
    {
        using (ProductRepository repository = new ProductRepository())
        {
            List<Product> products = new List<Product>();
            if (_prod_mode == 1)
                products = repository.GetNewProducts(1);
            else
                products = repository.GetSaleProducts(1);
            lvProducts.DataSource = products;
            lvProducts.DataBind();
          
        }
    }

    protected string GetCategories(int productId)
    {
        using (CathegoryRepository lrep=new CathegoryRepository())
        {
            List<Cathegory> cathegories = lrep.GetCathegoriesByProductExceptNew(productId);
            string ret = "";
            foreach (Cathegory cathegory in cathegories)
            {
                ret += string.Format("<a href=\"Products.aspx?CatId={0}\"<span class=\"incat\">{1}</span></a>",cathegory.CatID,cathegory.CatNameRus);
            }
            return ret;
        }
    }
}