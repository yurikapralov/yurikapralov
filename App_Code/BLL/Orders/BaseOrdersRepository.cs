using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for BaseOrdersRepository
    /// </summary>
    public class BaseOrdersRepository:BaseRepository
    {

        private OrdersEntities _Ordersctx;
        private bool disposedValue;

        public BaseOrdersRepository()
        {
            disposedValue = false;
            ConnectionString = Helpers.Settings.DefaultConnectionStringName;
        }

        public BaseOrdersRepository(string connectionString)
        {
            disposedValue = false;
            ConnectionString = connectionString;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if((!disposedValue && disposing)&& null !=_Ordersctx)
            {
                _Ordersctx.Dispose();
            }
            disposedValue = true;
        }

        public OrdersEntities Ordersctx
        {
            get
            {
                if(_Ordersctx==null)
                    _Ordersctx=new OrdersEntities(GetActualConnectionString());
                return _Ordersctx;
            }
            set { _Ordersctx = value; }
        }
    }
}
