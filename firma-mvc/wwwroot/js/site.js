﻿// count 50% of VAT tax on VATRegisterBuy create view
$('#btnVat50').click(function () {
    var vat = $('#TaxDeductibleValue').val().replace(',', '.');
    vat = vat / 2;
    vat = vat.toFixed(2);
    vat = vat.replace('.', ',')
    $('#TaxDeductibleValue').val(vat);
});

// count VAT value in VATRegisterBuy create view
$('#valNetto').change(function () {
    var netto = $('#valNetto').val().replace(',', '.');
    var brutto = $('#valBrutto').val().replace(',', '.');
    var vat = brutto - netto;
    vat = vat.toFixed(2);
    vat = vat.replace('.', ',')
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
        brutto = brutto.toFixed(2).replace('.', ',');
        netto = netto.replace('.', ',');
        vat = vat.replace('.', ',');

        $('#invoiceItemCreateVatValue').val(vat);
        $('#invoiceItemCreatePrice').val(netto);
        $('#invoiceItemCreatePriceBrutto').val(brutto);
    });
});

// invoiceItem/create count brutto price
$('#invoiceItemCreatePrice').change(function () {
    var netto = $('#invoiceItemCreatePrice').val().replace(',', '.');
    var vat = $('#invoiceItemCreateVatValue').val().replace(',', '.');
    var brutto = vat / 100 * netto + +netto;    
    brutto = brutto.toFixed(2).replace('.', ',');
    $('#invoiceItemCreatePriceBrutto').val(brutto);
});

// invoiceItem/create action for items selectlist
$('#MonthDropDownList').change(function () {
    var monthId = $(this).val();
    $.get('Invoices', { month: monthId }, function (data) {
    });
});