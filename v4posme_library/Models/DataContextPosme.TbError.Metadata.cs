using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbError.Metadata))]
    public partial class TbError
    {
        public partial class Metadata
        {
    
            [Key]
            public object ErrorID { get; set; }
    
            public object TagID { get; set; }
    
            public object Notificated { get; set; }
    
            public object Message { get; set; }
    
            public object IsActive { get; set; }
    
            public object IsRead { get; set; }
    
            public object UserID { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object ReadOn { get; set; }
        }
    }
}
