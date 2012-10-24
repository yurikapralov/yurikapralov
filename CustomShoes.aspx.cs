using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;

public partial class CustomShoes : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title =
                "Обувь на заказ, изготовление и пошив женской обуви на заказ в Москве";
        Master.MainHeader = "Обувь на заказ";
        CreateMetaControl("description", "Специально для женщин, предпочитающих единичные индивидуальные изделия, мы предлагаем пошив обуви на заказ.");
        CreateMetaControl("keywords", "обувь на заказ, пошив обуви на заказ, обувь на заказ москва, изготовление обуви на заказ");
    }
}