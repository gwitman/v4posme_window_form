using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbListPrice.Metadata))]
    public partial class TbListPrice
    {
        public partial class Metadata
        {
    
            [Key]
            public object ListPriceID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object StartOn { get; set; }
    
            public object EndOn { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object StatusID { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
