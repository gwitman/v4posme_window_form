using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomer.Metadata))]
    public partial class TbCustomer
    {
        public partial class Metadata
        {
    
            [Key]
            public object CustomerID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object CustomerNumber { get; set; }
    
            public object IdentificationType { get; set; }
    
            public object Identification { get; set; }
    
            public object CountryID { get; set; }
    
            public object StateID { get; set; }
    
            public object CityID { get; set; }
    
            public object Location { get; set; }
    
            public object Address { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object ClasificationID { get; set; }
    
            public object CategoryID { get; set; }
    
            public object SubCategoryID { get; set; }
    
            public object CustomerTypeID { get; set; }
    
            public object BirthDate { get; set; }
    
            public object StatusID { get; set; }
    
            public object TypePay { get; set; }
    
            public object PayConditionID { get; set; }
    
            public object SexoID { get; set; }
    
            public object TypeFirm { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object IsActive { get; set; }
    
            public object BalancePoint { get; set; }
    
            public object PhoneNumber { get; set; }
    
            public object DateContract { get; set; }
    
            public object EntityContactID { get; set; }
    
            public object Reference3 { get; set; }
    
            public object Reference4 { get; set; }
    
            public object Reference5 { get; set; }
    
            public object Reference6 { get; set; }
    
            public object Budget { get; set; }
    
            public object ModifiedOn { get; set; }
    
            public object FormContactID { get; set; }
        }
    }
}
