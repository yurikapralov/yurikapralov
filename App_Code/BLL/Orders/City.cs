using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for City
    /// </summary>
    public partial  class City:IBaseEntity 
    {
       
        private string _setName = "City";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get 
            { 
                if(!string.IsNullOrEmpty(City_RUS) && !string.IsNullOrEmpty(City_ENG) && ZN>=-1)
                    return true;
                return false;
            }
        }

        
    }
}