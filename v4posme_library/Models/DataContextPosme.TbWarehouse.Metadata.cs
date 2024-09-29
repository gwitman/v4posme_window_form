using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbWarehouse.Metadata))]
    public partial class TbWarehouse
    {
        public partial class Metadata
        {
    
            [Key]
            public object WarehouseID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object Number { get; set; }
    
            public object Name { get; set; }
    
            public object Address { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsActive { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object TypeWarehouse { get; set; }
    
            public object EmailResponsability { get; set; }
        }
    }
}
