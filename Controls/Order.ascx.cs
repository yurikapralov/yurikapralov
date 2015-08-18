using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL.Orders;
using echo.BLL.Products;

public partial class Controls_Order : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private int _orderId = 0;

    public int OrderId
    {
        get { return _orderId; }
        set { _orderId = value; }
    }

    public bool ViewOrderStatus
    {
        get { return pnlViewStatus.Visible; }
        set { pnlViewStatus.Visible = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Page.RegisterRequiresControlState(this);
    }

    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[])savedState;
        base.LoadControlState(ctlState[0]);
        _orderId = (int)ctlState[1];
    }

    protected override object SaveControlState()
    {
        object[] ctlState = new object[2];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = _orderId;
        return ctlState;
    }

    public void BindOrder()
    {
        //pnlOrdersList.Visible = false;
        using (OrdersRepository lRepository = new OrdersRepository())
        {
            //OrderId = orderId;
            Order order = lRepository.GetOrderById(OrderId);
            //pnlOrder.Visible = true;
            lblOrderNumber.Text = string.Format("Заказ № {0}", order.OrderNumber);
            lblAddedDate.Text = order.DateCreated.ToString();
            lblUser.Text = order.AddedBy;
            lblFIO.Text = order.FIO;
            if (order.DeliverTypeID == 4)
            {
                lblAddress.Text = order.Country.CountryNameRU + ", " + order.Adress;
            }
            else
            {
                lblAddress.Text = order.CityIndex + " " + order.Country.CountryNameRU + ", " + order.City.City_RUS;
                if (order.CityTypeID == 2)
                    lblAddress.Text += "-Территория области, г." + order.City2;
                lblAddress.Text += string.Format(" ул. {0}, дом {1}", order.Street, order.Home);
                if (order.Korpus != "")
                    lblAddress.Text += string.Format(" кор./стр. {0}", order.Korpus);
                if (order.Unit != "")
                    lblAddress.Text += string.Format(" кв./оф. {0}", order.Unit);
            }
            lblPhone.Text = order.Phone;
            lblEmail.Text = order.Email;
            lblTime.Text = string.Format("с: {0} по:{1}", order.time1, order.time2);
            lblNote.Text = order.Note;
            rptOrderItems.DataSource = order.OrdersItems;
            rptOrderItems.DataBind();
            lblOrderSum.Text = string.Format("{0:C}", order.OrderSum);
            switch (order.DeliverTypeID)
            {
                case 1:
                    lblDeliver.Text = string.Format("Курьерская служба: {0:C}", order.DeliverSum);
                    break;
                case 2:
                    lblDeliver.Text = "Наложенный платеж";
                    break;
                case 3:
                    lblDeliver.Text = string.Format("Доставка по Москве: {0:C}", order.DeliverSum);
                    break;
                case 4:
                    lblDeliver.Text = "Доставка за рубеж";
                    break;
                case 5:
                    lblDeliver.Text = "Доставка из США";
                    break;
            }
            lblTotalSum.Text = string.Format("{0:C}", order.TotalSum);
            BindOrderStatuses(ddlStatusEdit);
            ddlStatusEdit.SelectedValue = order.OrderStatusId.ToString();
        }
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

    public void ClearOrder()
    {
        OrderId = 0;
        //pnlOrder.Visible = false;
        lblOrderNumber.Text = "";
        lblAddedDate.Text = "";
        lblUser.Text = "";
        lblFIO.Text = "";
        lblAddress.Text = "";
        lblPhone.Text = "";
        lblEmail.Text = "";
        lblTime.Text = "";
        lblNote.Text = "";
        rptOrderItems.DataSource = null;
        rptOrderItems.DataBind();
        lblOrderSum.Text = "";
        lblDeliver.Text = "";

        lblTotalSum.Text = "";

        ddlStatusEdit.DataSource = null;
        ddlStatusEdit.DataBind();
       // lvOrders.SelectedIndex = -1;
       // BindOrders();
      //  pnlOrdersList.Visible = true;
    }

   

    public void UpdateOrder()
    {
        using (OrdersRepository lRepository = new OrdersRepository())
        {
            if (OrderId == 0)
                return;
            Order order = lRepository.GetOrderById(OrderId);
            int newOrderStatusId = int.Parse(ddlStatusEdit.SelectedValue);
            if (order.OrderStatusId != newOrderStatusId)
            {
                order.OrderStatusId = newOrderStatusId;
                order.DateUpdated = DateTime.Now;
                order = lRepository.AddOrder(order);
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
    
}
