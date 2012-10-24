using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    public partial  class Group:IBaseEntity
    {

        private string _setName = "Groups";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(GroupNameEng) && !string.IsNullOrEmpty(GroupNameRus))
                    return true;
                return false;
            }
        }
    }
}
