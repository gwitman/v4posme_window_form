using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbLogSession.Metadata))]
    public partial class TbLogSession
    {
        public partial class Metadata
        {
    
            [Key]
            public object SessionId { get; set; }
    
            public object IpAddress { get; set; }
    
            public object UserAgent { get; set; }
    
            public object LastActivity { get; set; }
    
            public object UserData { get; set; }
        }
    }
}
