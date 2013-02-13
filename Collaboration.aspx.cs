using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class Collaboration : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
                "Приглашение к сотрудничеству";
        Master.MainHeader = "Приглашение к сотрудничеству";
    }
}