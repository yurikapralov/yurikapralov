using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using echo.BLL;

public partial class Theme2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Title == string.Empty)
            header1.Title = Helpers.DefaultTitle;
        AddDefaultEnter();
    }


    protected void AddDefaultEnter()
    {
        searchform.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('searchbtn').click();return false;}} else {return true}; ");
    }


    /// <summary>
    /// Функция загружающая меню
    /// </summary>
    protected string LoadXMLMenu()
    {
        string xmlFile = Server.MapPath("~/SiteMap.xml");

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFile);
        return XMLMenu.GetRusMenu2(doc.ChildNodes, 0);
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
            if (string.IsNullOrEmpty(value))
                header1.Title = Helpers.DefaultTitle;
            else
                header1.Title = value;
        }
    }

    protected string _mainHeader;

    /// <summary>
    /// Свойство задающее заголовок
    /// </summary>
    public string MainHeader
    {
        get
        {
            return _mainHeader;
        }
        set
        {
            _mainHeader = value;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int productTypeSearch = 0;
        if (cbshoes.Checked && !cbclothes.Checked)
            productTypeSearch = 1;
        if (!cbshoes.Checked && cbclothes.Checked)
            productTypeSearch = 2;
        if (!cbshoes.Checked && !cbclothes.Checked)
            return;
        string searchtext = searchform.Text;
        if (searchtext == "")
            return;
        searchform.Text = "Я ищу...";
        Response.Redirect(string.Format("Search.aspx?SearchText={0}&ProdType={1}", searchtext, productTypeSearch));
    }

}
