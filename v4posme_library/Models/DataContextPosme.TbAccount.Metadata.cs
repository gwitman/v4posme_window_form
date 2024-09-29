using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbAccount.Metadata))]
    public partial class TbAccount
    {
        public partial class Metadata
        {
    
            [Key]
            public object AccountID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object AccountTypeID { get; set; }
    
            public object AccountLevelID { get; set; }
    
            public object ParentAccountID { get; set; }
    
            public object ClassID { get; set; }
    
            public object AccountNumber { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object IsOperative { get; set; }
    
            public object StatusID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
