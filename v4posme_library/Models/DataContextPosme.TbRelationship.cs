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
    public partial class TbRelationship {

        public TbRelationship()
        {
            this.EmployeeID = 0;
            this.CustomerID = 0;
            this.IsActive = false;
            OnCreated();
        }

        public TbRelationship(int relationshipID, DateTime startOn, DateTime endOn) : this()        {
            this.RelationshipID = relationshipID;
            this.StartOn = startOn;
            this.EndOn = endOn;
        }

        [Key]
        [Required()]
        public int RelationshipID { get; set; }

        [Required()]
        public int EmployeeID { get; set; }

        [Required()]
        public int CustomerID { get; set; }

        [Required()]
        public DateTime StartOn { get; set; }

        [Required()]
        public DateTime EndOn { get; set; }

        [Required()]
        public bool IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}