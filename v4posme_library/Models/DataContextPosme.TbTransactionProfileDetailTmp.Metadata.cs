using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionProfileDetailTmp.Metadata))]
    public partial class TbTransactionProfileDetailTmp
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionProfileDetailTmpID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object LoginID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object TransactionCausalID { get; set; }
    
            public object ConceptID { get; set; }
    
            public object AccountID { get; set; }
    
            public object ClassID { get; set; }
    
            public object Debit { get; set; }
    
            public object Credit { get; set; }
        }
    }
}
