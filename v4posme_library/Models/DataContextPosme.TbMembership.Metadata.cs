using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbMembership.Metadata))]
    public partial class TbMembership
    {
        public partial class Metadata
        {
    
            [Key]
            public object MembershipID { get; set; }
    
            public object RoleID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object UserID { get; set; }
        }
    }
}
