using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Collection
    /// </summary>
    public partial  class Collection:IBaseEntity 
    {
        private string _setName = "Collections";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (ProductId > 0 && CathegoryId > 0)
                    return true;
                return false;
            }
        }

        public int ProductId
        {
            get
            {
                if(ProductReference.EntityKey !=null)
                {
                    return int.Parse(ProductReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(ProductReference.EntityKey !=null)
                    ProductReference.EntityKey = null;
                ProductReference.EntityKey = new EntityKey("ProductsEntities.Products", "ProdID", value);
            }
        }

        public int CathegoryId
        {
            get
            {
                if(CathegoryReference.EntityKey !=null)
                {
                    return int.Parse(CathegoryReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(CathegoryReference.EntityKey!=null)
                    CathegoryReference.EntityKey = null;
                CathegoryReference.EntityKey = new EntityKey("ProductsEntities.Cathegories", "CatID", value);
            }
        }
    }
}
