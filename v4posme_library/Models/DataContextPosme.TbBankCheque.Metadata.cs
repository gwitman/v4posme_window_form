using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbBankCheque.Metadata))]
    public partial class TbBankCheque
    {
        public partial class Metadata
        {
    
            [Key]
            public object ChequeID { get; set; }
    
            public object ChequeNumber { get; set; }
    
            public object Name { get; set; }
    
            public object StatusID { get; set; }
    
            public object BankID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object ValueInitial { get; set; }
    
            public object ValueCurrent { get; set; }
    
            public object ValueFinal { get; set; }
    
            public object Serie { get; set; }
    
            public object ManagerID { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
