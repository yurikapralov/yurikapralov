using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Перечисление позволяющее выбрать тип сортировки
    /// </summary>
    public enum ProductSorted
    {
        NameAsc,NameDesc,PriceAsc,PriceDesc,DateAsc,DateDesc,UpdateAsc,UpdateDesc
    }

    /// <summary>
    /// Summary description for ProductRepository
    /// </summary>
    public class ProductRepository:BaseProductRepository 
    {
        
        public Product GetProductById(int productId)
        {
            string key = CacheKey + "_ProductById=" + productId;

            if (EnableCaching && Cache[key] != null)
            {
                return (Product)Cache[key];
            }

            Product product = Productctx.Products.Include("Platform").Where(p => p.ProdID == productId).FirstOrDefault();

            if(EnableCaching)
                CacheData(key,product,CacheDuration);
            return product;
        }

        public ProductColor GetProductByProdSizeId(int prodsizeId)
        {
          
            try
            {
                int prodcolorId =
               Productctx.ProductSizes.Include("ProductColor").Where(p => p.ProdSizeID == prodsizeId).Select(p => p.ProductColor.ProdColorID).
                   FirstOrDefault();
                ProductColor product =
                Productctx.ProductColors.Include("Product").Include("Color").Where(p => p.ProdColorID == prodcolorId).FirstOrDefault();
                return product;
            }
            catch (Exception)
            {

                return null;
            }
           
            
        }


        public List<Product> GetActiveProducts(ProductSorted sorted)
        {
            string key = CacheKey + "_ActiveProducts_SortedBy_"+sorted;

            if(EnableCaching && Cache[key]!=null)
            {
                return (List<Product>) Cache[key];
            }

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;
            
            switch(sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderBy(p=>p.DateCreated).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderByDescending(p => p.DateCreated).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderBy(p => p.SortedName).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderByDescending(p => p.SortedName).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderBy(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderByDescending(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderBy(p => p.DateUpdated).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = Productctx.Products.Where(p => p.Available).OrderByDescending(p => p.DateUpdated).ToList();
                    break;
            }

            if(EnableCaching)
                CacheData(key,lProducts,CacheDuration);

            return lProducts;
            
        }

        public List<Product> GetAllProducts()
        {
            return GetActiveProducts(ProductSorted.DateAsc);
        }

        public List<Product> GetAllProducts(ProductSorted sorted)
        {
            string key = CacheKey + "_AllProducts_SortedBy_" + sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = Productctx.Products.OrderBy(p => p.DateCreated).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = Productctx.Products.OrderByDescending(p => p.DateCreated).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = Productctx.Products.OrderBy(p => p.SortedName).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = Productctx.Products.OrderByDescending(p => p.SortedName).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = Productctx.Products.OrderBy(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = Productctx.Products.OrderByDescending(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = Productctx.Products.OrderBy(p => p.DateUpdated).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = Productctx.Products.OrderByDescending(p => p.DateUpdated).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);
            return lProducts;

        }

        public List<Product> GetAllProductsByProductType(int productType, ProductSorted sorted)
        {
            string key = CacheKey + "_AllProductsPrType="+productType+"_SortedBy_" + sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderBy(p => p.DateCreated).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderByDescending(p => p.DateCreated).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderBy(p => p.SortedName).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderByDescending(p => p.SortedName).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderBy(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderByDescending(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderBy(p => p.DateUpdated).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).OrderByDescending(p => p.DateUpdated).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);
            return lProducts;

        }

        public List<Product> GetActiveProductsByProductType(int productType, ProductSorted sorted)
        {
            string key = CacheKey + "_ActiveProductsPrType=" + productType + "_SortedBy_" + sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderBy(p => p.DateCreated).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderByDescending(p => p.DateCreated).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderBy(p => p.SortedName).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderByDescending(p => p.SortedName).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderBy(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderByDescending(p => p.OrigPrice).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderBy(p => p.DateUpdated).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = Productctx.Products.Where(p => p.ProdTypeID == productType).Where(p => p.Available).OrderByDescending(p => p.DateUpdated).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);

            return lProducts;

        }

        public List<Product> GetNewPlatinumProductsByPType(int pType, ProductSorted sorted)
        {
            //pType - тип новинок.
            //Возможные значения:
            //0-Вся продукция
            //1-Обувь
            //2-Сапоги
            string key = CacheKey + "_NewPlatinumProductsByPType=" + pType + "_SortedBy_" + sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            //Формируем список групп отображенных в платинум
         /*   List<int> groups = new List<int>();
            if (Helpers.Settings.PlatinumProduct.PlatinumGroupId > 0)
                groups.Add(Helpers.Settings.PlatinumProduct.PlatinumGroupId);
            if (Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId != null)
                foreach (int gid in Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId)
                    groups.Add(gid);
            if (groups == null)
                return null;*/

            int groupId = Helpers.Settings.PlatinumProduct.PlatinumGroupId;
            DateTime period = DateTime.Now.AddDays(-90);

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;
          

            switch (sorted)
            {
                case ProductSorted.DateAsc:
              /*      var lprodcol=from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID
                                     select ;*/

                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated>period && lp.Cathegory.Template.TempleID==pType
                                 orderby lp.Product.DateCreated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated > period
                                 orderby lp.Product.DateCreated descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated > period
                                 orderby lp.Product.SortedName
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated > period
                                 orderby lp.Product.SortedName descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated > period
                                 orderby lp.Product.OrigPrice
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated > period
                                 orderby lp.Product.OrigPrice descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated > period
                                 orderby lp.Product.DateUpdated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.DateCreated > period
                                 orderby lp.Product.DateUpdated descending
                                 select lp.Product).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);

            return lProducts;

        }




        public List<Product> GetSearchProducts(string searchtext, ProductSorted sorted, int productType)
        {
            string key = CacheKey +"_searchedby_"+searchtext+"_prodtype_"+productType+ "_SortedBy_" + sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            if (productType == 1 || productType == 2)
            {
                switch (sorted)
                {
                    case ProductSorted.DateAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderBy(p => p.DateCreated).ToList();
                        break;
                    case ProductSorted.DateDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderByDescending(p => p.DateCreated).ToList();
                        break;
                    case ProductSorted.NameAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderBy(p => p.SortedName).ToList();
                        break;
                    case ProductSorted.NameDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderByDescending(p => p.SortedName).ToList();
                        break;
                    case ProductSorted.PriceAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderBy(p => p.OrigPrice).ToList();
                        break;
                    case ProductSorted.PriceDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderByDescending(p => p.OrigPrice).ToList();
                        break;
                    case ProductSorted.UpdateAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderBy(p => p.DateUpdated).ToList();
                        break;
                    case ProductSorted.UpdateDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).Where(
                                p => p.ProdTypeID == productType).OrderByDescending(p => p.DateUpdated).ToList();
                        break;
                }
            }
            else
            {
                switch (sorted)
                {
                    case ProductSorted.DateAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderBy(p => p.DateCreated).ToList();
                        break;
                    case ProductSorted.DateDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderByDescending(p => p.DateCreated).ToList();
                        break;
                    case ProductSorted.NameAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderBy(p => p.SortedName).ToList();
                        break;
                    case ProductSorted.NameDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderByDescending(p => p.SortedName).ToList();
                        break;
                    case ProductSorted.PriceAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderBy(p => p.OrigPrice).ToList();
                        break;
                    case ProductSorted.PriceDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderByDescending(p => p.OrigPrice).ToList();
                        break;
                    case ProductSorted.UpdateAsc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderBy(p => p.DateUpdated).ToList();
                        break;
                    case ProductSorted.UpdateDesc:
                        lProducts =
                            Productctx.Products.Where(p => p.SortedName.ToLower().Contains(searchtext.ToLower())).OrderByDescending(p => p.DateUpdated).ToList();
                        break;
                }
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);
            return lProducts;

        }

        public List<Product> GetActiveProducts()
        {
            return GetActiveProducts(ProductSorted.DateAsc);
        }

        public List<Product> GetActiveProductsByCategory(int catId, ProductSorted sorted)
        {
            string key = CacheKey + "_ActiveProducts_ByCatId="+catId.ToString()+"_SortedBy_"+sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }


            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.DateCreated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.DateCreated descending 
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.SortedName
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.SortedName descending 
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.OrigPrice
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.OrigPrice descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.DateUpdated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId && lp.Product.Available
                                 orderby lp.Product.DateUpdated descending
                                 select lp.Product).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);

            return lProducts;

           
        }

        public List<Product> GetActiveProductsByCategory(int catId)
        {
            return GetActiveProductsByCategory(catId, ProductSorted.DateAsc);
        }

        public List<Product> GetAllProductsByCategory(int catId, ProductSorted sorted)
        {
            string key = CacheKey + "_AllProducts_ByCatId=" + catId.ToString() + "_SortedBy_" + sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }


            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId 
                                 orderby lp.Product.DateCreated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId 
                                 orderby lp.Product.DateCreated descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId 
                                 orderby lp.Product.SortedName
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId 
                                 orderby lp.Product.SortedName descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId 
                                 orderby lp.Product.OrigPrice
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId 
                                 orderby lp.Product.OrigPrice descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId
                                 orderby lp.Product.DateUpdated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.CatID == catId
                                 orderby lp.Product.DateUpdated descending
                                 select lp.Product).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);

            return lProducts;


        }

        public List<Product> GetAllProductsByCategory(int catId)
        {
            return GetAllProductsByCategory(catId, ProductSorted.DateAsc);
        }

        public List<Product> GetNewProducts(int productType)
        {

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

           lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections.Include("Group").Include("Template")
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == 2 && lp.Product.ProdTypeID==productType && lp.Cathegory.Template.TempleID!=2
                         select lp.Product).ToList();
            lProducts = MixList(lProducts);
            return lProducts.GetRange(0,3);


        }

        public List<Product> GetSaleProducts(int productType)
        {

            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections.Include("Group").Include("Template")
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == 10 && lp.Product.ProdTypeID == productType && lp.Cathegory.Template.TempleID != 2
                         select lp.Product).ToList();
            lProducts = MixList(lProducts);
            return lProducts.GetRange(0, 3);


        }

        private List<T> MixList<T>(List<T> inputList)
        {
            List<T> randomList = new List<T>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        public List<Product> GetActiveProductsByGroup(int groupId, ProductSorted sorted)
        {
            string key = CacheKey + "_ActiveProducts_ByGroupId="+groupId.ToString()+"_SortedBy_"+sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }


            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID  == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.DateCreated
                         select lp.Product).ToList();
            break;
                case ProductSorted.DateDesc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.DateCreated descending
                         select lp.Product).ToList();
            break;
                case ProductSorted.NameAsc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.SortedName
                         select lp.Product).ToList();
            break;
                case ProductSorted.NameDesc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.SortedName descending
                         select lp.Product).ToList();
            break;
                case ProductSorted.PriceAsc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.OrigPrice
                         select lp.Product).ToList();
            break;
                case ProductSorted.PriceDesc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.OrigPrice descending
                         select lp.Product).ToList();
            break;
                case ProductSorted.UpdateAsc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.DateUpdated
                         select lp.Product).ToList();
            break;
                case ProductSorted.UpdateDesc:
            lProducts = (from lProduct in Productctx.Products
                         join lCollection in Productctx.Collections
                             on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                         from lp in lprodcol
                         where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Cathegory.InGeneralLink
                         orderby lp.Product.DateUpdated descending
                         select lp.Product).ToList();
            break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);

            return lProducts;
        }

        public List<Product> GetAllProductsByGroup(int groupId, ProductSorted sorted)
        {
            string key = CacheKey + "_AllProducts_ByGroupId=" + groupId.ToString() + "_SortedBy_" + sorted;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }


            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.DateCreated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.DateCreated descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.SortedName
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.SortedName descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.OrigPrice
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.OrigPrice descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.DateUpdated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId
                                 orderby lp.Product.DateUpdated descending
                                 select lp.Product).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);

            return lProducts;
        }

        public List<Product> GetActiveProductsByGroupAndProdType(int groupId, ProductSorted sorted, int prodType)
        {
            if (!(prodType == 1 || prodType == 2))
                return GetActiveProductsByGroup(groupId, sorted);

            string key = CacheKey + "_ActiveProducts_ByGroupId=" + groupId.ToString() + "_SortedBy_" + sorted+"_prodType="+prodType;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Product>)Cache[key];
            }


            Productctx.Products.MergeOption = MergeOption.NoTracking;
            List<Product> lProducts = null;

            switch (sorted)
            {
                case ProductSorted.DateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID==prodType && lp.Cathegory.Template.TempleID !=2
                                 orderby lp.Product.DateCreated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.DateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID == prodType && lp.Cathegory.Template.TempleID != 2
                                 orderby lp.Product.DateCreated descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID == prodType && lp.Cathegory.Template.TempleID != 2
                                 orderby lp.Product.SortedName
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.NameDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID == prodType && lp.Cathegory.Template.TempleID != 2
                                 orderby lp.Product.SortedName descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID == prodType && lp.Cathegory.Template.TempleID != 2
                                 orderby lp.Product.OrigPrice
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.PriceDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID == prodType && lp.Cathegory.Template.TempleID != 2
                                 orderby lp.Product.OrigPrice descending
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateAsc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID == prodType && lp.Cathegory.Template.TempleID != 2
                                 orderby lp.Product.DateUpdated
                                 select lp.Product).ToList();
                    break;
                case ProductSorted.UpdateDesc:
                    lProducts = (from lProduct in Productctx.Products
                                 join lCollection in Productctx.Collections
                                     on lProduct.ProdID equals lCollection.Product.ProdID into lprodcol
                                 from lp in lprodcol
                                 where lp.Cathegory.Group.GroupID == groupId && lp.Product.Available && lp.Product.ProdTypeID == prodType && lp.Cathegory.Template.TempleID != 2
                                 orderby lp.Product.DateUpdated descending
                                 select lp.Product).ToList();
                    break;
            }

            if (EnableCaching)
                CacheData(key, lProducts, CacheDuration);

            return lProducts;
        }

        public List<Product> GetActiveProductsByGroup(int groupId)
        {
            return GetActiveProductsByGroup(groupId, ProductSorted.DateAsc);
        }

        public List<int> GetCategoryIDListByProduct(int productId)
        {
            string key = CacheKey + "_CategoryIDListByProductId=" + productId;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<int>)Cache[key];
            }

            List<int> catIds = (from lColection in Productctx.Collections
                                where lColection.Product.ProdID == productId
                                select lColection.Cathegory.CatID).ToList();
            if(EnableCaching)
                CacheData(key,catIds,CacheDuration);

            return catIds;
 
        }

        public Product AddProduct(Product product)
        {
            try
            {
                if(product.EntityState==EntityState.Detached)
                    Productctx.AddToProducts(product);
                PurgeCacheItems(CacheKey);

                return Productctx.SaveChanges() > 0 ? product : null;
            }
            catch (Exception ex)
            {

                ActiveExceptions.Add(CacheKey + "_product_" + product.ProdID, ex);
                return null;
            }
        }

        public bool DeleteProduct(Product product)
        {
            try
            {
                using (ProductColorRepository lRepository=new ProductColorRepository())
                {
                    List<ProductColor> productColors = lRepository.GetProductColorsByProduct(product.ProdID);
                    foreach (ProductColor productColor in productColors)
                    {
                        lRepository.DeleteProductColor(productColor);
                    }
                }
                using (CollectionRepository lCollectionRepository=new CollectionRepository())
                {
                    lCollectionRepository.DeleteCollectionByProd(product.ProdID);
                }
                Productctx.DeleteObject(product);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteProduct(int productId)
        {
            return DeleteProduct(GetProductById(productId));
        }
    }
}
