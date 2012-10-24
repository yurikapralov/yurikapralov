using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Info;
using echo.BLL.Orders;

public partial class Platinum_Shopping : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UpdateSubTotals();
            this.Title = "Ваша корзина";
        }



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
        lblSubtotal.Text = string.Format("{0:C}", this.Profile.ShoppingCart.Total);
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
        GoToShipping();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.Profile.ShoppingCart.Clear();
        UpdateSubTotals();
    }


    protected void GoToShipping()
    {
        pnlShipping.Visible = true;
        LoadCities();
        LoadCountries();
        btnMore.Enabled = false;
        pnlRusAdress.Visible = false;
        pnlOutsideAdress.Visible = false;
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
                                 ? "Сумма доставки оговаривается отдельно"
                                 : string.Format("{0:C}", deliversum);
        decimal totalSum = this.Profile.ShoppingCart.Total + deliversum;
        lblTotalSum.Text = string.Format("{0:C}", totalSum);
        lblFIO.Text = txtFIO.Text;
        lblDeliver.Text = ddlDeliverType.SelectedItem.Text;
        string Address = "";
        switch (ddlDeliverType.SelectedValue)
        {
            case "1":
            case "2":

                if (txtIndex.Text != "")
                    Address = txtIndex.Text + " ,";
                Address += "Россия, " + ddlCities.SelectedItem.Text;
                if (rblRegionType.SelectedValue == "2")
                {
                    Address += " (Территория области)";
                    if (txtCity2.Text != "")
                        Address += ", г." + txtCity2.Text;
                }
                Address += ", ул." + txtStreet.Text + ", дом " + txtHouse.Text;
                if (txtKorpus.Text != "")
                    Address += ", корпус " + txtKorpus.Text;
                if (txtUnit.Text != "")
                    Address += ", квартира(офис): " + txtUnit.Text;
                lblAdress.Text = Address;
                break;
            case "3":
                if (txtIndex.Text != "")
                    Address = txtIndex.Text + " ,";
                Address += "Россия, Москва";
                Address += ", ул." + txtStreet.Text + ", дом " + txtHouse.Text;
                if (txtKorpus.Text != "")
                    Address += ", корпус " + txtKorpus.Text;
                if (txtUnit.Text != "")
                    Address += ", квартира(офис): " + txtUnit.Text;
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
                                                  int.Parse(ddlCities.SelectedValue));
            LblOrderNumber.Text = string.Format("Номер вашего заказа: {0}", order.OrderNumber);
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
        Response.Redirect("http://www.PlatinumShoes.ru");
    }

    protected void SendingMail()
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        OrderCtrl.RenderControl(htw);
        string output = sb.ToString();
        string extratext = "";
        switch (ddlDeliverType.SelectedValue)
        {
            case "1":
                extratext =
                    @"<p><b>* ВНИМАНИЕ! ДЛЯ УТОЧНЕНИЯ ЗАКАЗА С ВАМИ СВЯЖЕТСЯ СОТРУДНИК КОМПАНИИ</b></p>
               <p>Схема Выполнения заказа:</p>
                <p align='left'>1. Вы оплачиваете счет через банк, и по факту прихода денег мы отправляем<br>
                &nbsp;&nbsp;&nbsp;&nbsp;Ваш заказ по указанному Вами адресу.<br>
               2. Доставка осуществляется службой экспресс доставки &quot;ЕМС Гарантпост&quot;<br>
                &nbsp;&nbsp;&nbsp;&nbsp;в течении 2-5 дней в зависимости от региона.</p>
                <p>Наши реквизиты.<br>
                ООО 'ЭХО XXI'<br>
                Почтовый адрес: 105215, г.Москва, ул. 9-я Парковая д.66, к. 1, офис 8.<br>
                Юридический адрес: 107076, г.Москва, ул. Краснобогатырская, д.90, стр.2.<br>
                Получатель : ИНН 7718251860<br>
                КПП 771801001<br>
                Р\С 40702810238290106392<br>
                Банк получателя: Московский банк Сбербанка России ОАО <br>
                ОАО 'Сбербанк России'  г.Москва  БИК 044525225<br>
                К/Сч 30101810400000000225<br>
                Тип платежа : предоплата за обувь</p>
                <p>Телефон для справок (495) 464-2365</p>
                <p>СПАСИБО ЗА ПОКУПКУ</p>
                <p><a href='http://www.platinumshoes.ru'>http://www.platinumshoes.ru</a><br>";
                break;
            case "2":
                extratext = @"<p>Стоимость доставки определяется почтой РФ и оплачивается при получении. </p>
                <p><b>ВНИМАНИЕ! ЗАКАЗ ВЫПОЛНЯЕТСЯ ТОЛЬКО ПОСЛЕ ЕГО ПОДТВЕРЖДЕНИЯ ПО ТЕЛЕФОНУ. </b></p>
                <p>Телефон для справок (495) 464-2365</p>
                <p>СПАСИБО ЗА ПОКУПКУ</p>
                <p><a href='http://www.platinumshoes.ru'>http://www.platinumshoes.ru</a><br>";
                break;
            case "3":
                extratext =
                    @"<p><b>ВНИМАНИЕ! ЗАКАЗ ВЫПОЛНЯЕТСЯ ТОЛЬКО ПОСЛЕ ЕГО ПОДТВЕРЖДЕНИЯ ПО ТЕЛЕФОНУ. </b></p>
                <p>Телефон для справок (495) 464-2365</p>
                <p>СПАСИБО ЗА ПОКУПКУ</p>
                <p><a href='http://www.platinumshoes.ru'>http://www.platinumshoes.ru</a><br>";
                break;
            case "4":
                extratext = @"<p>Стоимость доствки определяется менеджером компании.</p>
                    <p>Телефон для справок (495) 464-2365</p>
                    <p>СПАСИБО ЗА ПОКУПКУ</p>
                    <p><a href='http://www.platinumshoes.ru'>http://www.platinumshoes.ru</a><br>";
                break;
        }
        string fromAddress = "info@platinumshoes.ru";
        string toAddress = "info@echo-h.ru";
        SmtpClient smtp = new SmtpClient();
        try
        {
            MailMessage msg1 = new MailMessage(fromAddress, toAddress);
            msg1.Subject = "PlatimunShoes.ru - Новый заказ";
            msg1.Body = "<html><body>" + output + "</body></html>";
            msg1.IsBodyHtml = true;
            smtp.Send(msg1);
        }
        catch (Exception e)
        {

            int tst = 0;
        }
        try
        {
            MailMessage msg2 = new MailMessage(fromAddress, txtEmail.Text);
            msg2.Subject = "Интернет - магазин 'PlatimunShoes.ru' - ваш заказ";
            msg2.Body = "<html><body>" + output + extratext + "</body></html>";
            msg2.IsBodyHtml = true;
            smtp.Send(msg2);
        }
        catch (Exception e)
        {
            int tst = 0;
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        pnlShipping.Visible = false;
        pnlShoppingCart.Visible = true;
    }
}
