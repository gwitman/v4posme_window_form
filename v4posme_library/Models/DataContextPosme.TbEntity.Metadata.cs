using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEntity.Metadata))]
    public partial class TbEntity
    {
        public partial class Metadata
        {
    
            [Key]
            public object EntityID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object ImagenBiometric { get; set; }
        }
    }
}
