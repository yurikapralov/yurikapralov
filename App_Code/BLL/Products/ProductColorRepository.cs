using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for ProductColorRepository
    /// </summary>
    public class ProductColorRepository:BaseProductRepository
    {
        public ProductColor GetProductColorById(int productColorId)
        {
            return Productctx.ProductColors.Include("Color").Include("Product").Where(p => p.ProdColorID == productColorId).FirstOrDefault();
        }

        public List<ProductColor> GetProductColorsByProduct(int productId)
        {
            return Productctx.ProductColors.Include("Color").Where(p => p.Product.ProdID == productId).ToList();
        }


        public List<ProductColor> GetAllProductColors()
        {
            string key = CacheKey+"AllProductColors";

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>)Cache[key];

            Productctx.ProductColors.MergeOption = MergeOption.NoTracking;

            List<ProductColor> productColors =
                Productctx.ProductColors.Include("Color").Include("Product").OrderBy(p => p.Product.SortedName).ToList();

            if (EnableCaching)
                CacheData(key, productColors, CacheDuration);

            return productColors;
        }

        public List<ProductColor> GetActiveProductColors()
        {
            string key = CacheKey + "ActiveProductColors";

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>)Cache[key];

            Productctx.ProductColors.MergeOption = MergeOption.NoTracking;

            List<ProductColor> productColors =
                Productctx.ProductColors.Include("Color").Include("Product").Where(p => p.Product.Available).OrderBy(p => p.Product.SortedName).ToList();

            if (EnableCaching)
                CacheData(key, productColors, CacheDuration);

            return productColors;
        }

        public List<ProductColor> GetAllProductColorsByProductType(int productType)
        {
            string key = CacheKey + "AllProductColorsByProductType="+productType;

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>)Cache[key];

            Productctx.ProductColors.MergeOption = MergeOption.NoTracking;

            List<ProductColor> productColors =
                Productctx.ProductColors.Include("Color").Include("Product").Where(p => p.Product.ProdTypeID == productType).OrderBy(p => p.Product.SortedName).ToList();

            if (EnableCaching)
                CacheData(key, productColors, CacheDuration);

            return productColors;
        }

        public List<ProductColor> GetActiveProductColorsByProductType(int productType)
        {
            string key = CacheKey + "ActiveProductColorsByProductType=" + productType;

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>)Cache[key];

            Productctx.ProductColors.MergeOption = MergeOption.NoTracking;

            List<ProductColor> productColors =
                Productctx.ProductColors.Include("Color").Include("Product").Where(p => p.Product.ProdTypeID == productType).Where(p => p.Product.Available).OrderBy(p => p.Product.SortedName).ToList();

            if (EnableCaching)
                CacheData(key, productColors, CacheDuration);

            return productColors;
        }


        protected List<ProductColor> GetListProductColorFromProductIdList(List<int> prodIdList)
        {
            List<ProductColor> productColors = new List<ProductColor>();
            foreach (int prodId in prodIdList)
            {
                Productctx.ProductColors.MergeOption = MergeOption.NoTracking;
                productColors.AddRange(
                    Productctx.ProductColors.Include("Color").Include("Product").Where(p => p.Product.ProdID == prodId).OrderBy(p => p.Color.ColorNameRus).ToList());
            }
            return productColors;
        }

        public List<ProductColor> GetAllProductColorsByCathegory(int catId)
        {
            string key = CacheKey+"AllProductColorsByCathegory=" + catId;

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>) Cache[key];

            Productctx.Products.MergeOption = MergeOption.NoTracking;

            List<int> prodIds = (from lProduct in Productctx.Products
                           join lCollection in Productctx.Collections
                               on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                           from lp in lprodcol
                           where lp.Cathegory.CatID == catId
                           orderby lp.Product.SortedName 
                           select lp.Product.ProdID).ToList();
            List<ProductColor> productColors = GetListProductColorFromProductIdList(prodIds);

            if(EnableCaching)
                CacheData(key,productColors,CacheDuration);

            return productColors;
        }

        public List<ProductColor> GetActiveProductColorsByCathegory(int catId)
        {
            string key = CacheKey+"ActiveProductColorsByCathegory=" + catId;

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>)Cache[key];

            Productctx.Products.MergeOption = MergeOption.NoTracking;

            List<int> prodIds = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.SortedName
                                 select lp.Product.ProdID).ToList();
            List<ProductColor> productColors = GetListProductColorFromProductIdList(prodIds);

            if (EnableCaching)
                CacheData(key, productColors, CacheDuration);

            return productColors;
        }

        public List<ProductColor> GetAllProductColorsByGroup(int groupId)
        {
            string key = CacheKey + "AllProductColorsByGroup=" + groupId;

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>)Cache[key];

            Productctx.Products.MergeOption = MergeOption.NoTracking;

            List<int> prodIds = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.SortedName
                                 select lp.Product.ProdID).ToList();
            List<ProductColor> productColors = GetListProductColorFromProductIdList(prodIds);

            if (EnableCaching)
                CacheData(key, productColors, CacheDuration);

            return productColors;
        }


        public List<ProductColor> GetActiveProductColorsByGroup(int groupId)
        {
            string key = CacheKey + "ActiveProductColorsByGroup=" + groupId;

            if (EnableCaching && Cache[key] != null)
                return (List<ProductColor>)Cache[key];

            Productctx.Products.MergeOption = MergeOption.NoTracking;

            List<int> prodIds = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available
                                 orderby lp.Product.SortedName
                                 select lp.Product.ProdID).ToList();
            List<ProductColor> productColors = GetListProductColorFromProductIdList(prodIds);

            if (EnableCaching)
                CacheData(key, productColors, CacheDuration);

            return productColors;
        }

        public ProductColor GetProductColorByProductAndColor(int productId, int colorId)
        {
            
            return
                Productctx.ProductColors.Include("Product").Include("Color").Where(p => p.Product.ProdID == productId).Where(p => p.Color.ColorID == colorId)
                    .First();
        }

        public List<Color> GetColorsByProduct(int productId)
        {
            return (from lProdColor in Productctx.ProductColors
                    where lProdColor.Product.ProdID == productId
                    select lProdColor.Color).ToList();
        }

        public ProductColor AddProductColor(ProductColor productColor)
        {
            try
            {
                if(productColor.EntityState==EntityState.Detached)
                    Productctx.AddToProductColors(productColor);
                PurgeCacheItems(CacheKey);
                return Productctx.SaveChanges() > 0 ? productColor : null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool DeleteProductColor(ProductColor productColor)
        {
            try
            {
                using (ProductSizeRepository lRepository =new ProductSizeRepository())
                {
                    lRepository.DeleteProdSizeByProdColorId(productColor.ProdColorID);
                }
                Productctx.DeleteObject(productColor);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteProductColor(int productColorId)
        {
            return DeleteProductColor(GetProductColorById(productColorId));
        }

      
    }
}
