﻿//import { Modal } from "../lib/bootstrap/dist/js/bootstrap.bundle";

// count 50% of VAT tax on VATRegisterBuy create view
$('#btnVat50').click(function () {
    var vat = $('#TaxDeductibleValue').val();
    vat = vat.replace(',', '.')
    vat = vat / 2;
    vat = vat.toFixed(2);
    $('#TaxDeductibleValue').val(vat);
});

$('#valNetto').change(function () {
    var netto = $('#valNetto').val().replace(',', '.');
    var brutto = $('#valBrutto').val().replace(',', '.');
    var vat = brutto - netto;
    vat = vat.toFixed(2);
    $('#TaxDeductibleValue').val(vat);
});

// invoiceItem/create action for items selectlist
$('#ItemIdDropDownList').change(function () {
    var itemId = $(this).val();
    $.get('/Items/GetItem', { id: itemId }, function (data) {
        $('#invoiceItemCreateQuantity').val(1);
        $('#invoiceItemCreateName').val(data.name);        
        $('#invoiceItemCreateUnit').val(data.unitOfMeasure.shortName);       

        var netto = data.price.toFixed(2);
        var vat = data.vat.value.toFixed(2);
        var brutto = vat / 100 * netto + +netto;
        brutto = brutto.toFixed(2);

        $('#invoiceItemCreateVatValue').val(vat);
        $('#invoiceItemCreatePrice').val(netto);
        $('#invoiceItemCreatePriceBrutto').val(brutto);
    });
});

// invoiceItem/create count brutto price
$('#invoiceItemCreatePrice').change(function () {
    var netto = $('#invoiceItemCreatePrice').val();
    var vat = $('#invoiceItemCreateVatValue').val();
    var brutto = vat / 100 * netto+ +netto;    
    $('#invoiceItemCreatePriceBrutto').val(brutto);
});