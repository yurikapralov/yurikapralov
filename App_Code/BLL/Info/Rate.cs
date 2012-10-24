using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Rate
/// </summary>
namespace echo.BLL.Info
{

    public partial class Rate : IBaseEntity
    {
        private string _setName = "Rate";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (RateUS > 0)
                    return true;
                return false;
            }
        }

        public string Currency
        {
            get
            {
                switch (Id)
                {
                    case 1:
                        return "$";
                        break;
                    case 2:
                        return "EUR";
                        break;
                }
                return "";
            }
        }
    }
}
