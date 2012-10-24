using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Info;
using echo.BLL.Orders;
using echo.BLL.Products;

public partial class En_OrdersHistory_old : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCurrency(Currency);
            Master.MainHeader = "E-shop clothes and shoes - order history";
            Master.Title = "E-shop clothes and shoes - order history";
            Master.ContentStyle = "Content2";
            Master.HeaderImage = "Header.jpg";
        }
        
        BindOrders();
    }

    protected void BindOrders()
    {
        if (this.User.Identity.IsAuthenticated)
        {
            using (OrdersRepository lRepository = new OrdersRepository())
            {
                List<Order> orders = lRepository.GetOrdersByUserName(User.Identity.Name);
                DataList dlOrders = (DataList)LoginView1.FindControl("dlOrders");
                dlOrders.DataSource = orders;
                dlOrders.DataBind();
                if (dlOrders.Items.Count == 0)
                {
                    Label lblEmpty = (Label)LoginView1.FindControl("lblEmpty");
                    lblEmpty.Visible = true;
                }
            }
        }
    }

    protected string GetNavigateURL(int prodSizeId)
    {
        using (ProductRepository lRepository = new ProductRepository())
        {
            ProductColor productColor = lRepository.GetProductByProdSizeId(prodSizeId);
            if (productColor != null)
            {
                return string.Format("http://www.echo-h.ru/ProductItem.aspx?ProdID={0}", productColor.Product.ProdID);
            }
        }
        return "";
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

    protected string GetDeliverMethod(object objDelTypeId, object DeliverSum)
    {
        int DeliverTypeID = (int)objDelTypeId;
        switch (DeliverTypeID)
        {
            case 1:
                return string.Format("EMS GarantPost: {0:C}", DeliverSum);
                break;
            case 2:
                return "By Post";
                break;
            case 3:
                return string.Format("Courier to Moscow: {0:C}", DeliverSum);
                break;
            case 4:
                return "Outside the Russian Federation";
                break;
        }
        return "";
    }

    protected string formatFunction(object obj, bool isShow)
    {
        int i = (int)obj;
        if (isShow)
            return "ShowPreview('img_block_" + i.ToString() + "');";
        return "HidePreview('img_block_" + i.ToString() + "');";
    }

    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Currency"] = ddlCurrency.SelectedValue;
        BindOrders();
    }
}
