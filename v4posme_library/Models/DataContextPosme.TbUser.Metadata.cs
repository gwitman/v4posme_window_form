using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbUser.Metadata))]
    public partial class TbUser
    {
        public partial class Metadata
        {
    
            [Key]
            public object UserID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object Nickname { get; set; }
    
            public object Password { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object IsActive { get; set; }
    
            public object Email { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object EmployeeID { get; set; }
    
            public object UseMobile { get; set; }
    
            public object Phone { get; set; }
    
            public object LastPayment { get; set; }
    
            public object Comercio { get; set; }
    
            public object Foto { get; set; }
    
            public object TokenGoogleCalendar { get; set; }
    
            public object LocationID { get; set; }
        }
    }
}
