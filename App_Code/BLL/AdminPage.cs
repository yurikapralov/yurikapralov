using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL
{
    /// <summary>
    /// Базовый класс для админских страниц
    /// </summary>
    public class AdminPage:BasePage 
    {
        public AdminPage()
        {
            base.PreInit += new EventHandler(AdminPage_PreInit);
        }

        void AdminPage_PreInit(object sender, EventArgs e)
        {
            MoveHiddenFileds = false;
        }
    }
}
