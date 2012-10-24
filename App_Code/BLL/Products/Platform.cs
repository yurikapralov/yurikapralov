using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Platform
    /// </summary>
    public partial  class Platform:IBaseEntity
    {
        private string _setName = "Platforms";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(PlatformNameEng) && !string.IsNullOrEmpty(PlatformNameRus))
                    return true;
                return false;
            }
        }

        
    }
}
