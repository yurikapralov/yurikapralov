using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class En_Default_old : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        Master.MainHeader = "Echo Of Hollywood - Shop Online of Shoes and Clothes.";
        Master.ContentStyle = "Content2";
        Master.HeaderImage = "Header.jpg";       
    }
}
