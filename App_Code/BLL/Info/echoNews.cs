using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для News
/// </summary>

namespace echo.BLL.Info
{


    public partial class echoNews : IBaseEntity
    {

        private string _setName = "echoNews";

        public bool  IsValid
        {
	        get
	        {
                if (NewsDate > DateTime.MinValue)
                    return true;
	            return false;
	        }
        }

        public string  SetName
        {
            get { return _setName; }
            set {_setName=value;}
        }
    }
}