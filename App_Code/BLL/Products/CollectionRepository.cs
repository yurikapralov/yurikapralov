using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CollectionRepository
/// </summary>
namespace echo.BLL.Products
{
    public class CollectionRepository:BaseProductRepository
    {

        public Collection GetCollectionById(int collectionId)
        {
            return Productctx.Collections.Where(p => p.CollID == collectionId).FirstOrDefault();
        }

        public List<Collection> GetCollectionByProductId(int productId)
        {
            return Productctx.Collections.Where(p => p.Product.ProdID == productId).ToList();
        }

        public List<Collection> GetCollectionByCategoryId(int categoryId)
        {
            return Productctx.Collections.Where(p => p.Cathegory.CatID == categoryId).ToList();
        }

       public Collection AddCollection(Collection collection)
       {
           try
           {
               if(collection.EntityState==EntityState.Detached)
                   Productctx.AddToCollections(collection);
               PurgeCacheItems(CacheKey);
               return Productctx.SaveChanges() > 0 ? collection : null;
           }
           catch (Exception ex)
           {
               return null;
           }
       }
        

        public bool DeleteCollection(Collection collection)
        {
            try
            {
                Productctx.DeleteObject(collection);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool  DeleteCollectionByCat(int categoryId)
        {
            bool flag = true;
            List<Collection> collections = GetCollectionByCategoryId(categoryId);
            foreach(Collection collection in collections)
            {
                if(!DeleteCollection(collection))
                    flag = false;
            }
            return flag;
        }

        public bool DeleteCollectionByProd(int productId)
        {
            bool flag = true;
            List<Collection> collections =GetCollectionByProductId(productId);
            foreach (Collection collection in collections)
            {
                if (!DeleteCollection(collection))
                    flag = false;
            }
            return flag;
        }



    }
}
