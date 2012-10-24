using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class Admin_OrderPrint : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            OrderCtrl.OrderId = OrderId;
            OrderCtrl.BindOrder();
        }
    }
}
