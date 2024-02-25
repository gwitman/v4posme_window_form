using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer")]
[Index("CompanyId", Name = "IDX_CUSTOMER_001")]
[Index("BranchId", Name = "IDX_CUSTOMER_002")]
[Index("EntityId", Name = "IDX_CUSTOMER_003")]
[Index("CustomerNumber", Name = "IDX_CUSTOMER_004")]
[Index("IdentificationType", Name = "IDX_CUSTOMER_005")]
[Index("CountryId", Name = "IDX_CUSTOMER_006")]
[Index("StateId", Name = "IDX_CUSTOMER_007")]
[Index("CityId", Name = "IDX_CUSTOMER_008")]
[Index("CurrencyId", Name = "IDX_CUSTOMER_009")]
[Index("ClasificationId", Name = "IDX_CUSTOMER_010")]
[Index("CategoryId", Name = "IDX_CUSTOMER_011")]
[Index("SubCategoryId", Name = "IDX_CUSTOMER_012")]
[Index("CustomerTypeId", Name = "IDX_CUSTOMER_013")]
[Index("StatusId", Name = "IDX_CUSTOMER_014")]
[Index("TypePay", Name = "IDX_CUSTOMER_015")]
[Index("PayConditionId", Name = "IDX_CUSTOMER_016")]
[Index("SexoId", Name = "IDX_CUSTOMER_017")]
[Index("TypeFirm", Name = "IDX_CUSTOMER_018")]
[Index("EntityContactId", Name = "IDX_CUSTOMER_019")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbCustomer
{
    [Key]
    [Column("customerID", TypeName = "int(11)")]
    public int CustomerId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("customerNumber")]
    [StringLength(250)]
    public string CustomerNumber { get; set; } = null!;

    [Column("identificationType", TypeName = "int(11)")]
    public int? IdentificationType { get; set; }

    [Column("identification")]
    [StringLength(250)]
    public string Identification { get; set; } = null!;

    [Column("countryID", TypeName = "int(11)")]
    public int? CountryId { get; set; }

    [Column("stateID", TypeName = "int(11)")]
    public int? StateId { get; set; }

    [Column("cityID", TypeName = "int(11)")]
    public int? CityId { get; set; }

    [Column("location")]
    [StringLength(250)]
    public string? Location { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("currencyID", TypeName = "int(11)")]
    public int? CurrencyId { get; set; }

    [Column("clasificationID", TypeName = "int(11)")]
    public int? ClasificationId { get; set; }

    [Column("categoryID", TypeName = "int(11)")]
    public int? CategoryId { get; set; }

    [Column("subCategoryID", TypeName = "int(11)")]
    public int? SubCategoryId { get; set; }

    [Column("customerTypeID", TypeName = "int(11)")]
    public int? CustomerTypeId { get; set; }

    [Column("birthDate")]
    public DateTime? BirthDate { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int? StatusId { get; set; }

    [Column("typePay", TypeName = "int(11)")]
    public int? TypePay { get; set; }

    [Column("payConditionID", TypeName = "int(11)")]
    public int? PayConditionId { get; set; }

    [Column("sexoID", TypeName = "int(11)")]
    public int? SexoId { get; set; }

    /// <summary>
    /// Tipo de Firma
    /// </summary>
    [Column("typeFirm", TypeName = "int(11)")]
    public int? TypeFirm { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int? CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdAt", TypeName = "int(11)")]
    public int? CreatedAt { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }

    [Column("balancePoint")]
    [Precision(10, 2)]
    public decimal? BalancePoint { get; set; }

    [Column("phoneNumber")]
    [MaxLength(255)]
    public byte[]? PhoneNumber { get; set; }

    [Column("dateContract")]
    public DateOnly? DateContract { get; set; }

    /// <summary>
    /// Persona que contacto al cliente
    /// </summary>
    [Column("entityContactID", TypeName = "int(11)")]
    public int? EntityContactId { get; set; }

    [Column("reference3")]
    [StringLength(255)]
    public string? Reference3 { get; set; }

    [Column("reference4")]
    [StringLength(255)]
    public string? Reference4 { get; set; }

    [Column("reference5")]
    [StringLength(255)]
    public string? Reference5 { get; set; }

    [Column("reference6")]
    [StringLength(255)]
    public string? Reference6 { get; set; }

    [Column("budget")]
    [Precision(10, 2)]
    public decimal? Budget { get; set; }
}
