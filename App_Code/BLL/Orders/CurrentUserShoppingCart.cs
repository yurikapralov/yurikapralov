using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL.Orders
{
    /// <summary>
    /// Summary description for CurrentUserShoppingCart
    /// </summary>
    public class CurrentUserShoppingCart
    {
        public static ICollection GetItems()
        {
            return (HttpContext.Current.Profile as ProfileCommon).ShoppingCart.Items;
        }
        public static void DeleteItem(int id)
        {
            (HttpContext.Current.Profile as ProfileCommon).ShoppingCart.DeleteItem(id);
        }
        public static void DeleteProduct(int id)
        {
            (HttpContext.Current.Profile as ProfileCommon).ShoppingCart.DeleteProduct(id);
        }
    }
}
