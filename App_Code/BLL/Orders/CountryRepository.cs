using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{/// <summary>
    /// Summary description for CountryRepository
    /// </summary>
    public class CountryRepository:BaseOrdersRepository
    {
        public CountryRepository()
        {
            CacheKey = "Countries";
        }

        public CountryRepository(string connectionString):base(connectionString)
        {
            CacheKey = "Countries";
        }


        public List<Country> GetCountries()
        {
            string key = CacheKey;

            if (EnableCaching && Cache[key] != null)
                return (List<Country>) Cache[key];

            Ordersctx.Countries.MergeOption = MergeOption.NoTracking;
            List<Country> lCountries = (from lCountry in Ordersctx.Countries
                                        orderby lCountry.Sorted descending                                          
                                        select lCountry).ToList();

            if(EnableCaching)
                CacheData(key,lCountries,CacheDuration);

            return lCountries;
        }

        public Country GetCountryById(int countryId)
        {
            return (from lCountry in Ordersctx.Countries
                    where lCountry.CountryID == countryId
                    select lCountry).FirstOrDefault();
        }
    }
}