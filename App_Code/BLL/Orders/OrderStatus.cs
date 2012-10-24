using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using echo.BLL;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for OrderStatus
    /// </summary>
    public partial  class OrderStatus : IBaseEntity
    {
        private string _setName = "OrderStatus";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(OrderStaus))
                    return true;
                return false;
            }
        }
    }
}
