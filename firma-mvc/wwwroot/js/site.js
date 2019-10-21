// count 50% of VAT tax on VATRegisterBuy create view
$('#btnVat50').click(function () {
    var vat = $('#TaxDeductibleValue').val().replace(',', '.');
    vat = vat / 2;
    vat = vat.toFixed(2);
    vat = vat.replace('.', ',')
    $('#TaxDeductibleValue').val(vat);
});

// count VAT value in VATRegisterBuy create view
$('#valNetto').keyup(function () {
    var netto = $('#valNetto').val();    
    var brutto = netto*1.23;
    brutto=brutto.toFixed(2);
    $('#valBrutto').val(brutto);
    var vat = brutto - netto;
    vat = vat.toFixed(2);
    //vat = vat.replace('.', ',')
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
$('#netto').change(function () {
    var netto = $('#netto').val().replace(',', '.');
    var vat = $('#vat').val().replace(',', '.');
    var brutto = vat / 100 * netto + +netto;    
    brutto = brutto.toFixed(2).replace('.', ',');
    $('#brutto').val(brutto);
});

// invoice/index action for items months selectlist
$('#monthDropDown').change(function () {
    var monthId = $('#monthDropDown').val();
    var year = $('#yearDropDown').val();
    window.location.href = '?month=' + monthId + '&year=' + year;
});

// invoice/index action for items years selectlist
$('#yearDropDown').change(function () {
    var monthId = $('#monthDropDown').val();
    var year = $('#yearDropDown').val();
    window.location.href = '?month=' + monthId+'&year='+year;
});

// index action for tax related items years selectlist
$('#yearTaxDropDown').change(function () {
    var year = $('#yearTaxDropDown').val();
    window.location.href = '?year=' + year;
});

// index action for calculator type change
$('#calcTypeSelect').change(function () {
    var selected = $('#calcTypeSelect').val();

    if(selected==0)
    {        
        $('#calcNettoVal').prop('disabled', false);
        $('#calcBruttoVal').prop('disabled', true);
    }
    else
    {
        $('#calcNettoVal').prop('disabled', true);
        $('#calcBruttoVal').prop('disabled', false);
    }
});


// Invoices/CreatePartial
$('#search').keyup(function () {
    var searchQuery = $('#search').val();
    console.log(searchQuery);

    //clear table
    $('#searchResult').empty();

    $.get('/Contractors/SearchContractor', { query: searchQuery }, function (data) {
//   console.log(data);     
   $.each(data, function(key,value){
       var obj = "$('#contractorid').val("+value.id+")";
       var str = '<tr class="table-light result" onclick="'+obj+'"><td>'+value.name+'</td><td>'+value.nip+'</td></tr>';
    //    var str = '<tr onclick="'+obj+'"><td>'+value.name+'</td><td>'+value.nip+'</td></tr>';
    //    var str = '<tr class="table-light result" onclick="searchResultClick()"><td>'+value.name+'</td><td>'+value.nip+'</td></tr>';
       $('#searchResult').append($(str))

   });

    });
});

$('table tr').click(function () {
    $('.bg-primary').removeClass('bg-primary');
    $(this).addClass('bg-primary');
});

$('#vatRegBuyCreateDtOfIssue').keyup(function () {
    var date = $('#vatRegBuyCreateDtOfIssue').val();
    $('#vatRegBuyCreateDelDt').val(date);
});
