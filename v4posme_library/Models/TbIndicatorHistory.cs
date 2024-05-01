using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_indicator_history")]
[Index("CompanyId", Name = "IDX_INDICATOR_HISTORY_001")]
[Index("IndicatorId", Name = "IDX_INDICATOR_HISTORY_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbIndicatorHistory
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("indicatorID")]
    public int IndicatorId { get; set; }

    [Column("dateOn")]
    public DateOnly DateOn { get; set; }

    [Column("value")]
    [Precision(19, 2)]
    public decimal Value { get; set; }

    [Key]
    [Column("indicatorHistoryID")]
    public int IndicatorHistoryId { get; set; }
}
