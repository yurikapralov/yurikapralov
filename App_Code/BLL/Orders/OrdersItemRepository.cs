using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for OrdersItemRepository
    /// </summary>
    public class OrdersItemRepository : BaseOrdersRepository
    {
        public OrdersItemRepository()
        {
            CacheKey = "Orders";
        }

        public OrdersItemRepository(string connectionString)
            : base(connectionString)
        {
            CacheKey = "Orders";
        }

        public List<OrdersItem> GetOrderItemsByOrderId(int orderId)
        {
            return Ordersctx.OrdersItems.Include("Order").Where(p => p.Order.OrderID == orderId).ToList();
        }

        public OrdersItem GetOrderItemById(int orderItemsId)
        {
            return Ordersctx.OrdersItems.Include("Order").Where(p => p.OrderItemID == orderItemsId).FirstOrDefault();
        }

        public int  GetOrderItemCount()
        {
            return Ordersctx.OrdersItems.Count();
        }

        public OrdersItem AddOrderItem(OrdersItem ordersItem)
        {
            try
            {
                if(ordersItem.EntityState==EntityState.Detached)
                    Ordersctx.AddToOrdersItems(ordersItem);
                PurgeCacheItems(CacheKey);

                return Ordersctx.SaveChanges() > 0 ? ordersItem : null;
            }
            catch (Exception ex)
            {
               ActiveExceptions.Add(CacheKey+"_orderItem_"+ordersItem.OrderItemID,ex);
                return null;
            }
        }

        public bool DeleteOrderItem(OrdersItem ordersItem)
        {
            try
            {
                Ordersctx.DeleteObject(ordersItem);
                Ordersctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public bool DeleteOrderItem(int orderItemId)
        {
            return DeleteOrderItem(GetOrderItemById(orderItemId));
        }

    }
}
