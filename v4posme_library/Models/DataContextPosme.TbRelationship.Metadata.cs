using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbRelationship.Metadata))]
    public partial class TbRelationship
    {
        public partial class Metadata
        {
    
            [Key]
            public object RelationshipID { get; set; }
    
            public object EmployeeID { get; set; }
    
            public object CustomerID { get; set; }
    
            public object StartOn { get; set; }
    
            public object EndOn { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
