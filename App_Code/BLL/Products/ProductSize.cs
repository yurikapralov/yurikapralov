using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for ProductSize
    /// </summary>
    public partial  class ProductSize:IBaseEntity
    {
        private string _setName = "ProductSizes";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(SizeId>0 && ProductColorId>0)
                    return true;
                return false;
            }
        }


        public int ProductColorId
        {
            get
            {
                if(ProductColorReference.EntityKey !=null)
                {
                    return int.Parse(ProductColorReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(ProductColorReference.EntityKey!=null)
                    ProductColorReference.EntityKey = null;
                ProductColorReference.EntityKey = new EntityKey("ProductsEntities.ProductColors", "ProdColorID", value);
            }
        }

        public int SizeId
        {
            get
            {
                if(SizeReference.EntityKey !=null)
                {
                    return int.Parse(SizeReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(SizeReference.EntityKey !=null)
                    SizeReference.EntityKey = null;
                SizeReference.EntityKey = new EntityKey("ProductsEntities.Sizes", "SizeID", value);
            }
        }

        public string SizeNameRus
        {
            get
            {
                return Size.SizeNameRus;
            }
        }

        public string SizeNameEng
        {
            get { return Size.SizeNameEng; }
        }
    }
}
