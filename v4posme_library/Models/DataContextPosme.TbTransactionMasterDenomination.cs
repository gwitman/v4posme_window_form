﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 26/9/2024 4:44:37 PM
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
    public partial class TbTransactionMasterDenomination {

        public TbTransactionMasterDenomination()
        {
            this.CompanyID = 1;
            this.TransactionID = 1;
            this.TransactionMasterID = 1;
            this.IsActive = 1;
            this.ComponentID = 1;
            this.CatalogItemID = 1;
            this.CurrencyID = 1;
            this.ExchangeRate = 1.00000000m;
            this.Quantity = 1;
            this.Ratio = 1.00000000m;
            OnCreated();
        }

        public TbTransactionMasterDenomination(int transactionMasterDenominationID, string? reference1, string? reference2) : this()        {
            this.TransactionMasterDenominationID = transactionMasterDenominationID;
            this.Reference1 = reference1;
            this.Reference2 = reference2;
        }

        [Key]
        [Required()]
        public int TransactionMasterDenominationID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int TransactionID { get; set; }

        [Required()]
        public int TransactionMasterID { get; set; }

        [Required()]
        public int IsActive { get; set; }

        [Required()]
        public int ComponentID { get; set; }

        [Required()]
        public int CatalogItemID { get; set; }

        [Required()]
        public int CurrencyID { get; set; }

        [Required()]
        public decimal ExchangeRate { get; set; }

        [Required()]
        public int Quantity { get; set; }

        [Required()]
        public decimal Ratio { get; set; }

        [StringLength(150)]
        public string? Reference1 { get; set; }

        [StringLength(150)]
        public string? Reference2 { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}