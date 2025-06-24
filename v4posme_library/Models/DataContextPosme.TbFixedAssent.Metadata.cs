using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbFixedAssent.Metadata))]
    public partial class TbFixedAssent
    {
        public partial class Metadata
        {
    
            [Key]
            public object FixedAssentID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object FixedAssentCode { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object ModelNumber { get; set; }
    
            public object Marca { get; set; }
    
            public object ColorID { get; set; }
    
            public object ChasisNumber { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Year { get; set; }
    
            public object AsignedEmployeeID { get; set; }
    
            public object CategoryID { get; set; }
    
            public object TypeID { get; set; }
    
            public object TypeDepresiationID { get; set; }
    
            public object YearOfUtility { get; set; }
    
            public object PriceStart { get; set; }
    
            public object IsForaneo { get; set; }
    
            public object StatusID { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CountryID { get; set; }
    
            public object CityID { get; set; }
    
            public object MunicipalityID { get; set; }
    
            public object Address { get; set; }
    
            public object AreaID { get; set; }
    
            public object ProjectID { get; set; }
    
            public object Duration { get; set; }
    
            public object TypeFixedAssentID { get; set; }
    
            public object StartOn { get; set; }
    
            public object Ratio { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object CurrentAmount { get; set; }
    
            public object SettlementAmount { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
