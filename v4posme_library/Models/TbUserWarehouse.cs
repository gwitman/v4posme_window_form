using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_user_warehouse")]
[Index("CompanyId", Name = "IDX_USER_WAREHOUSE_001")]
[Index("BranchId", Name = "IDX_USER_WAREHOUSE_002")]
[Index("UserId", Name = "IDX_USER_WAREHOUSE_003")]
[Index("WarehouseId", Name = "IDX_USER_WAREHOUSE_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbUserWarehouse
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("userID", TypeName = "int(11)")]
    public int UserId { get; set; }

    [Column("warehouseID", TypeName = "int(11)")]
    public int WarehouseId { get; set; }

    [Key]
    [Column("userWarehouseID", TypeName = "int(11)")]
    public int UserWarehouseId { get; set; }
}
