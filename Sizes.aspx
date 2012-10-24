﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true"
    CodeFile="Sizes.aspx.cs" Inherits="Sizes" Theme="Theme2" %>

<%@ MasterType VirtualPath="~/Theme2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="Server">
    <h2>
        Снятие мерок стопы:</h2>
    <p>
        Снятие мерок стопы: Данные мерки снимаются для получения информации о длине стопы
        и полноте стопы клиента. Для точных результатов клиента надо поставить на пол равномерно,
        на обе ноги, он не должен переминаться с ноги на ногу или перекашиваться, стоит
        прямо.
        <br>
        Необходимые мерки:</p>
    <ul>
        <li>Длина стопы. Обводим стопу карандашом. Карандаш держим прямо к поверхности. Отмечаем
            самую выпуклую точку пятки и самого длинного пальца стопы.</li>
        <li>не сдвигая стопы клиента, подкладываем измерительную ленту и замеряем обхват по
            пучкам (наиболее выдающиеся точки по ширине, после пальцев)</li>
        <li>далее продвигаемся измерительной лентой дальше и нащупываем выпуклую точку на подъеме
            стопы. Ленту держим прямо. Измеряем обхват.</li>
        <li>далее измерительной лентой обхватываем стопу по краю пятки до точки сгибания стопы.
        </li>
    </ul>
    <p>
    </p>
    <img src="Images/Decoration/sizes1.jpg" alt="Снятие мерок стопы" style="border: none;">
    <p>
        &nbsp;</p>
    <h2>
        Снятие мерок ноги:</h2>
    <p>
        Размеры для создания модели полусапожек, сапог, ботфорт берутся следующим образом:</p>
    <p>
        Высота отмеряется по задней линии ноги от самой крайней точки пятки (соприкасающейся
        с полом) до определенной точки (уровень желаемой высоты). Для этого надо расположиться
        так, чтобы было удобно прикладывать измерительную ленту по задней поверхности голени
        и бедра по прямой. Во избежание ошибок измерительная лента должна начинаться с отметки
        «ноль». При снятии обхвата голени измерительная лента должна располагаться параллельно
        плоскости опоры.</p>
    <p>
        Сначала измерительной лентой отмеряем от крайней точки пятки необходимую высоту,
        фиксируем полученную точку пальцем и прикладываем ленту по обхвату. Снимаем мерки
        с правой и левой ног, так у каждого человека правая и левая ноги разные по развитости,
        а, следовательно, и отличаются по размерам.</p>
        <p>Необходимые высоты</p>
    <ul>
        <li>по косточкам. </li>
        <li>самое узкое место голени (щиколотка).</li>
        <li>Две дополнительные мерки под самым широким место икры. Здесь взять две высоты с
            разницей в 3см.</li>
        <li>самое широкое место в икре (середина икры).</li>
        <li>дополнительная мерка. На 3см выше самого широкого места в икре.</li>
        <li>под коленом</li>
        <li>по колену</li>
        <li>Дополнительные мерки.</li>
        <li>Мерка по высоте ботфорт.</li>
    </ul>
    <p>
        &nbsp;</p>
    <img src="Images/Decoration/sizes2.jpg" alt="Снятие мерок ноги" width="441" height="340"
        style="border: none;">
    <p>
    </p>
    <p>
        <a href="SizeTable.doc">Скачать файл для записи результатов измерений</a></p>
</asp:Content>