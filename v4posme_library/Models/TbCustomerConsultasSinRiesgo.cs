using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer_consultas_sin_riesgo")]
[Index("CompanyId", Name = "IDX_CUSTOMER_CONSULTA_SIN_RIESGO_001")]
[Index("RequestId", Name = "IDX_CUSTOMER_CONSULTA_SIN_RIESGO_002")]
[Index("Id", Name = "IDX_CUSTOMER_CONSULTA_SIN_RIESGO_003")]
[Index("UserId", Name = "IDX_CUSTOMER_CONSULTA_SIN_RIESGO_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerConsultasSinRiesgo
{
    [Key]
    [Column("requestID")]
    public int RequestId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("id")]
    [StringLength(50)]
    public string Id { get; set; } = null!;

    [Column("file")]
    [StringLength(150)]
    public string File { get; set; } = null!;

    [Column("userID")]
    public int UserId { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("createdIn")]
    [StringLength(50)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdAt")]
    public int CreatedAt { get; set; }

    [Column("modifiedOn", TypeName = "datetime")]
    public DateTime ModifiedOn { get; set; }

    [Column("isPay", TypeName = "bit(1)")]
    public ulong IsPay { get; set; }
}
