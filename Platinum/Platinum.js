/// <reference path="jquery-1.5.min.js" />

/*Функции работы меню*/
var timeout = 500;
var closetimer = 0;
var ddmenuitem = 0;

function $get(elem) {
    return document.getElementById(elem);
}

function jsddm_open() {
    jsddm_canceltimer();
    jsddm_close();
    ddmenuitem = $(this).find('ul').css('visibility', 'visible');
}

function jsddm_close() {
    if (ddmenuitem) ddmenuitem.css('visibility', 'hidden');
}

function jsddm_timer() {
    closetimer = window.setTimeout(jsddm_close, timeout);
}

function jsddm_canceltimer() {
    if (closetimer) {
        window.clearTimeout(closetimer);
        closetimer = null;
    }
}

$(document).ready(function() {
$('#menu>li').bind('mouseover', jsddm_open);
$('#menu>li').bind('mouseout', jsddm_timer);
});

document.onclick = jsddm_close;



/*Функции предпросмотра изображений*/

        function ShowPreview(strElem) {
            //Задаем ссылку на элемент- контейнер
            var elem = $get(strElem)
            elem.style.display = 'block';
            // из имени контейнера выделеляем id продукции.
            var prodId = elem.id.substring(10);
            GetLargeImages(prodId);

        }
        function HidePreview(strElem) {
            var elem = $get(strElem)
            elem.innerHTML = "";
            elem.style.display = 'none';
        }

        function GetLargeImages(productId) {
            // ListImages.GetLargeImagesByProductId(productId, onRequestComplete, onError);
            $.getJSON('GetPriviewFile.ashx?prodId=' + productId, function(data) {
                var elem = $get("img_block_" + productId);
                elem.innerHTML = "";
                $.each(data, function (i, imgobj) {
                    var img = document.createElement("img");
                    img.src = "http://www.echo-h.ru/Images/Products/Large/" + imgobj['imgurl'];
                    var increaseFactor = img.width / 50;
                    img.width = 50;
                    img.height = 50;
                    // img.height = img.height / increaseFactor;
                    var lft = -50 * (i + 1);
                    lft = lft + "px";
                    elem.style.left = lft;
                    elem.appendChild(img);
                });

            });
            return false;
        }

       