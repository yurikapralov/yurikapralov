using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using echo.BLL;

public partial class Platinum_Platinum : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadXMLMenu();
        lblShopCartCount.Text = Profile.ShoppingCart.Count.ToString();
    }

    private void LoadXMLMenu()
    {
        string xmlFile = Server.MapPath("~/SiteMap.xml");

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFile);
      /*  lblNavigation.Text = "<ul id='menu'><li><a href='NewProducts.aspx?ptype=0' style='border-left-style:none;'>Новинки</a><ul>" +
                               // XMLMenu.GetPlatinumMenu(doc.ChildNodes, 0,true)+
                               "<li><a href='NewProducts.aspx?ptype=1'>Обувь  - Новинки </a></li><li><a href='NewProducts.aspx?ptype=2'>Сапоги  - Новинки </a></li>"+
                                "</ul></li>"
                                + XMLMenu.GetPlatinumMenu(doc.ChildNodes, 0,false) +"</ul>";*/
        lblNavigation.Text = "<ul id='menu'>"+XMLMenu.GetPlatinumNewMenu()+ XMLMenu.GetPlatinumMenu(doc.ChildNodes, 0, false)
             + XMLMenu.GetPlatinumMenu(doc.ChildNodes, 0, true) 
            + "</ul>";
    }
}
