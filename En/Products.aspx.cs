using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class En_Products : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Подключаемя к обработчик к событию на мастер странице
        Master.DDlCurrency.SelectedIndexChanged += new EventHandler(DDlCurrency_SelectedIndexChanged);
        if (!IsPostBack)
        {
            Sort = ProductSorted.DateAsc;
            //int count = GetProductCount();
            int count = BindProducts();
            pnlNavigationTop.Visible = (count != 0);
            pnlNavigationBottom.Visible = (count != 0);
            lblCounts.Text = string.Format(" {0}", count);
            lblCount2.Text = lblCounts.Text;
            //Для внешнего запроса, напрямую указывающего что необходимо вывести весь товар на одной странице
            if (Request.QueryString["all"] != null)
            {
                ddlPageSize.SelectedValue = "0";
                ddlPageSize2.SelectedValue = "0";
                pagerBottom.SetPageProperties(0, count, true);
                pagerTop.SetPageProperties(0, count, true);
            }
            else
            {
                SetDefultPageSize();
            }
            LoadHeadersData();
        }
    }

    protected int BindProducts()
    {
        using (ProductRepository lRepository = new ProductRepository())
        {
            List<Product> lProducts = new List<Product>();
            if (CategoryId > 0)
            {
                lProducts = lRepository.GetActiveProductsByCategory(CategoryId, Sort);
                lvProducts.DataSource = lProducts;
                lvProducts.DataBind();
            }
            else if (GroupId > 0)
            {
                if (GroupId == 2)
                {
                    // Специально для новинок
                    if (ProdType == 0 || ProdType > 2)
                        Response.Redirect("Default.aspx");
                    lProducts = lRepository.GetActiveProductsByGroupAndProdType(GroupId, Sort, ProdType);
                }
                else
                    lProducts = lRepository.GetActiveProductsByGroup(GroupId, Sort);
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

    protected void SetPageSize()
    {
        int allCount = int.Parse(ddlPageSize.SelectedValue);
        if (allCount == 0)
            allCount = BindProducts();
        pagerBottom.SetPageProperties(0, allCount, false);
    }

    protected void SetDefultPageSize()
    {
        using (CathegoryRepository lRepository = new CathegoryRepository())
        {
            PageType pageType = 0;
            if (CategoryId > 0)
                pageType = lRepository.GetPageTypeByCategory(CategoryId);
            else if (GroupId > 0)
                pageType = lRepository.GetPageTypeByGroupId(GroupId, ProdType);
            if (pageType == PageType.Short)
            {
                ddlPageSize.SelectedValue = "16";
                ddlPageSize2.SelectedValue = "16";
                pagerBottom.PageSize = 16;
                pagerTop.PageSize = 16;
                pagerBottom.SetPageProperties(0, 16, false);
                pagerTop.SetPageProperties(0, 16, false);
            }
            else //if(pageType==PageType.Lohg)
            {
                pagerBottom.PageSize = 8;
                pagerTop.PageSize = 8;
                ddlPageSize.SelectedValue = "8";
                ddlPageSize2.SelectedValue = "8";
                pagerBottom.SetPageProperties(0, 8, false);
                pagerTop.SetPageProperties(0, 8, false);
            }
        }
    }

    protected void lvProducts_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindProducts();
    }


    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPageSize2.SelectedValue = ddlPageSize.SelectedValue;
        SetPageSize();
        BindProducts();
    }
    protected void ddlPageSize2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPageSize.SelectedValue = ddlPageSize2.SelectedValue;
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
        if (CategoryId > 0)
        {
            using (CathegoryRepository lRepository = new CathegoryRepository())
            {
                Cathegory cathegory = lRepository.GetCathegoryById(CategoryId);
                if (cathegory != null)
                {
                    Master.MainHeader = cathegory.CatNameEng;
                    Master.Title = "Echo of Hollywood - " + cathegory.CatNameEng;
                    CreateMetaControl("description", !string.IsNullOrEmpty(cathegory.MetaDescription) ? cathegory.MetaDescription : cathegory.CatNameRus);
                    CreateMetaControl("keywords", cathegory.MetaKeywords);
                    lblDescription.Text = string.Format("<div id='first_description'>{0}</div>", cathegory.DescriptionEng);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        else if (GroupId > 0)
        {
            using (GroupRepository lRepository = new GroupRepository())
            {
                Group _group = lRepository.GetGroupById(GroupId);
                if (_group != null)
                {
                    Master.MainHeader = _group.GroupNameEng;
                    Master.Title = _group.GroupNameEng;
                    lblDescription.Text = "";
                   
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
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

    protected void DDlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProducts();
    }
}