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
    public partial class TbBankCheque {

        public TbBankCheque()
        {
            OnCreated();
        }

        public int ChequeID { get; set; }

        public string? ChequeNumber { get; set; }

        public string? Name { get; set; }

        public int? StatusID { get; set; }

        public int? BankID { get; set; }

        public int? CurrencyID { get; set; }

        public string? ValueInitial { get; set; }

        public string? ValueCurrent { get; set; }

        public string? ValueFinal { get; set; }

        public string? Serie { get; set; }

        public int? ManagerID { get; set; }

        public sbyte? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
