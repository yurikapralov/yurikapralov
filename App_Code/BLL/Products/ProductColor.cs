using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for ProductColor
    /// </summary>
    public partial  class ProductColor:IBaseEntity 
    {
        private string _setName = "ProductColors";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(ImageURL) && ColorId>0 && ProductId>0)
                    return true;
                return false;
            }
        }

        public int ProductId
        {
            get
            {
                if (ProductReference.EntityKey != null)
                {
                    return int.Parse(ProductReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if (ProductReference.EntityKey != null)
                    ProductReference.EntityKey = null;
                ProductReference.EntityKey = new EntityKey("ProductsEntities.Products", "ProdID", value);
            }
        }

        public int ColorId
        {
            get
            {
                if(ColorReference.EntityKey !=null)
                {
                    return int.Parse(ColorReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if(ColorReference.EntityKey !=null)
                    ColorReference.EntityKey = null;
                ColorReference.EntityKey = new EntityKey("ProductsEntities.Colors", "ColorID", value);
            }
        }

        public string ColorNameRus
        {
            get
            {
                return Color.ColorNameRus;
            }
        }

        public bool IsAvilable
        {
            get
            {
                using (ProductSizeRepository lProductSizeRepository = new ProductSizeRepository())
                {
                    List<ProductSize> productSizes =
                        lProductSizeRepository.GetProductSizeByProductColor(this.ProdColorID);
                    if (productSizes.Count == 1 && (productSizes[0].SizeId == 24 || productSizes[0].SizeId ==25))  // идентификатор размера нет в наличии
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
}
