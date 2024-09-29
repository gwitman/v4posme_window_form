using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyComponentFlavor.Metadata))]
    public partial class TbCompanyComponentFlavor
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyComponentFlavorID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object FlavorID { get; set; }
        }
    }
}
