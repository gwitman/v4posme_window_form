using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerCreditDocumentEntityRelated.Metadata))]
    public partial class TbCustomerCreditDocumentEntityRelated
    {
        public partial class Metadata
        {
    
            [Key]
            public object CcEntityRelatedID { get; set; }
    
            public object CustomerCreditDocumentID { get; set; }
    
            public object EntityID { get; set; }
    
            public object Type { get; set; }
    
            public object TypeCredit { get; set; }
    
            public object StatusCredit { get; set; }
    
            public object TypeGarantia { get; set; }
    
            public object TypeRecuperation { get; set; }
    
            public object RatioDesembolso { get; set; }
    
            public object RatioBalance { get; set; }
    
            public object RatioBalanceExpired { get; set; }
    
            public object RatioShare { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
