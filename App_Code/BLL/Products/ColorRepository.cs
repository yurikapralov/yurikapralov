using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for ColorRepository
    /// </summary>
    public class ColorRepository:BaseProductRepository
    {

        public List<Color> GetColors()
        {
            return GetColors(true);
        }


       public List<Color> GetColors(bool all)
       {
           string key = CacheKey + "_Colors_all="+all.ToString();

           if(EnableCaching && Cache[key]!=null)
           {
               return (List<Color>) Cache[key];
           }

           Productctx.Colors.MergeOption = MergeOption.NoTracking;
           List<Color> colors = null;
           if(all)
              colors = Productctx.Colors.OrderBy(p => p.ColorNameRus).ToList();
           else
               colors = Productctx.Colors.Where(p => p.IsMain == true).OrderBy(p => p.ColorNameRus).ToList();

           if(EnableCaching)
           {
               CacheData(key,colors,CacheDuration);
           }

           return colors;
       }

       public Color GetColorById(int ColorId)
       {
           string key = CacheKey + "_ColorById=" + ColorId;

           if (EnableCaching && Cache[key] != null)
               return (Color) Cache[key];

           Color color = Productctx.Colors.Where(p => p.ColorID == ColorId).FirstOrDefault();

           if (EnableCaching)
           {
               CacheData(key, color, CacheDuration);
           }

           return color;
       }

        public List<string> GetListProductNamesUsingColor(int colorId)
        {
            string key = CacheKey + "_ProductNamesUsingColorId=" + colorId;

            if (EnableCaching && Cache[key] != null)
                return (List<string>) Cache[key];

            Productctx.ProductColors.MergeOption = MergeOption.NoTracking;
            List<string> productNames = (from productColor in Productctx.ProductColors.Include("Product")
                                         where productColor.Color.ColorID == colorId
                                         select productColor.Product.ProductNameRus).ToList();

            if(EnableCaching)
                CacheData(key,productNames,CacheDuration);

            return productNames;
        }

        public bool IsUsedColorName(string colorNameRus, string colorNameEng)
        {
            List<Color> colors = GetColors();
            foreach(Color color in colors)
            {
                if(color.ColorNameRus.ToLower()==colorNameRus.ToLower() && color.ColorNameEng.ToLower()==colorNameEng.ToLower())
                    return true;
               
            }
            return false;
        }

        public int GetColorIdByColorNameRusAndColorNameEng(string colorNameRus, string colorNameEng)
        {
            
           Color  color =   Productctx.Colors.Where(p => p.ColorNameRus.ToLower() == colorNameRus.ToLower()).Where(
                    p => p.ColorNameEng.ToLower() == colorNameEng.ToLower()).FirstOrDefault();
            return color.ColorID;
        }

        public Color AddColor(Color color)
        {
            try
            {
                if(color.EntityState==EntityState.Detached)
                    Productctx.AddToColors(color);
                PurgeCacheItems(CacheKey);

                return Productctx.SaveChanges() > 0 ? color : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey+"_color_"+color.ColorID,ex);
                return null;
            }
        }

        public bool DeleteColor(Color color)
        {
            if(GetListProductNamesUsingColor(color.ColorID).Count!=0)
                return false;
            try
            {
                Productctx.DeleteObject(color);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteColor(int colorId)
        {
            return DeleteColor(GetColorById(colorId));
        }
    }
}