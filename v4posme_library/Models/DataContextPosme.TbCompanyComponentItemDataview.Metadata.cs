using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyComponentItemDataview.Metadata))]
    public partial class TbCompanyComponentItemDataview
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyComponentItemDataviewID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object DataViewID { get; set; }
    
            public object CallerID { get; set; }
    
            public object FlavorID { get; set; }
        }
    }
}
