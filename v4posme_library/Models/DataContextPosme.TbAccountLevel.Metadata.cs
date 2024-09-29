using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbAccountLevel.Metadata))]
    public partial class TbAccountLevel
    {
        public partial class Metadata
        {
    
            [Key]
            public object AccountLevelID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object LengthTotal { get; set; }
    
            public object Split { get; set; }
    
            public object LengthGroup { get; set; }
    
            public object IsOperative { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
