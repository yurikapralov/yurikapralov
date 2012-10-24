using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Orders;
using echo.BLL.Products;


/*public enum OrderSelectParameter
{
    OrderStatus, FIO, Email, User, Number, City
}*/

public partial class Admin_Orders_old : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.Title = "Управление заказами";
            Master.HeaderText = "Управление заказами";
            OrderStatusInitialisation();
            BindOrders();

            //Очистка профиля от старых записей - могла быть в любом другом месте сайта
            ProfileManager.DeleteInactiveProfiles(ProfileAuthenticationOption.Anonymous, DateTime.Now.AddDays(-7));
        }

    }

    public OrderSelectParameter OrderParameter
    {
        get
        {
            if (ViewState["OrderParameter"] != null)
                return (OrderSelectParameter)ViewState["OrderParameter"];
            return OrderSelectParameter.OrderStatus;
        }
        set { ViewState["OrderParameter"] = value; }
    }

    protected void BindOrderStatuses(DropDownList control)
    {
        using (OrderStatusRepository lOrderStatusRepository = new OrderStatusRepository())
        {
            List<OrderStatus> orderStatuses = lOrderStatusRepository.GetOrderStatuses();
            control.DataSource = orderStatuses;
            control.DataBind();
        }
    }

    protected void OrderStatusInitialisation()
    {
        ClearOrders();
        pnlOrderStatus.Visible = true;
        pnlFIO.Visible = false;
        pnlEmails.Visible = false;
        pnlUser.Visible = false;
        pnlNumber.Visible = false;
        pnlCity.Visible = false;
        BindOrderStatuses(ddlOrderStatus);
        txtToDate.Text = DateTime.Now.ToShortDateString();
        DateTime fromDate = DateTime.Now.Subtract(new TimeSpan(Helpers.Settings.Store.DefaultOrderListInterval, 0, 0, 0));
        txtFromDate.Text = fromDate.ToShortDateString();
        ClearOrder();

    }

    protected void FIOInitialisation()
    {
        ClearOrders();
        pnlOrderStatus.Visible = false;
        pnlFIO.Visible = true;
        pnlEmails.Visible = false;
        pnlUser.Visible = false;
        pnlNumber.Visible = false;
        pnlCity.Visible = false;
        ClearOrder();
    }

    protected void EmailInitialisation()
    {
        ClearOrders();
        pnlOrderStatus.Visible = false;
        pnlFIO.Visible = false;
        pnlEmails.Visible = true;
        pnlUser.Visible = false;
        pnlNumber.Visible = false;
        pnlCity.Visible = false;
        ClearOrder();
    }

    protected void UserInitialisation()
    {
        ClearOrders();
        pnlOrderStatus.Visible = false;
        pnlFIO.Visible = false;
        pnlEmails.Visible = false;
        pnlUser.Visible = true;
        pnlNumber.Visible = false;
        pnlCity.Visible = false;
        ClearOrder();
    }

    protected void NumberInitialisation()
    {
        ClearOrders();
        pnlOrderStatus.Visible = false;
        pnlFIO.Visible = false;
        pnlEmails.Visible = false;
        pnlUser.Visible = false;
        pnlNumber.Visible = true;
        pnlCity.Visible = false;
        ClearOrder();
    }

    protected void CitiesInitialisation()
    {
        ClearOrders();
        pnlOrderStatus.Visible = false;
        pnlFIO.Visible = false;
        pnlEmails.Visible = false;
        pnlUser.Visible = false;
        pnlNumber.Visible = false;
        pnlCity.Visible = true;
        ClearOrder();
        using (CityRepository lRepository=new CityRepository())
        {
            List<City> lCities = lRepository.GetCities();
            ddlCities.DataSource = lCities;
            ddlCities.DataBind();
        }
    }

    [System.Web.Services.WebMethod]
    public static string[] GetFIOs(string prefixText, int count)
    {
        using (OrdersRepository lOrdersRepository = new OrdersRepository())
        {
            return lOrdersRepository.GetFIOS(prefixText, count);
        }
    }

    [System.Web.Services.WebMethod]
    public static string[] GetEmails(string prefixText, int count)
    {
        using (OrdersRepository lOrdersRepository = new OrdersRepository())
        {
            return lOrdersRepository.GetEmails(prefixText, count);
        }
    }

    [System.Web.Services.WebMethod]
    public static string[] GetCities(string prefixText, int count)
    {
        using (OrdersRepository lOrdersRepository = new OrdersRepository())
        {
            return lOrdersRepository.GetCities(prefixText, count);
        }
    }

    [System.Web.Services.WebMethod]
    public static string[] GetUsers(string prefixText, int count)
    {
        if (count == 0)
            count = 10;
        List<String> lusers = new List<string>();
        MembershipUserCollection users = Membership.FindUsersByName(prefixText + "%");
        foreach (MembershipUser user in users)
        {
            lusers.Add(user.UserName);
        }
        return lusers.ToArray();
    }

    protected void lbtnOrderStatus_Click(object sender, EventArgs e)
    {
        OrderStatusInitialisation();
    }
    protected void lbtnFio_Click(object sender, EventArgs e)
    {
        FIOInitialisation();
    }
    protected void lbtnEmail_Click(object sender, EventArgs e)
    {
        EmailInitialisation();
    }
    protected void lbtnUser_Click(object sender, EventArgs e)
    {
        UserInitialisation();
    }
    protected void lbtnOrdersNumber_Click(object sender, EventArgs e)
    {
        NumberInitialisation();
    }
    protected void lbtnCity_Click(object sender, EventArgs e)
    {
        CitiesInitialisation();
    }



    protected string GetNavigateURL(int prodSizeId)
    {
        using (ProductRepository lRepository = new ProductRepository())
        {
            ProductColor productColor = lRepository.GetProductByProdSizeId(prodSizeId);
            if (productColor != null)
            {
                return string.Format("~/ProductItem.aspx?ProdID={0}", productColor.Product.ProdID);
            }
        }
        return "";
    }

  

    protected void btnShowByStatus_Click(object sender, EventArgs e)
    {
        OrderParameter = OrderSelectParameter.OrderStatus;
        BindOrders();
    }

    protected void BindOrders()
    {
        List<Order> orders = null;
        using (OrdersRepository lRepository = new OrdersRepository())
        {
            switch (OrderParameter)
            {
                case OrderSelectParameter.OrderStatus:
                    DateTime fromDate = DateTime.Parse(txtFromDate.Text);
                    DateTime toDate = DateTime.Parse(txtToDate.Text);
                    int orderStatusId = int.Parse(ddlOrderStatus.SelectedValue);
                    orders = lRepository.GetOrdersByDateRange(fromDate, toDate, orderStatusId);
                    break;
                    case OrderSelectParameter.FIO:
                    string FIO = txtFIO.Text;
                    orders = lRepository.GetOrdersByFIO(FIO);
                    break;
                    case OrderSelectParameter.User:
                    string user = txtLogin.Text;
                    orders = lRepository.GetOrdersByUserName(user);
                    break;
                    case OrderSelectParameter.Email:
                    string email=txtEmail.Text;
                    orders = lRepository.GetOrdersByEmail(email);
                    break;
                    case OrderSelectParameter.Number:
                    string orderNumber = txtNumber.Text;
                    orders = lRepository.GetOrdersByOrderNumber(orderNumber);
                    break;
                case OrderSelectParameter.City:
                    int city = int.Parse(ddlCities.SelectedValue);
                    orders = lRepository.GetOrdersByCity(city);
                    break;
            }
        }
        lvOrders.DataSource = orders;
        lvOrders.DataBind();
        if(orders.Count<=pagerBottom.PageSize)
            pnlPager.Visible = false;
        else
            pnlPager.Visible = true;
    }

    protected void lvOrders_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindOrders();
    }
    protected void btnShowByFIO_Click(object sender, EventArgs e)
    {
        OrderParameter = OrderSelectParameter.FIO;
        BindOrders();
    }
    protected void btnShowByUser_Click(object sender, EventArgs e)
    {
        OrderParameter = OrderSelectParameter.User;
        BindOrders();
    }
    protected void btnShowByEmail_Click(object sender, EventArgs e)
    {
        OrderParameter = OrderSelectParameter.Email;
        BindOrders();
    }
    protected void btnNumber_Click(object sender, EventArgs e)
    {
        OrderParameter = OrderSelectParameter.Number;
        BindOrders();
    }
    protected void btnCity_Click(object sender, EventArgs e)
    {
        OrderParameter = OrderSelectParameter.City;
        BindOrders();
    }

    protected void ClearOrders()
    {
        lvOrders.DataSource = null;
        lvOrders.DataBind();
    }
    protected void lvOrders_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        lvOrders.SelectedIndex = e.NewSelectedIndex;
        BindOrders();
    }

    protected void lbtnSelect_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton) sender;
        int orderId = int.Parse(lbtn.CommandArgument);
        BindOrder(orderId);
    }

    protected void BindOrder(int orderId)
    {
        pnlOrdersList.Visible = false;
        pnlOrder.Visible = true;
        OrderId = orderId;
        OrderCtrl.OrderId = orderId;
        OrderCtrl.BindOrder();
        lnkPrint.NavigateUrl = string.Format("~/Admin/OrderPrint.aspx?OrderId={0}", OrderId); ;
    }

    protected void ClearOrder()
    {
        OrderId = 0;
        pnlOrder.Visible = false;
        OrderCtrl.ClearOrder();
        lvOrders.SelectedIndex = -1;
        BindOrders();
        pnlOrdersList.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearOrder();
    }

    protected void UpdateOrder()
    {
        OrderCtrl.UpdateOrder();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateOrder();
        ClearOrder();
    }
    protected void lvOrders_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int orderId = int.Parse(lvOrders.DataKeys[e.ItemIndex].Value.ToString());
        using (OrdersItemRepository lOIreRepository =new OrdersItemRepository())
        {
            List<OrdersItem> ordersItems = lOIreRepository.GetOrderItemsByOrderId(orderId);
            foreach (OrdersItem ordersItem in ordersItems)
                lOIreRepository.DeleteOrderItem(ordersItem);
            BindOrders();
        }
        using (OrdersRepository lRepository=new OrdersRepository())
        {
            lRepository.DeleteOrder(orderId);
            BindOrders();
        }
    }
}
