using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    public partial  class Product:IBaseEntity 
    {

        private string _setName = "Products";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(OrigPrice>=0 && (ProdTypeID==1 || ProdTypeID==2) && !string.IsNullOrEmpty(SortedName)
                    && !string.IsNullOrEmpty(ProductNameRus) && !string.IsNullOrEmpty(ProductNameEng) 
                    && !string.IsNullOrEmpty(ThumbURL))
                    return true;
                return false;
            }
        }

        public int BrandId
        {
            get
            {
                if (BrandReference.EntityKey != null)
                {
                    return int.Parse(BrandReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if (BrandReference.EntityKey != null)
                    BrandReference.EntityKey = null;
                BrandReference.EntityKey = new EntityKey("ProductsEntities.Brands", "BrandId", value);
            }
        }



        public int PlatformId
        {
            get
            {
                if(PlatformReference.EntityKey!=null)
                {
                    return int.Parse(PlatformReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(PlatformReference.EntityKey !=null)
                    PlatformReference.EntityKey = null;
                PlatformReference.EntityKey = new EntityKey("ProductsEntities.Platforms", "PlatformID", value);
            }
        }
    }
}
