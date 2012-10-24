using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using echo.BLL;

public partial class V2_MasterPageV2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadXMLMenu();
        lblShopCartCount.Text = Profile.ShoppingCart.Count.ToString();
    }



    /// <summary>
    /// Функция загружающая меню
    /// </summary>
    private void LoadXMLMenu()
    {
        string xmlFile = Server.MapPath("~/SiteMap.xml");

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFile);
        lblNavigation.Text = XMLMenu.GetV2Menu(doc.ChildNodes, 0);
    }

    public string MainTitle
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

    public string HeaderTitle
    {
        get
        {
            return Head1.Title;
        }
        set
        {
            Head1.Title = value + " - " + "Интернет-магазин женской Обуви, Одежды, Нижнего белья";
        }
    }

    public string DefaultNavButtonValue
    {
        get { return DefaultNavButton.Attributes["class"]; }
        set { DefaultNavButton.Attributes["class"] = value; }
    }

    public string BasketNavButtonValue
    {
        get { return BasketNavButton.Attributes["class"]; }
        set { BasketNavButton.Attributes["class"] = value; }
    }

    public string DeliverNavButtonValue
    {
        get { return DeliverNavButton.Attributes["class"]; }
        set { DeliverNavButton.Attributes["class"] = value; }
    }

    public string ShopNavButtonValue
    {
        get { return ShopNavButton.Attributes["class"]; }
        set { ShopNavButton.Attributes["class"] = value; }
    }

    public string AboutNavButtonValue
    {
        get { return AboutNavButton.Attributes["class"]; }
        set { AboutNavButton.Attributes["class"] = value; }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int productTypeSearch = 0;
        if (cbxShoes.Checked && !cbxClothers.Checked)
            productTypeSearch = 1;
        if (!cbxShoes.Checked && cbxClothers.Checked)
            productTypeSearch = 2;
        if (!cbxShoes.Checked && !cbxClothers.Checked)
            return;
        string searchtext = txtSearch.Text;
        if (searchtext == "")
            return;
        txtSearch.Text = "";
        Response.Redirect(string.Format("Search.aspx?SearchText={0}&ProdType={1}", searchtext, productTypeSearch));
    }
  
}
