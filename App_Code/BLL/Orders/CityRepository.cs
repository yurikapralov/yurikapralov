using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{
/// <summary>
/// Summary description for CityRepository
/// </summary>
public class CityRepository:BaseOrdersRepository
{
	public CityRepository()
	{
	    CacheKey = "Cities";
	}

    public CityRepository(string connectionString):base(connectionString)
    {
        CacheKey = "Cities";
    }

    public List<City> GetCities()
    {
        string key = CacheKey;

        if(EnableCaching && Cache[key]!=null)
        {
            return (List<City>) Cache[key];
        }

        Ordersctx.Cities.MergeOption = MergeOption.NoTracking;
        List<City> lCities = (from lCity in Ordersctx.Cities
                              where lCity.CityID != 253
                              select lCity).ToList();

        if(EnableCaching)
            CacheData(key,lCities,CacheDuration);

        return lCities;
    }

    public List<City> GetCities(int zone)
    {
        string key = CacheKey+"_zoneby_"+zone;

        if (EnableCaching && Cache[key] != null)
        {
            return (List<City>)Cache[key];
        }

        Ordersctx.Cities.MergeOption = MergeOption.NoTracking;
        List<City> lCities = (from lCity in Ordersctx.Cities
                              where lCity.ZN==zone
                              select lCity).ToList();

        if (EnableCaching)
            CacheData(key, lCities, CacheDuration);

        return lCities;
    }

    public City GetCityById(int cityId)
    {
        return (from lCity in Ordersctx.Cities
                where lCity.CityID == cityId
                select lCity).FirstOrDefault();
    }

    public bool IsUsedCityName(string cityNameRus, string cityNameEng)
    {
        List<City> cities = GetCities();
        foreach (City city in cities)
        {
            if (city.City_RUS.ToLower() == cityNameRus.ToLower() && city.City_ENG.ToLower() == cityNameEng.ToLower())
                return true;
        }
        return false;
    }

    public City AddCity(City city)
    {
        try
        {
            if(city.EntityState==EntityState.Detached)
                Ordersctx.AddToCities(city);
            PurgeCacheItems(CacheKey);

            return Ordersctx.SaveChanges() > 0 ? city : null;
        }
        catch (Exception ex)
        {
            ActiveExceptions.Add(CacheKey+"_orders_"+city.CityID,ex);
            return null;
        }
    }
}
}
