/// <reference path="jquery-1.5.1-vsdoc.js" />

function CopyEnglishName() {
    var sortedtext = $('#txtSorted').val();
    sortedtext = 'Art. ' + sortedtext;
    $('#txtEngName').val(sortedtext);
    return false;
}

function SetRusTemplate() {
    var templatetext = $('#rusnametemplate option:selected').html();
    var sortedtext = $('#txtSorted').val();
    var rustext = $('#txtRusName').val();
    var prevousval = $('#previousrusval').val();
    if (templatetext != 'Пустой') {
        $('#txtRusName').val(templatetext + ' ' + sortedtext);
        $('#previousrusval').val(rustext);
    }
    else {
        $('#txtRusName').val(prevousval);
    }
}