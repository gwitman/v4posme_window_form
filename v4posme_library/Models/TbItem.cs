using System;
using System.Collections.Generic;
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
[MySqlCharSet("utf8")]
[MySqlCollation("utf8_general_ci")]
public partial class TbItem
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("inventoryCategoryID", TypeName = "int(11)")]
    public int InventoryCategoryId { get; set; }

    [Key]
    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }

    [Column("familyID", TypeName = "int(11)")]
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

    [Column("displayID", TypeName = "int(11)")]
    public int? DisplayId { get; set; }

    [Column("capacity", TypeName = "int(11)")]
    public int? Capacity { get; set; }

    [Column("displayUnitMeasureID", TypeName = "int(11)")]
    public int? DisplayUnitMeasureId { get; set; }

    [Column("defaultWarehouseID", TypeName = "int(11)")]
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

    [Column("statusID", TypeName = "int(11)")]
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

    [Column("createdAt", TypeName = "int(11)")]
    public int? CreatedAt { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int? CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("isInvoiceQuantityZero", TypeName = "tinyint(4)")]
    public sbyte? IsInvoiceQuantityZero { get; set; }

    [Column("isServices", TypeName = "tinyint(4)")]
    public sbyte? IsServices { get; set; }

    [Column("currencyID", TypeName = "int(11)")]
    public int CurrencyId { get; set; }

    [Column("isInvoice", TypeName = "int(11)")]
    public int IsInvoice { get; set; }

    [Column("realStateWallInCloset", TypeName = "tinyint(4)")]
    public sbyte? RealStateWallInCloset { get; set; }

    [Column("realStatePiscinaPrivate", TypeName = "tinyint(4)")]
    public sbyte? RealStatePiscinaPrivate { get; set; }

    [Column("realStateClubPiscina", TypeName = "tinyint(4)")]
    public sbyte? RealStateClubPiscina { get; set; }

    [Column("realStateAceptanMascota", TypeName = "tinyint(4)")]
    public sbyte? RealStateAceptanMascota { get; set; }

    [Column("realStateContractCorrentaje", TypeName = "tinyint(4)")]
    public sbyte? RealStateContractCorrentaje { get; set; }

    [Column("realStatePlanReference", TypeName = "tinyint(4)")]
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

    [Column("realStateRoomServices", TypeName = "tinyint(4)")]
    public sbyte? RealStateRoomServices { get; set; }

    [Column("realStateRoomBatchServices", TypeName = "tinyint(4)")]
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

    [Column("realStateEmployerAgentID", TypeName = "int(11)")]
    public int? RealStateEmployerAgentId { get; set; }

    [Column("realStateCountryID", TypeName = "int(11)")]
    public int? RealStateCountryId { get; set; }

    [Column("realStateStateID", TypeName = "int(11)")]
    public int? RealStateStateId { get; set; }

    [Column("realStateCityID", TypeName = "int(11)")]
    public int? RealStateCityId { get; set; }

    [Column("modifiedOn", TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    [Column("realStateRooBatchVisit", TypeName = "tinyint(4)")]
    public sbyte? RealStateRooBatchVisit { get; set; }

    [Column("realStateGerenciaExclusive", TypeName = "int(11)")]
    public int? RealStateGerenciaExclusive { get; set; }
}
