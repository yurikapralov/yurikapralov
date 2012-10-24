using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Info
{
    /// <summary>
    /// Summary description for MoscowDelivery
    /// </summary>
    public partial  class MoscowDelivery:IBaseEntity 
    {
        private string _setName = "MoscowDelivery";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(Price >=0)
                    return true;
                return false;
            }
        }
    }
}
