using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InformationRepository
/// </summary>
/// 
namespace echo.BLL.Info
{

    public class InformationRepository : BaseInfoRepository
    {
        public InformationRepository()
        {
            CacheKey = "Info";
        }

        public InformationRepository(string connectionString):base(connectionString)
        {
            CacheKey = "Info";
        }

        public string GetInfo()
        {
            string key = CacheKey;

            if (EnableCaching && Cache[key] != null)
                return Cache[key].ToString();

            string info = Infoctx.Informations.Select(p => p.News).FirstOrDefault();

            if(EnableCaching)
                CacheData(key,info,CacheDuration);

            return info;
        }

        public bool UpdateInfo(string info)
        {
            try
            {
                Information information = Infoctx.Informations.FirstOrDefault();
                information.News = info;
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
