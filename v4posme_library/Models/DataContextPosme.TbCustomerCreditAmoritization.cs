﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2025-06-24 14:25:27
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable enable annotations
#nullable disable warnings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace v4posme_library.Models
{
    public partial class TbCustomerCreditAmoritization {

        public TbCustomerCreditAmoritization()
        {
            this.CustomerCreditDocumentID = 0;
            this.BalanceStart = 0.000000000m;
            this.Interest = 0.000000000m;
            this.Capital = 0.000000000m;
            this.Share = 0.000000000m;
            this.BalanceEnd = 0.000000000m;
            this.Remaining = 0.000000000m;
            this.ShareCapital = 0.000000000m;
            this.DayDelay = 0;
            this.StatusID = 0;
            this.IsActive = false;
            this.Sequence = 0;
            OnCreated();
        }

        public int CreditAmortizationID { get; set; }

        public int CustomerCreditDocumentID { get; set; }

        public DateTime DateApply { get; set; }

        public decimal BalanceStart { get; set; }

        public decimal Interest { get; set; }

        public decimal Capital { get; set; }

        public decimal Share { get; set; }

        public decimal BalanceEnd { get; set; }

        public decimal Remaining { get; set; }

        public decimal ShareCapital { get; set; }

        public int DayDelay { get; set; }

        public string? Note { get; set; }

        public int StatusID { get; set; }

        public bool IsActive { get; set; }

        public int? Sequence { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
