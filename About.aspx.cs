using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class About : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "О Компании Эхо Голливуда";
        Master.MainHeader = "О Компании Эхо Голливуда";
    }
}