﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 3/1/2025 3:48:59 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable enable annotations
#nullable disable warnings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace v4posme_library.Models
{
    public partial class TbTransactionProfileDetailTmp {

        public TbTransactionProfileDetailTmp()
        {
            OnCreated();
        }

        public TbTransactionProfileDetailTmp(int transactionProfileDetailTmpID, int? companyID, int? branchID, int? loginID, int? transactionID, int? transactionMasterID, int? transactionCausalID, int? conceptID, int? accountID, int? classID, decimal? debit, decimal? credit) : this()        {
            this.TransactionProfileDetailTmpID = transactionProfileDetailTmpID;
            this.CompanyID = companyID;
            this.BranchID = branchID;
            this.LoginID = loginID;
            this.TransactionID = transactionID;
            this.TransactionMasterID = transactionMasterID;
            this.TransactionCausalID = transactionCausalID;
            this.ConceptID = conceptID;
            this.AccountID = accountID;
            this.ClassID = classID;
            this.Debit = debit;
            this.Credit = credit;
        }

        [Key]
        [Required()]
        public int TransactionProfileDetailTmpID { get; set; }

        public int? CompanyID { get; set; }

        public int? BranchID { get; set; }

        public int? LoginID { get; set; }

        public int? TransactionID { get; set; }

        public int? TransactionMasterID { get; set; }

        public int? TransactionCausalID { get; set; }

        public int? ConceptID { get; set; }

        public int? AccountID { get; set; }

        public int? ClassID { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
