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
    public partial class TbCustomerCreditExternalSharonTmp {

        public TbCustomerCreditExternalSharonTmp()
        {
            this.CompanyName = @"0";
            this.DocumentNumber = @"0";
            this.CustomerName = @"0";
            this.CustomerIdentification = @"0";
            this.CustomerPhone = @"0";
            this.AmountAurotize = 0.00m;
            this.Plazo = @"0";
            this.FormPay = @"0";
            this.AmountShare = 0.00m;
            this.AmountBalance = 0.00m;
            this.DayMora = @"0";
            this.Address = @"0";
            this.IsActive = true;
            OnCreated();
        }

        public string CompanyName { get; set; }

        public int CustomerID { get; set; }

        public string DateCredit { get; set; }

        public string DocumentNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerIdentification { get; set; }

        public string CustomerPhone { get; set; }

        public decimal AmountAurotize { get; set; }

        public string Plazo { get; set; }

        public string FormPay { get; set; }

        public decimal AmountShare { get; set; }

        public decimal AmountBalance { get; set; }

        public string DayMora { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
