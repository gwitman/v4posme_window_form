using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbUserWarehouse.Metadata))]
    public partial class TbUserWarehouse
    {
        public partial class Metadata
        {
    
            [Key]
            public object UserWarehouseID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object UserID { get; set; }
    
            public object WarehouseID { get; set; }
        }
    }
}
