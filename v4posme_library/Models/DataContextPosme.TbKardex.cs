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
    public partial class TbKardex {

        public TbKardex()
        {
            this.ItemID = 0;
            this.CompanyID = 0;
            this.BranchID = 0;
            this.WarehouseID = 0;
            this.Sign = 0;
            this.OldQuantity = 0.0000m;
            this.OldCost = 0.0000m;
            this.TransactionQuantity = 0.0000m;
            this.TransactionCost = 0.0000m;
            this.NewQuantity = 0.0000m;
            this.NewCost = 0.0000m;
            this.QuantityInWarehouseCurrent = 0.0000m;
            this.QuantityInCurrent = 0.0000m;
            OnCreated();
        }

        public int ItemID { get; set; }

        public int CompanyID { get; set; }

        public int BranchID { get; set; }

        public int WarehouseID { get; set; }

        public int KardexID { get; set; }

        public int? KardexCode { get; set; }

        public DateTime? KardexDate { get; set; }

        public int Sign { get; set; }

        public int? TransactionID { get; set; }

        public int? TransactionMasterID { get; set; }

        public int? TransactionDetailID { get; set; }

        public DateTime MovementOn { get; set; }

        public decimal OldQuantity { get; set; }

        public decimal? OldQuantityWarehouse { get; set; }

        public decimal OldCost { get; set; }

        public decimal? OldCostWarehouse { get; set; }

        public decimal TransactionQuantity { get; set; }

        public decimal TransactionCost { get; set; }

        public decimal NewQuantity { get; set; }

        public decimal? NewQuantityWarehouse { get; set; }

        public decimal NewCost { get; set; }

        public decimal? NewCostWarehouse { get; set; }

        public decimal QuantityInWarehouseCurrent { get; set; }

        public decimal QuantityInCurrent { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
