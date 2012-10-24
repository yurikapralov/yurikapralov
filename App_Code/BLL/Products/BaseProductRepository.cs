using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for BaseProductRepository
    /// </summary>
    public class BaseProductRepository:BaseRepository
    {
        private ProductsEntities _Productctx;
        private bool disposedValue;

        public BaseProductRepository()
        {
            disposedValue = false;
            ConnectionString = Helpers.Settings.DefaultConnectionStringName;
            CacheKey = "Product_";
        }

        public BaseProductRepository(string sConnectionString)
        {
            disposedValue = false;
            ConnectionString = sConnectionString;
            CacheKey = "Product_";
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposedValue && disposing) && Productctx != null)
                Productctx.Dispose();
            disposedValue = true;
        }

        public ProductsEntities Productctx
        {
            get
            {
                if(_Productctx ==null)
                    _Productctx=new ProductsEntities(this.GetActualConnectionString());
                return _Productctx;
            }
            set { _Productctx = value; }
        }
    }
}
