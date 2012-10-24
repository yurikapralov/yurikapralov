using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for Order
    /// </summary>
    public partial  class Order:IBaseEntity
    {
        private string _setName = "Order";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(OrderSum>=0 && DeliverSum>=0 && TotalSum>=0 && (CityTypeID==0 ||CityTypeID==1) 
                    && DeliverTypeID>0 && DeliverTypeID <=4)
                    return true;
                return false;
            }
        }

        public int CityId
        {
            get
            {
                if(CityReference.EntityKey !=null)
                {
                    return int.Parse(CityReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(CityReference.EntityKey!=null)
                    CityReference.EntityKey = null;
                CityReference.EntityKey=new EntityKey("OrdersEntities.Cities","CityID",value);
            }
        }

        public int CountryId
        {
            get
            {
                if(CountryReference.EntityKey !=null)
                {
                    return int.Parse(CountryReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(CountryReference.EntityKey!=null)
                    CountryReference.EntityKey = null;
                CountryReference.EntityKey=new EntityKey("OrdersEntities.Countries","CountryID",value);
            }
        }

        public int OrderStatusId
        {
            get
            {
                if (OrderStatusReference.EntityKey != null)
                    return int.Parse(OrderStatusReference.EntityKey.EntityKeyValues[0].Value.ToString());
                return 0;
            }
            set
            {
                if(OrderStatusReference.EntityKey!=null)
                    OrderStatusReference.EntityKey = null;
                OrderStatusReference.EntityKey = new EntityKey("OrdersEntities.OrderStatuses", "OrderStatusID", value);
            }
        }
    }
}