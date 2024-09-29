using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemCategory.Metadata))]
    public partial class TbItemCategory
    {
        public partial class Metadata
        {
    
            [Key]
            public object InventoryCategoryID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object IsActive { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedAt { get; set; }
        }
    }
}
