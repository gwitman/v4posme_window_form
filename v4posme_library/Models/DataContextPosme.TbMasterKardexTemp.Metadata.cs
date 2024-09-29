using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbMasterKardexTemp.Metadata))]
    public partial class TbMasterKardexTemp
    {
        public partial class Metadata
        {
    
            [Key]
            public object MasterKardexTempID { get; set; }
    
            public object UserID { get; set; }
    
            public object TokenID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ItemID { get; set; }
    
            public object ItemNumber { get; set; }
    
            public object ItemName { get; set; }
    
            public object MinKardexID { get; set; }
    
            public object QuantityInicial { get; set; }
    
            public object CostInicial { get; set; }
    
            public object QuantityInput { get; set; }
    
            public object CostInput { get; set; }
    
            public object QuantityOutput { get; set; }
    
            public object CostOutput { get; set; }
    
            public object ItemCategoryName { get; set; }
        }
    }
}
