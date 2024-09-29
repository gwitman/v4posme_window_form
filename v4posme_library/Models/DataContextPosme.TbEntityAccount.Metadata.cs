using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEntityAccount.Metadata))]
    public partial class TbEntityAccount
    {
        public partial class Metadata
        {
    
            [Key]
            public object EntityAccountID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object AccountTypeID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object ClassID { get; set; }
    
            public object Balance { get; set; }
    
            public object CreditLimit { get; set; }
    
            public object MaxCredit { get; set; }
    
            public object DebitLimit { get; set; }
    
            public object MaxDebit { get; set; }
    
            public object StatusID { get; set; }
    
            public object AccountID { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
