using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_warehouse")]
[Index("CompanyId", Name = "IDX_WAREHOUSE_001")]
[Index("WarehouseId", Name = "IDX_WAREHOUSE_002")]
[Index("BranchId", Name = "IDX_WAREHOUSE_003")]
[Index("StatusId", Name = "IDX_WAREHOUSE_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbWarehouse
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Key]
    [Column("warehouseID", TypeName = "int(11)")]
    public int WarehouseId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("number")]
    [StringLength(50)]
    public string Number { get; set; } = null!;

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(50)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdAt", TypeName = "int(11)")]
    public int CreatedAt { get; set; }

    [Column("typeWarehouse", TypeName = "int(11)")]
    public int TypeWarehouse { get; set; }

    [Column("emailResponsability")]
    [StringLength(255)]
    public string? EmailResponsability { get; set; }
}
