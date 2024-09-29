using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterDetail.Metadata))]
    public partial class TbTransactionMasterDetail
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionMasterDetailID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object PromotionID { get; set; }
    
            public object Amount { get; set; }
    
            public object Cost { get; set; }
    
            public object Quantity { get; set; }
    
            public object Discount { get; set; }
    
            public object UnitaryAmount { get; set; }
    
            public object Tax1 { get; set; }
    
            public object Tax2 { get; set; }
    
            public object Tax3 { get; set; }
    
            public object Tax4 { get; set; }
    
            public object UnitaryCost { get; set; }
    
            public object UnitaryPrice { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object Reference4 { get; set; }
    
            public object Reference5 { get; set; }
    
            public object Reference6 { get; set; }
    
            public object Reference7 { get; set; }
    
            public object DescriptionReference { get; set; }
    
            public object ExchangeRateReference { get; set; }
    
            public object CatalogStatusID { get; set; }
    
            public object InventoryStatusID { get; set; }
    
            public object IsActive { get; set; }
    
            public object QuantityStock { get; set; }
    
            public object QuantiryStockInTraffic { get; set; }
    
            public object QuantityStockUnaswared { get; set; }
    
            public object RemaingStock { get; set; }
    
            public object Lote { get; set; }
    
            public object ExpirationDate { get; set; }
    
            public object InventoryWarehouseSourceID { get; set; }
    
            public object InventoryWarehouseTargetID { get; set; }
    
            public object ItemFormulatedApplied { get; set; }
    
            public object TypePriceID { get; set; }
    
            public object SkuCatalogItemID { get; set; }
    
            public object SkuQuantity { get; set; }
    
            public object SkuQuantityBySku { get; set; }
    
            public object SkuFormatoDescription { get; set; }
    
            public object ItemNameLog { get; set; }
    
            public object AmountCommision { get; set; }
    
            public object ItemNameDescriptionLog { get; set; }
        }
    }
}
