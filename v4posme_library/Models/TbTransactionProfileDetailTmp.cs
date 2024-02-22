using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_profile_detail_tmp")]
[Index("CompanyId", "BranchId", "LoginId", "TransactionId", Name = "IDX_1")]
[Index("CompanyId", "BranchId", "LoginId", "TransactionId", "TransactionMasterId", "TransactionCausalId", Name = "IDX_2")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbTransactionProfileDetailTmp
{
    [Column("companyID", TypeName = "int(11)")]
    public int? CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int? BranchId { get; set; }

    [Column("loginID", TypeName = "int(11)")]
    public int? LoginId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int? TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int? TransactionMasterId { get; set; }

    [Column("transactionCausalID", TypeName = "int(11)")]
    public int? TransactionCausalId { get; set; }

    [Column("conceptID", TypeName = "int(11)")]
    public int? ConceptId { get; set; }

    [Column("accountID", TypeName = "int(11)")]
    public int? AccountId { get; set; }

    [Column("classID", TypeName = "int(11)")]
    public int? ClassId { get; set; }

    [Column("debit")]
    [Precision(26, 8)]
    public decimal? Debit { get; set; }

    [Column("credit")]
    [Precision(26, 8)]
    public decimal? Credit { get; set; }

    [Key]
    [Column("transactionProfileDetailTmpID", TypeName = "int(11)")]
    public int TransactionProfileDetailTmpId { get; set; }
}
