using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbBranch.Metadata))]
    public partial class TbBranch
    {
        public partial class Metadata
        {
    
            [Key]
            public object BranchID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object IsActive { get; set; }
    
            public object Address { get; set; }
    
            public object Serie { get; set; }
        }
    }
}
