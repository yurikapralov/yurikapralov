using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class Stores : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Адреса магазинов-салонов Эхо Голливуда;";
        Master.MainHeader = HttpUtility.HtmlDecode("Адреса фирменных магазинов - салонов обуви и одежды \"Echo&nbsp;Of&nbsp;Hollywood\"");
    }
}