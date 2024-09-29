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
    
            public object AccountID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object Balance { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
