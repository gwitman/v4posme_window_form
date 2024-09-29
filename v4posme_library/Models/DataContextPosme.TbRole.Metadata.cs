using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbRole.Metadata))]
    public partial class TbRole
    {
        public partial class Metadata
        {
    
            [Key]
            public object RoleID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object IsAdmin { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object UrlDefault { get; set; }
    
            public object IsActive { get; set; }
    
            public object CreatedBy { get; set; }
        }
    }
}
