using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Orders;
using echo.BLL.Products;

public partial class OrdersHistory : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MainHeader = "Интернет-магазин обуви и одежды - история заказов";
        Master.Title = "Интернет-магазин обуви и одежды - история заказов";
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

    protected string GetDeliverMethod(object objDelTypeId, object DeliverSum)
    {
        int DeliverTypeID = (int)objDelTypeId;
        switch (DeliverTypeID)
        {
            case 1:
                return string.Format("ЕМС Гарантпост: {0:C}", DeliverSum);
                break;
            case 2:
                return "Наложенный платеж";
                break;
            case 3:
                return string.Format("Доставка по москве: {0:C}", DeliverSum);
                break;
            case 4:
                return "Доставка за рубеж";
                break;
        }
        return "";
    }
}