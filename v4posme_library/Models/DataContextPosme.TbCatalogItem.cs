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
    public partial class TbCatalogItem {

        public TbCatalogItem()
        {
            this.CatalogID = 0;
            this.Ratio = 1.00000000m;
            OnCreated();
        }

        public TbCatalogItem(int catalogItemID, string? name, string? display, int? flavorID, string? description, int? sequence, int? parentCatalogID, int? parentCatalogItemID, string? reference1, string? reference2, string? reference3, string? reference4) : this()        {
            this.CatalogItemID = catalogItemID;
            this.Name = name;
            this.Display = display;
            this.FlavorID = flavorID;
            this.Description = description;
            this.Sequence = sequence;
            this.ParentCatalogID = parentCatalogID;
            this.ParentCatalogItemID = parentCatalogItemID;
            this.Reference1 = reference1;
            this.Reference2 = reference2;
            this.Reference3 = reference3;
            this.Reference4 = reference4;
        }

        [Key]
        [Required()]
        public int CatalogItemID { get; set; }

        [Required()]
        public int CatalogID { get; set; }

        [StringLength(250)]
        public string? Name { get; set; }

        [StringLength(250)]
        public string? Display { get; set; }

        public int? FlavorID { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public int? Sequence { get; set; }

        public int? ParentCatalogID { get; set; }

        public int? ParentCatalogItemID { get; set; }

        [Required()]
        public decimal Ratio { get; set; }

        [StringLength(150)]
        public string? Reference1 { get; set; }

        [StringLength(255)]
        public string? Reference2 { get; set; }

        [StringLength(255)]
        public string? Reference3 { get; set; }

        [StringLength(255)]
        public string? Reference4 { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}