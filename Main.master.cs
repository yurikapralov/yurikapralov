using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using echo.BLL;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadXMLMenu();
        if(Title==string.Empty)
            header1.Title = Helpers.DefaultTitle;
        lblShopCartCount.Text = Profile.ShoppingCart.Count.ToString();
    }



    /// <summary>
    /// Функция загружающая меню
    /// </summary>
    private void LoadXMLMenu()
    {
        string xmlFile = Server.MapPath("~/SiteMap.xml");
      
        XmlDocument doc=new XmlDocument();
        doc.Load(xmlFile);
        lblNavigation.Text = XMLMenu.GetRusMenu(doc.ChildNodes, 0);
    }

    /// <summary>
    /// Свойство задающее заголовок
    /// </summary>
    public string MainHeader
    {
        get
        {
            return Header.InnerText;
        }
        set
        {
            Header.InnerHtml = string.Format("<h1>{0}</h1>", value);
        }
    }

    /// <summary>
    /// Свойство задающее стиль главного div
    /// </summary>
    public string  ContentStyle
    {
        get
        {
            return Content.Attributes["class"];
        }
        set
        {
            Content.Attributes["class"] = value;
        }
    }
    /// <summary>
    /// Свойство задающее заголовок страницы
    /// </summary>
    /// <returns></returns>
    public string Title
    {
        get
        {
            return header1.Title;
        }
        set
        {
            header1.Title = value + " - " + Helpers.DefaultTitle;
        }
    }

    
    /// <summary>
    /// Свойство задающее картинку под заголовком
    /// </summary>
    public string HeaderImage
    {
        set
        {
                RightColumn.Attributes["style"] = @"background-image: url(Images/Headers/" + value + ");";
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int productTypeSearch = 0;
        if (cbxShoes.Checked && !cbxClothers.Checked)
            productTypeSearch = 1;
        if (!cbxShoes.Checked && cbxClothers.Checked)
            productTypeSearch = 2;
        if(!cbxShoes.Checked && !cbxClothers.Checked)
            return;
        string searchtext = txtSearch.Text;
        if (searchtext=="")
            return;
        txtSearch.Text = "";
        Response.Redirect(string.Format("Search.aspx?SearchText={0}&ProdType={1}",searchtext,productTypeSearch));
    }
}
