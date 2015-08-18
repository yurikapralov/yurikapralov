using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Orders;
using echo.BLL.Products;
using System.Web.Security;
using System.Web.Profile;


public enum OrderSelectParameter
{
    OrderStatus, FIO, Email, User, Number, City
}

public partial class Admin_Orders : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.Title = "Управление заказами";
            Master.HeaderText = "Управление заказами";
            Initialisation();

            BindOrders();

            //Очистка профиля от старых записей - могла быть в любом другом месте сайта
            //ProfileManager.DeleteInactiveProfiles(ProfileAuthenticationOption.Anonymous, DateTime.Now.AddDays(-7));
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

    protected void Initialisation()
    {
        BindOrderStatuses(ddlOrderStatus);
        txtToDate.Text = DateTime.Now.ToShortDateString();
        DateTime fromDate = DateTime.Now.Subtract(new TimeSpan(Helpers.Settings.Store.DefaultOrderListInterval, 0, 0, 0));
        txtFromDate.Text = fromDate.ToShortDateString();
        using (CityRepository lRepository = new CityRepository())
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
        string key = "";
        string script = "";
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
                    ltlHeader.Text = string.Format("Результат поиска: {0}, с {1} по {2}", ddlOrderStatus.SelectedItem.Text, txtFromDate.Text, txtToDate.Text);
                    //key="ShowStat";
                    //script = "$('#order_search_tab a[href=\"#stat_search\"]').tab('show');";
                    break;
                case OrderSelectParameter.FIO:
                    string FIO = txtFIO.Text;
                    orders = lRepository.GetOrdersByFIO(FIO);
                    ltlHeader.Text = "Результат поиска по имени: " + FIO;
                    key="ShowName";
                    script = "$('#order_search_tab a[href=\"#name_search\"]').tab('show');";
                    break;
                case OrderSelectParameter.User:
                    string user = txtLogin.Text;
                    orders = lRepository.GetOrdersByUserName(user);
                    ltlHeader.Text = "Результат поиска по логину: " + user;
                    key="ShowUser";
                    script = "$('#order_search_tab a[href=\"#user_search\"]').tab('show');";
                    break;
                case OrderSelectParameter.Email:
                    string email = txtEmail.Text;
                    orders = lRepository.GetOrdersByEmail(email);
                    ltlHeader.Text = "Результат поиска по email: " + email;
                    key="ShowEmail";
                    script = "$('#order_search_tab a[href=\"#email_search\"]').tab('show');";
                    break;
                case OrderSelectParameter.Number:
                    string orderNumber = txtNumber.Text;
                    orders = lRepository.GetOrdersByOrderNumber(orderNumber);
                    ltlHeader.Text = "Результат поиска по номеру заказа: " + orderNumber;
                    key="ShowNumber";
                    script = "$('#order_search_tab a[href=\"#nmbr_search\"]').tab('show');";
                    break;
                case OrderSelectParameter.City:
                    int city = int.Parse(ddlCities.SelectedValue);
                    orders = lRepository.GetOrdersByCity(city);
                    ltlHeader.Text = "Результат поиска по городу: " + ddlCities.SelectedItem.Text;
                    key="ShowCity";
                    script = "$('#order_search_tab a[href=\"#city_search\"]').tab('show');";
                    break;
            }
        }
        lvOrders.DataSource = orders;
        lvOrders.DataBind();
        if(!string.IsNullOrEmpty(key))
            ScriptManager.RegisterStartupScript(uppnlTotal, typeof(string), key, script, true);
        if (orders.Count <= pagerBottom.PageSize)
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
        LinkButton lbtn = (LinkButton)sender;
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
        using (OrdersItemRepository lOIreRepository = new OrdersItemRepository())
        {
            List<OrdersItem> ordersItems = lOIreRepository.GetOrderItemsByOrderId(orderId);
            foreach (OrdersItem ordersItem in ordersItems)
                lOIreRepository.DeleteOrderItem(ordersItem);
            BindOrders();
        }
        using (OrdersRepository lRepository = new OrdersRepository())
        {
            lRepository.DeleteOrder(orderId);
            BindOrders();
        }
    }
}