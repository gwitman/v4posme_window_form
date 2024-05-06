using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_menu_element")]
[Index("CompanyId", Name = "IDX_MENU_ELEMENT_001")]
[Index("ElementId", Name = "IDX_MENU_ELEMENT_002")]
[Index("ParentMenuElementId", Name = "IDX_MENU_ELEMENT_003")]
[Index("TypeMenuElementId", Name = "IDX_MENU_ELEMENT_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbMenuElement
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("elementID")]
    public int ElementId { get; set; }

    [Key]
    [Column("menuElementID")]
    public int MenuElementId { get; set; }

    [Column("parentMenuElementID")]
    public int? ParentMenuElementId { get; set; }

    [Column("display")]
    [StringLength(250)]
    public string? Display { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("orden")]
    [StringLength(40)]
    public string? Orden { get; set; }

    [Column("icon")]
    [StringLength(50)]
    public string Icon { get; set; } = null!;

    [Column("template")]
    [StringLength(250)]
    public string Template { get; set; } = null!;

    [Column("nivel")]
    public int Nivel { get; set; }

    [Column("typeMenuElementID")]
    public int TypeMenuElementId { get; set; }

    [Column("isActive")]
    public sbyte IsActive { get; set; }

    [Column("iconWindowForm")]
    [StringLength(1200)]
    public string? IconWindowForm { get; set; }

    [Column("formRedirectWindowForm")]
    [StringLength(500)]
    public string? FormRedirectWindowForm { get; set; }

    [Column("typeUrlRedirect")]
    [StringLength(255)]
    public string? TypeUrlRedirect { get; set; }
}
