using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbAccountingBalanceTemp.Metadata))]
    public partial class TbAccountingBalanceTemp
    {
        public partial class Metadata
        {
    
            [Key]
            public object AccountingBalanceTempID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object LoginID { get; set; }
    
            public object Tocken { get; set; }
    
            public object AccountID { get; set; }
    
            public object ParentAccountID { get; set; }
    
            public object AccountNumber { get; set; }
    
            public object Name { get; set; }
    
            public object IsOperative { get; set; }
    
            public object StatusID { get; set; }
    
            public object AccountTypeID { get; set; }
    
            public object Naturaleza { get; set; }
    
            public object BalanceStart { get; set; }
    
            public object Debit { get; set; }
    
            public object Credit { get; set; }
    
            public object BalanceEnd { get; set; }
        }
    }
}
