using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Orders
{
    /// <summary>
    /// Класс, описывающий единицу корзины
    /// </summary>
    [Serializable]
    public class ShoppingCartItem
    {
        public ShoppingCartItem(int prodSizeId, decimal priceForSale,string title, int prodId)
        {
            this.ProdSizeId = prodSizeId;
            this.PriceForSale = priceForSale;
            this.Title = title;
            this.ProdId = prodId;
        }

        private int _prodSizeId = 0;
        private decimal _priceForSale = 0;
        private int _qty = 1;
        private string _title = "";
        private int _prodId = 0;

        public int ProdSizeId
        {
            get { return _prodSizeId; }
            set { _prodSizeId = value; }
        }

        public decimal PriceForSale
        {
            get { return _priceForSale; }
            set { _priceForSale = value; }
        }

        public int Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int ProdId
        {
            get { return _prodId; }
            set { _prodId = value; }
        }
    }
}
