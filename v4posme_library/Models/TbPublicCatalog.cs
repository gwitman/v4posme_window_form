using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_public_catalog")]
[Index("StatusId", Name = "IDX_PUBLIC_CATALOG_001")]
public class TbPublicCatalog
{
    [Key]
    [Column("publicCatalogID", TypeName = "int(11)")]
    public int PublicCatalogId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("systemName")]
    [StringLength(255)]
    public string? SystemName { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int? StatusId { get; set; }

    [Column("orden", TypeName = "int(11)")]
    public int? Orden { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }

    [Column("flavorID", TypeName = "int(11)")]
    public int? FlavorId { get; set; }
}
