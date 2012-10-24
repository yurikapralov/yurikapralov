using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Size
    /// </summary>
    public partial  class Size:IBaseEntity
    {
        private string _setName = "Sizes";

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(SizeNameEng) && !string.IsNullOrEmpty(SizeNameRus) && (ProdTypeID==1 || ProdTypeID==2) )
                    return true;
                else
                    return false;
            }
        }

        public string FullSizeName
        {
            get
            {
                return SizeNameRus + " - " + SizeNameEng;
            }
        }

        public bool IsLargeShoesSize()
        {
            if(SizeID==9 || SizeID==10 || SizeID==11)
            {
                return true;
            }
            return false;
        }
    }
}
