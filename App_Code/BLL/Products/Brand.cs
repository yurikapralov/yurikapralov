using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Products
{
    /// <summary>
    /// Сводное описание для Brand
    /// </summary>
    public partial class Brand : IBaseEntity
    {

        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrEmpty(BrandName))
                    return true;
                return false;
            }
        }

        private string _setName = "Brands";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }
    }
}