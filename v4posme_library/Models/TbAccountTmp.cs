using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_account_tmp")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbAccountTmp
{
    [Key]
    [Column("accountID", TypeName = "int(11)")]
    public int AccountId { get; set; }

    [Column("accountParentID", TypeName = "int(11)")]
    public int? AccountParentId { get; set; }

    [Column("n1")]
    [StringLength(10)]
    public string? N1 { get; set; }

    [Column("n2")]
    [StringLength(10)]
    public string? N2 { get; set; }

    [Column("n3")]
    [StringLength(10)]
    public string? N3 { get; set; }

    [Column("n4")]
    [StringLength(10)]
    public string? N4 { get; set; }

    [Column("n5")]
    [StringLength(10)]
    public string? N5 { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("nivel", TypeName = "int(11)")]
    public int? Nivel { get; set; }

    [Column("operative", TypeName = "bit(1)")]
    public ulong? Operative { get; set; }

    [Column("balance")]
    [Precision(30, 8)]
    public decimal? Balance { get; set; }
}
