using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_provider")]
[Index("CompanyId", Name = "IDX_PROVIDER_001")]
[Index("BranchId", Name = "IDX_PROVIDER_002")]
[Index("EntityId", Name = "IDX_PROVIDER_003")]
[Index("ProviderNumber", Name = "IDX_PROVIDER_004")]
[Index("IdentificationTypeId", Name = "IDX_PROVIDER_005")]
[Index("ProviderType", Name = "IDX_PROVIDER_006")]
[Index("ProviderCategoryId", Name = "IDX_PROVIDER_007")]
[Index("ProviderClasificationId", Name = "IDX_PROVIDER_008")]
[Index("PayConditionId", Name = "IDX_PROVIDER_009")]
[Index("CountryId", Name = "IDX_PROVIDER_010")]
[Index("StateId", Name = "IDX_PROVIDER_011")]
[Index("CityId", Name = "IDX_PROVIDER_012")]
[Index("CurrencyId", Name = "IDX_PROVIDER_013")]
[Index("StatusId", Name = "IDX_PROVIDER_014")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbProvider
{
    [Key]
    [Column("providerID")]
    public int ProviderId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("entityID")]
    public int EntityId { get; set; }

    [Column("providerNumber")]
    [StringLength(250)]
    public string ProviderNumber { get; set; } = null!;

    [Column("numberIdentification")]
    [StringLength(250)]
    public string? NumberIdentification { get; set; }

    [Column("identificationTypeID")]
    public int? IdentificationTypeId { get; set; }

    [Column("providerType")]
    public int? ProviderType { get; set; }

    [Column("providerCategoryID")]
    public int? ProviderCategoryId { get; set; }

    [Column("providerClasificationID")]
    public int? ProviderClasificationId { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("payConditionID")]
    public int? PayConditionId { get; set; }

    [Column("isLocal")]
    public bool? IsLocal { get; set; }

    [Column("countryID")]
    public int? CountryId { get; set; }

    [Column("stateID")]
    public int? StateId { get; set; }

    [Column("cityID")]
    public int? CityId { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("currencyID")]
    public int? CurrencyId { get; set; }

    [Column("statusID")]
    public int? StatusId { get; set; }

    [Column("deleveryDay")]
    public int? DeleveryDay { get; set; }

    [Column("deleveryDayReal")]
    public int? DeleveryDayReal { get; set; }

    [Column("distancia")]
    [Precision(18, 4)]
    public decimal? Distancia { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }

    [Column("createdAt")]
    public int? CreatedAt { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }
}
