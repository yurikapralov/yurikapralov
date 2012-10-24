using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class V2_About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.HeaderTitle = "О торговой марке 'Echo Of Hollywood'.";
        Master.MainTitle = "О торговой марке 'Echo Of Hollywood'.";
        Master.AboutNavButtonValue = "SelectedMainNavigationButton";
    }
}
