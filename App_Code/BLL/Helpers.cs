using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.UI.WebControls;
using System.Linq.Expressions;


namespace echo.BLL
{
    /// <summary>
    /// Вспомогательный класс
    /// </summary>
    public static  class Helpers
    {
       
        /// <summary>
        /// Переменная доступа к классу конфигурации
        /// </summary>
        public static readonly EchoSection Settings =
            (EchoSection)WebConfigurationManager.GetSection("echo");


        public static string DefaultTitle
        {
            get { return Settings.Store.DefaultTitle; }
        }
        
        public static IPrincipal CurrentUser
        {
            get{ return HttpContext.Current.User;}
        }

        public static string CurrentUserIP
        {
            get { return HttpContext.Current.Request.UserHostAddress;}
        }

        public static string CurrentUserName
        {
            get
            {
                string userName = string.Empty;
                if(CurrentUser.Identity.IsAuthenticated)
                {
                    userName = CurrentUser.Identity.Name;
                }
                return userName;
            }
        }

        public static ProfileBase GetUserProfile()
        {
            return ProfileBase.Create(CurrentUserName, CurrentUser.Identity.IsAuthenticated);
        }

        public static string GetRandomPrefix()
        {
            Random random = new Random();
            int rndValue = random.Next(1, 1000);
            string prefix = rndValue.ToString().PadLeft(4, '0') + '_';
            return prefix;
        }

        static public void EnumToListBox(Type EnumType, ListControl TheListBox)
        {
            Array Values = System.Enum.GetValues(EnumType);

            foreach (int Value in Values)
            {
                string Display = Enum.GetName(EnumType, Value);
                ListItem Item = new ListItem(Display, Value.ToString());
                TheListBox.Items.Add(Item);
            }
        }

        public static string GetURLPath(string vURL)
        {
            var _Regex = new Regex("://[^/]+/(?<path>[^?\\s<>#\"]+)");
            if (_Regex.Matches(vURL).Count > 0)
            {
                return _Regex.Match(vURL).Groups[1].ToString();
            }
            return vURL;
        }

        //Специальный метод для поддержки Contains в SqlToEntities
        static public Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(

            Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {

            if (null == valueSelector) { throw new ArgumentNullException("valueSelector"); }

            if (null == values) { throw new ArgumentNullException("values"); }

            ParameterExpression p = valueSelector.Parameters.Single();

            // p => valueSelector(p) == values[0] || valueSelector(p) == ...

            if (!values.Any())
            {

                return e => false;

            }

            var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));

            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);

        }
    }
}
