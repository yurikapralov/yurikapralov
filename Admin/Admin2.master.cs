using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Admin_Admin2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string HeaderText
    {
        get
        {
            return lblHeader.Text;
        }
        set
        {
            lblHeader.Text = value;
        }
    }
    public string Title
    {
        get
        {
            return Page.Header.Title;

        }
        set
        {
            Page.Header.Title = value + " - Система администрирования сайта";
        }

    }

    protected string GetMenu()
    {
        if (Roles.IsUserInRole("Administrators"))
        {
            return @"<li id='m_users'><a href='ManageUsers.aspx'>Пользователи</a></li>
                      <li id='m_groups'><a href='Groups.aspx'>Разделы</a></li>
                      <li id='m_cats'><a href='Cathegories.aspx'>Категории</a></li>
                      <li id='m_prods'><a href='Products.aspx'>Продукция</a></li>
                      <li id='m_plats'><a href='Platforms.aspx'>Платформы, Размеры, Цвета</a></li>
                      <li id='m_orders'><a href='Orders.aspx'>Заказы</a></li>
                      <li id='m_ships'><a href='Shipping.aspx'>Доставка</a></li>
                      <li id='m_info'><a href='Info.aspx'>Прочее</a></li>";
        }
        if (Roles.IsUserInRole("Seo"))
        {
            return @"<li id='m_groups'><a href='Groups.aspx'>Разделы</a></li>
                      <li id='m_cats'><a href='Cathegories.aspx'>Категории</a></li>
                      <li id='m_prods'><a href='Products.aspx'>Продукция</a></li>";
        }
        return "";
    }
}
