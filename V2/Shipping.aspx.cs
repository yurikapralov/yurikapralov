using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class V2_Shipping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.HeaderTitle = "Доставка по Москве и регионам.";
        Master.MainTitle = "Доставка по Москве и регионам.";
        Master.DeliverNavButtonValue = "SelectedMainNavigationButton";
    }
}
