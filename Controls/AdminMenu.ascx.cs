using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_AdminMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
            BindMenu();
    }

    protected void BindMenu()
    {
        List<AdminMenuItem> menu=new List<AdminMenuItem>();
        menu.Add(new AdminMenuItem("Пользователи","ManageUsers.aspx"));
        menu.Add(new AdminMenuItem("Разделы", "Groups.aspx"));
        menu.Add(new AdminMenuItem("Категории", "Cathegories.aspx"));
        menu.Add(new AdminMenuItem("Продукция", "Products.aspx"));
        menu.Add(new AdminMenuItem("Платформы, Размеры, Цвета", "Platforms.aspx"));
        menu.Add(new AdminMenuItem("Заказы", "Orders.aspx"));
        menu.Add(new AdminMenuItem("Доставка", "Shipping.aspx"));
        menu.Add(new AdminMenuItem("Прочее", "Info.aspx"));

        string filename = this.Page.Request.AppRelativeCurrentExecutionFilePath.Replace("~/Admin/", "");

        StringBuilder sb = new StringBuilder();
        sb.Append("<ul class='glossymenu'>");
        foreach(AdminMenuItem menuItem in menu)
        {
            if (menuItem.MenuUrl == filename)
                sb.Append("<li class='current'>");
            else
                sb.Append("<li>");
            sb.Append("<a href='" + menuItem.MenuUrl + "'><b>" + menuItem.MenuName + "</b></a></li>");
        }
        sb.Append("</ul>");
        lblMenu.Text = sb.ToString();
    }


}

public class AdminMenuItem
{
    public AdminMenuItem(string menuName, string menuUrl)
    {
        MenuName = menuName;
        MenuUrl = menuUrl;
    }
    
    public string MenuName{ get; set;}
    public string MenuUrl { get; set;}
}
