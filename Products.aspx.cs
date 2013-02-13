using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class Products : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Sort = ProductSorted.DateAsc;
            //int count = GetProductCount();
            if(CategoryId==54 || CategoryId==55) //это новинки
                Sort=ProductSorted.DateDesc;
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
            
        }
        LoadHeadersData();
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
                ddlPageSize.SelectedValue = "32";
                ddlPageSize2.SelectedValue = "32";
                pagerBottom.PageSize = 32;
                pagerTop.PageSize = 32;
                pagerBottom.SetPageProperties(0, 32, false);
                pagerTop.SetPageProperties(0, 32, false);
            }
            else //if(pageType==PageType.Lohg)
            {
                pagerBottom.PageSize = 16;
                pagerTop.PageSize = 16;
                ddlPageSize.SelectedValue = "16";
                ddlPageSize2.SelectedValue = "16";
                pagerBottom.SetPageProperties(0, 16, false);
                pagerTop.SetPageProperties(0, 16, false);
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
                    Master.MainHeader = cathegory.CatNameRus;
                    Master.Title = string.IsNullOrEmpty(cathegory.GroupTitle) ? cathegory.CatNameRus : cathegory.GroupTitle;
                    CreateMetaControl("description", !string.IsNullOrEmpty(cathegory.MetaDescription) ? cathegory.MetaDescription : cathegory.CatNameRus);
                    CreateMetaControl("keywords", cathegory.MetaKeywords);
                    lblDescription.Text = string.Format("<div id='first_description'>{0}</div>", cathegory.DescriptionRus);
                    lblDescription2.Text = string.Format("<div id='last_description'>{0}</div>", cathegory.Description2Rus);
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
                    Master.MainHeader = _group.GroupNameRus;
                    Master.Title = _group.GroupNameRus;
                    lblDescription.Text = "";
                    lblDescription2.Text = "";
                    //Вставим такой вот костыль
                    if (GroupId == 3)
                    {
                        Master.Title = "Обувь для стриптиза, стрип обувь для танцев Go-Go ( Гоу-Гоу ), купить туфли стрипы - интернет-магазин Эхо Голливуда";
                        CreateMetaControl("description", "В интернет-магазине Эхо Голливуда вы найдете богатый ассортимент обуви для стриптиза, обуви для танцев и стрип обуви, купить которую можно и оптом, ведь мы являемся производителями продукции бренда Echo of Hollywood.");
                        CreateMetaControl("keywords", "обувь для стриптиза,  магазин обуви для стриптиза, обувь для танцев, обувь для go go, туфли для стриптиза, стрип обувь, туфли стрипы");
                    }
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
            if(c_count>1 && c_count<5)
                return string.Format("<span class=\"prodthumb_color\">{0} варианта цвета</span>", c_count);
            if (c_count > 4)
                return string.Format("<span class=\"prodthumb_color\">{0} вариантов цвета</span>", c_count);
            return "";
        }
    }
}