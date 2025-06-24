using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbBank.Metadata))]
    public partial class TbBank
    {
        public partial class Metadata
        {
    
            [Key]
            public object BankID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object AccountNumber { get; set; }
    
            public object AccountID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object Balance { get; set; }
    
            public object ManagerID { get; set; }
    
            public object CardNumber { get; set; }
    
            public object DateExpired { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Note { get; set; }
    
            public object UrlBank { get; set; }
    
            public object User { get; set; }
    
            public object Password { get; set; }
    
            public object Pin { get; set; }
    
            public object StatusID { get; set; }
    
            public object Invoiceable { get; set; }
    
            public object IsActive { get; set; }
    
            public object ComisionPos { get; set; }
    
            public object ComisionSave { get; set; }
        }
    }
}
