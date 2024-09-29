using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerPaymentMethod.Metadata))]
    public partial class TbCustomerPaymentMethod
    {
        public partial class Metadata
        {
    
            [Key]
            public object CustomerPaymentMethod { get; set; }
    
            public object EntityID { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsActive { get; set; }
    
            public object Name { get; set; }
    
            public object Number { get; set; }
    
            public object Email { get; set; }
    
            public object ExpirationDate { get; set; }
    
            public object Cvc { get; set; }
    
            public object TypeId { get; set; }
        }
    }
}
