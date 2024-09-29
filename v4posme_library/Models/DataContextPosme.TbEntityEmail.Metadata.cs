using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEntityEmail.Metadata))]
    public partial class TbEntityEmail
    {
        public partial class Metadata
        {
    
            [Key]
            public object EntityEmailID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object Email { get; set; }
    
            public object IsPrimary { get; set; }
        }
    }
}
