using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbProviderItem.Metadata))]
    public partial class TbProviderItem
    {
        public partial class Metadata
        {
    
            [Key]
            public object ProviderItemID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object ItemID { get; set; }
        }
    }
}
