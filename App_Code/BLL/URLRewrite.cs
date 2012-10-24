using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace echo.BLL
{
    /// <summary>
    /// Summary description for URLRewrite
    /// </summary>
    public class URLRewrite:IHttpModule
    {
       

        #region IHttpModule Members

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

       

        #endregion

        void context_BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication) sender;
            Rewrite(app);
        }

        private void Do301Redirect(HttpResponse response, string redirectUrl)
        {
           // response.Write(redirectUrl);
            response.RedirectLocation = redirectUrl;
            response.StatusCode = 0x12d;
            response.End();
        }

        private void Rewrite(HttpApplication app)
        {
            /*
            var host = app.Context.Request.Url.Host;
            if (host.Equals("echoofhollywood.com", StringComparison.OrdinalIgnoreCase))
            {
                var newUrl = new UriBuilder(app.Context.Request.Url);
                //newUrl.Host = "www." + host;
                newUrl.Host = "www.echo-h.ru";
                app.Context.Response.StatusCode = 301;
                app.Context.Response.Status = "301 Moved Permanently";
                app.Context.Response.AddHeader("Location", newUrl.Uri.AbsoluteUri);
                app.Context.Response.End();
                return;
            }  
            */
            List<string> oldlinks=new List<string>();
            oldlinks.Add("StripShoes.aspx");
            oldlinks.Add("Boots.aspx");
            oldlinks.Add("EveningShoes.aspx");
            oldlinks.Add("EveningDress.aspx");
            oldlinks.Add("StripDress.aspx");
            oldlinks.Add("Lingeries.aspx");

            List<string> oldlinksRu = new List<string>();
            oldlinksRu.Add("/Ru/StripShoes.aspx");
            oldlinksRu.Add("/Ru/Boots.aspx");
            oldlinksRu.Add("/Ru/EveningShoes.aspx");
            oldlinksRu.Add("/Ru/EveningDress.aspx");
            oldlinksRu.Add("/Ru/StripDress.aspx");
            oldlinksRu.Add("/Ru/Lingeries.aspx");

            List<string> oldlinksRu2=new List<string>();
            oldlinksRu2.Add("RU/About.aspx");
            oldlinksRu2.Add("RU/Shipping.aspx");
            oldlinksRu2.Add("RU/Stores.aspx");
            oldlinksRu2.Add("RU/Sizes.aspx");

            string UrlFile = app.Context.Request.Url.ToString();
            string urlred = Helpers.GetURLPath(UrlFile);
          //  urlred = urlred.Replace("echo-h3/", "");

          /*  if(urlred.ToLower().IndexOf("test.aspx")!=-1)
            {
                //Do301Redirect(app.Response,Path.Combine("http://www.echo-h.ru",urlred));
                Do301Redirect(app.Response,"http://www.echo-h.ru/test.aspx");
            }
            */
           /* List<string> syncs=new List<string>();
            syncs.Add("echoofhollywood.com");
            syncs.Add("legavenue.ru");

            foreach (string sync in syncs)
            {
                if (UrlFile.ToLower().IndexOf(sync.ToLower()) != -1)
                {
                    UrlFile = UrlFile.ToLower().Replace(sync.ToLower(), "echo-h.ru");
                    string virtUrl = UrlFile.Substring(UrlFile.IndexOf("echo-h.ru"));
                    HttpContext.Current.RewritePath("~/" + virtUrl);
                    return;
                }
            }
            */



            foreach (string oldlinkRu in oldlinksRu)
            {
                if(UrlFile.ToLower().IndexOf(oldlinkRu.ToLower())!=-1)
                {
                    UrlFile=UrlFile.ToLower().Replace(oldlinkRu.ToLower(), "products.aspx");
                    string virtUrl = UrlFile.Substring(UrlFile.IndexOf("products.aspx"));
                    //HttpContext.Current.RewritePath("~/" + virtUrl);
                    HttpContext.Current.Response.Redirect("~/" + virtUrl);
                    return;
                }
            }
            /*

            foreach (string oldlinkRu in oldlinksRu2)
            {
                if (UrlFile.ToLower().IndexOf(oldlinkRu.ToLower()) != -1)
                {
                   // UrlFile = UrlFile.ToLower().Replace(oldlinkRu.ToLower(), "products.aspx");
                    string virtUrl = oldlinkRu.Substring(3);
                    HttpContext.Current.Response.Redirect("~/" + virtUrl);
                    return;
                }
            }*/

            foreach (string oldlink in oldlinks)
            {
                if (UrlFile.ToLower().IndexOf(oldlink.ToLower()) != -1)
                {
                    UrlFile = UrlFile.ToLower().Replace(oldlink.ToLower(), "products.aspx");
                    string virtUrl = UrlFile.Substring(UrlFile.IndexOf("products.aspx"));
                    HttpContext.Current.RewritePath("~/"+virtUrl);
                    return;
                }
            }
        }
    }
}
