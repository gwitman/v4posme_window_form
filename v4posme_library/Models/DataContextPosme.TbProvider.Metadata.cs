using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbProvider.Metadata))]
    public partial class TbProvider
    {
        public partial class Metadata
        {
    
            [Key]
            public object ProviderID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object ProviderNumber { get; set; }
    
            public object NumberIdentification { get; set; }
    
            public object IdentificationTypeID { get; set; }
    
            public object ProviderType { get; set; }
    
            public object ProviderCategoryID { get; set; }
    
            public object ProviderClasificationID { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object PayConditionID { get; set; }
    
            public object IsLocal { get; set; }
    
            public object CountryID { get; set; }
    
            public object StateID { get; set; }
    
            public object CityID { get; set; }
    
            public object Address { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object StatusID { get; set; }
    
            public object DeleveryDay { get; set; }
    
            public object DeleveryDayReal { get; set; }
    
            public object Distancia { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
