using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Info
{
    /// <summary>
    /// Summary description for BaseInfoRepository
    /// </summary>
    public class BaseInfoRepository:BaseRepository 
    {
        private InfoEntities _Infoctx;
        private bool disposedValue;

        public BaseInfoRepository()
        {
            disposedValue = false;
            ConnectionString = Helpers.Settings.DefaultConnectionStringName;
        }

        public BaseInfoRepository(string connectionString)
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
            if((!disposedValue && disposing)&& _Infoctx!=null )
            {
                _Infoctx.Dispose();
            }
            disposedValue = true;
        }

        public InfoEntities Infoctx
        {
            get
            {
                if(_Infoctx==null)
                    _Infoctx=new InfoEntities(GetActualConnectionString());
                return _Infoctx;
            }
            set { _Infoctx = value; }
        }
    }
}
