using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for echoZone
    /// </summary>
    public partial  class echoZone:IBaseEntity
    {

        private string _setName = "Zone";



        #region IBaseEntity Members

        public bool IsValid
        {
            get { return true; }
        }

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        #endregion

        public Zone GetZone(int zIndex)
        {
            Zone zone=new Zone();
            zone.Id = this.ID;
            zone.Qty = QTY != null ? (int)QTY : 0;
            zone.zIndex = zIndex;
            switch (zIndex)
            {
                case 0:
                    zone.CenterPrice = Zn0_1!=null ? (decimal)Zn0_1:0;
                    zone.RegionPrice = Zn0_2 != null ? (decimal)Zn0_2 : 0;
                    break;
                case 1:
                    zone.CenterPrice = Zn1_1 != null ? (decimal)Zn1_1 : 0;
                    zone.RegionPrice = Zn1_2 != null ? (decimal)Zn1_2 : 0;
                    break;
                case 2:
                    zone.CenterPrice = Zn2_1 != null ? (decimal)Zn2_1 : 0;
                    zone.RegionPrice = Zn2_2 != null ? (decimal)Zn2_2 : 0;
                    break;
                case 3:
                    zone.CenterPrice = Zn3_1 != null ? (decimal)Zn3_1 : 0;
                    zone.RegionPrice = Zn3_2 != null ? (decimal)Zn3_2 : 0;
                    break;
                case 4:
                    zone.CenterPrice = Zn4_1 != null ? (decimal)Zn4_1 : 0;
                    zone.RegionPrice = Zn4_2 != null ? (decimal)Zn4_2 : 0;
                    break;
                case 5:
                    zone.CenterPrice = Zn5_1 != null ? (decimal)Zn5_1 : 0;
                    zone.RegionPrice = Zn5_2 != null ? (decimal)Zn5_2 : 0;
                    break;
                case 6:
                    zone.CenterPrice = Zn6_1 != null ? (decimal)Zn6_1 : 0;
                    zone.RegionPrice = Zn6_2 != null ? (decimal)Zn6_2 : 0;
                    break;
                case 7:
                    zone.CenterPrice = Zn7_1 != null ? (decimal)Zn7_1 : 0;
                    zone.RegionPrice = Zn7_2 != null ? (decimal)Zn7_2 : 0;
                    break;
            }
            return zone;
        }
    }
}
