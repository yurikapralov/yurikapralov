using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using echo.BLL.Products;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Класс корзины
    /// </summary>
    [Serializable]
    public class ShoppingCart
    {
        private Dictionary<int,ShoppingCartItem> _items=new Dictionary<int, ShoppingCartItem>();

        public ICollection Items
        {
            get { return _items.Values; }
        }

        public decimal Total
        {
            get
            {
                decimal sum = 0;
                foreach(ShoppingCartItem item in _items.Values)
                    sum += item.PriceForSale*item.Qty;
                return sum;
            }
        }

        public int Count
        {
            get {
 
                int sum = 0;
                foreach (ShoppingCartItem item in _items.Values)
                {
                    sum += item.Qty;
                }
                return sum;
            }
        }

        public void InsertItem(int prodSizeId, decimal priceForSale, string title, int prodId)
        {
            if (_items.ContainsKey(prodSizeId))
                _items[prodSizeId].Qty += 1;
            else 
                _items.Add(prodSizeId,new ShoppingCartItem(prodSizeId,priceForSale,title,prodId));
        }

        public void DeleteItem(int prodSizeId)
        {
            if(_items.ContainsKey(prodSizeId))
            {
                ShoppingCartItem item = _items[prodSizeId];
                item.Qty -= 1;
                if (item.Qty == 0)
                    _items.Remove(prodSizeId);
            }
        }

        public void DeleteProduct(int prodSizeId)
        {
            if(_items.ContainsKey(prodSizeId))
            {
                _items.Remove(prodSizeId);
            }
        }

        public void UpdateItemQuantity(int prodSizeId, int quantity)
        {
            if(_items.ContainsKey(prodSizeId))
            {
                ShoppingCartItem item = _items[prodSizeId];
                item.Qty = quantity;
                if (item.Qty == 0)
                    _items.Remove(prodSizeId);
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool HaveUsaProduct()
        {
            bool fromUsa = false;
            foreach (ShoppingCartItem shopcart in _items.Values)
            {
                int prodsizeId = shopcart.ProdSizeId;
                using (ProductRepository lRepository = new ProductRepository())
                {
                    ProductColor productColor = lRepository.GetProductByProdSizeId(prodsizeId);
                    if (productColor != null)
                    {
                        if (productColor.Product.FromUsa)
                            fromUsa = true;
                    }
                }
            }
            return fromUsa;
        }
    }
}
