<%@ WebHandler Language="C#" Class="GetPriviewFile" %>

using System;
using System.Web;
using System.Collections.Generic;
using echo.BLL.Products;


public class GetPriviewFile : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        int prodId;
        string prodIdReq = context.Request.QueryString["prodId"];
        if (int.TryParse(prodIdReq, out prodId))
        {
            using (ProductColorRepository lRepository = new ProductColorRepository())
            {
                string jsontext = "[";
                try
                {
                    List<ProductColor> productColors = lRepository.GetProductColorsByProduct(prodId);
                    foreach (ProductColor productColor in productColors)
                    {
                        jsontext+="{";
                        jsontext += string.Format("\"imgurl\":\"{0}\"", productColor.ImageURL);
                        jsontext += "},";
                    }
                    jsontext = jsontext.Substring(0, jsontext.Length - 1);
                    jsontext += "]";
                    context.Response.Write(jsontext);
                }
                catch (Exception)
                {

                }


            }
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}