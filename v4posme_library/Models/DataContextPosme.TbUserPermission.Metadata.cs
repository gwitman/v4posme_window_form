using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbUserPermission.Metadata))]
    public partial class TbUserPermission
    {
        public partial class Metadata
        {
    
            [Key]
            public object UserPermissionID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object ElementID { get; set; }
    
            public object RoleID { get; set; }
    
            public object Selected { get; set; }
    
            public object Inserted { get; set; }
    
            public object Deleted { get; set; }
    
            public object Edited { get; set; }
        }
    }
}
