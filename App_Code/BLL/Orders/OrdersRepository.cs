using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrdersRepository
/// </summary>
namespace echo.BLL.Orders
{
    public class OrdersRepository : BaseOrdersRepository
    {
        public OrdersRepository()
        {
            CacheKey = "Orders";
        }

        public OrdersRepository(string connectionString)
            : base(connectionString)
        {
            CacheKey = "Orders";
        }

        public Order GetOrderById (int orderId)
        {
            string key = CacheKey + "_by_Id_"+orderId;

            if (EnableCaching && (Cache[key] != null))
            {
                return (Order)Cache[key];
            }

            Order order =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").
                Where(p=>p.OrderID==orderId).FirstOrDefault();

            if (EnableCaching)
                CacheData(key, order, CacheDuration);

            return order;
        }

        public List<Order> GetAllOrders()
        {
            string key = CacheKey + "_All";

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>)Cache[key];
            }

            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> orders =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").ToList();

            if (EnableCaching)
                CacheData(key, orders, CacheDuration);

            return orders;
        }

        public List<Order> GetOrdersByOrderStatus(int orderStatusId)
        {
            string key = CacheKey + "_byOrderStatus_"+orderStatusId;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>)Cache[key];
            }

            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> orders =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").
                Where(p => p.OrderStatus.OrderStatusID == orderStatusId).ToList();

            if (EnableCaching)
                CacheData(key, orders, CacheDuration);

            return orders;
        }

        public List<Order> GetOrdersByUserName(string userName)
        {
            string key = CacheKey + "_byUserName_"+userName;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>)Cache[key];
            }

            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> orders =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").
                Where(p => p.AddedBy==userName).ToList();

            if (EnableCaching)
                CacheData(key, orders, CacheDuration);

            return orders;
        }

        public List<Order> GetOrdersByEmail(string eMail)
        {
            string key = CacheKey + "_byEMail_"+eMail;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>)Cache[key];
            }

            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> orders =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").
                Where(p => p.Email.ToLower() == eMail.ToLower()).ToList();

            if (EnableCaching)
                CacheData(key, orders, CacheDuration);

            return orders;
        }

        public List<Order> GetOrdersByCity(int cityId)
        {
            string key = CacheKey + "_byCity_" + cityId;

            if (EnableCaching && (Cache[key] != null))
            {
                return (List<Order>)Cache[key];
            }

            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> orders =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").
                Where(p => p.City.CityID==cityId).ToList();

            if (EnableCaching)
                CacheData(key, orders, CacheDuration);

            return orders;
        }

        public List<Order> GetOrdersByFIO(string fio)
        {
            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> orders =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").
                Where(p => p.FIO.ToLower().Contains(fio.ToLower())).ToList();            
            return orders;
        }

        public List<Order> GetOrdersByOrderNumber(string orderNumber)
        {
            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<Order> orders =
                Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems").
                Where(p => p.OrderNumber.ToLower().Contains(orderNumber.ToLower())).ToList();
            return orders;
        }

        public string[] GetFIOS (string prefixText, int count)
        {
            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<String> fois = Ordersctx.Orders.Where(p => p.FIO.ToLower().Contains(prefixText.ToLower())).Select(p=>p.FIO).Distinct().Take(count).ToList();
            return fois.ToArray();
        }

        public string[] GetEmails(string prefixText, int count)
        {
            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<String> emails = Ordersctx.Orders.Where(p => p.Email.ToLower().Contains(prefixText.ToLower())).Select(p => p.Email).Distinct().Take(count).ToList();
            return emails.ToArray();
        }

        public string[] GetCities(string prefixText, int count)
        {
            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            List<String> cities = Ordersctx.Orders.Include("City").Where(p => p.City.City_RUS.ToLower().Contains(prefixText.ToLower())).Select(p => p.City.City_RUS).Distinct().Take(count).ToList();
            return cities.ToArray();
        }

        public List<Order> GetOrdersByDateRange(DateTime vFromDate, DateTime vToDate, int orderStatusId)
        {
            vToDate=vToDate.AddDays(1);
            Ordersctx.Orders.MergeOption = MergeOption.NoTracking;
            return
                (from lOrder in
                     Ordersctx.Orders.Include("Country").Include("OrderStatus").Include("City").Include("OrdersItems")
                 where lOrder.DateCreated >= vFromDate && lOrder.DateCreated <= vToDate && lOrder.OrderStatus.OrderStatusID==orderStatusId
                 orderby lOrder.DateCreated descending  
                 select lOrder).ToList();
        }

        public Order AddOrder(Order order)
        {
            try
            {
                if(order.EntityState==EntityState.Detached)
                    Ordersctx.AddToOrders(order);
                PurgeCacheItems(CacheKey);

                return Ordersctx.SaveChanges() > 0 ? order : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey+order.OrderID,ex);
                return null;
            }
        }

        public Order InsertOrder(ShoppingCart vshoppingCart, string adress, string city2,string cityIndex, int cityTypeId,
            decimal deliverSum, int deliverTypeId, string email, string fio, string home, string korpus, string note, 
            string phone, string street, string time1, string time2 , string unit,int countryId, int cityId)
        {
            string addedBy = Helpers.CurrentUserName;

            Order order = Order.CreateOrder(cityTypeId, DateTime.Now, deliverSum, deliverTypeId, 0, "", vshoppingCart.Total,
                                            vshoppingCart.Total+deliverSum);
            order.Adress = adress;
            order.AddedBy = addedBy;
            order.City2 = city2;
            order.CityIndex = cityIndex;
            order.Email = email;
            order.FIO = fio;
            order.Home = home;
            order.Korpus = korpus;
            order.Note = note;
            order.Phone = phone;
            order.Street = street;
            order.time1 = time1;
            order.time2 = time2;
            order.Unit = unit;
            order.OrderStatusId = 1;
            order.CountryId = countryId;
            order.CityId = cityId;

            foreach (ShoppingCartItem item in vshoppingCart.Items)
            {
                OrdersItem ordersItem = OrdersItem.CreateOrdersItem(0, item.PriceForSale, item.ProdSizeId, item.Qty);
                //Защита от больших имен
                if (item.Title.Length>= 255)
                    item.Title = item.Title.Substring(0, 254);
                ordersItem.Title = item.Title;
                order.OrdersItems.Add(ordersItem);

            }
            order = AddOrder(order);
            order.OrderNumber = order.OrderID + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;           
            order = AddOrder(order);

            return order;
        }

        public bool DeleteOrder(Order order)
        {
            try
            {
                using (OrdersItemRepository lRepository=new OrdersItemRepository())
                {
                    List<OrdersItem> ordersItems = lRepository.GetOrderItemsByOrderId(order.OrderID);
                    foreach (OrdersItem item in ordersItems)
                    {
                        lRepository.DeleteOrderItem(item);
                    }
                }
                Ordersctx.DeleteObject(order);
                Ordersctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteOrder(int orderId)
        {
            return DeleteOrder(GetOrderById(orderId));
        }
    }
}
