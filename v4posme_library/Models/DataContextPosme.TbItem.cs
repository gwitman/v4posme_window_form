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
    public partial class TbItem {

        public TbItem()
        {
            this.CompanyID = 0;
            this.BranchID = 0;
            this.InventoryCategoryID = 0;
            this.ItemNumber = @"0";
            this.Name = @"0";
            this.Quantity = 0.0000m;
            this.QuantityMax = 0.0000m;
            this.QuantityMin = 0.0000m;
            this.Cost = 0.0000m;
            this.IsInvoiceQuantityZero = 0;
            this.IsServices = 0;
            this.CurrencyID = 1;
            this.IsInvoice = 1;
            this.RealStateWallInCloset = 0;
            this.RealStatePiscinaPrivate = 0;
            this.RealStateClubPiscina = 0;
            this.RealStateAceptanMascota = 0;
            this.RealStateContractCorrentaje = 0;
            this.RealStatePlanReference = 0;
            this.RealStateRoomServices = 0;
            this.RealStateRoomBatchServices = 0;
            this.RealStateRooBatchVisit = 0;
            this.RealStateGerenciaExclusive = 0;
            OnCreated();
        }

        public TbItem(int itemID, int? familyID, string? barCode, string? description, string? unitMeasureID, int? displayID, int? capacity, int? displayUnitMeasureID, int? defaultWarehouseID, string? reference1, string? reference2, string? reference3, int? statusID, bool? isPerishable, decimal? factorBox, decimal? factorProgram, string? createdIn, int? createdAt, int? createdBy, DateTime? createdOn, bool? isActive, string? realStateLinkYoutube, string? realStateLinkPaginaWeb, string? realStateLinkPhontos, string? realStateLinkGoogleMaps, string? realStateLinkOther, string? realStateStyleKitchen, string? realStateReferenceUbicacion, string? realStateReferenceZone, string? realStateReferenceCondominio, int? realStateEmployerAgentID, int? realStateCountryID, int? realStateStateID, int? realStateCityID, DateTime? modifiedOn, string? realStatePhone, string? realStateEmail, DateTime? dateLastUse, decimal? quantityInvoice) : this()        {
            this.ItemID = itemID;
            this.FamilyID = familyID;
            this.BarCode = barCode;
            this.Description = description;
            this.UnitMeasureID = unitMeasureID;
            this.DisplayID = displayID;
            this.Capacity = capacity;
            this.DisplayUnitMeasureID = displayUnitMeasureID;
            this.DefaultWarehouseID = defaultWarehouseID;
            this.Reference1 = reference1;
            this.Reference2 = reference2;
            this.Reference3 = reference3;
            this.StatusID = statusID;
            this.IsPerishable = isPerishable;
            this.FactorBox = factorBox;
            this.FactorProgram = factorProgram;
            this.CreatedIn = createdIn;
            this.CreatedAt = createdAt;
            this.CreatedBy = createdBy;
            this.CreatedOn = createdOn;
            this.IsActive = isActive;
            this.RealStateLinkYoutube = realStateLinkYoutube;
            this.RealStateLinkPaginaWeb = realStateLinkPaginaWeb;
            this.RealStateLinkPhontos = realStateLinkPhontos;
            this.RealStateLinkGoogleMaps = realStateLinkGoogleMaps;
            this.RealStateLinkOther = realStateLinkOther;
            this.RealStateStyleKitchen = realStateStyleKitchen;
            this.RealStateReferenceUbicacion = realStateReferenceUbicacion;
            this.RealStateReferenceZone = realStateReferenceZone;
            this.RealStateReferenceCondominio = realStateReferenceCondominio;
            this.RealStateEmployerAgentID = realStateEmployerAgentID;
            this.RealStateCountryID = realStateCountryID;
            this.RealStateStateID = realStateStateID;
            this.RealStateCityID = realStateCityID;
            this.ModifiedOn = modifiedOn;
            this.RealStatePhone = realStatePhone;
            this.RealStateEmail = realStateEmail;
            this.DateLastUse = dateLastUse;
            this.QuantityInvoice = quantityInvoice;
        }

        [Key]
        [Required()]
        public int ItemID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int BranchID { get; set; }

        [Required()]
        public int InventoryCategoryID { get; set; }

        public int? FamilyID { get; set; }

        [StringLength(250)]
        [Required()]
        public string ItemNumber { get; set; }

        [StringLength(1200)]
        public string? BarCode { get; set; }

        [StringLength(250)]
        [Required()]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? UnitMeasureID { get; set; }

        public int? DisplayID { get; set; }

        public int? Capacity { get; set; }

        public int? DisplayUnitMeasureID { get; set; }

        public int? DefaultWarehouseID { get; set; }

        [Required()]
        public decimal Quantity { get; set; }

        [Required()]
        public decimal QuantityMax { get; set; }

        [Required()]
        public decimal QuantityMin { get; set; }

        [Required()]
        public decimal Cost { get; set; }

        [StringLength(250)]
        public string? Reference1 { get; set; }

        [StringLength(250)]
        public string? Reference2 { get; set; }

        [StringLength(255)]
        public string? Reference3 { get; set; }

        public int? StatusID { get; set; }

        public bool? IsPerishable { get; set; }

        public decimal? FactorBox { get; set; }

        public decimal? FactorProgram { get; set; }

        [StringLength(250)]
        public string? CreatedIn { get; set; }

        public int? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? IsActive { get; set; }

        public sbyte? IsInvoiceQuantityZero { get; set; }

        public sbyte? IsServices { get; set; }

        [Required()]
        public int CurrencyID { get; set; }

        [Required()]
        public int IsInvoice { get; set; }

        public sbyte? RealStateWallInCloset { get; set; }

        public sbyte? RealStatePiscinaPrivate { get; set; }

        public sbyte? RealStateClubPiscina { get; set; }

        public sbyte? RealStateAceptanMascota { get; set; }

        public sbyte? RealStateContractCorrentaje { get; set; }

        public sbyte? RealStatePlanReference { get; set; }

        [StringLength(255)]
        public string? RealStateLinkYoutube { get; set; }

        [StringLength(255)]
        public string? RealStateLinkPaginaWeb { get; set; }

        [StringLength(255)]
        public string? RealStateLinkPhontos { get; set; }

        [StringLength(255)]
        public string? RealStateLinkGoogleMaps { get; set; }

        [StringLength(255)]
        public string? RealStateLinkOther { get; set; }

        [StringLength(255)]
        public string? RealStateStyleKitchen { get; set; }

        public sbyte? RealStateRoomServices { get; set; }

        public sbyte? RealStateRoomBatchServices { get; set; }

        [StringLength(255)]
        public string? RealStateReferenceUbicacion { get; set; }

        [StringLength(255)]
        public string? RealStateReferenceZone { get; set; }

        [StringLength(255)]
        public string? RealStateReferenceCondominio { get; set; }

        public int? RealStateEmployerAgentID { get; set; }

        public int? RealStateCountryID { get; set; }

        public int? RealStateStateID { get; set; }

        public int? RealStateCityID { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public sbyte? RealStateRooBatchVisit { get; set; }

        public int? RealStateGerenciaExclusive { get; set; }

        [StringLength(255)]
        public string? RealStatePhone { get; set; }

        [StringLength(255)]
        public string? RealStateEmail { get; set; }

        public DateTime? DateLastUse { get; set; }

        public decimal? QuantityInvoice { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}