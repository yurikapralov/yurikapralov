using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Info
{
    /// <summary>
    /// Summary description for MoscowDeliveryRepository
    /// </summary>
    public class MoscowDeliveryRepository:BaseInfoRepository
    {
        public MoscowDeliveryRepository()
        {
            CacheKey = "MoscowDelivery";
        }

         public MoscowDeliveryRepository(string connectionString):base(connectionString)
        {
             CacheKey = "MoscowDelivery";
        }

        public decimal GetMoscowDeliverPrice()
        {
            string key = CacheKey;

            if (EnableCaching && Cache[key] != null)
                return (decimal) Cache[key];
            decimal price = Infoctx.MoscowDeliveris.Select(p=>p.Price).FirstOrDefault();

            if(EnableCaching)
                CacheData(key,price,CacheDuration);

            return price;
        }

        public bool UpdateDeliverPrice(decimal price)
        {
            try
            {
                MoscowDelivery md = Infoctx.MoscowDeliveris.FirstOrDefault();
                md.Price = price;
                PurgeCacheItems(CacheKey);
                return (Infoctx.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                return false;
            }        

        }
    }
}
