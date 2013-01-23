using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Info;
using echo.BLL.Orders;

public partial class En_Shopping_old : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UpdateSubTotals();
        }
        Master.ContentStyle = "Content2";


    }


    protected void gvwOrderItems_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btn = e.Row.Cells[3].Controls[0] as LinkButton;
            btn.OnClientClick = "if(confirm('Вы уверены что хотите удалить этот товар?')==false) return false;";
        }
    }
    protected void gvwOrderItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int prodSizeId = Convert.ToInt32(e.Keys[0]);
        this.Profile.ShoppingCart.DeleteProduct(prodSizeId);
        e.Cancel = true;
        UpdateSubTotals();
    }

    protected void UpdateSubTotals()
    {
        foreach (GridViewRow row in gvwOrderItems.Rows)
        {
            int id = Convert.ToInt32(gvwOrderItems.DataKeys[row.RowIndex][0]);
            int quantity = int.Parse(((TextBox)(row.FindControl("txtQuantity"))).Text);
            this.Profile.ShoppingCart.UpdateItemQuantity(id, quantity);
        }
        lblSubtotal.Text = ConvertPrice(string.Format("{0:F2}", this.Profile.ShoppingCart.Total));
        if (this.Profile.ShoppingCart.Items.Count == 0)
            pnlTotals.Visible = false;
        gvwOrderItems.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateSubTotals();
    }
    protected void btnOrder_Click(object sender, EventArgs e)
    {
        pnlShoppingCart.Visible = false;
        if (!this.User.Identity.IsAuthenticated)
        {
            pnlAuntification.Visible = true;
        }
        else
        {
            GoToShipping();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.Profile.ShoppingCart.Clear();
        UpdateSubTotals();
    }
    protected void btnEsc_Click(object sender, EventArgs e)
    {
        GoToShipping();
    }

    protected void GoToShipping()
    {
        pnlAuntification.Visible = false;
        pnlShipping.Visible = true;
        LoadCities();
        LoadCountries();
        if (User.Identity.IsAuthenticated)
        {
            GetProfileInfo();
        }
        btnMore.Enabled = false;
        pnlRusAdress.Visible = false;
        pnlOutsideAdress.Visible = false;
    }

    protected void GetProfileInfo()
    {
        txtFIO.Text = Profile.LastName + " " + Profile.FirstName + " " + Profile.MiddleName;
        int countryId = Profile.Address.CountryId;
        //Если не учтановлена страна, данные из профиля не считываются
        if (countryId != 0)
        {
            ddlCountry.SelectedValue = countryId.ToString();
            int cityId = Profile.Address.CityId;
            if (cityId != 0)
                ddlCities.SelectedValue = cityId.ToString();
            rblRegionType.SelectedValue = ((int)Profile.Address.RegionTypeId).ToString();
            txtCity2.Text = Profile.Address.City2;
            txtIndex.Text = Profile.Address.PostalCode;
            txtStreet.Text = Profile.Address.Street;
            txtHouse.Text = Profile.Address.House;
            txtKorpus.Text = Profile.Address.Building;
            txtUnit.Text = Profile.Address.Room;
        }
        txtEmail.Text = Membership.GetUser().Email;
        txtPhone.Text = Profile.Contacts.Phone;
        txtTime1.Text = Profile.Contacts.fromContact;
        txtTime2.Text = Profile.Contacts.toContact;
    }

    private void LoadCities()
    {
        using (CityRepository cityRepository = new CityRepository())
        {
            ddlCities.DataSource = cityRepository.GetCities();
            ddlCities.DataBind();
        }
    }

    private void LoadCountries()
    {
        using (CountryRepository countryRepository = new CountryRepository())
        {
            ddlCountry.DataSource = countryRepository.GetCountries();
            ddlCountry.DataBind();
        }
    }

    protected void ddlDeliverType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int deliverTypeId = int.Parse(ddlDeliverType.SelectedValue);
        switch (deliverTypeId)
        {
            case 0:
                btnMore.Enabled = false;
                pnlRusAdress.Visible = false;
                pnlOutsideAdress.Visible = false;
                break;
            case 1:
            case 2:
                btnMore.Enabled = true;
                pnlRusAdress.Visible = true;
                pnlOutsideAdress.Visible = false;
                ddlCountry.Enabled = false;
                ddlCountry.SelectedValue = "105";
                ddlCities.Enabled = true;
                rblRegionType.Enabled = true;
                break;
            case 3:
                btnMore.Enabled = true;
                pnlRusAdress.Visible = true;
                pnlOutsideAdress.Visible = false;
                ddlCountry.Enabled = false;
                ddlCountry.SelectedValue = "105";
                ddlCities.Enabled = false;
                ddlCities.SelectedValue = "207";
                rblRegionType.Enabled = false;
                rblRegionType.SelectedValue = "1";
                pnlRegionCity.Visible = false;
                break;
            case 4:
                btnMore.Enabled = true;
                pnlRusAdress.Visible = false;
                pnlOutsideAdress.Visible = true;
                ddlCountry.Enabled = true;
                break;
        }
    }

    protected void rblRegionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlRegionCity.Visible = rblRegionType.SelectedValue == "2" ? true : false;
    }
    protected void btnMore_Click(object sender, EventArgs e)
    {
        pnlShipping.Visible = false;
        pnlShippingConfirmation.Visible = true;
        lblShippingSum.Text = lblSubtotal.Text;
        decimal deliversum = GetDeliverSum(int.Parse(ddlDeliverType.SelectedValue));
        lblDeliverSum.Text = deliversum == 0
                                 ? "Delivery fee is negotiated separately"
                                 : ConvertPrice(string.Format("{0:F2}", deliversum));
        decimal totalSum = this.Profile.ShoppingCart.Total + deliversum;
        lblTotalSum.Text = ConvertPrice(string.Format("{0:F2}", totalSum));
        lblFIO.Text = txtFIO.Text;
        lblDeliver.Text = ddlDeliverType.SelectedItem.Text;
        string Address = "";
        switch (ddlDeliverType.SelectedValue)
        {
            case "1":
            case "2":

                if (txtIndex.Text != "")
                    Address = txtIndex.Text + " ,";
                Address += "Russia, " + ddlCities.SelectedItem.Text;
                if (rblRegionType.SelectedValue == "2")
                {
                    Address += " (Territory area)";
                    if (txtCity2.Text != "")
                        Address += ", г." + txtCity2.Text;
                }
                Address += ", st." + txtStreet.Text + ", house " + txtHouse.Text;
                if (txtKorpus.Text != "")
                    Address += ", building " + txtKorpus.Text;
                if (txtUnit.Text != "")
                    Address += ", appartaments(office): " + txtUnit.Text;
                lblAdress.Text = Address;
                break;
            case "3":
                if (txtIndex.Text != "")
                    Address = txtIndex.Text + " ,";
                Address += "Russia, Moscow";
                Address += ", ул." + txtStreet.Text + ", house " + txtHouse.Text;
                if (txtKorpus.Text != "")
                    Address += ", building " + txtKorpus.Text;
                if (txtUnit.Text != "")
                    Address += ", appartaments(office): " + txtUnit.Text;
                lblAdress.Text = Address;
                break;
            case "4":
                lblAdress.Text = ddlCountry.SelectedItem.Text + ", " + txtAdress.Text;
                break;
        }
        lblEmail.Text = txtEmail.Text;
        lblPhone.Text = txtPhone.Text;
        lblNote.Text = txtNote.Text;
        lblTime1.Text = txtTime1.Text;
        lblTime2.Text = txtTime2.Text;
    }

    protected decimal GetDeliverSum(int deliverType)
    {
        if (deliverType == 1)
        {
            using (ZoneRepository lZoneRepository = new ZoneRepository())
            {
                using (CityRepository lCityRepository = new CityRepository())
                {
                    City city = lCityRepository.GetCityById(int.Parse(ddlCities.SelectedValue));
                    int zIndex = city.ZN;
                    int qTy = this.Profile.ShoppingCart.Count;
                    Zone zone = lZoneRepository.GetZoneByzIndexAndQty(zIndex, qTy);
                    if (rblRegionType.SelectedValue == "1")
                        return zone.CenterPrice;
                    return zone.RegionPrice;
                }
            }
        }
        if (deliverType == 3)
        {
            using (MoscowDeliveryRepository lRepository = new MoscowDeliveryRepository())
            {
                return lRepository.GetMoscowDeliverPrice();
            }
        }
        return 0;
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        pnlShipping.Visible = true;
        pnlShippingConfirmation.Visible = false;
    }
    protected void btnAdmit_Click(object sender, EventArgs e)
    {
        using (OrdersRepository lRepository = new OrdersRepository())
        {
            int deliverType = int.Parse(ddlDeliverType.SelectedValue);
            Order order = lRepository.InsertOrder(this.Profile.ShoppingCart, txtAdress.Text, txtCity2.Text,
                                                  txtIndex.Text,
                                                  int.Parse(rblRegionType.SelectedValue), GetDeliverSum(deliverType),
                                                  deliverType, txtEmail.Text, txtFIO.Text, txtHouse.Text,
                                                  txtKorpus.Text, txtNote.Text, txtPhone.Text, txtStreet.Text,
                                                  txtTime1.Text, txtTime2.Text, txtUnit.Text,
                                                  int.Parse(ddlCountry.SelectedValue),
                                                  int.Parse(ddlCities.SelectedValue),this.Profile.ShoppingCart.InCredit);
            LblOrderNumber.Text = string.Format("Your order number: {0}", order.OrderNumber);
            pnlShippingConfirmation.Visible = false;
            pnlFinal.Visible = true;
            OrderCtrl.OrderId = order.OrderID;
            OrderCtrl.BindOrder();
            this.Profile.ShoppingCart.Clear();
            SendingMail();
        }
    }
    protected void btnDefault_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void SendingMail()
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        OrderCtrl.RenderControl(htw);
        string output = sb.ToString();
        string extratext = @"<p>Thank you for your order<br/>
        In the near future you will contact our staff.</p>
        <p>
        Echo of Hollywood - Shop Online of Shoes and Clothes. Shoes and Clothes for striptease, dance and show. Erotic lingerie, evening dresses.
        </p>
        <p>
        Phones: +7(495)464-2365; +7(495)652-4297; +7(499)748-8584
        </p>
        <p><a href='http://www.echoofhollywood.com'>http://www.echoofhollywood.com</a>";
        
        string fromAddress = Helpers.Settings.ContactForm.MailFrom;
        string toAddress = fromAddress;
        SmtpClient smtp = new SmtpClient();
        try
        {
            MailMessage msg1 = new MailMessage(fromAddress, toAddress);
            msg1.Subject = "Новый заказ - английская версия";
            msg1.Body = "<html><body>" + output + "</body></html>";
            msg1.IsBodyHtml = true;
            smtp.Send(msg1);
        }
        catch (Exception)
        {


        }
        try
        {
            MailMessage msg2 = new MailMessage(fromAddress, txtEmail.Text);
            msg2.Subject = "'Echo Of Hollywood' online store - your order";
            msg2.Body = "<html><body>" + output + extratext + "</body></html>";
            msg2.IsBodyHtml = true;
            smtp.Send(msg2);
        }
        catch (Exception)
        {

        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        pnlShipping.Visible = false;
        pnlShoppingCart.Visible = true;
    }
}
