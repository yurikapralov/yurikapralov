using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Info
{
    public class echoNewsRepository:BaseInfoRepository
    {
        public echoNewsRepository()
        {
            CacheKey = "echoNews";
        }

        public echoNewsRepository(string connectionString):base(connectionString)
        {
            CacheKey = "echoNews";
        }

        public echoNews GetNewsById(int newsId)
        {
            try
            {
                string key = CacheKey + "_id=" + newsId.ToString();

                if (EnableCaching && Cache[key] != null)
                    return (echoNews) Cache[key];

                echoNews news = Infoctx.echoNews.Where(e => e.Id == newsId).FirstOrDefault();

                if(EnableCaching)
                    CacheData(key,news,CacheDuration);

                return news;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<echoNews> GetNews()
        {
            try
            {
                string key = CacheKey + "_all";

                if (EnableCaching && Cache[key] != null)
                    return (List<echoNews>)Cache[key];

                List<echoNews> news = Infoctx.echoNews.OrderByDescending(e=>e.NewsDate).ToList();

                if (EnableCaching)
                    CacheData(key, news, CacheDuration);

                return news;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<echoNews> GetTop2News()
        {
            try
            {
                string key = CacheKey + "_top2";

                if (EnableCaching && Cache[key] != null)
                    return (List<echoNews>)Cache[key];

                List<echoNews> news = Infoctx.echoNews.OrderByDescending(e => e.NewsDate).Take(2).ToList();

                if (EnableCaching)
                    CacheData(key, news, CacheDuration);

                return news;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public echoNews AddNews(echoNews news)
        {
            try
            {
                if(news.EntityState==EntityState.Detached)
                    Infoctx.AddToechoNews(news);
                PurgeCacheItems(CacheKey);

                return Infoctx.SaveChanges() > 0 ? news : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteNews(echoNews news)
        {
            try
            {
                Infoctx.DeleteObject(news);
                Infoctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteNews(int newsId)
        {
            return DeleteNews(GetNewsById(newsId));
        }
    }

}

