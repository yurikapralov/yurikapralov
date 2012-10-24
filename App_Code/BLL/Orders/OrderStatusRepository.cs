using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;


namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for OrderStatusRepository
    /// </summary>
    public class OrderStatusRepository:BaseOrdersRepository
    {
        public OrderStatusRepository()
        {
            CacheKey = "Orders";
        }

        public OrderStatusRepository(string connectionString):base(connectionString)
        {
            CacheKey = "Orders";
        }

        public List<OrderStatus> GetOrderStatuses()
        {
            string key = CacheKey + "_OrderStatuses";

            if(EnableCaching && Cache[key]!=null)
            {
                return (List<OrderStatus>) Cache[key];
            }

            Ordersctx.OrderStatuses.MergeOption = MergeOption.NoTracking;
            List<OrderStatus> list = Ordersctx.OrderStatuses.ToList();

            if(EnableCaching)
                CacheData(key,list,CacheDuration);

            return list;
        }

        public OrderStatus GetOrderStatusById(int statusId)
        {
            return Ordersctx.OrderStatuses.Where(p => p.OrderStatusID == statusId).FirstOrDefault();
        }

        public bool OrdersHaveThisOrderStatus(int orderStatusId)
        {
            if(Ordersctx.Orders.Include("OrderStatus").Where(p=>p.OrderStatus.OrderStatusID==orderStatusId).Count()>0)
                return true;
            return false;
        }

        public OrderStatus AddOrderStatus(OrderStatus orderStatus)
        {
            try
            {
                if(orderStatus.EntityState==EntityState.Detached)
                    Ordersctx.AddToOrderStatuses(orderStatus);
                PurgeCacheItems(CacheKey);

                return Ordersctx.SaveChanges() > 0 ? orderStatus : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey+"_orderStatuses_"+orderStatus.OrderStatusID,ex);
                return null;
            }
        }

        public bool DeleteOrderStatus(OrderStatus orderStatus)
        {
            try
            {
                //Первые три статуса фиксированы
                if(orderStatus.OrderStatusID<=3 || OrdersHaveThisOrderStatus(orderStatus.OrderStatusID))
                    return false;
                Ordersctx.DeleteObject(orderStatus);
                Ordersctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteOrderStatus(int orderStatusId)
        {
            return DeleteOrderStatus(GetOrderStatusById(orderStatusId));
        }
    }
}
