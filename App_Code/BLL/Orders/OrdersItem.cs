using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using echo.BLL.Products;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for OrderItem
    /// </summary>
    public partial  class OrdersItem
    {
        private string _setName = "OrdersItem";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(PriceForSale>=0 && ProdSizeID>0 && Qty>=0)
                    return true;
                return false;
            }
        }

        public int OrderId
        {
            get
            {
                if(OrderReference.EntityKey!=null)
                {
                    return int.Parse(OrderReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if (OrderReference.EntityKey != null)
                    OrderReference.EntityKey = null;
                OrderReference.EntityKey = new EntityKey("OrdersEntities.Orders", "OrderID", value);
            }

        }

        public string DinamicTitle
        {
            get
            {
                if (Title != null)
                    return Title;
                string retTitle = "";
                using (ProductRepository lRepository = new ProductRepository())
                {
                    ProductColor productColor = lRepository.GetProductByProdSizeId(ProdSizeID);
                    if (productColor != null)
                    {
                        retTitle = productColor.Product.ProductNameRus + ", цвет: " + productColor.Color.ColorNameRus;
                    }
                }
                using (ProductSizeRepository lSizeRepository = new ProductSizeRepository())
                {
                    ProductSize productSize = lSizeRepository.GetProductSizeById(ProdSizeID);
                    {
                        if (productSize != null)
                        {
                            retTitle = retTitle + ", размер: " + productSize.Size.SizeNameRus;
                        }
                    }
                }
                return retTitle;
            }
        }

        public string DinamicTitleEng
        {
            get
            {
                if (Title != null)
                    return Title;
                string retTitle = "";
                using (ProductRepository lRepository = new ProductRepository())
                {
                    ProductColor productColor = lRepository.GetProductByProdSizeId(ProdSizeID);
                    if (productColor != null)
                    {
                        retTitle = productColor.Product.ProductNameEng + ", color: " + productColor.Color.ColorNameEng;
                    }
                }
                using (ProductSizeRepository lSizeRepository = new ProductSizeRepository())
                {
                    ProductSize productSize = lSizeRepository.GetProductSizeById(ProdSizeID);
                    {
                        if (productSize != null)
                        {
                            retTitle = retTitle + ", size: " + productSize.Size.SizeNameEng;
                        }
                    }
                }
                return retTitle;
            }
        }

        public int Summ
        {
            get
            {
                try
                {
                    return Convert.ToInt32(PriceForSale)*Qty;
                }
                catch (Exception)
                {
                    
                    return 0;
                }
            }
        }
    }
}
