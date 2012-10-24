using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Information
/// </summary>
/// 
namespace echo.BLL.Info
{

    public partial  class Information:IBaseEntity
    {
        private string _setName = "Info";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get { return true; }
        }
    }
}
