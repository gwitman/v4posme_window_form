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
    public partial class PrBoxGetReportAbonoResult {

        public PrBoxGetReportAbonoResult()
        {
            OnCreated();
        }

        public string TransactionName { get; set; }

        public string CustomerNumber { get; set; }

        public string? FirstName { get; set; }

        public string TransactionNumber { get; set; }

        public string? TransactionOn { get; set; }

        public decimal? MontoTotal { get; set; }

        public string? Estado { get; set; }

        public string? Note { get; set; }

        public string? Fac { get; set; }

        public decimal? MontoCordoba { get; set; }

        public decimal? MontoFac { get; set; }

        public string Moneda { get; set; }

        public decimal? TipoCambio { get; set; }

        public int? PERMISSIONME { get; set; }

        public int? PrAuthorization { get; set; }

        public int? CreatedBy { get; set; }

        public string? Nickname { get; set; }

        public string ConceptosName { get; set; }

        public string ConceptosSubName { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
