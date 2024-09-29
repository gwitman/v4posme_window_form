using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItem.Metadata))]
    public partial class TbItem
    {
        public partial class Metadata
        {
    
            [Key]
            public object ItemID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object InventoryCategoryID { get; set; }
    
            public object FamilyID { get; set; }
    
            public object ItemNumber { get; set; }
    
            public object BarCode { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object UnitMeasureID { get; set; }
    
            public object DisplayID { get; set; }
    
            public object Capacity { get; set; }
    
            public object DisplayUnitMeasureID { get; set; }
    
            public object DefaultWarehouseID { get; set; }
    
            public object Quantity { get; set; }
    
            public object QuantityMax { get; set; }
    
            public object QuantityMin { get; set; }
    
            public object Cost { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsPerishable { get; set; }
    
            public object FactorBox { get; set; }
    
            public object FactorProgram { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object IsActive { get; set; }
    
            public object IsInvoiceQuantityZero { get; set; }
    
            public object IsServices { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object IsInvoice { get; set; }
    
            public object RealStateWallInCloset { get; set; }
    
            public object RealStatePiscinaPrivate { get; set; }
    
            public object RealStateClubPiscina { get; set; }
    
            public object RealStateAceptanMascota { get; set; }
    
            public object RealStateContractCorrentaje { get; set; }
    
            public object RealStatePlanReference { get; set; }
    
            public object RealStateLinkYoutube { get; set; }
    
            public object RealStateLinkPaginaWeb { get; set; }
    
            public object RealStateLinkPhontos { get; set; }
    
            public object RealStateLinkGoogleMaps { get; set; }
    
            public object RealStateLinkOther { get; set; }
    
            public object RealStateStyleKitchen { get; set; }
    
            public object RealStateRoomServices { get; set; }
    
            public object RealStateRoomBatchServices { get; set; }
    
            public object RealStateReferenceUbicacion { get; set; }
    
            public object RealStateReferenceZone { get; set; }
    
            public object RealStateReferenceCondominio { get; set; }
    
            public object RealStateEmployerAgentID { get; set; }
    
            public object RealStateCountryID { get; set; }
    
            public object RealStateStateID { get; set; }
    
            public object RealStateCityID { get; set; }
    
            public object ModifiedOn { get; set; }
    
            public object RealStateRooBatchVisit { get; set; }
    
            public object RealStateGerenciaExclusive { get; set; }
    
            public object RealStatePhone { get; set; }
    
            public object RealStateEmail { get; set; }
    
            public object DateLastUse { get; set; }
    
            public object QuantityInvoice { get; set; }
        }
    }
}
