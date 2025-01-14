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
    public partial class TbWorkflowStage {

        public TbWorkflowStage()
        {
            this.ComponentID = 0;
            this.WorkflowID = 0;
            this.IsActive = false;
            this.IsInit = false;
            OnCreated();
        }

        public TbWorkflowStage(int workflowStageID, string? name, string? description, string? display, int? flavorID, bool? editableParcial, bool? editableTotal, bool? eliminable, bool? aplicable, bool? vinculable) : this()        {
            this.WorkflowStageID = workflowStageID;
            this.Name = name;
            this.Description = description;
            this.Display = display;
            this.FlavorID = flavorID;
            this.EditableParcial = editableParcial;
            this.EditableTotal = editableTotal;
            this.Eliminable = eliminable;
            this.Aplicable = aplicable;
            this.Vinculable = vinculable;
        }

        [Key]
        [Required()]
        public int WorkflowStageID { get; set; }

        [Required()]
        public int ComponentID { get; set; }

        [Required()]
        public int WorkflowID { get; set; }

        [StringLength(250)]
        public string? Name { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? Display { get; set; }

        public int? FlavorID { get; set; }

        public bool? EditableParcial { get; set; }

        public bool? EditableTotal { get; set; }

        public bool? Eliminable { get; set; }

        /// <summary>
        /// Este campo es util para saber si el documento debe de aumentar o disminuir inventario o para saver si el documento debe de ser contabilizado
        /// </summary>
        public bool? Aplicable { get; set; }

        public bool? Vinculable { get; set; }

        [Required()]
        public bool IsActive { get; set; }

        [Required()]
        public bool IsInit { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
