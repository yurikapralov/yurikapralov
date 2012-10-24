using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin : System.Web.UI.MasterPage
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
   /* public string LeftHeader
    {
        get
        {
            return lblLeftHeader.Text;
        }
        set
        {
            lblLeftHeader.Text = value;
        }
    }
    public string RightHeader
    {
        get
        {
            return lblRightHeader.Text;
        }
        set
        {
            lblRightHeader.Text = value;
        }
    }*/
}
