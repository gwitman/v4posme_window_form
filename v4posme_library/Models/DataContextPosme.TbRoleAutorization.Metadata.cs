using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbRoleAutorization.Metadata))]
    public partial class TbRoleAutorization
    {
        public partial class Metadata
        {
    
            [Key]
            public object RoleAurotizationID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentAutorizationID { get; set; }
    
            public object RoleID { get; set; }
    
            public object BranchID { get; set; }
        }
    }
}
