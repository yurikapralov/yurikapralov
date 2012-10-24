using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
   

    /// <summary>
    /// Summary description for TemplateRepository
    /// </summary>
    public class TemplateRepository:BaseProductRepository
    {

        public List<Template> GetTemplates()
        {
            string key = CacheKey + "Templates";
            if (EnableCaching && Cache[key] != null)
                return (List <Template>) Cache[key];

            Productctx.Groups.MergeOption = MergeOption.NoTracking;
            List<Template> templ = Productctx.Templates.ToList();

            if (EnableCaching)
                CacheData(key, templ, CacheDuration);

            return templ;
        }

        public Template GetTemplateById(int templateId)
        {
            string key = Cache + "TemplateId=" + templateId.ToString();

            if (EnableCaching && Cache[key] != null)
                return (Template)Cache[key];

            Productctx.Groups.MergeOption = MergeOption.NoTracking;
            Template templ = Productctx.Templates.Where(p => p.TempleID == templateId).FirstOrDefault();

            if (EnableCaching)
                CacheData(key, templ, CacheDuration);

            return templ;
        }

       
    }
}
