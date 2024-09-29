using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemWarehouse.Metadata))]
    public partial class TbItemWarehouse
    {
        public partial class Metadata
        {
    
            [Key]
            public object ItemWarehouseId { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object WarehouseID { get; set; }
    
            public object ItemID { get; set; }
    
            public object Quantity { get; set; }
    
            public object Cost { get; set; }
    
            public object QuantityMax { get; set; }
    
            public object QuantityMin { get; set; }
        }
    }
}
