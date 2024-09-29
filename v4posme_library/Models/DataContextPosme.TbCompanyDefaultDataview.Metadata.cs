using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyDefaultDataview.Metadata))]
    public partial class TbCompanyDefaultDataview
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyDefaultDataviewID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object DataViewID { get; set; }
    
            public object CallerID { get; set; }
    
            public object TargetComponentID { get; set; }
        }
    }
}
