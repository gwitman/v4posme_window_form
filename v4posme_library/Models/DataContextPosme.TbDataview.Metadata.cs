using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbDataview.Metadata))]
    public partial class TbDataview
    {
        public partial class Metadata
        {
    
            [Key]
            public object DataViewID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object SqlScript { get; set; }
    
            public object VisibleColumns { get; set; }
    
            public object NonVisibleColumns { get; set; }
    
            public object IsActive { get; set; }
    
            public object CallerID { get; set; }
    
            public object ComponentID { get; set; }
        }
    }
}
