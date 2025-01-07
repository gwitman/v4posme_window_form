using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEmployee.Metadata))]
    public partial class TbEmployee
    {
        public partial class Metadata
        {
    
            [Key]
            public object EmployeeID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object EmployeNumber { get; set; }
    
            public object NumberIdentification { get; set; }
    
            public object IdentificationTypeID { get; set; }
    
            public object SocialSecurityNumber { get; set; }
    
            public object Address { get; set; }
    
            public object CountryID { get; set; }
    
            public object StateID { get; set; }
    
            public object CityID { get; set; }
    
            public object DepartamentID { get; set; }
    
            public object AreaID { get; set; }
    
            public object ClasificationID { get; set; }
    
            public object CategoryID { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object TypeEmployeeID { get; set; }
    
            public object HourCost { get; set; }
    
            public object ComissionPorcentage { get; set; }
    
            public object ParentEmployeeID { get; set; }
    
            public object StartOn { get; set; }
    
            public object EndOn { get; set; }
    
            public object StatusID { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object IsActive { get; set; }
    
            public object VacationBalanceDay { get; set; }
    
            public object AmountSaving { get; set; }
        }
    }
}
