using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class Search : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadHeadersData();
        if (!IsPostBack)
        {
            SetDefultPageSize();
            Sort = ProductSorted.DateAsc;
            int count = BindProducts();
            pnlNavigationTop.Visible = (count != 0);
            pnlNavigationBottom.Visible = (count != 0);
            lblCounts.Text = string.Format(" {0}", count);
        }
    }

    protected int BindProducts()
    {
        using (ProductRepository lRepository = new ProductRepository())
        {
            List<Product> lProducts = new List<Product>();
            if (SearchText != "")
            {
                lProducts = lRepository.GetSearchProducts(SearchText, Sort, ProdType);
                lvProducts.DataSource = lProducts;
                lvProducts.DataBind();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            if (lProducts.Count <= pagerBottom.PageSize)
            {
                pnlPager.Visible = false;
                pnlTopPager.Visible = false;
            }
            else
            {
                pnlPager.Visible = true;
                pnlTopPager.Visible = true;
            }
            return lProducts.Count;

        }
    }

    protected void
        SetPageSize()
    {
        int allCount = int.Parse(ddlPageSize.SelectedValue);
        if (allCount == 0)
            allCount = BindProducts();
        pagerBottom.SetPageProperties(0, allCount, false);
    }

    protected void SetDefultPageSize()
    {
        ddlPageSize.SelectedValue = "8";
        pagerBottom.SetPageProperties(0, 8, false);
    }

    protected void lvProducts_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindProducts();
    }


    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPageSize();
        BindProducts();
    }
    protected void NameASC_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.NameAsc;
        BindProducts();
        ChangeLinkButtonProperty((LinkButton)sender);
    }
    protected void NameDESC_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.NameDesc;
        BindProducts();
        ChangeLinkButtonProperty((LinkButton)sender);
    }
    protected void PriceASC_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.PriceAsc;
        BindProducts();
        ChangeLinkButtonProperty((LinkButton)sender);
    }
    protected void PriceDESC_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.PriceDesc;
        BindProducts();
        ChangeLinkButtonProperty((LinkButton)sender);
    }
    protected void DateASC_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.DateAsc;
        BindProducts();
        ChangeLinkButtonProperty((LinkButton)sender);
    }
    protected void DateDESC_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.DateDesc;
        BindProducts();
        ChangeLinkButtonProperty((LinkButton)sender);
    }

    protected void ChangeLinkButtonProperty(LinkButton lbtn)
    {
        List<LinkButton> linkButtons = new List<LinkButton>();
        linkButtons.Add(NameASC);
        linkButtons.Add(NameDESC);
        linkButtons.Add(DateASC);
        linkButtons.Add(DateDESC);
        linkButtons.Add(PriceASC);
        linkButtons.Add(PriceDESC);

        foreach (LinkButton linkButton in linkButtons)
        {
            if (lbtn == linkButton)
            {
                linkButton.Font.Underline = false;
                linkButton.Enabled = false;
                linkButton.Font.Bold = true;
            }
            else
            {
                linkButton.Font.Underline = true;
                linkButton.Enabled = true;
                linkButton.Font.Bold = false;
            }
        }
    }




    protected void LoadHeadersData()
    {
        Master.MainHeader = "Результаты поиска по запросу: "+SearchText;
        Master.Title = "Поиск обуви и одежды: - Результаты поиска по запросу: " + SearchText;
        //lblDescription.Text = string.Format("<div id='first_description'>Результаты поиска по запросу: '{0}'</div>", SearchText);
    }

    protected string DescRestrict(object oDescription)
    {
        if (oDescription == null)
            return "";
        string description = (string)oDescription;
        int numberOfLetters;

        numberOfLetters = 150;

        if (description.Length < numberOfLetters)
            return description;

        return description.Substring(0, numberOfLetters) + "...";
    }

    protected string formatFunction(object obj, bool isShow)
    {
        int i = (int)obj;
        if (isShow)
            return "ShowPreview('img_block_" + i.ToString() + "');";
        return "HidePreview('img_block_" + i.ToString() + "');";
    }

    /// <summary>
    /// В случе нескольких вариантов цвета выдает соответствующую надпись. В противном случае ничего не выдает
    /// </summary>
    /// <param name="prodId"></param>
    /// <returns></returns>
    protected string ColorVariation(object prodId)
    {
        int productId = (int)prodId;
        using (ProductColorRepository rep = new ProductColorRepository())
        {
            int c_count = rep.GetColorCount(productId);
            if (c_count > 1 && c_count < 5)
                return string.Format("<span class=\"prodthumb_color\">{0} варианта цвета</span>", c_count);
            if (c_count > 4)
                return string.Format("<span class=\"prodthumb_color\">{0} вариантов цвета</span>", c_count);
            return "";
        }
    }
}