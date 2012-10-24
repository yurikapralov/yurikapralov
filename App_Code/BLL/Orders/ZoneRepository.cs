using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for ZoneRepository
    /// </summary>
    public class ZoneRepository:BaseOrdersRepository
    {
       public ZoneRepository()
       {
           CacheKey = "Zone";
       }

        public ZoneRepository(string connectionString):base (connectionString)
        {
            CacheKey = "Zone";
        }

        public List<Zone> GetZonesByzIndex(int zIndex)
        {
            string key = CacheKey + "_zIndex=" + zIndex;

            if(EnableCaching && Cache[key]!=null)
            {
                return (List<Zone>) Cache[key];
            }

            List<echoZone> eZones = (from lzone in Ordersctx.echoZones
                                select lzone).ToList();
            List<Zone>zones=new List<Zone>();
            foreach (echoZone eZone in eZones)
            {
                zones.Add(eZone.GetZone(zIndex));
            }
            if(EnableCaching)
                CacheData(key,zones,CacheDuration);
            return zones;
        }

        public Zone GetZoneByzIndexAndQty(int zIndex, int qty)
        {
            string key = CacheKey + "_zIndex=" + zIndex+"Qty="+qty;

            if (EnableCaching && Cache[key] != null)
            {
                return (Zone)Cache[key];
            }

            echoZone eZone = (from lzone in Ordersctx.echoZones
                              where lzone.QTY==qty 
                                     select lzone).FirstOrDefault();
            Zone zone = eZone.GetZone(zIndex);
            if (EnableCaching)
                CacheData(key, zone, CacheDuration);
            return zone;
        }

        public echoZone UpdateZone(Zone zone)
        {
            try
            {
                echoZone _echoZone = Ordersctx.echoZones.Where(p => p.ID == zone.Id).FirstOrDefault();
                switch (zone.zIndex)
                {
                    case 0:
                        _echoZone.Zn0_1 = zone.CenterPrice;
                        _echoZone.Zn0_2 = zone.RegionPrice;
                        break;
                    case 1:
                        _echoZone.Zn1_1 = zone.CenterPrice;
                        _echoZone.Zn1_2 = zone.RegionPrice;
                        break;
                    case 2:
                        _echoZone.Zn2_1 = zone.CenterPrice;
                        _echoZone.Zn2_2 = zone.RegionPrice;
                        break;
                    case 3:
                        _echoZone.Zn3_1 = zone.CenterPrice;
                        _echoZone.Zn3_2 = zone.RegionPrice;
                        break;
                    case 4:
                        _echoZone.Zn4_1 = zone.CenterPrice;
                        _echoZone.Zn4_2 = zone.RegionPrice;
                        break;
                    case 5:
                        _echoZone.Zn5_1 = zone.CenterPrice;
                        _echoZone.Zn5_2 = zone.RegionPrice;
                        break;
                    case 6:
                        _echoZone.Zn6_1 = zone.CenterPrice;
                        _echoZone.Zn6_2 = zone.RegionPrice;
                        break;
                    case 7:
                        _echoZone.Zn7_1 = zone.CenterPrice;
                        _echoZone.Zn7_2 = zone.RegionPrice;
                        break;
                }
                PurgeCacheItems(CacheKey);
                return Ordersctx.SaveChanges() > 0 ? _echoZone : null;
            }
            catch (Exception ex)
            {
                
                ActiveExceptions.Add(CacheKey+"_"+zone.Id,ex);
                return null;
            }
        }

        
    }

    public class Zone
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public decimal CenterPrice { get; set; }
        public decimal RegionPrice { get; set; }
        public int zIndex { get; set; }
    }
}
