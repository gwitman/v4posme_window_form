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
    public partial class TbRelationship {

        public TbRelationship()
        {
            this.EmployeeID = 0;
            this.CustomerID = 0;
            this.IsActive = false;
            OnCreated();
        }

        public int RelationshipID { get; set; }

        public int EmployeeID { get; set; }

        public int CustomerID { get; set; }

        public DateTime StartOn { get; set; }

        public DateTime EndOn { get; set; }

        public bool IsActive { get; set; }

        public int? OrderNo { get; set; }

        public string? Reference1 { get; set; }

        public string? Reference2 { get; set; }

        public string? Reference3 { get; set; }

        public int? CustomerIDAfter { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
