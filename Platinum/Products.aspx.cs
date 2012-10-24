using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class Platinum_Products : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Sort = ProductSorted.DateAsc;
            //int count = GetProductCount();
            SetDefultPageSize();
            int count = BindProducts();
            pnlNavigation.Visible = (count != 0);
            lblCounts.Text = string.Format("из: {0}", count);

            
            LoadHeadersData();
        }
        CreateSorting();
    }




    public PageType PageTypeProperty
    {
        get
        {
            using (CathegoryRepository lRepository = new CathegoryRepository())
            {
                if (CategoryId > 0)
                    return lRepository.GetPageTypeByCategory(CategoryId);
                if (GroupId > 0)
                    return lRepository.GetPageTypeByGroupId(GroupId, ProdType);
                return PageType.Unknown;

            }
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
                
                lProducts = lRepository.GetActiveProductsByGroup(GroupId, Sort);
                lvProducts.DataSource = lProducts;
                lvProducts.DataBind();
            }
            else
            {
                Response.Redirect("http://platinumshoes.ru");
            }
            if (lProducts.Count <= pagerBottom.PageSize)
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
                ddlPageSize.SelectedValue = "18";
                pagerBottom.SetPageProperties(0, 18, false);
            }
            else if (pageType == PageType.Lohg || pageType==PageType.Unknown)
            {
                ddlPageSize.SelectedValue = "9";
                pagerBottom.SetPageProperties(0, 9, false);
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
    

    protected void LoadHeadersData()
    {
        string extraText = " - Эксклюзивная обувь - Platinum Shoes.Интернет-магазин, салон-ателье. Обувь на заказ.";
        if (CategoryId > 0)
        {
            using (CathegoryRepository lRepository = new CathegoryRepository())
            {
                Cathegory cathegory = lRepository.GetCathegoryById(CategoryId);
                if (cathegory != null)
                {
                    lblDescription.Text = cathegory.DescriptionRus;
                    Page.Title = cathegory.CatNameRus + extraText;
                }
                else
                {
                    Response.Redirect("http://platinumshoes.ru");
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
                    Page.Title = _group.GroupNameRus+extraText;
                    lblDescription.Text = "";
                }
                else
                {
                    Response.Redirect("http://platinumshoes.ru");
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

    //в CreateSorting imagebuttom передается в имени команды способ сортировки из enum
    //следующие значения:
    //public enum ProductSorted
    //{
   //     NameAsc=0,NameDesc=1,PriceAsc=2,PriceDesc=3,DateAsc=4,DateDesc=5,UpdateAsc=6,UpdateDesc=7
   // }
    
    private void CreateSorting()
    {
        phlSorting.Controls.Clear();
        if (Sort == ProductSorted.NameAsc || Sort == ProductSorted.NameDesc)
        {
            Label lblTitleSort = new Label();
            lblTitleSort.ID = "lblTitleSort";
            lblTitleSort.Text = "названию";
            phlSorting.Controls.Add(lblTitleSort);
            phlSorting.Controls.Add(new LiteralControl(" "));
            if (Sort == ProductSorted.NameAsc)
            {
                ImageButton imbtn = new ImageButton();
                imbtn.ID = "imbtnSort";
                imbtn.CommandName = "1";//NameDesc
                imbtn.ImageUrl = @"Images/arrdown2.gif";
                imbtn.AlternateText = "Текущая сортировка по возрастанию, кликните для сортировки по убыванию";
                imbtn.Click += new ImageClickEventHandler(imbtn_Click);
                phlSorting.Controls.Add(imbtn);
            }
            else
            {
                ImageButton imbtn = new ImageButton();
                imbtn.ID = "imbtnSort";
                imbtn.CommandName = "0";//NameAsc
                imbtn.ImageUrl = @"Images/arrup2.gif";
                imbtn.AlternateText = "Текущая сортировка по убыванию, кликните для сортировки по возрастанию";
                imbtn.Click += new ImageClickEventHandler(imbtn_Click);
                phlSorting.Controls.Add(imbtn);
            }
        }
        else
        {
            LinkButton lbtnTitleSort = new LinkButton();
            lbtnTitleSort.ID = "lbtnTitleSort";
            lbtnTitleSort.Text = "названию";
            lbtnTitleSort.Click += new EventHandler(lbtnTitleSort_Click);
            phlSorting.Controls.Add(lbtnTitleSort);
            phlSorting.Controls.Add(new LiteralControl(" "));
        }
        phlSorting.Controls.Add(new LiteralControl(", "));
        if (Sort == ProductSorted.PriceAsc || Sort == ProductSorted.PriceDesc)
        {
            Label lblPriceSort = new Label();
            lblPriceSort.ID = "lblPriceSort";
            lblPriceSort.Text = "цене";
            phlSorting.Controls.Add(lblPriceSort);
            phlSorting.Controls.Add(new LiteralControl(" "));
            if (Sort == ProductSorted.PriceAsc)
            {
                ImageButton imbtn = new ImageButton();
                imbtn.ID = "imbtnSort";
                imbtn.CommandName = "3";//PriceDesc
                imbtn.ImageUrl = @"Images/arrdown2.gif";
                imbtn.AlternateText = "Текущая сортировка по возрастанию, кликните для сортировки по убыванию";
                imbtn.Click += new ImageClickEventHandler(imbtn_Click);
                phlSorting.Controls.Add(imbtn);
            }
            else
            {
                ImageButton imbtn = new ImageButton();
                imbtn.ID = "imbtnSort";
                imbtn.CommandName = "2";//PriceAsc
                imbtn.ImageUrl = @"Images/arrup2.gif";
                imbtn.AlternateText = "Текущая сортировка по убыванию, кликните для сортировки по возрастанию";
                imbtn.Click += new ImageClickEventHandler(imbtn_Click);
                phlSorting.Controls.Add(imbtn);
            }
        }
        else
        {
            LinkButton lbtnPriceSort = new LinkButton();
            lbtnPriceSort.ID = "lbtnPriceSort";
            lbtnPriceSort.Text = "цене";
            lbtnPriceSort.Click += new EventHandler(lbtnPriceSort_Click);
            phlSorting.Controls.Add(lbtnPriceSort);
            phlSorting.Controls.Add(new LiteralControl(" "));
        }
        phlSorting.Controls.Add(new LiteralControl(", "));
        if (Sort == ProductSorted.DateAsc || Sort == ProductSorted.DateDesc)
        {
            Label lblDateSort = new Label();
            lblDateSort.ID = "lblDateSort";
            lblDateSort.Text = "новизне";
            phlSorting.Controls.Add(lblDateSort);
            phlSorting.Controls.Add(new LiteralControl(" "));
            if (Sort == ProductSorted.DateAsc)
            {
                ImageButton imbtn = new ImageButton();
                imbtn.ID = "imbtnSort";
                imbtn.CommandName = "5";//DateDesc
                imbtn.ImageUrl = @"Images/arrdown2.gif";
                imbtn.AlternateText = "Текущая сортировка по возрастанию, кликните для сортировки по убыванию";
                imbtn.Click += new ImageClickEventHandler(imbtn_Click);
                phlSorting.Controls.Add(imbtn);
            }
            else
            {
                ImageButton imbtn = new ImageButton();
                imbtn.ID = "imbtnSort";
                imbtn.CommandName = "4";//DateAsc
                imbtn.ImageUrl = @"Images/arrup2.gif";
                imbtn.AlternateText = "Текущая сортировка по убыванию, кликните для сортировки по возрастанию";
                imbtn.Click += new ImageClickEventHandler(imbtn_Click);
                phlSorting.Controls.Add(imbtn);
            }
        }
        else
        {
            LinkButton lbtnDateSort = new LinkButton();
            lbtnDateSort.ID = "lbtnDateSort";
            lbtnDateSort.Text = "новизне";
            lbtnDateSort.Click += new EventHandler(lbtnDateSort_Click);
            phlSorting.Controls.Add(lbtnDateSort);
            phlSorting.Controls.Add(new LiteralControl(" "));
        }
    }
    protected void imbtn_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imbtn = (ImageButton)sender;
        Sort = (ProductSorted) int.Parse(imbtn.CommandName);
        BindProducts();
        CreateSorting();
    }
    protected void lbtnPriceSort_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.PriceAsc;
        BindProducts();
        CreateSorting();
    }
    protected void lbtnDateSort_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.DateAsc;
        BindProducts();
        CreateSorting();
    }
    protected void lbtnTitleSort_Click(object sender, EventArgs e)
    {
        Sort = ProductSorted.NameAsc;
        BindProducts();
        CreateSorting();
    }

    protected string formatFunction(object obj, bool isShow)
    {
        int i = (int)obj;
        if (isShow)
            return "ShowPreview('img_block_" + i.ToString() + "');";
        return "HidePreview('img_block_" + i.ToString() + "');";
    }
     
}
