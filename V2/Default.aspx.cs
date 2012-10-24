using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class V2_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MainTitle = "Добро пожаловать в интернет-магазин обуви, одежды и нижего белья.";
        Master.DefaultNavButtonValue = "SelectedMainNavigationButton";
    }
}
