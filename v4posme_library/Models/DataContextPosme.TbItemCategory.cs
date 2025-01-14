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
    public partial class TbItemCategory {

        public TbItemCategory()
        {
            this.CompanyID = 0;
            this.BranchID = 0;
            this.Name = @"0";
            OnCreated();
        }

        public TbItemCategory(int inventoryCategoryID, string? description, bool? isActive, string? createdIn, int? createdBy, DateTime? createdOn, int? createdAt) : this()        {
            this.InventoryCategoryID = inventoryCategoryID;
            this.Description = description;
            this.IsActive = isActive;
            this.CreatedIn = createdIn;
            this.CreatedBy = createdBy;
            this.CreatedOn = createdOn;
            this.CreatedAt = createdAt;
        }

        [Key]
        [Required()]
        public int InventoryCategoryID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int BranchID { get; set; }

        [StringLength(250)]
        [Required()]
        public string Name { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(250)]
        public string? CreatedIn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedAt { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
