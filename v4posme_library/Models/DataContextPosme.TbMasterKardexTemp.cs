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
    public partial class TbMasterKardexTemp {

        public TbMasterKardexTemp()
        {
            this.UserID = 0;
            this.CompanyID = 0;
            this.ItemID = 0;
            this.ItemNumber = @"0";
            this.ItemName = @"0";
            this.MinKardexID = 0;
            this.QuantityInicial = 0.000000000m;
            this.CostInicial = 0.000000000m;
            this.QuantityInput = 0.000000000m;
            this.CostInput = 0.000000000m;
            this.QuantityOutput = 0.000000000m;
            this.CostOutput = 0.000000000m;
            OnCreated();
        }

        public TbMasterKardexTemp(int masterKardexTempID, string? tokenID, string? itemCategoryName) : this()        {
            this.MasterKardexTempID = masterKardexTempID;
            this.TokenID = tokenID;
            this.ItemCategoryName = itemCategoryName;
        }

        [Key]
        [Required()]
        public int MasterKardexTempID { get; set; }

        [Required()]
        public int UserID { get; set; }

        [StringLength(50)]
        public string? TokenID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int ItemID { get; set; }

        [StringLength(50)]
        [Required()]
        public string ItemNumber { get; set; }

        [StringLength(50)]
        [Required()]
        public string ItemName { get; set; }

        [Required()]
        public int MinKardexID { get; set; }

        [Required()]
        public decimal QuantityInicial { get; set; }

        [Required()]
        public decimal CostInicial { get; set; }

        [Required()]
        public decimal QuantityInput { get; set; }

        [Required()]
        public decimal CostInput { get; set; }

        [Required()]
        public decimal QuantityOutput { get; set; }

        [Required()]
        public decimal CostOutput { get; set; }

        [StringLength(50)]
        public string? ItemCategoryName { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}