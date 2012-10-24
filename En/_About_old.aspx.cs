using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class En_About_old : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.Title =
                "About Echo Of Hollywood";
            Master.MainHeader = "About Echo Of Hollywood";
            Master.ContentStyle = "Content2";
            Master.HeaderImage = "Header.jpg";
        }
    }
}
