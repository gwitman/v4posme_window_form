﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer_credit_amoritization")]
[Index("CustomerCreditDocumentId", Name = "IDX_CUSTOMER_CREDIT_AMORITIZATION_001")]
[Index("DateApply", Name = "IDX_CUSTOMER_CREDIT_AMORITIZATION_002")]
[Index("StatusId", Name = "IDX_CUSTOMER_CREDIT_AMORITIZATION_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerCreditAmortization
{
    [Key]
    [Column("creditAmortizationID")]
    public int CreditAmortizationId { get; set; }

    [Column("customerCreditDocumentID")]
    public int CustomerCreditDocumentId { get; set; }

    [Column("dateApply")]
    public DateOnly? DateApply { get; set; }

    [Column("balanceStart")]
    [Precision(19, 9)]
    public decimal BalanceStart { get; set; }

    [Column("interest")]
    [Precision(19, 9)]
    public decimal Interest { get; set; }

    [Column("capital")]
    [Precision(19, 9)]
    public decimal Capital { get; set; }

    [Column("share")]
    [Precision(19, 9)]
    public decimal Share { get; set; }

    [Column("balanceEnd")]
    [Precision(19, 9)]
    public decimal BalanceEnd { get; set; }

    [Column("remaining")]
    [Precision(19, 9)]
    public decimal Remaining { get; set; }

    [Column("shareCapital")]
    [Precision(19, 9)]
    public decimal ShareCapital { get; set; }

    [Column("dayDelay")]
    public int DayDelay { get; set; }

    [Column("note")]
    [StringLength(350)]
    public string? Note { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
