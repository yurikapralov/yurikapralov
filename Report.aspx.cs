using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class Report : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCategories();
            BindGroups();
        }
    }


    protected void BindCategories()
    {
        using (CathegoryRepository lRepository = new CathegoryRepository())
        {
            ddlSelectedCategory.DataSource = lRepository.GetCathegories();
            ddlSelectedCategory.DataBind();
        }
        AddGeneralItems(ddlSelectedCategory);
    }

    protected void BindGroups()
    {
        using (GroupRepository lRepository = new GroupRepository())
        {
            ddlSelectedGroups.DataSource = lRepository.GetGroups();
            ddlSelectedGroups.DataBind();
        }
        AddGeneralItems(ddlSelectedGroups);
    }

    protected void AddGeneralItems(DropDownList control)
    {
        control.Items.Insert(0, new ListItem("Вся продукция", "0"));
        control.Items.Insert(1, new ListItem("Обувь", "-1"));
        control.Items.Insert(2, new ListItem("Одежда", "-2"));
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rblReportMode.SelectedValue == "0")
        {
            BingGeneralList();
        }
        else
        {
            BindTotalList();
        }
    }

    protected void BingGeneralList()
    {
        pnlGenetal.Visible = true;
        pnlTotal.Visible = false;
        if (TabContainer1.ActiveTabIndex == 0)
        {
            lblTitleGeneral.Text = ddlSelectedCategory.SelectedItem.Text;
            title1.Text = lblTitleGeneral.Text;
            int catId = int.Parse(ddlSelectedCategory.SelectedValue);
            using (ProductRepository lRepository = new ProductRepository())
            {
                List<Product> products = null;
                if (catId == 0)
                    products = cbxActive.Checked ? lRepository.GetActiveProducts(ProductSorted.NameAsc) : lRepository.GetAllProducts(ProductSorted.NameAsc);
                else if (catId == -1)
                    products = cbxActive.Checked ? lRepository.GetActiveProductsByProductType(1, ProductSorted.NameAsc) : lRepository.GetAllProductsByProductType(1, ProductSorted.NameAsc);
                else if (catId == -2)
                    products = cbxActive.Checked ? lRepository.GetActiveProductsByProductType(2, ProductSorted.NameAsc) : lRepository.GetAllProductsByProductType(2, ProductSorted.NameAsc);
                else
                    products = cbxActive.Checked ? lRepository.GetActiveProductsByCategory(catId, ProductSorted.NameAsc) : lRepository.GetAllProductsByCategory(catId, ProductSorted.NameAsc);
                lvProducts.DataSource = products;
                lvProducts.DataBind();
                lblCount.Text = string.Format("Всего предложений: {0}", products.Count);
            }
        }
        else
        {
            lblTitleGeneral.Text = ddlSelectedGroups.SelectedItem.Text;
            title1.Text = lblTitleGeneral.Text;
            int groupId = int.Parse(ddlSelectedGroups.SelectedValue);
            using (ProductRepository lRepository = new ProductRepository())
            {
                List<Product> products = null;
                if (groupId == 0)
                    products = cbxActive.Checked ? lRepository.GetActiveProducts(ProductSorted.NameAsc) : lRepository.GetAllProducts(ProductSorted.NameAsc);
                else if (groupId == -1)
                    products = cbxActive.Checked ? lRepository.GetActiveProductsByProductType(1, ProductSorted.NameAsc) : lRepository.GetAllProductsByProductType(1, ProductSorted.NameAsc);
                else if (groupId == -2)
                    products = cbxActive.Checked ? lRepository.GetActiveProductsByProductType(2, ProductSorted.NameAsc) : lRepository.GetAllProductsByProductType(2, ProductSorted.NameAsc);
                else
                    products = cbxActive.Checked ? lRepository.GetActiveProductsByGroup(groupId,ProductSorted.NameAsc): lRepository.GetAllProductsByGroup(groupId, ProductSorted.NameAsc);
                lvProducts.DataSource = products;
                lvProducts.DataBind();
                lblCount.Text = string.Format("Всего предложений: {0}", products.Count);
            }
        }
    }
    protected void lvProducts_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Panel pnlGeneralPrice = (Panel)e.Item.FindControl("pnlGeneralPrice");
            pnlGeneralPrice.Visible = cbxShowPrice.Checked;
        }
    }

    protected void BindTotalList()
    {
        pnlGenetal.Visible = false;
        pnlTotal.Visible = true;
        if (TabContainer1.ActiveTabIndex == 0)
        {
            lblTitleTotal.Text = ddlSelectedCategory.SelectedItem.Text;
            title1.Text = lblTitleTotal.Text;
            int catId = int.Parse(ddlSelectedCategory.SelectedValue);
            using (ProductColorRepository lRepository = new ProductColorRepository())
            {
                List<ProductColor> products = null;
                 if (catId == 0)
                     products = cbxActive.Checked ? lRepository.GetActiveProductColors(): lRepository.GetAllProductColors();
                 else if (catId == -1)
                     products = cbxActive.Checked ? lRepository.GetActiveProductColorsByProductType(1):lRepository.GetAllProductColorsByProductType(1);
                 else if (catId == -2)
                     products = cbxActive.Checked ? lRepository.GetActiveProductColorsByProductType(2):lRepository.GetAllProductColorsByProductType(2);
                 else
                      products = cbxActive.Checked ? lRepository.GetActiveProductColorsByCathegory(catId) : lRepository.GetAllProductColorsByCathegory(catId);
                /*List<ProductColor> productColors=new List<ProductColor>();
                productColors.AddRange(products.Take(200));*/
                lvProductColors.DataSource = products;
                lvProductColors.DataBind();
                lblCount.Text = string.Format("Всего предложений: {0}", products.Count);
            }
        }
        else
        {
            lblTitleTotal.Text = ddlSelectedCategory.SelectedItem.Text;
            title1.Text = lblTitleTotal.Text;
            int groupId = int.Parse(ddlSelectedGroups.SelectedValue);
            using (ProductColorRepository lRepository = new ProductColorRepository())
            {
                List<ProductColor> products = null;
                if (groupId == 0)
                    products = cbxActive.Checked ? lRepository.GetActiveProductColors() : lRepository.GetAllProductColors();
                else if (groupId == -1)
                    products = cbxActive.Checked ? lRepository.GetActiveProductColorsByProductType(1) : lRepository.GetAllProductColorsByProductType(1);
                else if (groupId == -2)
                    products = cbxActive.Checked ? lRepository.GetActiveProductColorsByProductType(2) : lRepository.GetAllProductColorsByProductType(2);
                else
                    products = cbxActive.Checked ? lRepository.GetActiveProductColorsByGroup(groupId) : lRepository.GetAllProductColorsByGroup(groupId);
                lvProductColors.DataSource = products;
                lvProductColors.DataBind();
                lblCount.Text = string.Format("Всего предложений: {0}", products.Count);
            }
        }
    }
}
