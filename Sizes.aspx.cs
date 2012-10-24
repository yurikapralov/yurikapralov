using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class Sizes : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Как снимать мерки для сапог.";
        Master.MainHeader = "Как снимать мерки для сапог.";
    }
}