using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RateRepository
/// </summary>
/// 
namespace echo.BLL.Info
{

    public class RateRepository : BaseInfoRepository
    {
        public RateRepository()
        {
            CacheKey = "Rate";
        }

        public RateRepository(string connectionString):base(connectionString)
        {
            CacheKey = "Rate";
        }

        public List<Rate> GetRates()
        {
            string key = CacheKey + "All";

            if(EnableCaching && Cache[key]!=null)
            {
                return (List<Rate>) Cache[key];
            }
            Infoctx.Rates.MergeOption = MergeOption.NoTracking;
            List<Rate> rates = Infoctx.Rates.ToList();

            if(EnableCaching)
            {
                CacheData(key,rates,CacheDuration);
            }

            return rates;
        }

        public Rate GetRateById(int rateId)
        {
            string key = CacheKey + "_Id=" + rateId;

            if(EnableCaching && Cache[key]!=null)
            {
                return (Rate) Cache[key];
            }
            Rate rate = Infoctx.Rates.Where(p => p.Id == rateId).FirstOrDefault();

            if(EnableCaching)
                CacheData(key,rate,CacheDuration);

            return rate;
        }

        public bool UpdateRate(int id, decimal newRate)
        {
            try
            {
                Rate rate = GetRateById(id);
                rate.RateUS = newRate;
                PurgeCacheItems(CacheKey);
                return (Infoctx.SaveChanges() > 0);
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
