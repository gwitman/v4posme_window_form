using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbRemember.Metadata))]
    public partial class TbRemember
    {
        public partial class Metadata
        {
    
            [Key]
            public object RememberID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Title { get; set; }
    
            public object Description { get; set; }
    
            public object Period { get; set; }
    
            public object Day { get; set; }
    
            public object StatusID { get; set; }
    
            public object LastNotificationOn { get; set; }
    
            public object IsTemporal { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
    
            public object TagID { get; set; }
    
            public object LeerFile { get; set; }
        }
    }
}
