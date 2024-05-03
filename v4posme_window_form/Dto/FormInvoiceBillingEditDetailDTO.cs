using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;

namespace v4posme_window.Dto
{
    public  class FormInvoiceBillingEditDetailDTO 
    {
        public int? TransactionMasterDetailId { get; set; }
        public int? ItemId { get; set; }
        public string? ItemNumber { get; set; }
        public string? TransactionDetailName { get; set; }
        public int Sku { get; set; }
        [Precision(18,2)]
        public decimal Quantity { get; set; }
        [Precision(18,2)]
        public decimal Price { get; set; }
        [Precision(18,2)]
        public decimal SubTotal { get; set; }
        [Precision(18,2)]
        public decimal Iva { get; set; }
        public decimal SkuQuantityBySku { get; set; }
        public decimal UnitaryPriceIndividual { get; set; }
        public string? SkuFormatoDescription { get; set; }
        public decimal ItemPrecio2 { get; set; }
        public decimal ItemPrecio3 { get; set; }
        
        public string? DetailVencimiento { get; set; }
        public string? AccionMenos { get; set; }
        public string? AccionMas { get; set; }
        public decimal? AccionPrecios { get; set; }
    }
}