using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item")]
[Index("CompanyId", Name = "IDX_ITEM_001")]
[Index("BranchId", Name = "IDX_ITEM_002")]
[Index("InventoryCategoryId", Name = "IDX_ITEM_003")]
[Index("FamilyId", Name = "IDX_ITEM_004")]
[Index("ItemNumber", Name = "IDX_ITEM_005")]
[Index("UnitMeasureId", Name = "IDX_ITEM_007")]
[Index("DisplayId", Name = "IDX_ITEM_008")]
[Index("DisplayUnitMeasureId", Name = "IDX_ITEM_009")]
[Index("DefaultWarehouseId", Name = "IDX_ITEM_010")]
[Index("StatusId", Name = "IDX_ITEM_011")]
[Index("CurrencyId", Name = "IDX_ITEM_012")]
[MySqlCharSet("utf8mb3")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class TbItem
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("inventoryCategoryID")]
    public int InventoryCategoryId { get; set; }

    [Key]
    [Column("itemID")]
    public int ItemId { get; set; }

    [Column("familyID")]
    public int? FamilyId { get; set; }

    [Column("itemNumber")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string ItemNumber { get; set; } = null!;

    [Column("barCode")]
    [StringLength(1200)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? BarCode { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Column("unitMeasureID")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? UnitMeasureId { get; set; }

    [Column("displayID")]
    public int? DisplayId { get; set; }

    [Column("capacity")]
    public int? Capacity { get; set; }

    [Column("displayUnitMeasureID")]
    public int? DisplayUnitMeasureId { get; set; }

    [Column("defaultWarehouseID")]
    public int? DefaultWarehouseId { get; set; }

    [Column("quantity")]
    [Precision(18, 4)]
    public decimal Quantity { get; set; }

    [Column("quantityMax")]
    [Precision(18, 4)]
    public decimal QuantityMax { get; set; }

    [Column("quantityMin")]
    [Precision(18, 4)]
    public decimal QuantityMin { get; set; }

    [Column("cost")]
    [Precision(18, 4)]
    public decimal Cost { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(255)]
    public string? Reference3 { get; set; }

    [Column("statusID")]
    public int? StatusId { get; set; }

    [Column("isPerishable")]
    public bool? IsPerishable { get; set; }

    [Column("factorBox")]
    [Precision(18, 4)]
    public decimal? FactorBox { get; set; }

    [Column("factorProgram")]
    [Precision(18, 4)]
    public decimal? FactorProgram { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? CreatedIn { get; set; }

    [Column("createdAt")]
    public int? CreatedAt { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("isInvoiceQuantityZero")]
    public sbyte? IsInvoiceQuantityZero { get; set; }

    [Column("isServices")]
    public sbyte? IsServices { get; set; }

    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("isInvoice")]
    public int IsInvoice { get; set; }

    [Column("realStateWallInCloset")]
    public sbyte? RealStateWallInCloset { get; set; }

    [Column("realStatePiscinaPrivate")]
    public sbyte? RealStatePiscinaPrivate { get; set; }

    [Column("realStateClubPiscina")]
    public sbyte? RealStateClubPiscina { get; set; }

    [Column("realStateAceptanMascota")]
    public sbyte? RealStateAceptanMascota { get; set; }

    [Column("realStateContractCorrentaje")]
    public sbyte? RealStateContractCorrentaje { get; set; }

    [Column("realStatePlanReference")]
    public sbyte? RealStatePlanReference { get; set; }

    [Column("realStateLinkYoutube")]
    [StringLength(255)]
    public string? RealStateLinkYoutube { get; set; }

    [Column("realStateLinkPaginaWeb")]
    [StringLength(255)]
    public string? RealStateLinkPaginaWeb { get; set; }

    [Column("realStateLinkPhontos")]
    [StringLength(255)]
    public string? RealStateLinkPhontos { get; set; }

    [Column("realStateLinkGoogleMaps")]
    [StringLength(255)]
    public string? RealStateLinkGoogleMaps { get; set; }

    [Column("realStateLinkOther")]
    [StringLength(255)]
    public string? RealStateLinkOther { get; set; }

    [Column("realStateStyleKitchen")]
    [StringLength(255)]
    public string? RealStateStyleKitchen { get; set; }

    [Column("realStateRoomServices")]
    public sbyte? RealStateRoomServices { get; set; }

    [Column("realStateRoomBatchServices")]
    public sbyte? RealStateRoomBatchServices { get; set; }

    [Column("realStateReferenceUbicacion")]
    [StringLength(255)]
    public string? RealStateReferenceUbicacion { get; set; }

    [Column("realStateReferenceZone")]
    [StringLength(255)]
    public string? RealStateReferenceZone { get; set; }

    [Column("realStateReferenceCondominio")]
    [StringLength(255)]
    public string? RealStateReferenceCondominio { get; set; }

    [Column("realStateEmployerAgentID")]
    public int? RealStateEmployerAgentId { get; set; }

    [Column("realStateCountryID")]
    public int? RealStateCountryId { get; set; }

    [Column("realStateStateID")]
    public int? RealStateStateId { get; set; }

    [Column("realStateCityID")]
    public int? RealStateCityId { get; set; }

    [Column("modifiedOn", TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    [Column("realStateRooBatchVisit")]
    public sbyte? RealStateRooBatchVisit { get; set; }

    [Column("realStateGerenciaExclusive")]
    public int? RealStateGerenciaExclusive { get; set; }
}
