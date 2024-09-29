using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionConcept.Metadata))]
    public partial class TbTransactionConcept
    {
        public partial class Metadata
        {
    
            [Key]
            public object ConceptID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object Name { get; set; }
    
            public object Orden { get; set; }
    
            public object Sign { get; set; }
    
            public object Visible { get; set; }
    
            public object Base { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
