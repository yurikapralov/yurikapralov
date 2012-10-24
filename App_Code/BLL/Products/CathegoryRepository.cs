using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace echo.BLL.Products
{
    /// <summary>
    /// Перечесление указывающее величину размера превьюшек на странице
    /// </summary>
    public enum PageType
    {
        Short, Lohg, Unknown
    }

    /// <summary>
    /// Перечесление указывающее поле сортировки
    /// </summary>
    public enum CathegorySortField
    {
        DateCreated, DateUpdated, Name,GroupOrder,CatOrder
    }

    /// <summary>
    /// Перечесление указывающее тип сортировки
    /// </summary>
    public enum CathegorySortType
    {
        Asc, Desc
    }


    /// <summary>
    /// Summary description for CathegoryRepository
    /// </summary>
    public class CathegoryRepository:BaseProductRepository
    {


        public Cathegory GetCathegoryById(int categoryId)
        {
            try
            {

            
            string key = CacheKey + "_CathegoryByCategoryId=" + categoryId.ToString();

            if (EnableCaching && Cache[key] != null)
            {
                return (Cathegory)Cache[key];
            }

           // Productctx.Cathegories.MergeOption = MergeOption.NoTracking;
            Cathegory cathegory = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.CatID == categoryId).FirstOrDefault();

            if (EnableCaching)
                CacheData(key, cathegory, CacheDuration);
            return cathegory;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Cathegory> GetCathegories(CathegorySortField sortField, CathegorySortType sortType)
        {
            string key = CacheKey + "_Cathegories_sortedBy_" + sortField + "_" + sortType;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Cathegory>)Cache[key];
            }

           Productctx.Cathegories.MergeOption = MergeOption.NoTracking;
            List<Cathegory> lCathegories = null;
            switch (sortField)
            {
                case CathegorySortField.DateCreated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderBy(p => p.DateCreated).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderByDescending(p => p.DateCreated).ToList();
                    break;
                case CathegorySortField.DateUpdated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderBy(p => p.DateUpdated).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderByDescending(p => p.DateUpdated).ToList();
                    break;
                case CathegorySortField.Name:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderBy(p => p.CatNameRus).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderByDescending(p => p.CatNameRus).ToList();
                    break;
                case CathegorySortField.GroupOrder:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderBy(p => p.Group.GroupOrder).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderByDescending(p => p.Group.GroupOrder).ToList();
                    break;
                case CathegorySortField.CatOrder:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderBy(p => p.CatOrder).ThenBy(p => p.DateCreated).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").OrderByDescending(p => p.CatOrder).ThenByDescending(p=>p.DateCreated).ToList();
                    break;
            }


            if (EnableCaching)
                CacheData(key, lCathegories, CacheDuration);
            return lCathegories;
        }


        public List<Cathegory> GetCathegories(CathegorySortField sortField, CathegorySortType sortType, int pageIndex, int pageSize)
        {
            string key = CacheKey + "_Cathegories_sortedBy_" + sortField + "_" + sortType+"_"+pageIndex+"_"+pageSize;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Cathegory>)Cache[key];
            }

            Productctx.Cathegories.MergeOption = MergeOption.NoTracking;
            List<Cathegory> lCathegories = null;
            switch (sortField)
            {
                case CathegorySortField.DateCreated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").OrderBy(p => p.DateCreated).Skip(pageSize*pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").OrderByDescending(p => p.DateCreated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
                case CathegorySortField.DateUpdated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").OrderBy(p => p.DateUpdated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").OrderByDescending(p => p.DateUpdated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
                case CathegorySortField.Name:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").OrderBy(p => p.CatNameRus).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").OrderByDescending(p => p.CatNameRus).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
                case CathegorySortField.CatOrder:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").OrderBy(p => p.CatOrder).ThenBy(p => p.DateCreated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").OrderByDescending(p => p.CatOrder).ThenByDescending(p => p.DateCreated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
            }


            if (EnableCaching)
                CacheData(key, lCathegories, CacheDuration);
            return lCathegories;
        }


         public List<Cathegory> GetCathegories()
         {
             return GetCathegories(CathegorySortField.DateCreated, CathegorySortType.Asc);
         }

        public List<Cathegory> GetCathegoriesByProduct(int productId)
        {
            string key = CacheKey + "_CathegoryByProductId=" + productId;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Cathegory>)Cache[key];
            }

            Productctx.Cathegories.MergeOption = MergeOption.NoTracking;

            List<Cathegory> lCathegories = (from lCollection in Productctx.Collections.Include("Cathegory").Include("Product")
                                            where lCollection.Product.ProdID   == productId
                                            select lCollection.Cathegory).ToList();

            if (EnableCaching)
                CacheData(key, lCathegories, CacheDuration);
            return lCathegories;
        }

        public List<Cathegory> GetCathegoriesByProductExceptNew(int productId)
        {
            string key = CacheKey + "_CathegoryExceptNewByProductId=" + productId;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Cathegory>)Cache[key];
            }

            Productctx.Cathegories.MergeOption = MergeOption.NoTracking;

            List<Cathegory> lCathegories = (from lCollection in Productctx.Collections.Include("Cathegory").Include("Product")
                                            where lCollection.Product.ProdID == productId
                                            select lCollection.Cathegory).ToList();

            List<Cathegory> lret=new List<Cathegory>();
            //Удалим из списка все те категории, которые относятся к новинкам
            
            foreach (Cathegory lCathegory in lCathegories)
            {
                if (lCathegory.GroupId != 2)
                    lret.Add(lCathegory);
            }

            if (EnableCaching)
                CacheData(key, lret, CacheDuration);
            return lret;
        }

        public List<Cathegory> GetCathegoryByGroup(int groupId, CathegorySortField sortField, CathegorySortType sortType)
        {
            string key = CacheKey + "_CathegoryByGroupId=" + groupId.ToString() + "_sortedBy_" + sortField + "_" + sortType;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Cathegory>)Cache[key];
            }

            //Productctx.Cathegories.MergeOption = MergeOption.NoTracking;
            List<Cathegory> lCathegories = null;
            switch (sortField)
            {
                case CathegorySortField.DateCreated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.DateCreated).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.DateCreated).ToList();
                    break;
                case CathegorySortField.DateUpdated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.DateUpdated).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.DateUpdated).ToList();
                    break;
                case CathegorySortField.Name:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.CatNameRus).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.CatNameRus).ToList();
                    break;
                case CathegorySortField.GroupOrder:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.Group.GroupOrder).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.Group.GroupOrder).ToList();
                    break;
                case CathegorySortField.CatOrder:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.CatOrder).ThenBy(p => p.DateCreated).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Include("Group").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.CatOrder).ThenByDescending(p => p.DateCreated).ToList();
                    break;
            }


            if (EnableCaching)
                CacheData(key, lCathegories, CacheDuration);
            return lCathegories;
        }

        public List<Cathegory> GetCathegoryByGroup(int groupId, CathegorySortField sortField, CathegorySortType sortType, int pageIndex, int pageSize)
        {
            string key = CacheKey + "_CathegoryByGroupId=" + groupId.ToString() + "_sortedBy_" + sortField + "_" + sortType + "_" + pageIndex + "_" + pageSize;

            if (EnableCaching && Cache[key] != null)
            {
                return (List<Cathegory>)Cache[key];
            }

            Productctx.Cathegories.MergeOption = MergeOption.NoTracking;
            List<Cathegory> lCathegories = null;
            switch (sortField)
            {
                case CathegorySortField.DateCreated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.DateCreated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.DateCreated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
                case CathegorySortField.DateUpdated:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.DateUpdated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.DateUpdated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
                case CathegorySortField.Name:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.CatNameRus).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.CatNameRus).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
                case CathegorySortField.CatOrder:
                    if (sortType == CathegorySortType.Asc)
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderBy(p => p.CatOrder).ThenBy(p => p.DateCreated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    else
                        lCathegories = Productctx.Cathegories.Include("Template").Where(p => p.Group.GroupID == groupId).OrderByDescending(p => p.CatOrder).ThenByDescending(p => p.DateCreated).Skip(pageSize * pageIndex).Take(pageSize).ToList();
                    break;
            }


            if (EnableCaching)
                CacheData(key, lCathegories, CacheDuration);
            return lCathegories;
        }

        public List<Cathegory> GetCathegoryByGroup(int groupId)
        {
            return GetCathegoryByGroup(groupId, CathegorySortField.CatOrder, CathegorySortType.Asc);
        }

        public List<int> GetTemplatesIdsForPlatinumNews()
        {
            string key = CacheKey + "_CathegoryTemlatePlatinumNews";
            if (EnableCaching && Cache[key] != null)
            {
                return (List<int>)Cache[key];
            }

            //Формируем список групп отображенных в платинум
            List<int> groups=new List<int>();
            if(Helpers.Settings.PlatinumProduct.PlatinumGroupId>0)
                groups.Add(Helpers.Settings.PlatinumProduct.PlatinumGroupId);
            if (Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId != null)
                foreach (int gid in Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId)
                    groups.Add(gid);
            if (groups == null)
                return null;

            Productctx.Cathegories.MergeOption = MergeOption.NoTracking;
     
            var q1=Productctx.Cathegories.Where(Helpers.BuildContainsExpression<Cathegory, int>(p => p.Group.GroupID,groups));
            List<int> templateIds = q1.Select(p => p.Template.TempleID).Distinct().ToList();

            if (EnableCaching)
                CacheData(key, templateIds, CacheDuration);
            return templateIds;
        }

        

         public string GetThemeByCategoryId(int categoryId)
        {
             Productctx.Products.MergeOption = MergeOption.NoTracking;
             return (from lCategory in Productctx.Cathegories.Include("Template")
                     where lCategory.CatID == categoryId
                     select lCategory.Template.Theme).FirstOrDefault();
        }

        public string GetThemeByGroupId(int groupId)
        {
            List<Cathegory> cathegories = GetCathegoryByGroup(groupId);
            int switcher = 0;
            string theme = "";
            foreach (Cathegory cathegory in cathegories)
            {
                string currenttheme = GetThemeByCategoryId(cathegory.CatID);
                if(theme!=currenttheme)
                {
                    theme = currenttheme;
                    switcher++;
                }
            }
            if(switcher==1)
                return theme;
            return "Default";
        }

       
         public PageType GetPageTypeByCategory(int categoryId)
         {
             Productctx.Products.MergeOption = MergeOption.NoTracking;
             int templeId = (from lCategory in Productctx.Cathegories.Include("Template")
                             where lCategory.CatID == categoryId
                             select lCategory.Template.TempleID).FirstOrDefault();

             switch (templeId)
             {
                 case 1:
                 case 3:
                     return PageType.Short;
                     break;
                 case 2:
                 case 4:
                 case 5:
                 case 6:
                     return PageType.Lohg;
                     break;
                 default:
                     return PageType.Unknown;
                     break;
             }
         }

         public PageType GetPageTypeByGroupId(int groupId,int prodTypeId)
         {
             //Блок для новинок
             if(groupId==2)
             {
                 if(prodTypeId==1)
                     return PageType.Short;
                 if(prodTypeId==2)
                     return PageType.Lohg;
             }
             //поставим костыль для обуви для стриптиза:
             if (groupId == 3)
                 return PageType.Short;
             List<Cathegory> cathegories = GetCathegoryByGroup(groupId);
             PageType finishPageType= PageType.Unknown;
             int switcher = 0;
             foreach(Cathegory cathegory in cathegories)
             {
                 PageType currentPageType = GetPageTypeByCategory(cathegory.CatID);
                 if(finishPageType!=currentPageType)
                 {
                     finishPageType = currentPageType;
                     switcher++;
                 }
             }
             
             if (switcher == 1)
                 return finishPageType;

                 return PageType.Unknown;
         }


        public Cathegory AddCathegory(Cathegory cathegory)
        {
            try
            {
               if(cathegory.EntityState==EntityState.Detached)
                    Productctx.AddToCathegories(cathegory);
                PurgeCacheItems(CacheKey);

                return Productctx.SaveChanges() > 0 ? cathegory : null;
            }
            catch (Exception ex)
            {
               ActiveExceptions.Add(CacheKey +"_"+cathegory.CatID,ex);
                return null;
            }
        }

        public bool DeleteCathegory(Cathegory cathegory)
        {
            try
            {
                using (CollectionRepository lCollectionRepository = new CollectionRepository())
                {
                    lCollectionRepository.DeleteCollectionByCat(cathegory.CatID);
                }
                Productctx.DeleteObject(cathegory);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteCathegory(int categoryId)
        {
            return DeleteCathegory(GetCathegoryById(categoryId));
        }

        
    }
}
