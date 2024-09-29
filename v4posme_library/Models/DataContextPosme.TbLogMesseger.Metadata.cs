using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbLogMesseger.Metadata))]
    public partial class TbLogMesseger
    {
        public partial class Metadata
        {
    
            [Key]
            public object Id { get; set; }
    
            public object Errno { get; set; }
    
            public object Errtype { get; set; }
    
            public object Errstr { get; set; }
    
            public object Errfile { get; set; }
    
            public object Errline { get; set; }
    
            public object UserAgent { get; set; }
    
            public object IpAddress { get; set; }
    
            public object Time { get; set; }
        }
    }
}
