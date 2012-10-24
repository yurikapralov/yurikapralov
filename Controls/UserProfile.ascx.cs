using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Orders;

public partial class Controls_UserProfile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            LoadCities();
            LoadCountries();
            LoadProfile();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Page.RegisterRequiresControlState(this);
    }

    protected override void LoadControlState(object savedState)
    {
        object[] ctlState = (object[]) savedState;
        base.LoadControlState(ctlState[0]);
        _userName = (string) ctlState[1];
    }

    protected override object SaveControlState()
    {
        object[] ctlState=new object[2];
        ctlState[0] = base.SaveControlState();
        ctlState[1] = _userName;
        return ctlState;
    }

    private string _userName = "";

    public string UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }

    private void LoadCities()
    {
        using(CityRepository cityRepository=new CityRepository())
        {
            ddlCities.DataSource = cityRepository.GetCities();
            ddlCities.DataBind();
        }
    }

    private void LoadCountries()
    {
        using(CountryRepository countryRepository=new CountryRepository())
        {
            ddlCountries.DataSource = countryRepository.GetCountries();
            ddlCountries.DataBind();
        }
    }

    public  void LoadProfile()
    {
        ProfileCommon profile = this.Profile;
        if (UserName.Length > 0)
            profile = Profile.GetProfile(UserName);
        txtFirstName.Text = profile.FirstName;
        txtLastName.Text = profile.LastName;
        txtMiddleName.Text = profile.MiddleName;
        //Если не учтановлена страна, данные из профиля не считываются
        if (profile.Address.CountryId != 0)
        {
            int countryId = profile.Address.CountryId;
            ddlCountries.SelectedValue = countryId.ToString();
            if (countryId == 105)
            {
                pnlRusCountry.Visible = true;
                pnlNotRusCountry.Visible = false;
                if (profile.Address.CityId != 0)
                    ddlCities.SelectedValue = profile.Address.CityId.ToString();
                if ((int)profile.Address.RegionTypeId == 1)
                {
                    rblRegionType.SelectedValue = "1";
                    pnlRegionCity.Visible = false;
                }
                else
                {
                    rblRegionType.SelectedValue = "2";
                    pnlRegionCity.Visible = true;
                    txtCity2.Text = profile.Address.City2;
                }
                txtCityIndex.Text = profile.Address.PostalCode;
                txtStreet.Text = profile.Address.Street;
                txtHome.Text = profile.Address.House;
                txtKorpus.Text = profile.Address.Building;
                txtUnit.Text = profile.Address.Room;
            }
            else
            {
                txtAddress.Text = profile.Address.Address;
                pnlRusCountry.Visible = false;
                pnlNotRusCountry.Visible = true;
            }
            
        }
        txtPhone.Text = profile.Contacts.Phone;
        txtTime1.Text = profile.Contacts.fromContact;
        txtTime2.Text = profile.Contacts.toContact;
    }

    private void ClearRussianAddressData()
    {
        txtCityIndex.Text ="";
        txtStreet.Text = "";
        txtHome.Text = "";
        txtKorpus.Text = "";
        txtUnit.Text = "";
        txtCity2.Text = "";
    }

    public void SaveProfile()
    {
        ProfileCommon profile = this.Profile;
        if (UserName.Length > 0)
            profile = Profile.GetProfile(UserName);
        profile.FirstName = txtFirstName.Text;
        profile.LastName = txtLastName.Text;
        profile.MiddleName = txtMiddleName.Text;
        int countryId = int.Parse(ddlCountries.SelectedValue);
        profile.Address.CountryId = countryId;
        if(countryId==105)
        {
            txtAddress.Text = "";
            profile.Address.CityId = int.Parse(ddlCities.SelectedValue);
        }
        else
        {
            ClearRussianAddressData();
            // Записываем город "За пределы РФ" - код 253
            profile.Address.CityId = 253;
        }       
        profile.Address.Address = txtAddress.Text;
        if(rblRegionType.SelectedValue=="1")
        {
            profile.Address.RegionTypeId = RegionType.Center;
            txtCity2.Text = "";
        }
        else
        {
            profile.Address.RegionTypeId = RegionType.Area;
        }
        profile.Address.City2 = txtCity2.Text;
        profile.Address.PostalCode = txtCityIndex.Text;
        profile.Address.Street = txtStreet.Text;
        profile.Address.House = txtHome.Text;
        profile.Address.Building = txtKorpus.Text;
        profile.Address.Room = txtUnit.Text;
        profile.Contacts.Phone = txtPhone.Text;
        profile.Contacts.fromContact = txtTime1.Text;
        profile.Contacts.toContact = txtTime2.Text;
        
        profile.Save();
    }


    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCountries.SelectedIndex!=-1)
        {
            int countryId = int.Parse(ddlCountries.SelectedValue);
            if (countryId == 105)
            {
                pnlNotRusCountry.Visible = false;
                pnlRusCountry.Visible = true;
            }
            else
            {
                pnlNotRusCountry.Visible = true;
                pnlRusCountry.Visible = false;
            }
        }
    }

    protected void rblRegionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlRegionCity.Visible = rblRegionType.SelectedValue == "2" ? true : false;
    }
}
