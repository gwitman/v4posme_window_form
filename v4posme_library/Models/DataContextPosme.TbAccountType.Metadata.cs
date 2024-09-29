using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbAccountType.Metadata))]
    public partial class TbAccountType
    {
        public partial class Metadata
        {
    
            [Key]
            public object AccountTypeID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object Naturaleza { get; set; }
    
            public object Description { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
