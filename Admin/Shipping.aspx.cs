using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Orders;
using echo.BLL.Info;

public partial class Admin_Shipping : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Master.Title = "Регулирование тарифов доставки";
            Master.HeaderText = "Регулирование тарифов доставки";
            ddlDeliveryMethods.Items.Insert(0, new ListItem("--Выберите метод--", "0"));
            ddlZone.Items.Insert(0, new ListItem("--Выберите зону --", "-1"));
        }
    }
    protected void ddlDeliveryMethods_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem finded = ddlDeliveryMethods.Items.FindByValue("0");
        if (finded != null)
            ddlDeliveryMethods.Items.Remove(finded);
        int method = int.Parse(ddlDeliveryMethods.SelectedValue);
        switch (method)
        {
            case 1:
                BindEMS();
                break;
            case 2:
                BindMoscowPrice();
                break;
        }
    }
    protected void BindEMS()
    {
        pnlEMS.Visible = true;
        pnlMoscow.Visible = false;
        BindCities();
        lblDeliveyTitle.Text = "EMS Гарантпост";
    }

    protected void BindCities()
    {
        using (CityRepository lCityRepository = new CityRepository())
        {
            List<City> cities = lCityRepository.GetCities();
            lbCiities.DataSource = cities;
            lbCiities.DataBind();
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem finded = ddlZone.Items.FindByValue("-1");
        if (finded != null)
            ddlZone.Items.Remove(finded);
        SelZone = int.Parse(ddlZone.SelectedValue);
        BindCitiesByZone(SelZone);
        BindZonePrice(SelZone, lvZonePrice);
        pnlPrices.Visible = true;
    }

    private void BindCitiesByZone(int zone)
    {
        using (CityRepository lCityRepository = new CityRepository())
        {
            List<City> selectedcities = lCityRepository.GetCities(zone);
            lbCiities.SelectedIndex = -1;
            foreach (City selectedcity in selectedcities)
            {
                foreach (ListItem item in lbCiities.Items)
                {
                    if (int.Parse(item.Value) == selectedcity.CityID)
                        item.Selected = true;
                }
            }
        }
    }

    protected void BindZonePrice(int zone, ListView control)
    {
        using (ZoneRepository lZoneRepository = new ZoneRepository())
        {
            List<Zone> zones = lZoneRepository.GetZonesByzIndex(zone);
            control.DataSource = zones;
            control.DataBind();
        }
    }

    protected void btnCityAdd_Click(object sender, EventArgs e)
    {
        string cityNameRus = txtRusName.Text;
        string cityNameEng = txtEngName.Text;
        using (CityRepository lCityRepository = new CityRepository())
        {
            if (lCityRepository.IsUsedCityName(cityNameRus, cityNameEng))
                return;
            City city = new City();
            city.City_ENG = cityNameEng;
            city.City_RUS = cityNameRus;
            city.ZN = SelZone;
            city.Description = txtDescription.Text;
            city = lCityRepository.AddCity(city);
        }
        BindCitiesByZone(int.Parse(ddlZone.SelectedValue));
        BindCities();
        txtRusName.Text = "";
        txtEngName.Text = "";
        txtDescription.Text = "";
    }
    protected void btnUpdateZone_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in lbCiities.Items)
        {
            if (item.Selected)
            {
                int cityId = int.Parse(lbCiities.SelectedValue);

                UpdateCityZone(cityId, SelZone);
            }
        }
        BindCitiesByZone(SelZone);
    }

    protected void UpdateCityZone(int cityId, int zone)
    {
        using (CityRepository lCityRepository = new CityRepository())
        {
            City city = lCityRepository.GetCityById(cityId);
            city.ZN = zone;
            city = lCityRepository.AddCity(city);
        }
    }
    protected void btnEditEMSPrice_Click(object sender, EventArgs e)
    {
        BindZonePrice(SelZone, lvZonePriceEdit);
        lvZonePrice.Visible = false;
        lvZonePriceEdit.Visible = true;
        btnEditEMSPrice.Visible = false;
        btnUpdateEMSPrice.Visible = true;
        btnCancelEMSPrice.Visible = true;
    }
    protected void btnUpdateEMSPrice_Click(object sender, EventArgs e)
    {
        foreach (ListViewItem item in lvZonePriceEdit.Items)
        {
            if (item.ItemType == ListViewItemType.DataItem)
            {
                int id = int.Parse(lvZonePriceEdit.DataKeys[((ListViewDataItem)item).DataItemIndex].Value.ToString());
                decimal centerPrice = decimal.Parse(((TextBox)item.FindControl("txtCenterPrice")).Text);
                decimal regionPrice = decimal.Parse(((TextBox)item.FindControl("txtRegionPrice")).Text);
                using (ZoneRepository lZoneRepository = new ZoneRepository())
                {
                    Zone zone = new Zone();
                    zone.Id = id;
                    zone.Qty = 0;
                    zone.zIndex = SelZone;
                    zone.CenterPrice = centerPrice;
                    zone.RegionPrice = regionPrice;
                    lZoneRepository.UpdateZone(zone);

                }
            }
        }
        BindZonePrice(SelZone, lvZonePrice);
        lvZonePrice.Visible = true;
        lvZonePriceEdit.Visible = false;
        btnEditEMSPrice.Visible = true;
        btnUpdateEMSPrice.Visible = false;
        btnCancelEMSPrice.Visible = false;
    }

    protected void BindMoscowPrice()
    {
        pnlEMS.Visible = false;
        pnlMoscow.Visible = true;
        using (MoscowDeliveryRepository lDeliveryRepository = new MoscowDeliveryRepository())
        {
            pnlMoscowPriceInfo.Visible = true;
            pnlMoscowPriceEdit.Visible = false;
            lblMoscowPrice.Text = string.Format("{0:C}", lDeliveryRepository.GetMoscowDeliverPrice());
        }
        lblDeliveyTitle.Text = "Доставка по Москве";
    }
    protected void btnMoscowPriceEdit_Click(object sender, EventArgs e)
    {
        using (MoscowDeliveryRepository lDeliveryRepository = new MoscowDeliveryRepository())
        {
            pnlMoscowPriceInfo.Visible = false;
            pnlMoscowPriceEdit.Visible = true;
            txtMoscowPrice.Text = string.Format("{0:F2}", lDeliveryRepository.GetMoscowDeliverPrice());
        }
    }
    protected void btnMoscowPriceUpdate_Click(object sender, EventArgs e)
    {
        using (MoscowDeliveryRepository lDeliveryRepository = new MoscowDeliveryRepository())
        {
            decimal price = decimal.Parse(txtMoscowPrice.Text);
            lDeliveryRepository.UpdateDeliverPrice(price);
        }
        BindMoscowPrice();
    }
    protected void btnCancelEMSPrice_Click(object sender, EventArgs e)
    {
        BindZonePrice(SelZone, lvZonePrice);
        lvZonePrice.Visible = true;
        lvZonePriceEdit.Visible = false;
        btnEditEMSPrice.Visible = true;
        btnUpdateEMSPrice.Visible = false;
        btnCancelEMSPrice.Visible = false;
    }
    protected void btnMoscowPriceCancel_Click(object sender, EventArgs e)
    {
        BindMoscowPrice();
    }
}