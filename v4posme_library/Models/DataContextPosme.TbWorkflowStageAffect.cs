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
    public partial class TbWorkflowStageAffect {

        public TbWorkflowStageAffect()
        {
            OnCreated();
        }

        public int WorkflowStageAffectID { get; set; }

        public string? TransactionID { get; set; }

        public int? FlavorID { get; set; }

        public string? TransactionCausalID { get; set; }

        public string? ComponentSourceID { get; set; }

        public string? WorkflowSourceID { get; set; }

        public string? WorkflowSourceStageID { get; set; }

        public string? ComponentTargetID { get; set; }

        public string? WorkflowTargetID { get; set; }

        public string? WorkflowTargetStageID { get; set; }

        public sbyte? IsActive { get; set; }

        public string? Condition1 { get; set; }

        public string? Condition2 { get; set; }

        public string? Condition3 { get; set; }

        public string? Reference1 { get; set; }

        public string? Reference2 { get; set; }

        public string? Reference3 { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
