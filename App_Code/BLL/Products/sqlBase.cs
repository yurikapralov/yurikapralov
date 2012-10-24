using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using echo.BLL;

/// <summary>
/// Сводное описание для sqlBase
/// </summary>
public abstract class sqlBase
{



    private int _cacheDuration = 0;
    private string _cacheKey = "CacheKey";
    private string _connectionString = "Set the ConnectionString";
    private bool _enableCaching = true;
    public const int DefPageSize = 50;
    protected const int MAXROWS = int.MaxValue;

    protected static void CacheData(string key, object data)
    {
        CacheData(key, data, 120);
    }

    protected static void CacheData(string key, object data, int cacheDuration)
    {
        if (data != null)
            Cache.Insert(key, data, null, DateTime.Now.AddSeconds(cacheDuration), TimeSpan.Zero);
    }

    protected static string ConvertNullToEmptyString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        return input;
    }

    protected static string EncodeText(string content)
    {
        content = HttpUtility.HtmlEncode(content);
        content = content.Replace("  ", " &nbsp;&nbsp;").Replace(@"\n", "<br>");
        return content;
    }

    protected string GetActualConnectionString()
    {
        return ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
    }

    protected static int GetPageIndex(int startRowIndex, int maximumRows)
    {
        if (maximumRows <= 0)
            return 0;
        return (int)Math.Round(Math.Floor((double)(((double)startRowIndex) / ((double)maximumRows))));
    }

    public int GetRandItem(int min, int max)
    {
        Random rnd = new Random();
        return rnd.Next(min, max);
    }

    protected void PurgeCacheItems(string prefix)
    {
        prefix = prefix.ToLower();
        List<string> itemsToRemove = new List<string>();

        IDictionaryEnumerator enumerator = Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                itemsToRemove.Add(enumerator.Key.ToString());
        }

        foreach (string itemToRemove in itemsToRemove)
        {
            Cache.Remove(itemToRemove);
        }
    }

    private Dictionary<string, Exception> _activeExceptions;

    public Dictionary<string, Exception> ActiveExceptions
    {
        get
        {
            if (_activeExceptions == null)
                _activeExceptions = new Dictionary<string, Exception>();
            return _activeExceptions;
        }
        set { _activeExceptions = value; }
    }

    protected static Cache Cache
    {
        get { return HttpContext.Current.Cache; }
    }

    public int CacheDuration
    {
        get { return _cacheDuration; }
        set { _cacheDuration = value; }
    }

    public string CacheKey
    {
        get { return _cacheKey; }
        set { _cacheKey = value; }
    }

    public string ConnectionString
    {
        get { return _connectionString; }
        set { _connectionString = value; }
    }

    protected static IPrincipal CurrentUser
    {
        get { return Helpers.CurrentUser; }
    }

    protected static string CurrentUserIP
    {
        get { return Helpers.CurrentUserIP; }
    }

    protected static string CurrentUserName
    {
        get { return Helpers.CurrentUserName; }
    }

    public bool EnableCaching
    {
        get { return _enableCaching; }
        set { _enableCaching = value; }
    }


}