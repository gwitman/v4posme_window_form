using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbKardex.Metadata))]
    public partial class TbKardex
    {
        public partial class Metadata
        {
    
            [Key]
            public object KardexID { get; set; }
    
            public object ItemID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object WarehouseID { get; set; }
    
            public object KardexCode { get; set; }
    
            public object KardexDate { get; set; }
    
            public object Sign { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object TransactionDetailID { get; set; }
    
            public object MovementOn { get; set; }
    
            public object OldQuantity { get; set; }
    
            public object OldQuantityWarehouse { get; set; }
    
            public object OldCost { get; set; }
    
            public object OldCostWarehouse { get; set; }
    
            public object TransactionQuantity { get; set; }
    
            public object TransactionCost { get; set; }
    
            public object NewQuantity { get; set; }
    
            public object NewQuantityWarehouse { get; set; }
    
            public object NewCost { get; set; }
    
            public object NewCostWarehouse { get; set; }
    
            public object QuantityInWarehouseCurrent { get; set; }
    
            public object QuantityInCurrent { get; set; }
        }
    }
}
