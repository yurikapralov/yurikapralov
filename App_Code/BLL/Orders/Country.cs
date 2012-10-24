using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for Country
    /// </summary>
    public partial  class Country:IBaseEntity
    {
        private string _setName = "Country";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrEmpty(CountryNameRU) && !string.IsNullOrEmpty(CountryNameEN))
                    return true;
                return false;
            }
        }
    }
}
