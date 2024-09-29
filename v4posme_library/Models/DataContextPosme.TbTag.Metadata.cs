using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTag.Metadata))]
    public partial class TbTag
    {
        public partial class Metadata
        {
    
            [Key]
            public object TagID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object SendEmail { get; set; }
    
            public object SendNotificationApp { get; set; }
    
            public object SendSMS { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
