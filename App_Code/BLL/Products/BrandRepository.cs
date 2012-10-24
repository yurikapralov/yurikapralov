using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data;


namespace echo.BLL.Products
{
    /// <summary>
    /// Сводное описание для BrandRepository
    /// </summary>
    public class BrandRepository : BaseProductRepository
    {
        public List<Brand> GetBrands()
        {
            string key = CacheKey + "_Brands";

            if (EnableCaching && Cache[key] != null)
                return (List<Brand>)Cache[key];

            Productctx.Brands.MergeOption = MergeOption.NoTracking;
            
            List<Brand> brands = null;
            brands = Productctx.Brands.OrderBy(p => p.BrandName).ToList();

            if (EnableCaching)
                CacheData(key, brands, CacheDuration);

            return brands;
        }

        public Brand GetBrandById(int brandId)
        {
            string key = CacheKey + "_BrandById="+brandId;

            if (EnableCaching && Cache[key] != null)
                return (Brand)Cache[key];

           Brand brand = Productctx.Brands.Where(p=>p.BrandId==brandId).FirstOrDefault();

            if (EnableCaching)
                CacheData(key, brand, CacheDuration);

            return brand;
        }

        public Brand AddBrand(Brand brand)
        {
            try
            {
                if (brand.EntityState == EntityState.Detached)
                    Productctx.AddToBrands(brand);
                PurgeCacheItems(CacheKey);
                return Productctx.SaveChanges() > 0 ? brand : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_brand_" + brand.BrandId, ex);
                return null;
            }
        }
    }
}