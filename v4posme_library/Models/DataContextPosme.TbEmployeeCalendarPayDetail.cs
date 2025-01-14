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
    public partial class TbEmployeeCalendarPayDetail {

        public TbEmployeeCalendarPayDetail()
        {
            this.CalendarID = 0;
            this.EmployeeID = 0;
            this.PlusSalary = 0.00m;
            this.PlusCommission = 0.00m;
            this.MinusAdelantos = 0.00m;
            this.EqualNeto = 0.00m;
            this.IsActive = false;
            OnCreated();
        }

        public TbEmployeeCalendarPayDetail(int calendarDetailID, decimal? plusBonus, decimal? minusDeductionForLoans, decimal? minusDeductionForLateArrival, decimal? minusInss, decimal? inssPatronal, decimal? minusIr, decimal? saving, string? reference1, string? reference2, string? reference3) : this()        {
            this.CalendarDetailID = calendarDetailID;
            this.PlusBonus = plusBonus;
            this.MinusDeductionForLoans = minusDeductionForLoans;
            this.MinusDeductionForLateArrival = minusDeductionForLateArrival;
            this.MinusInss = minusInss;
            this.InssPatronal = inssPatronal;
            this.MinusIr = minusIr;
            this.Saving = saving;
            this.Reference1 = reference1;
            this.Reference2 = reference2;
            this.Reference3 = reference3;
        }

        [Key]
        [Required()]
        public int CalendarDetailID { get; set; }

        [Required()]
        public int CalendarID { get; set; }

        [Required()]
        public int EmployeeID { get; set; }

        [Required()]
        public decimal PlusSalary { get; set; }

        [Required()]
        public decimal PlusCommission { get; set; }

        public decimal? PlusBonus { get; set; }

        [Required()]
        public decimal MinusAdelantos { get; set; }

        public decimal? MinusDeductionForLoans { get; set; }

        public decimal? MinusDeductionForLateArrival { get; set; }

        public decimal? MinusInss { get; set; }

        public decimal? InssPatronal { get; set; }

        public decimal? MinusIr { get; set; }

        public decimal? Saving { get; set; }

        [Required()]
        public decimal EqualNeto { get; set; }

        [StringLength(255)]
        public string? Reference1 { get; set; }

        [StringLength(255)]
        public string? Reference2 { get; set; }

        [StringLength(255)]
        public string? Reference3 { get; set; }

        [Required()]
        public bool IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
