using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using echo.BLL.Info;
using echo.BLL.Products;
using System.Web.UI.HtmlControls;


namespace echo.BLL
{
    /// <summary>
    /// Базовый класс для всех страниц
    /// </summary>
    public class BasePage:Page
    {
        private bool _moveHiddenFileds;
        public ProfileBase PageProfile;

        public bool MoveHiddenFileds
        {
            get { return _moveHiddenFileds; }
            set { _moveHiddenFileds = value; }
        }

        public BasePage()
        {
            _moveHiddenFileds = true;
            PageProfile = Helpers.GetUserProfile();
        }

        private string MoveHiddenFieldsToBottom(string html)
        {

            //AccordionExtender_ClientState
            string sPattern = "<input type=\"hidden\".*/*>|<input type=\"hidden\".*></input>";
            MatchCollection mc = Regex.Matches(html, sPattern, RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);
            StringBuilder sb = new StringBuilder();

            foreach (Match m in mc)
            {

                sb.AppendLine(m.Value);

                html = html.Replace(m.Value, string.Empty);
            }

            return html.Replace("</form>", sb.ToString() + "</form>");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            base.Render(htmlWriter);
            string html = stringWriter.ToString();
            if (MoveHiddenFileds)
            {
                html = MoveHiddenFieldsToBottom(html);
            }
            writer.Write(html);
        }

       

        public int PrimaryKeyId(string vPrimaryKey)
        {
            try
            {
                {
                    if ((ViewState[vPrimaryKey] != null) && int.Parse(ViewState[vPrimaryKey].ToString()) != 0)
                    {
                        return int.Parse(ViewState[vPrimaryKey].ToString());
                    }
                    else if (Request.QueryString[vPrimaryKey] != null &&
                        int.Parse(Request.QueryString[vPrimaryKey]) != 0)
                    {
                        ViewState[vPrimaryKey] = int.Parse(Request.QueryString[vPrimaryKey]);
                        return int.Parse(Request.QueryString[vPrimaryKey].ToString());
                    }
                    return 0;
                }
            }
            catch (Exception)
            {

                Response.Redirect("Default.aspx");
                return 0;
            }

        }

        public string PrimaryKeyIdAsString(string vPrimaryKey)
        {
            if (null != ViewState[vPrimaryKey])
            {
                return ViewState[vPrimaryKey].ToString();
            }
            if (null != Request.QueryString[vPrimaryKey])
            {
                ViewState[vPrimaryKey] = Request.QueryString[vPrimaryKey];
                return Request.QueryString[vPrimaryKey];
            }
            return string.Empty;
        }

        public string UserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public int CategoryId
        {
            get
            {
                return PrimaryKeyId("CatId");
            }
            set
            {
                ViewState["CatId"] = value;
            }
        }

        public int PType
        {
            get
            {
                return PrimaryKeyId("pType");
            }
            set
            {
                ViewState["pType"] = value;
            }
        }

        public int ProductId
        {
            get
            {
                return PrimaryKeyId("ProdId");
            }
            set
            {
                ViewState["ProdId"] = value;
            }
        }

        public int GroupId
        {
            get
            {
                return PrimaryKeyId("GroupId");
            }
            set
            {
                ViewState["GroupId"] = value;
            }
        }

        public int PlatformId
        {
            get
            {
                return PrimaryKeyId("GroupId");
            }
            set
            {
                ViewState["GroupId"] = value;
            }
        }

        public int ColorId
        {
            get
            {
                return PrimaryKeyId("ColorId");
            }
            set
            {
                ViewState["ColorId"] = value;
            }
        }

        public int SizeId
        {
            get
            {
                return PrimaryKeyId("SizeId");
            }
            set
            {
                ViewState["SizeId"] = value;
            }
        }


        public int ProdColorId
        {
            get
            {
                return PrimaryKeyId("ProdColorId");
            }
            set
            {
                ViewState["ProdColorId"] = value;
            }
        }

        public int OrderId
        {
            get
            {
                return PrimaryKeyId("OrderId");
            }
            set
            {
                ViewState["OrderId"] = value;
            }
        }


        public int NewsId
        {
            get
            {
                return PrimaryKeyId("NewsId");
            }
            set
            {
                ViewState["NewsId"] = value;
            }
        }

        public int BrandId
        {
            get { return PrimaryKeyId("BrandId"); }
            set { ViewState["BrandId"] = value; }
        }

        public int SelZone
        {
            get
            {
                return PrimaryKeyId("Zone");
            }
            set
            {
                ViewState["Zone"] = value;
            }
        }

        public ProductSorted Sort
        {
            get
            {
                return (ProductSorted) ViewState["ProductSorted"];
            }
            set
            {
                ViewState["ProductSorted"] = value;
            }
        }

        public string CurrentTheme
        {
            get
            {
                if (Session["CurrentTheme"] != null)
                    return Session["CurrentTheme"].ToString();
                return "Default";
            }
            set
            {
                Session["CurrentTheme"] = value;
            }
        }

        public string Currency
        {
            get
            {
                if (Session["Currency"] != null)
                    return Session["Currency"].ToString();
                return "0";
            }
            set
            {
                Session["Currency"] = value;
            }
        }

        public string SearchText
        {
            get
            {
                return PrimaryKeyIdAsString("SearchText");
            }
            set
            {
                ViewState["SearchText"] = value;
            }
        }

        public int ProdType
        {
            get
            {
                return PrimaryKeyId("ProdType");
            }
            set
            {
                ViewState["ProdType"] = value;
            }
        }


        protected string oldSalePrice(string salePrice, bool rusVers)
        {
            if (string.IsNullOrEmpty(salePrice))
                return "";
            if (rusVers)
                return "<span class=\"prodthumb_oldprice\">" + salePrice + "</span>";
            return "&nbsp;<span class=;salePrice'><s>" + ConvertPrice(salePrice) + "</s></span>&nbsp";
        }

        protected string oldPlatSalePrice(string salePrice, bool rusVers)
        {
            if (string.IsNullOrEmpty(salePrice) || salePrice=="0,00р.")
                return "";
            if (rusVers)
                return "&nbsp;<span class=;salePrice'><s>" + salePrice + "</s></span>";
            return "&nbsp;<span class=;salePrice'><s>" + ConvertPrice(salePrice) + "</s></span>&nbsp";
        }

        public void ShowMessage(string Message)
        {
            Response.Write("<Script language='Javascript'>alert('" + Message + "')</Script>");
        }

        protected string ConvertPrice(string strPrice)
        {
            using (RateRepository lRepository = new RateRepository())
            {
                int icur = int.Parse(Currency);
                if(icur>0)
                {
                    Rate rate = lRepository.GetRateById(icur);
                    if (rate.RateUS <= 0)
                        return "";
                    decimal inPrice = decimal.Parse(strPrice);
                    decimal outPrice = inPrice / rate.RateUS;
                    return string.Format("{0:F2} {1}", outPrice, rate.Currency);
                }
                return strPrice + " Rub.";

            }
        }

        public void CreateMetaControl(string sTagName, string TagValue)
        {
            if (!string.IsNullOrEmpty(TagValue))
            {
                HtmlMeta meta = new HtmlMeta();
                meta.Name = sTagName;
                meta.Content = TagValue;
                if ((null != Master) && ((null != Master.Page) ? 1 : 0) != 0)
                {
                    Master.Page.Header.Controls.Add(meta);
                }
                else
                {
                    Page.Header.Controls.Add(meta);
                }
            }
        }
    }
}
