using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Teplate
    /// </summary>
    public partial  class Template:IBaseEntity
    {

        private string _setName = "Templates";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(TempleItemURL) && !string.IsNullOrEmpty(TempleName) 
                    && !string.IsNullOrEmpty(TempleURL) && !string.IsNullOrEmpty(Theme))
                    return true;
                return false;
            }
        }
    }
}