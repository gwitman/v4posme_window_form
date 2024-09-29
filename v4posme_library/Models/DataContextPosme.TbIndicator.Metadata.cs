using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbIndicator.Metadata))]
    public partial class TbIndicator
    {
        public partial class Metadata
        {
    
            [Key]
            public object IndicadorID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Code { get; set; }
    
            public object Name { get; set; }
    
            public object Label { get; set; }
    
            public object Description { get; set; }
    
            public object Order { get; set; }
    
            public object Script { get; set; }
    
            public object Posfix { get; set; }
    
            public object Prefix { get; set; }
    
            public object IsActive { get; set; }
    
            public object IsGroup { get; set; }
        }
    }
}
