using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_component_concept")]
[Index("CompanyId", Name = "IDX_COMPANY_COMPONENT_CONCEPT_001")]
[Index("ComponentId", Name = "IDX_COMPANY_COMPONENT_CONCEPT_002")]
[Index("ComponentItemId", Name = "IDX_COMPANY_COMPONENT_CONCEPT_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbCompanyComponentConcept
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("componentItemID", TypeName = "int(11)")]
    public int ComponentItemId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("valueIn")]
    [Precision(18, 8)]
    public decimal? ValueIn { get; set; }

    [Column("valueOut")]
    [Precision(18, 8)]
    public decimal? ValueOut { get; set; }

    [Key]
    [Column("companyComponentConceptID", TypeName = "int(11)")]
    public int CompanyComponentConceptId { get; set; }
}
