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
    public partial class TbCashBox {

        public TbCashBox()
        {
            this.CompanyID = 0;
            this.BranchID = 0;
            this.CashBoxCode = @"0";
            this.Name = @"0";
            this.StatusID = 0;
            this.IsActive = false;
            OnCreated();
        }

        public TbCashBox(int cashBoxID, string? description) : this()        {
            this.CashBoxID = cashBoxID;
            this.Description = description;
        }

        [Key]
        [Required()]
        public int CashBoxID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int BranchID { get; set; }

        [StringLength(10)]
        [Required()]
        public string CashBoxCode { get; set; }

        [StringLength(50)]
        [Required()]
        public string Name { get; set; }

        [StringLength(550)]
        public string? Description { get; set; }

        [Required()]
        public int StatusID { get; set; }

        [Required()]
        public bool IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}