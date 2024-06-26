﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_component")]
[Index("ComponentId", Name = "IDX_COMPANY_COMPONENT_001")]
[Index("CompanyId", Name = "IDX_COMPANY_COMPONENT_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbCompanyComponent
{
    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Key]
    [Column("companyComponentID")]
    public int CompanyComponentId { get; set; }
}
