using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for ProductSizeRepository
    /// </summary>
    public class ProductSizeRepository:BaseProductRepository
    {
        public List<ProductSize> GetProductSizeByProductColor(int productColorId)
        {
            return
                Productctx.ProductSizes.Include("Size").Where(p => p.ProductColor.ProdColorID == productColorId).ToList();
        }

        public ProductSize GetProductSizeById(int productSizeId)
        {
            return Productctx.ProductSizes.Include("Size").Where(p => p.ProdSizeID == productSizeId).FirstOrDefault();
        }


        public ProductSize AddProductSize(ProductSize productSize)
        {
            try
            {
                if(productSize.EntityState==EntityState.Detached)
                    Productctx.AddToProductSizes(productSize);
                PurgeCacheItems(CacheKey);

                return Productctx.SaveChanges() > 0 ? productSize : null;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool DeleteProdSize(ProductSize productSize)
        {
            try
            {
                Productctx.DeleteObject(productSize);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteProdSizeByProdColorId(int prodcolorId)
        {
            bool flag = true;
            List<ProductSize> productSizes = GetProductSizeByProductColor(prodcolorId);
            foreach (ProductSize productSize in productSizes)
            {
                if(!DeleteProdSize(productSize))
                    flag = flag;
            }
            return flag;
        }
    }
}
