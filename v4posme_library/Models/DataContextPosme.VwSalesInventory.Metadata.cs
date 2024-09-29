using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwSalesInventory.Metadata))]
    public partial class VwSalesInventory
    {
        public partial class Metadata
        {
    
            public object CreatedOn { get; set; }
    
            public object CreatedOnDay { get; set; }
    
            public object Currency { get; set; }
    
            public object Tipo { get; set; }
    
            public object Causal { get; set; }
    
            public object TransactionNumber { get; set; }
    
            public object StatusName { get; set; }
    
            public object CompaniaName { get; set; }
    
            public object WarehouseName { get; set; }
    
            public object CustomerNumber { get; set; }
    
            public object FirstName { get; set; }
    
            public object ItemNumber { get; set; }
    
            public object Name { get; set; }
    
            public object CategoryName { get; set; }
    
            public object TipoCambio { get; set; }
    
            public object Quantity { get; set; }
    
            public object UnitaryCost { get; set; }
    
            public object Cost { get; set; }
    
            public object UnitaryAmount { get; set; }
    
            public object Amount { get; set; }
    
            public object Utility { get; set; }
        }
    }
}
