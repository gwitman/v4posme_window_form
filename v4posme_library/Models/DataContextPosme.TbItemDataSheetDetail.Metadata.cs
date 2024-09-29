using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemDataSheetDetail.Metadata))]
    public partial class TbItemDataSheetDetail
    {
        public partial class Metadata
        {
    
            [Key]
            public object ItemDataSheetDetailID { get; set; }
    
            public object ItemDataSheetID { get; set; }
    
            public object ItemID { get; set; }
    
            public object Quantity { get; set; }
    
            public object RelatedItemID { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
