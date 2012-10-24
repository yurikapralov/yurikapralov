using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpRequest req = this.Context.Request;
        lblInfo.Text = "";
        lblInfo.Text += string.Format("Host: {0}<br/>", req.Url.Host);
        lblInfo.Text += string.Format("Url: {0}<br/>", req.Url.ToString());
        lblInfo.Text += string.Format("App: {0}<br/>", req.ApplicationPath);
        lblInfo.Text += string.Format("App: {0}<br/>", req.Url.OriginalString);
    }
}