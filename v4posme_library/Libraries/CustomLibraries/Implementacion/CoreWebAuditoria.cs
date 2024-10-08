﻿using System.Net;
using System.Reflection;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebAuditoria : ICoreWebAuditoria
{
    private readonly IElementModel _elementModel;

    private readonly IComponentAuditDetailModel _componentAuditDetailModel;

    private readonly ICompanySubElementAuditModel _companySubElementAuditModel;

    private readonly IComponentAuditModel _componentAuditModel;

    public CoreWebAuditoria(IElementModel elementModel, IComponentAuditDetailModel componentAuditDetailModel, ICompanySubElementAuditModel companySubElementAuditModel, IComponentAuditModel componentAuditModel)
    {
        _elementModel = elementModel;
        _componentAuditDetailModel = componentAuditDetailModel;
        _companySubElementAuditModel = companySubElementAuditModel;
        _componentAuditModel = componentAuditModel;
    }


    public void SetAuditCreated<T>(T obj, TbUser dataUser, string? request)
    {
        var ipAddresses = Dns.GetHostAddresses(Dns.GetHostName()).First();
        if (obj == null) return;
        var properties = obj.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property is { Name: "CreatedOn", CanWrite: true })
            {
                property.SetValue(obj, DateTime.Now);
            }

            if (property is { Name: "CreatedBy", CanWrite: true })
            {
                if (property.PropertyType == typeof(long?) || property.PropertyType == typeof(long))
                {
                    property.SetValue(obj, (long?)dataUser.UserID);
                }
                else
                {
                    property.SetValue(obj, dataUser.UserID);
                }
            }

            if (property is { Name: "CreatedIn", CanWrite: true })
            {
                property.SetValue(obj, ipAddresses.ToString());
            }

            if (property is { Name: "CreatedAt", CanWrite: true })
            {
                property.SetValue(obj, dataUser.BranchID);
            }
        }
    }

    public void SetAuditCreatedAdmin<T>(T obj, string? request)
    {
        var appUserAdmin = VariablesGlobales.ConfigurationBuilder["APP_USERADMIN"];
        var appBranch = VariablesGlobales.ConfigurationBuilder["APP_BRANCH"];
        var ipAddresses = Dns.GetHostAddresses(Dns.GetHostName()).First();
        if (obj == null) return;
        var properties = obj.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property is { Name: "createdOn", CanWrite: true })
            {
                property.SetValue(obj, DateTime.Now);
            }

            if (property is { Name: "createdBy", CanWrite: true })
            {
                property.SetValue(obj, appUserAdmin);
            }

            if (property is { Name: "createdIn", CanWrite: true })
            {
                property.SetValue(obj, ipAddresses.ToString());
            }

            if (property is { Name: "createdAt", CanWrite: true })
            {
                property.SetValue(obj, appBranch);
            }
        }
    }

    public List<TbComponentAuditDetailDto> GetAuditDetail(int companyId, int id, string? tableName)
    {
        var elementTypeTable = VariablesGlobales.ConfigurationBuilder["ELEMENT_TYPE_TABLE"];
        var objElement = _elementModel.GetRowByName(tableName, Convert.ToInt32(elementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA '{tableName}' DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        return _componentAuditDetailModel.GetAuditDetail(companyId, id, objElement.ElementID);
    }

    public void SetAudit(string? tableName, object oldObject, object newObject, TbUser user, string? request)
    {
        var elementTypeTable = VariablesGlobales.ConfigurationBuilder["ELEMENT_TYPE_TABLE"];
        var ipAddresses = Dns.GetHostAddresses(Dns.GetHostName()).First();
        // Obtener Elemento
        var objElement = _elementModel.GetRowByName(tableName, Convert.ToInt32(elementTypeTable));
        if (objElement == null)
            throw new Exception("NO EXISTE LA TABLA '" + tableName + "' DENTRO DE LOS REGISTROS DE ELEMENT ");

        // Obtener subElementos Auditables
        var listSubElementAuditables =
            _companySubElementAuditModel.ListElementAudit(user.CompanyID, objElement.ElementID);
        if (listSubElementAuditables is null)
            return;

        if (listSubElementAuditables.Count == 0)
            return;


        if (oldObject.GetType() != newObject.GetType())
            throw new Exception("LOS OBJET. EN LA AUDITORIA NO SON DE IGUAL TIPO");
        var columnAutoIncrement = objElement.ColumnAutoIncrement;
        if (columnAutoIncrement == null)
            throw new Exception("LA TABLA NO TIENE UNA COLUMNA AUTO IDENTIFICADORA");
        var elementSalvar = new Dictionary<int, Dictionary<string?, object?>>();
        var aux = 0;
        foreach (var auditable in listSubElementAuditables)
        {
            var fieldName = auditable.Name!;
            var fieldId = auditable.SubElementId;
            var propertyInfoOfOldObject = oldObject.GetType().GetProperty(fieldName);
            var propertyInfoOfNewObject = newObject.GetType().GetProperty(fieldName);
            var fieldValueOld = propertyInfoOfOldObject!.GetValue(oldObject)!;
            var fieldValueNew = propertyInfoOfNewObject!.GetValue(newObject)!;
            if (fieldValueOld == fieldValueNew)
            {
                continue;
            }

            elementSalvar[aux] = new Dictionary<string?, object?>
            {
                { "subelementid", fieldId },
                { "oldvalue", fieldValueOld },
                { "newvalue", fieldValueNew }
            };
            aux++;
        }

        var elementItemId = oldObject.GetType().GetProperty(columnAutoIncrement.ToString()!)!.GetValue(oldObject);
        var data = new TbComponentAudit
        {
            CompanyID = user.CompanyID,
            BranchID = user.BranchID,
            ElementID = objElement.ElementID,
            ElementItemID = elementItemId as int?,
            ModifiedOn = DateTime.Now,
            ModifiedAt = user.BranchID,
            ModifiedIn = ipAddresses.ToString(),
            ModifiedBy = user.UserID
        };
        var componentAuditId = _componentAuditModel.InsertAppPosme(data);
        foreach (var (key, values) in elementSalvar)
        {
            var subElementId = 0;
            object? oldValue = null;
            object? newValue = null;
            foreach (var (fieldName, value) in values)
            {
                switch (fieldName)
                {
                    case "subelementid":
                        subElementId = (int)value!;
                        break;
                    case "oldvalue":
                        oldValue = value;
                        break;
                    case "newvalue":
                        newValue = value;
                        break;
                }
            }

            var componentAUditDetail = new TbComponentAuditDetail
            {
                CompanyID = data.CompanyID,
                BranchID = data.BranchID,
                ComponentAuditID = componentAuditId,
                FieldID = subElementId,
                OldValue = oldValue!.ToString(),
                NewValue = newValue!.ToString()
            };
            _componentAuditDetailModel.InsertAppPosme(componentAUditDetail);
        }
    }
}