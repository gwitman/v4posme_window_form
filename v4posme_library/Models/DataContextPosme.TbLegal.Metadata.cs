using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbLegal.Metadata))]
    public partial class TbLegal
    {
        public partial class Metadata
        {
    
            [Key]
            public object LegalID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object ComercialName { get; set; }
    
            public object LegalName { get; set; }
    
            public object Address { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
