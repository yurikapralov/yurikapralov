using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Перечесление указывающее поле сортировки платформы
    /// </summary>
    public enum PlatformSortField
    {
        DateCreated, Name
    }

    /// <summary>
    /// Перечесление указывающее тип сортировки платформы
    /// </summary>
    public enum PlatformSortType
    {
        Asc, Desc
    }
    /// <summary>
    /// Summary description for PlatformRepository
    /// </summary>
    public class PlatformRepository:BaseProductRepository
    {
       public Platform GetPlatformByProduct(int ProductId)
       {
           return (from lProduct in Productctx.Products
                   where lProduct.ProdID == ProductId
                   select lProduct.Platform).FirstOrDefault();
       }

        public Platform GetPlatformById(int platformId)
        {
            string key = CacheKey + "_PlatformById=" + platformId.ToString();

            if (EnableCaching && Cache[key] != null)
                return (Platform) Cache[key];

            Platform platform = Productctx.Platforms.Where(p => p.PlatformID == platformId).FirstOrDefault();

            if(EnableCaching)
                CacheData(key,platform,CacheDuration);

            return platform;
        }

        public List<string> GetListProductNamesUsingPlatform(int platformId)
        {
            string key = CacheKey + "_ProductNamesUsingPlatformId=" + platformId.ToString();

            if (EnableCaching && Cache[key] != null)
                return (List<string>)Cache[key];

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<string> productnames = (from product in Productctx.Products.Include("Platform")
                                         where product.Platform.PlatformID == platformId
                                         select product.ProductNameRus).ToList();

            if (EnableCaching)
                CacheData(key, productnames, CacheDuration);

            return productnames;
        }

        public List<Platform> GetPlatforms(PlatformSortField sortField ,PlatformSortType sortType)
        {
            string key=CacheKey+"_Platforms_sortedBy_"+sortField+"_"+sortType;

            if(EnableCaching && Cache[key]!=null)
                return (List<Platform>)Cache[key];

            Productctx.Platforms.MergeOption = MergeOption.NoTracking;
            List<Platform> lplatforms = null;

            switch (sortField)
            {
                case PlatformSortField.DateCreated:
                    if(sortType==PlatformSortType.Asc)
                        lplatforms = Productctx.Platforms.OrderBy(p => p.DateCreated).ToList();
                    else
                        lplatforms = Productctx.Platforms.OrderByDescending(p => p.DateCreated).ToList();
                    break;

                case PlatformSortField.Name:
                    if (sortType == PlatformSortType.Asc)
                        lplatforms = Productctx.Platforms.OrderBy(p => p.PlatformNameRus).ToList();
                    else
                        lplatforms = Productctx.Platforms.OrderByDescending(p => p.PlatformNameEng).ToList();
                    break;
            }

            if(EnableCaching)
                CacheData(key,lplatforms,CacheDuration);

            return lplatforms;
        }

        public List<Platform> GetPlatforms()
        {
            return GetPlatforms(PlatformSortField.Name, PlatformSortType.Asc);
        }

        public Platform AddPlatform(Platform platform)
        {
            try
            {
                if(platform.EntityState==EntityState.Detached)
                    Productctx.AddToPlatforms(platform);
                PurgeCacheItems(CacheKey);

                return Productctx.SaveChanges() > 0 ? platform : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey+"_platform_"+platform.PlatformID,ex);
                return null;
            }
        }

        public bool DeletePlatform(Platform platform)
        {
            if(GetListProductNamesUsingPlatform(platform.PlatformID).Count!=0)
                return false;
            try
            {
                Productctx.DeleteObject(platform);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePlatform(int platformId)
        {
            return DeletePlatform(GetPlatformById(platformId));
        }
    }
}
