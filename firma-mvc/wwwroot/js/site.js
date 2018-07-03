// count 50% of VAT tax on VATRegisterBuy create view
$('#btnVat50').click(function () {
    var vat = $('#TaxDeductibleValue').val();
    vat=vat.replace(',','.')
    vat = vat / 2;
    vat = vat.toFixed(2);
        $('#TaxDeductibleValue').val(vat);
});

$('#valNetto').change(function () {
    var netto = $('#valNetto').val().replace(',', '.');
    var brutto = $('#valBrutto').val().replace(',', '.');
    var vat = brutto-netto;
    vat = vat.toFixed(2);
    $('#TaxDeductibleValue').val(vat);
});