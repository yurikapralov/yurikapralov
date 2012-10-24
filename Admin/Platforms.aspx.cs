using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;
using System.Text;

public partial class Admin_Platforms : AdminPage
{
    #region general
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            Master.Title = "Управление платформами, размерами, цветами.";
            Master.HeaderText = "Управление платформами, размерами, цветами.";
            BindPlatforms();
            BindProductTypes();
            BindColors();
            BindBrands();
        }
       
    }

    protected void ShowErrorMessage(string ErrorMessage)
    {
        pnlErorr.Visible = true;
        lblErrorMessage.Text = ErrorMessage;
    }

    protected void CloseErrorMessage()
    {
        lblErrorMessage.Text = "";
        pnlErorr.Visible = false;
    }

    protected void lbtnCloseErrorMessage_Click(object sender, EventArgs e)
    {
        CloseErrorMessage();
    }



    #endregion

    #region platforms
    public PlatformSortField SortField
    {
        get
        {
            if (ViewState["PlatformSortField"] != null)
                return (PlatformSortField)ViewState["PlatformSortField"];
            return PlatformSortField.Name;
        }
        set
        {
            ViewState["PlatformSortField"] = value;
        }
    }

    public PlatformSortType SortType
    {
        get
        {
            if (ViewState["PlatformSortType"] != null)
                return (PlatformSortType)ViewState["PlatformSortType"];
            return PlatformSortType.Asc;
        }
        set
        {
            ViewState["PlatformSortType"] = value;
        }
    }

    protected void BindPlatforms()
    {
        using (PlatformRepository lRepository = new PlatformRepository())
        {
            List<Platform> platforms = lRepository.GetPlatforms(SortField, SortType);
            lvPlatforms.DataSource = platforms;
            lvPlatforms.DataBind();
            if (platforms.Count < pagerBottom.PageSize)
                pnlPager.Visible = false;
            else
                pnlPager.Visible = true;
        }
    }

    protected void BindPlatform(int platformId)
    {
        using (PlatformRepository lRepository = new PlatformRepository())
        {
            PlatformId = platformId;
            Platform platform = lRepository.GetPlatformById(platformId);
            lblEditHeader.Text = string.Format("Редактирование платформы:{0}", platform.PlatformNameRus);
            txtPlatformNameRus.Text = platform.PlatformNameRus;
            txtDescriptionRus.Text = platform.PlatformDescriptionRus;
            txtDescriptionEng.Text = platform.PlatformDescriptionEng;
            txtSizeInfo.Text = platform.SizeInfo;
            btnAddPlatform.Text = "Изменить";
        }
    }

    protected void ClearPlatform()
    {
        PlatformId = 0;
        lblEditHeader.Text = "Добавление новой платформы";
        btnAddPlatform.Text = "Добавить";
        txtPlatformNameRus.Text = "";
        txtDescriptionRus.Text = "Высота платформы см<br/>Высота каблука см.";
        txtDescriptionEng.Text = "";
        txtSizeInfo.Text = "";
        lvPlatforms.SelectedIndex = -1;
        BindPlatforms();
    }

    protected void UpdatePlatform()
    {
        using (PlatformRepository lRepository = new PlatformRepository())
        {
            Platform platform;
            if (PlatformId > 0)
            {
                platform = lRepository.GetPlatformById(PlatformId);
                platform.DateUpdated = DateTime.Now;
            }
            else
            {
                platform = new Platform();
                platform.DateCreated = DateTime.Now;
            }
            platform.PlatformNameRus = txtPlatformNameRus.Text;
            platform.PlatformNameEng = txtPlatformNameRus.Text;
            platform.PlatformDescriptionRus = txtDescriptionRus.Text;
            platform.PlatformDescriptionEng = txtDescriptionEng.Text;
            platform.SizeInfo = txtSizeInfo.Text;

            platform = lRepository.AddPlatform(platform);
        }
    }

    protected void lvPlatforms_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindPlatforms();
    }

    protected void lblSorted_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        PlatformSortField selSortField = (PlatformSortField)Enum.Parse(typeof(PlatformSortField), lbtn.CommandArgument);


        if (SortField == selSortField)
        {
            if (SortType == PlatformSortType.Asc)
                SortType = PlatformSortType.Desc;
            else
                SortType = PlatformSortType.Asc;
        }
        else
        {
            SortField = selSortField;
            SortType = PlatformSortType.Asc;
        }

        BindPlatforms();
    }

    protected void lbtnSelect_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        int platfromId = int.Parse(lbtn.CommandArgument);
        BindPlatform(platfromId);
    }
    protected void lvPlatforms_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        lvPlatforms.SelectedIndex = e.NewSelectedIndex;
        BindPlatforms();
    }
    protected void btnAddPlatform_Click(object sender, EventArgs e)
    {
        UpdatePlatform();
        ClearPlatform();
    }
    protected void btnCancelPlatform_Click(object sender, EventArgs e)
    {
        ClearPlatform();
    }
    protected void lvPlatforms_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        using (PlatformRepository lRepository = new PlatformRepository())
        {
            int platId = int.Parse(lvPlatforms.DataKeys[e.ItemIndex].Value.ToString());
            List<string> products = lRepository.GetListProductNamesUsingPlatform(platId);
            if (products.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                foreach (string productName in products)
                {
                    sb.Append(productName + " ");
                    i++;
                    if (i > 30)
                    {
                        sb.Append("и т.д.");
                        break;
                    }
                }
                ShowErrorMessage("Удаление платформы невозможно. Она используется в следующих моделях:\n" + sb.ToString());
            }
            else
            {
                lRepository.DeletePlatform(platId);
                ClearPlatform();
                BindPlatforms();
            }
        }
    }
    #endregion


    #region colors

    protected void BindColors()
    {
        using (ColorRepository lRepository = new ColorRepository())
        {
            List<Color> colors = lRepository.GetColors();
            lbColors.DataSource = colors;
            lbColors.DataBind();
        }
    }

    protected void BindColor(int colorId)
    {
        using (ColorRepository lRepository = new ColorRepository())
        {
            ColorId = colorId;
            Color color = lRepository.GetColorById(colorId);
            txtRusColor.Text = color.ColorNameRus;
            txtEngColor.Text = color.ColorNameEng;
            cbxIsMain.Checked = color.IsMain;
            btnAddColor.Text = "Изменить";
            btnDeleteColor.Visible = true;
        }
    }

    protected void UpdateColor()
    {
        using (ColorRepository lRepository = new ColorRepository())
        {
            Color color;
            if (ColorId > 0)
            {
                color = lRepository.GetColorById(ColorId);
            }
            else
            {
                color = new Color();
            }
            color.ColorNameRus = txtRusColor.Text;
            color.ColorNameEng = txtEngColor.Text;
            color.IsMain = cbxIsMain.Checked;

            color = lRepository.AddColor(color);
        }
    }

    protected void DeleteColor()
    {
        using (ColorRepository lRepository = new ColorRepository())
        {
            if (ColorId > 0)
            {
                List<string> products = lRepository.GetListProductNamesUsingColor(ColorId);
                if (products.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    foreach (string productName in products)
                    {
                        sb.Append(productName);
                        i++;
                        if (i > 30)
                        {
                            sb.Append(" и т.д.");
                            break;
                        }
                        sb.Append(", ");
                    }
                    ShowErrorMessage("Удаление цвета невозможно. Он используется в следующих моделях: " + sb.ToString());
                }
                else
                {
                    lRepository.DeleteColor(ColorId);
                    ClearColor();
                    BindColors();
                }
            }
        }
    }


    protected void ClearColor()
    {
        ColorId = 0;
        btnAddColor.Text = "Добавить";
        txtRusColor.Text = "";
        txtEngColor.Text = "";
        cbxIsMain.Checked = false;
        lbColors.SelectedIndex = -1;
        BindColors();
        btnDeleteColor.Visible = false;
    }

    protected void lbColors_SelectedIndexChanged(object sender, EventArgs e)
    {
        int colorId = int.Parse(lbColors.SelectedValue);
        BindColor(colorId);
    }

    protected void btnAddColor_Click(object sender, EventArgs e)
    {
        UpdateColor();
        ClearColor();
    }


    protected void btnDeleteColor_Click(object sender, EventArgs e)
    {
        DeleteColor();
    }

    protected void btnCancelColor_Click(object sender, EventArgs e)
    {
        ClearColor();
        CloseErrorMessage();
    }

    #endregion


    #region sizes


    protected void BindProductTypes()
    {
        ddlProductTypes.Items.Clear();
        Helpers.EnumToListBox(typeof(ProductType), ddlProductTypes);
        ddlProductTypes.Items.Insert(0, new ListItem("Все типы", "0"));
        BindSizes(0);
    }

    protected void ddlProductTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        int productType = int.Parse(ddlProductTypes.SelectedValue);
        BindSizes(productType);
    }

    protected void lbSizes_SelectedIndexChanged(object sender, EventArgs e)
    {
        int sizeId = int.Parse(lbSizes.SelectedValue);
        BindSize(sizeId);
    }

    protected void BindSizes(int productType)
    {
        using (SizeRepository lRepository = new SizeRepository())
        {
            List<Size> sizes = new List<Size>();

            if (productType == 0)
                sizes = lRepository.GetSizes();
            else
                sizes = lRepository.GetSizes(productType);

            lbSizes.DataSource = sizes;
            lbSizes.DataBind();
            Helpers.EnumToListBox(typeof(ProductType), ddlProductTypeEdit);
        }
    }

    protected void BindSize(int sizeId)
    {
        using (SizeRepository lRepository = new SizeRepository())
        {
            SizeId = sizeId;
            Size size = lRepository.GetSizeById(sizeId);
            txtRusSize.Text = size.SizeNameRus;
            txtEngSize.Text = size.SizeNameEng;
            ddlProductTypeEdit.SelectedValue = size.ProdTypeID.ToString();
            btnAddSize.Text = "Изменить";
            btnDeleteSize.Visible = true;
        }
    }

    protected void ClearSize()
    {
        SizeId = 0;
        btnAddSize.Text = "Добавить";
        txtRusSize.Text = "";
        txtEngSize.Text = "";
        ddlProductTypeEdit.SelectedIndex = -1;
        lbSizes.SelectedIndex = -1;
        BindSizes(int.Parse(ddlProductTypes.SelectedValue));
        btnDeleteSize.Visible = false;
    }

    protected void UpdateSize()
    {
        using (SizeRepository lRepository = new SizeRepository())
        {
            Size size;
            if (SizeId > 0)
            {
                size = lRepository.GetSizeById(SizeId);
            }
            else
            {
                size = new Size();
            }
            size.SizeNameRus = txtRusSize.Text;
            size.SizeNameEng = txtEngSize.Text;
            size.ProdTypeID = int.Parse(ddlProductTypeEdit.SelectedValue);
            size = lRepository.AddSize(size);
        }
    }

    protected void DeleteSize()
    {
        using (SizeRepository lRepository = new SizeRepository())
        {
            if (SizeId > 0)
            {
                List<string> products = lRepository.GetListProductNamesUsingSize(SizeId);
                if (products.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    foreach (string productName in products)
                    {
                        sb.Append(productName);
                        i++;
                        if (i > 30)
                        {
                            sb.Append(" и т.д.");
                            break;
                        }
                        sb.Append(", ");
                    }
                    ShowErrorMessage("Удаление размера невозможно. Он используется в следующих моделях: " + sb.ToString());
                }
                else
                {
                    lRepository.DeleteSize(SizeId);
                    ClearSize();
                    BindSizes(int.Parse(ddlProductTypes.SelectedValue));
                }
            }
        }
    }

    protected void btnCancelSize_Click(object sender, EventArgs e)
    {
        ClearSize();
        CloseErrorMessage();
    }

    protected void btnDeleteSize_Click(object sender, EventArgs e)
    {
        DeleteSize();
    }

    protected void btnAddSize_Click(object sender, EventArgs e)
    {
        UpdateSize();
        ClearSize();
    }

    #endregion

    #region brands

    protected void BindBrands()
    {
        using (BrandRepository rep = new BrandRepository())
        {
            List<Brand> brands = rep.GetBrands();
            lbBrands.DataSource = brands;
            lbBrands.DataBind();
        }
    }

    protected void BindBrand(int brandId)
    {
        using (BrandRepository rep = new BrandRepository())
        {
            BrandId = brandId;
            Brand brand = rep.GetBrandById(brandId);
            txtBrandName.Text = brand.BrandName;
            btnAddBrand.Text = "Изменить";
        }
    }

    protected void UpdateBrand()
    {
        using (BrandRepository rep = new BrandRepository())
        {
            Brand brand;
            if (BrandId > 0)
                brand = rep.GetBrandById(BrandId);
            else
                brand = new Brand();
            brand.BrandName = txtBrandName.Text;
            brand = rep.AddBrand(brand);
        }
    }

    protected void ClearBrand()
    {
        BrandId = 0;
        btnAddBrand.Text = "Добавить";
        txtBrandName.Text = "";
        lbBrands.SelectedIndex = -1;
        BindBrands();
    }

    protected void lbBrands_SelectedIndexChanged(object sender, EventArgs e)
    {
        int brandId = int.Parse(lbBrands.SelectedValue);
        BindBrand(brandId);
    }
    protected void btnAddBrand_Click(object sender, EventArgs e)
    {
        UpdateBrand();
        ClearBrand();
    }
    protected void btnCancelBrand_Click(object sender, EventArgs e)
    {
        ClearBrand();
    }

    #endregion
}