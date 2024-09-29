using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionProfileDetail.Metadata))]
    public partial class TbTransactionProfileDetail
    {
        public partial class Metadata
        {
    
            [Key]
            public object ProfileDetailID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionCausalID { get; set; }
    
            public object ConceptID { get; set; }
    
            public object AccountID { get; set; }
    
            public object ClassID { get; set; }
    
            public object Sign { get; set; }
        }
    }
}
