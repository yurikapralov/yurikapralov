using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Info;

public partial class Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Master.MainHeader = "Магазин женской обуви \"Echo Of Hollywood\"";
            Master.Title = "Женская обувь оптом | Интернет-магазин женской обуви больших размеров - Эхо Голливуда";
            BindNews();
        }
        CreateMetaControl("description", "В интернет-магазине Эхо Голливуда вы найдете богатый ассортимент женской обуви, купить которую можно и оптом, ведь мы являемся производителями продукции бренда Echo of Hollywood.");
        CreateMetaControl("keywords", "магазин женской обуви, женская обувь интернет магазин, интернет магазин женской обуви, магазин женской обуви больших размеров, обувь оптом, обувь оптом от производителя, купить обувь оптом, женская обувь оптом");
    }

    protected void BindNews()
    {
        using(echoNewsRepository rep=new echoNewsRepository())
        {
            rptNews.DataSource = rep.GetTop2News();
            rptNews.DataBind();
        }
    }
}