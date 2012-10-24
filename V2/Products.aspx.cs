using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class V2_Products : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Sort = ProductSorted.DateAsc;
            //int count = GetProductCount();
            int count = BindProducts();
            pnlNavigation.Visible = (count !=0);
            lblCounts.Text = string.Format("Всего предложений: {0}", count);
            SetDefultPageSize();
            
            LoadHeadersData();
        }
    }

     

   
    public PageType PageTypeProperty
    {
        get
        {
           using(CathegoryRepository lRepository=new CathegoryRepository())
           {
               if (CategoryId > 0)
                   return lRepository.GetPageTypeByCategory(CategoryId);
               if (GroupId > 0)
                   return lRepository.GetPageTypeByGroupId(GroupId,ProdType);
               return PageType.Unknown;

           }
        }
    }

    

    protected int BindProducts()
    {
        using(ProductRepository lRepository=new ProductRepository())
        {
            List<Product> lProducts=new List<Product>();
            if(CategoryId > 0)
            {
                lProducts=lRepository.GetActiveProductsByCategory(CategoryId, Sort);
                lvProducts.DataSource = lProducts;
                lvProducts.DataBind();
            }
            else if (GroupId>0)
            {
                if(GroupId==2)
                {
                    // Специально для новинок
                    if(ProdType==0 || ProdType >2)
                        Response.Redirect("http://www.Stripshoes.ru");
                    lProducts = lRepository.GetActiveProductsByGroupAndProdType(GroupId, Sort, ProdType);
                }
                else 
                    lProducts = lRepository.GetActiveProductsByGroup(GroupId, Sort);
                lvProducts.DataSource = lProducts;
                lvProducts.DataBind();
            }
            else
            {
                Response.Redirect("http://www.Stripshoes.ru");
            }
            if(lProducts.Count<=pagerBottom.PageSize)
            {
                pnlPager.Visible = false;
            }
            else
            {
                pnlPager.Visible = true;
            }
            return lProducts.Count;
           
        }
    }

    protected void SetPageSize()
    {
        int allCount = int.Parse(ddlPageSize.SelectedValue);
        if (allCount==0)
            allCount = BindProducts();
        pagerBottom.SetPageProperties(0,allCount,false);
    }

    protected void SetDefultPageSize()
    {
        using(CathegoryRepository lRepository=new CathegoryRepository())
        {
            PageType pageType=0;
            if (CategoryId > 0)
                pageType = lRepository.GetPageTypeByCategory(CategoryId);
            else if (GroupId > 0)
                pageType = lRepository.GetPageTypeByGroupId(GroupId,ProdType);
            if (pageType==PageType.Short)
            {
                ddlPageSize.SelectedValue = "16";
                pagerBottom.SetPageProperties(0,16,false);
            }
            else if(pageType==PageType.Lohg)
            {
                ddlPageSize.SelectedValue = "8";
                pagerBottom.SetPageProperties(0, 8, false);
            }
        }
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
        List<LinkButton> linkButtons=new List<LinkButton>();
        linkButtons.Add(NameASC);
        linkButtons.Add(NameDESC);
        linkButtons.Add(DateASC);
        linkButtons.Add(DateDESC);
        linkButtons.Add(PriceASC);
        linkButtons.Add(PriceDESC);

        foreach(LinkButton linkButton in linkButtons)
        {
            if(lbtn==linkButton)
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
                    Master.HeaderTitle = cathegory.CatNameRus;
                    Master.MainTitle = cathegory.CatNameRus;
                    lblDescription.Text = cathegory.DescriptionRus;
                }
                else
                {
                    Response.Redirect("http://www.Stripshoes.ru");
                }
            }
        }
        else if (GroupId > 0)
        {
            using (GroupRepository lRepository = new GroupRepository())
            {
                Group _group = lRepository.GetGroupById(GroupId);
                if(_group !=null)
                {
                    Master.HeaderTitle = _group.GroupNameRus;
                    Master.MainTitle = _group.GroupNameRus;
                    lblDescription.Text = "";
                }
            else
                {
                    Response.Redirect("http://www.Stripshoes.ru");
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
        if (PageTypeProperty == PageType.Lohg)
            numberOfLetters = 350;
        else
            numberOfLetters = 150;

        if (description.Length < numberOfLetters)
            return description;

        return description.Substring(0, numberOfLetters) + "...";
    }
}
