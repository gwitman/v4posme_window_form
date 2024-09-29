using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemDataSheet.Metadata))]
    public partial class TbItemDataSheet
    {
        public partial class Metadata
        {
    
            [Key]
            public object ItemDataSheetID { get; set; }
    
            public object ItemID { get; set; }
    
            public object Version { get; set; }
    
            public object StatusID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
