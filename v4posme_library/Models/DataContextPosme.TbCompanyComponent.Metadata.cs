using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyComponent.Metadata))]
    public partial class TbCompanyComponent
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyComponentID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object CompanyID { get; set; }
        }
    }
}
