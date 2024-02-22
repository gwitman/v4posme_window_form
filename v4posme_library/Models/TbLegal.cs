using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_legal")]
[Index("CompanyId", Name = "IDX_LEGAL_001")]
[Index("BranchId", Name = "IDX_LEGAL_002")]
[Index("EntityId", Name = "IDX_LEGAL_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbLegal
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("comercialName")]
    [StringLength(250)]
    public string? ComercialName { get; set; }

    [Column("legalName")]
    [StringLength(250)]
    public string? LegalName { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Key]
    [Column("legalID", TypeName = "int(11)")]
    public int LegalId { get; set; }
}
