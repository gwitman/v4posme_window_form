using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEntityLocation.Metadata))]
    public partial class TbEntityLocation
    {
        public partial class Metadata
        {
    
            [Key]
            public object EntityLocationID { get; set; }
    
            public object EntityID { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object IsActive { get; set; }
    
            public object Latituded { get; set; }
    
            public object Longituded { get; set; }
    
            public object Reference1 { get; set; }
    
            public object UserName { get; set; }
    
            public object CompanyName { get; set; }
        }
    }
}
