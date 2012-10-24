using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class Shipping : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Доставка по Москве и регионам.";
        Master.MainHeader = "Оплата и Доставка";
    }
}