using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbUserTag.Metadata))]
    public partial class TbUserTag
    {
        public partial class Metadata
        {
    
            [Key]
            public object UserTagID { get; set; }
    
            public object TagID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object UserID { get; set; }
        }
    }
}
