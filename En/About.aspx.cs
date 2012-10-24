using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class En_About : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MainHeader = "About Echo Of Hollywood";
        Master.Title = "About Echo Of Hollywood";
    }
}