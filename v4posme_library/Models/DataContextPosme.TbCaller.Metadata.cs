using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCaller.Metadata))]
    public partial class TbCaller
    {
        public partial class Metadata
        {
    
            [Key]
            public object CallerID { get; set; }
    
            public object IsActive { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
        }
    }
}
