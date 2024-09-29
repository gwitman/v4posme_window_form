using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemWarehouseExpired.Metadata))]
    public partial class TbItemWarehouseExpired
    {
        public partial class Metadata
        {
    
            [Key]
            public object ItemWarehouseExpiredID { get; set; }
    
            public object WarehouseID { get; set; }
    
            public object ItemID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Quantity { get; set; }
    
            public object Lote { get; set; }
    
            public object DateExpired { get; set; }
        }
    }
}
