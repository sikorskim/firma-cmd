using Microsoft.EntityFrameworkCore;
using System;

namespace firma_lib
{
    public class InvoiceHeader
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public int ContractorId { get; set; }
        public int PaymentMethodId { get; set; }        
        public int ItemsCount { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalValueInclVat { get; set; }
    }
}