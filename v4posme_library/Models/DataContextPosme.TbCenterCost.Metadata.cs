using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCenterCost.Metadata))]
    public partial class TbCenterCost
    {
        public partial class Metadata
        {
    
            [Key]
            public object ClassID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object AccountLevelID { get; set; }
    
            public object ParentAccountID { get; set; }
    
            public object ParentClassID { get; set; }
    
            public object Number { get; set; }
    
            public object Description { get; set; }
    
            public object IsActive { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedIn { get; set; }
        }
    }
}
