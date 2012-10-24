﻿<%@ Page Title="" Language="C#" MasterPageFile="~/V2/MasterPageV2.master" AutoEventWireup="true" CodeFile="Shipping.aspx.cs" Inherits="V2_Shipping" %>
<%@ MasterType VirtualPath="~/V2/MasterPageV2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="MainBlockArea">
<div class="adressBlock">
<div class="metro">Существуют 3 способа доставки:</div> 
1.по предоплате:<br />
1а. при помощи службы экспресс доставки "ЕМС Гарантпост" (по России)
стоимость доставки расчитывается автоматически при офрмлении заказа<br />
1б. при помощи службы экспресс доставки "ЕМС Гарантпост" (по ближнему и дальнему зарубежью)
стоимость доставки сообщается менеджером компании<br />
2. курьером (только по Москве) (до 40-го размера включительно)
стоимость доставки - 300 рублей<br />
3. наложенным платежом (по России)(до 40-го размера включительно, сумма заказа меньше 10000 руб.)
стоимость доставки сообщается менеджером компании.<br />

</div>
<div class="adressBlock">
<div class="metro">Схема выполнения заказов:</div> 
1. Вы указываете ваши координаты по которым сотрудник компании связывается с вами.<br />
2. С Вами оговариваются детали заказа, способ доставки, стоимость*, и в случае предоплаты сообщаются наши банковские реквизиты.<br />
3а. По предоплате: Вы оплачиваете счет через банк и по факту прихода денег мы отправляем ваш заказ по указанному вами адресу. Доставка осуществляется в течении 3-5 рабочих дней в зависимости от региона.<br />
3б. Курьером: обговаривается время когда к Вам может подъехать курьер с товаром.<br />
3в. Наложенным платежом:по вашему адресу высылается товар, который получается и оплачивается на почте.<br />
Дополнительная информация по телефонам (495) 464-2365 и (495) 925-6981 <br />
</div>
<div class="adressBlock">
* - в стоимость входит: цена из каталога, стоимость доставки<br />
- при размере больше 41-ого взымается надбавка за размер (20$)
</div>
</div>
</asp:Content>

