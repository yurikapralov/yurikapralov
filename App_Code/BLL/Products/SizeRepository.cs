using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for SizeRepository
    /// </summary>
    public class SizeRepository:BaseProductRepository 
    {
        public List<Size> GetSizes()
        {
            string key = CacheKey + "Sizes";

            if(EnableCaching && Cache[key]!=null)
            {
                return (List<Size>) Cache[key];
            }

            Productctx.Sizes.MergeOption = MergeOption.NoTracking;
            List<Size> sizes = Productctx.Sizes.ToList();

            if(EnableCaching)
            {
                CacheData(key,sizes,CacheDuration);
            }

            return sizes;
        }

        public List<Size> GetSizes(int productType)
        {
            string key = CacheKey + "Sizes_" + productType;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Size>)Cache[key];
            }


            Productctx.Sizes.MergeOption = MergeOption.NoTracking;
            List<Size> sizes = Productctx.Sizes.Where(p => p.ProdTypeID == productType).ToList();

            if (EnableCaching)
            {
                CacheData(key, sizes, CacheDuration);
            }

            return sizes;
        }

        public List<Size> GetSizes(ProductType productType)
        {
            string key = CacheKey + "Sizes_" + productType;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Size>)Cache[key];
            }

            int intProdType = (int) productType;

            Productctx.Sizes.MergeOption = MergeOption.NoTracking;
            List<Size> sizes = Productctx.Sizes.Where(p=>p.ProdTypeID==intProdType).ToList();

            if (EnableCaching)
            {
                CacheData(key, sizes, CacheDuration);
            }

            return sizes;
        }

        public Size GetSizeById(int sizeId)
        {
            string key = CacheKey + "_SizeById=" + sizeId;

            if (EnableCaching && Cache[key] != null)
                return (Size) Cache[key];

            Size size = Productctx.Sizes.Where(p => p.SizeID == sizeId).FirstOrDefault();

            if (EnableCaching)
            {
                CacheData(key, size, CacheDuration);
            }

            return size;
        }

        public List<string> GetListProductNamesUsingSize(int sizeId)
        {
            string key = CacheKey + "_ProductNamesUsingSizeId=" + sizeId;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<string>)Cache[key];
            }

            var  productColor = (from productSize in Productctx.ProductSizes
                                         where productSize.Size.SizeID == sizeId
                                         select productSize.ProductColor);

            List<string> productNames = (from pc in productColor
                                         select pc.Product.ProductNameRus).ToList();

           if(EnableCaching)
               CacheData(key,productNames,CacheDuration);

            return productNames;
        }

        public Size AddSize(Size size)
        {
            try
            {
                if(size.EntityState==EntityState.Detached)
                    Productctx.AddToSizes(size);
                PurgeCacheItems(CacheKey);

                return Productctx.SaveChanges() > 0 ? size : null;
            }
            catch (Exception ex)
            {
              ActiveExceptions.Add(CacheKey+"_size_"+size.SizeID,ex);
                return null;
            }
        }

        public bool DeleteSize(Size size)
        {
            //Нельзя удалять размер с идентификатором 24
            if(size.SizeID==24 || size.SizeID==25)
                return false;
            if(GetListProductNamesUsingSize(size.SizeID).Count !=0)
                return false;
            try
            {
                Productctx.DeleteObject(size);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSize(int sizeId)
        {
            return DeleteSize(GetSizeById(sizeId));
        }
    }
}
