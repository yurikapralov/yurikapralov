using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace echo.BLL.Orders
{
    /// <summary>
    /// Enum определяющий является ли город административным центром или находится на территории области
    /// </summary>
    public enum RegionType:int
    {
        Center=1,Area=2
    }
}

