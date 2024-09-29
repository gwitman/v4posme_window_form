using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbAccountTmp.Metadata))]
    public partial class TbAccountTmp
    {
        public partial class Metadata
        {
    
            [Key]
            public object AccountID { get; set; }
    
            public object AccountParentID { get; set; }
    
            public object N1 { get; set; }
    
            public object N2 { get; set; }
    
            public object N3 { get; set; }
    
            public object N4 { get; set; }
    
            public object N5 { get; set; }
    
            public object Name { get; set; }
    
            public object Nivel { get; set; }
    
            public object Operative { get; set; }
    
            public object Balance { get; set; }
        }
    }
}
