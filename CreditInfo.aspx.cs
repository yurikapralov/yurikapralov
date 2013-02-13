using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreditInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Покупка в кредит";
        Master.MainHeader = "Покупка в кредит";
    }
}