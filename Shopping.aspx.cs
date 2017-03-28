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
using Newtonsoft.Json;
using echo.BLL;
using echo.BLL.Info;
using echo.BLL.Orders;
using System.Collections;

public partial class Shopping : BasePage
{

    public ClientScriptManager Scr
    {
        get { return this.Page.ClientScript; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UpdateSubTotals();
           
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
        lblSubtotal.Text = string.Format("Итого: {0:C}", this.Profile.ShoppingCart.Total);
        lblUsa.Visible = this.Profile.ShoppingCart.HaveUsaProduct();
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
        if(this.Profile.ShoppingCart.HaveUsaProduct())
        {
            this.Profile.ShoppingCart.InCredit = false;
            cbxCreditPayment.Visible = false;
            ddlDeliverType.Items.Clear();
            ddlDeliverType.Items.Add(new ListItem("Условия доставки оговариваются отдельно","5"));

            //Начальные установки панелей
            btnMore.Enabled = true;
            pnlRusAdress.Visible = true;
            pnlOutsideAdress.Visible = false;
            ddlCountry.Enabled = false;
            ddlCountry.SelectedValue = "105";
            ddlCities.Enabled = true;
            rblRegionType.Enabled = true;
        }
        else
        {
           SetDeliverType(this.Profile.ShoppingCart.InCredit);

            //Начальные установки панелей
            btnMore.Enabled = false;
            pnlRusAdress.Visible = false;
            pnlOutsideAdress.Visible = false;
        }
        cbxCreditPayment.Checked = this.Profile.ShoppingCart.InCredit;
    }

    private void SetDeliverType(bool in_credit)
    {
        ddlDeliverType.Items.Clear();
        if(in_credit)
        {
            var items = new[] { new ListItem("--Выберите способ доставки--","0"),
            new ListItem("Курьерская служба по России","1"),
            new ListItem("Курьером по Москве","3")};
            ddlDeliverType.Items.AddRange(items);
        }
        else
        {
            var items = new[] { new ListItem("--Выберите способ доставки--","0"),
            new ListItem("Курьерская служба по России","1"),
            new ListItem("Наложенный платеж","2"),
            new ListItem("Курьером по Москве","3"),
            new ListItem("За пределы РФ","4")};
            ddlDeliverType.Items.AddRange(items);
        }
    }

    protected void GetProfileInfo()
    {
        txtFirstName.Text = Profile.FirstName;
        txtLastName.Text = Profile.LastName;
        txtMiddleName.Text = Profile.MiddleName;
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
        lblShippingSum.Text = lblSubtotal.Text.Replace("Итого:","");
        decimal deliversum = GetDeliverSum(int.Parse(ddlDeliverType.SelectedValue));
        lblDeliverSum.Text = deliversum == 0
                                 ? "Сумма доставки оговаривается отдельно"
                                 : string.Format("{0:C}", deliversum);
        decimal totalSum = this.Profile.ShoppingCart.Total + deliversum;
        lblTotalSum.Text = string.Format("{0:C}", totalSum);
        lblFIO.Text = (txtLastName.Text + " " + txtFirstName.Text + " " + txtMiddleName.Text).Trim();
        lblDeliver.Text = ddlDeliverType.SelectedItem.Text;
        string Address = "";
        switch (ddlDeliverType.SelectedValue)
        {
            case "1":
            case "2":
            case "5":

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
        /*if (deliverType == 1)
        {
            using (ZoneRepository lZoneRepository = new ZoneRepository())
            {
                using (CityRepository lCityRepository = new CityRepository())
                {
                    City city = lCityRepository.GetCityById(int.Parse(ddlCities.SelectedValue));
                    int zIndex = city.ZN;
                    int qTy = this.Profile.ShoppingCart.Count+1;
                    Zone zone = lZoneRepository.GetZoneByzIndexAndQty(zIndex, qTy);
                    if (rblRegionType.SelectedValue == "1")
                        return zone.CenterPrice;
                    return zone.RegionPrice;
                }
            }
        }*/
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

    private string GetCreaditArray(string order_number)
    {
       Hashtable order=new Hashtable();
        object[] items=new object[this.Profile.ShoppingCart.Items.Count+1];
       int i = 0;
       foreach (ShoppingCartItem sh_item in this.Profile.ShoppingCart.Items)
       {
           Hashtable item = new Hashtable();
           item.Add("title", sh_item.Title);
           item.Add("category", "");
           item.Add("qty", sh_item.Qty);
           item.Add("price", (int)Math.Ceiling(sh_item.PriceForSale));
           items[i] = item;
           i++;
       }
       Hashtable deliver_item = new Hashtable();
       deliver_item.Add("title", "Доставка");
       deliver_item.Add("category", "");
       deliver_item.Add("qty", 1);
       deliver_item.Add("price", (int)GetDeliverSum(int.Parse(ddlDeliverType.SelectedValue)));

        items[i] = deliver_item;
       order.Add("items", items);
       
       Hashtable details = new Hashtable();
       details.Add("firstname", txtFirstName.Text);
       details.Add("lastname", txtLastName.Text);
       details.Add("middlename", txtMiddleName.Text);
       details.Add("email", txtEmail.Text);
       details.Add("cellphone", txtPhone.Text);
       order.Add("details", details);

       order.Add("partnerId", "1-8R8GI0J");
       order.Add("partnerName", "Echo Of Hollywood");
       order.Add("partnerOrderId", order_number);
       order.Add("deliveryType", "");

       string ret = JsonConvert.SerializeObject(order);
       return ret;
    }

    private string Get64CreditArray(string orderNo)
    {
        string json = GetCreaditArray(orderNo);
        byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(json);
        return Convert.ToBase64String(encbuff);
    }

    public static string GetSign(string message, string salt)
    {
        int iterationCount = 1102;
        message += salt;
        string result = SymmetricEncryptionUtility.getMd5Hash(message, Encoding.UTF8) + SymmetricEncryptionUtility.getSHA1Hash(message, Encoding.UTF8);
        byte[] data = Encoding.UTF8.GetBytes(result);
        for (int i = 0; i < iterationCount; i++)
        {
            result = SymmetricEncryptionUtility.getMd5Hash(result, Encoding.UTF8);
        }
        return result;
    }

    protected void btnAdmit_Click(object sender, EventArgs e)
    {
        string order_number = "";
        using (OrdersRepository lRepository = new OrdersRepository())
        {
            int deliverType = int.Parse(ddlDeliverType.SelectedValue);
            Order order = lRepository.InsertOrder(this.Profile.ShoppingCart, txtAdress.Text, txtCity2.Text,
                                                  txtIndex.Text,
                                                  int.Parse(rblRegionType.SelectedValue), GetDeliverSum(deliverType),
                                                  deliverType, txtEmail.Text, lblFIO.Text, txtHouse.Text,
                                                  txtKorpus.Text, txtNote.Text, txtPhone.Text, txtStreet.Text,
                                                  txtTime1.Text, txtTime2.Text, txtUnit.Text,
                                                  int.Parse(ddlCountry.SelectedValue),
                                                  int.Parse(ddlCities.SelectedValue), this.Profile.ShoppingCart.InCredit);
            OrderCtrl.OrderId = order.OrderID;
            OrderCtrl.BindOrder();
            order_number = order.OrderNumber;


            LblOrderNumber.Text = string.Format("Номер вашего заказа: {0}", order_number);
            pnlShippingConfirmation.Visible = false;
            if (this.Profile.ShoppingCart.InCredit)
            {
                string test = Get64CreditArray(order_number);
                pnlCredit.Visible = true;
                string script =
                    @"<script>
                vkredit = new VkreditWidget(
                1, /*цвет кнопки*/";
                script +=
                    string.Format("{0}, /*запрашиваемая сумма - служит для расчета суммы, отображаемой на кнопке.*/",(int)Math.Ceiling(order.OrderSum) );
                script += "{";
                script += string.Format(" order: '{0}',  sig: '{1}'", test, GetSign(test, "echoh-secret-yn397nd7"));
                script += @" }
                );
                </script>";
                lblCreditScript.Text = script;
            }
            else
            {
                ShowFinalPanel();
            }
            SendingMail();
            this.Profile.ShoppingCart.Clear();
        }

    }

    protected void ShowFinalPanel()
    {
        pnlCredit.Visible = false;
        pnlFinal.Visible = true;            
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
        string extratext = "";
        switch (ddlDeliverType.SelectedValue)
        {
            case "1":
                extratext =
                    @"<p><b>* ВНИМАНИЕ! ДЛЯ УТОЧНЕНИЯ ЗАКАЗА С ВАМИ СВЯЖЕТСЯ СОТРУДНИК КОМПАНИИ</b></p>
               <p>Схема Выполнения заказа:</p>
                <p align='left'>1. Вы оплачиваете счет через банк, и по факту прихода денег мы отправляем<br>
                &nbsp;&nbsp;&nbsp;&nbsp;Ваш заказ по указанному Вами адресу.<br>
               2. Доставка осуществляется службой экспресс доставки <br>
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
                <h3>Способы оплаты:</h3>
                <ul>
                <li>Банковским переводом</li>
                <li>Через платежную систему Qiwi</li>
                <li>Через платежную систему Яндекс-Деньги</li>
                </ul>
                <p>Подробнее на странице: <a href='http://echo-h.ru/PayMethods.aspx'>Способы оплаты</a><br>
                <p>Телефон для справок (499) 748-8584</p>
                <p>СПАСИБО ЗА ПОКУПКУ</p>
                <p><a href='http://echo-h.ru'>http://echo-h.ru</a><br>";
                break;
            case "2":
                extratext = @"<p>Стоимость доставки определяется почтой РФ и оплачивается при получении. </p>
                <p><b>ВНИМАНИЕ! ЗАКАЗ ВЫПОЛНЯЕТСЯ ТОЛЬКО ПОСЛЕ ЕГО ПОДТВЕРЖДЕНИЯ ПО ТЕЛЕФОНУ. </b></p>
                <p>Телефон для справок (499) 748-8584</p>
                <p>СПАСИБО ЗА ПОКУПКУ</p>
                <p><a href='http://echo-h.ru'>http://echo-h.ru</a><br>";
                break;
            case "3":
                extratext =
                    @"<p><b>ВНИМАНИЕ! ЗАКАЗ ВЫПОЛНЯЕТСЯ ТОЛЬКО ПОСЛЕ ЕГО ПОДТВЕРЖДЕНИЯ ПО ТЕЛЕФОНУ. </b></p>
                <p>Телефон для справок (499) 748-8584</p>
                <p>СПАСИБО ЗА ПОКУПКУ</p>
                <p><a href='http://echo-h.ru'>http://echo-h.ru</a><br>";
                break;
            case "4":
                extratext = @"<p>Стоимость доствки определяется менеджером компании.</p>
                    <p>Телефон для справок (499) 748-8584</p>
                    <p>СПАСИБО ЗА ПОКУПКУ</p>
                    <p><a href='http://echo-h.ru'>http://echo-h.ru</a><br>";
                break;
             case "5":
                extratext = @"<p>Стоимость доствки определяется менеджером компании.</p>
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
                    <h3>Способы оплаты:</h3>
                    <ul>
                    <li>Банковским переводом</li>
                    <li>Через платежную систему Qiwi</li>
                    <li>Через платежную систему Яндекс-Деньги</li>
                    </ul>
                    <p>Подробнее на странице: <a href='http://echo-h.ru/PayMethods.aspx'>Способы оплаты</a><br>
                    <p>Телефон для справок (499) 748-8584</p>
                    <p>СПАСИБО ЗА ПОКУПКУ</p>
                    <p><a href='http://echo-h.ru'>http://echo-h.ru</a><br>";
                break;
        }
        string fromAddress = Helpers.Settings.ContactForm.MailFrom;
        string copyAddress = Helpers.Settings.ContactForm.MailCopy;
        string toAddress = fromAddress;
        SmtpClient smtp = new SmtpClient();
        try
        {
            MailMessage msg1 = new MailMessage(fromAddress, toAddress);
            msg1.Subject = "Новый заказ";
            msg1.Body = "<html><body>" + output + "</body></html>";
            msg1.IsBodyHtml = true;
            smtp.Send(msg1);
        }
        catch (Exception)
        {


        }
        if (copyAddress != "")
        {
            try
            {
                MailMessage msg3 = new MailMessage(fromAddress, copyAddress);
                msg3.Subject = "Новый заказ";
                msg3.Body = "<html><body>" + output + "</body></html>";
                msg3.IsBodyHtml = true;
                smtp.Send(msg3);
            }
            catch (Exception)
            {


            }
        }
        try
        {
            MailMessage msg2 = new MailMessage(fromAddress, txtEmail.Text);
            msg2.Subject = "Интернет - магазин 'Echo Of Hollywood' - ваш заказ";
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
    protected void cbxCreditPayment_CheckedChanged(object sender, EventArgs e)
    {
        this.Profile.ShoppingCart.InCredit = cbxCreditPayment.Checked;
        SetDeliverType(this.Profile.ShoppingCart.InCredit);
    }
    protected void btnBuyCash_Click(object sender, EventArgs e)
    {
        ShowFinalPanel();
    }
}