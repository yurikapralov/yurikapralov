using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Color
    /// </summary>
    public partial  class Color:IBaseEntity
    {

        private string _setName = "Colors";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(ColorNameEng) && !string.IsNullOrEmpty(ColorNameRus))
                    return true;
                return false;
            }
        }

        public string FullColorName
        {
            get
            {
                return ColorNameRus + " - " + ColorNameEng;
            }
        }
    }
}
