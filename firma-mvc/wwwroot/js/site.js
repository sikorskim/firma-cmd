// count 50% of VAT tax on VATRegisterBuy create view
$('#btnVat50').click(function () {
    var vat = $('#TaxDeductibleValue').val();
    vat=vat.replace(',','.')
    vat = vat / 2;
    vat = vat.toFixed(2);
        $('#TaxDeductibleValue').val(vat);
});