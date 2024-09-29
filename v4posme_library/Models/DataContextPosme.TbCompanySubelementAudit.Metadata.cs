using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanySubelementAudit.Metadata))]
    public partial class TbCompanySubelementAudit
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanySubelementAudiID { get; set; }
    
            public object ElementID { get; set; }
    
            public object SubElementID { get; set; }
    
            public object CompanyID { get; set; }
        }
    }
}
