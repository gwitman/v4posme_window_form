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
    public partial class PrCollectionGetReportSummaryCreditResult {

        public PrCollectionGetReportSummaryCreditResult()
        {
            OnCreated();
        }

        public string? Nickname { get; set; }

        public int CountCustomer { get; set; }

        public int CountCredit { get; set; }

        public int CountCustomerAcumulados { get; set; }

        public int CountCustomerCancel { get; set; }

        public int CountCustomerNew { get; set; }

        public int CountCustomerRecuperation { get; set; }

        public int AmountCartera { get; set; }

        public int AmountCapital { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
