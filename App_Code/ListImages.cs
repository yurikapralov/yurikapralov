using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using echo.BLL.Products;

/// <summary>
/// Summary description for ListImages
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ListImages : System.Web.Services.WebService {

    public ListImages () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<extListImages> GetLargeImagesByProductId(int productId)
    {
        using (ProductColorRepository lRepository=new ProductColorRepository())
        {
            try
            {
                List<ProductColor> productColors = lRepository.GetProductColorsByProduct(productId);
                List<extListImages> extUrls = new List<extListImages>();
                foreach (ProductColor productColor in productColors)
                {
                    extUrls.Add(new extListImages(productId.ToString(), "Images/Products/Large/" + productColor.ImageURL));
                }
                return extUrls;
            }
            catch (Exception)
            {

                return null ;
            }
            
           
        }
        
    }
    
}

public class extListImages
{
    public string ProdID;
    public string ImageUrl;

    public extListImages(string prodId, string imageUrl)
    {
        this.ProdID = prodId;
        this.ImageUrl = imageUrl;
    }
}

