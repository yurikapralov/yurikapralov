using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class V2_Stores : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.HeaderTitle = "Адреса фирменных магазинов - салонов обуви и одежды 'Echo Of Hollywood'.";
        Master.MainTitle = "Адреса фирменных магазинов - салонов обуви и одежды 'Echo Of Hollywood'.";
        Master.ShopNavButtonValue = "SelectedMainNavigationButton";
    }
}
