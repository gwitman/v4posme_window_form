using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer_credit")]
[Index("CompanyId", Name = "IDX_CUSTOMER_CREIDT_001")]
[Index("BranchId", Name = "IDX_CUSTOMER_CREIDT_002")]
[Index("EntityId", Name = "IDX_CUSTOMER_CREIDT_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerCredit
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("limitCreditDol")]
    [Precision(19, 9)]
    public decimal LimitCreditDol { get; set; }

    [Column("balanceDol")]
    [Precision(19, 9)]
    public decimal BalanceDol { get; set; }

    [Column("incomeDol")]
    [Precision(19, 9)]
    public decimal IncomeDol { get; set; }

    [Key]
    [Column("customerCreditID", TypeName = "int(11)")]
    public int CustomerCreditId { get; set; }
}
