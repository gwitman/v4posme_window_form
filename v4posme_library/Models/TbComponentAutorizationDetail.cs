﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_component_autorization_detail")]
[Index("CompanyId", Name = "IDX_COMPONENT_AUTORIZATSION_DETAIL_001")]
[Index("ComponentAutorizationId", Name = "IDX_COMPONENT_AUTORIZATSION_DETAIL_002")]
[Index("ComponentId", Name = "IDX_COMPONENT_AUTORIZATSION_DETAIL_003")]
[Index("WorkflowId", Name = "IDX_COMPONENT_AUTORIZATSION_DETAIL_004")]
[Index("WorkflowStageId", Name = "IDX_COMPONENT_AUTORIZATSION_DETAIL_005")]
[Index("CompanyId", "ComponentAutorizationId", Name = "IDX_COMPONENT_AUTORIZATSION_DETAIL_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbComponentAutorizationDetail
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("componentAutorizationID")]
    public int ComponentAutorizationId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("workflowID")]
    public int WorkflowId { get; set; }

    [Column("workflowStageID")]
    public int WorkflowStageId { get; set; }

    [Key]
    [Column("componentAurotizationDetailID")]
    public int ComponentAurotizationDetailId { get; set; }
}
