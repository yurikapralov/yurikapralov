using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL.Info;

public partial class News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
               "Новости компании Эхо Голливуда";
        Master.MainHeader = "Новости компании Эхо Голливуда";
        BindNews();
    }

    protected void BindNews()
    {
        using (echoNewsRepository rep = new echoNewsRepository())
        {
            rptNews.DataSource = rep.GetNews();
            rptNews.DataBind();
        }
    }
}