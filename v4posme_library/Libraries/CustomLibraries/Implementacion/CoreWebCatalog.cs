using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebCatalog : ICoreWebCatalog
{
    private static readonly string? ElementTypeTable = VariablesGlobales.ConfigurationBuilder["ELEMENT_TYPE_TABLE"];

    private readonly ICompanyModel _companyModel;

    private readonly IComponentModel _componentModel;

    private readonly IElementModel _elementModel;

    private readonly ISubElementModel _subElementModel;

    private readonly ICompanyComponentFlavorModel _componentFlavor;

    private readonly ICatalogModel _catalogModel;

    private readonly ICatalogItemModel _catalogItemModel;

    public CoreWebCatalog(ICompanyModel companyModel, IComponentModel componentModel, IElementModel elementModel, ISubElementModel subElementModel, ICompanyComponentFlavorModel componentFlavor, ICatalogModel catalogModel, ICatalogItemModel catalogItemModel)
    {
        _companyModel = companyModel;
        _componentModel = componentModel;
        _elementModel = elementModel;
        _subElementModel = subElementModel;
        _componentFlavor = componentFlavor;
        _catalogModel = catalogModel;
        _catalogItemModel = catalogItemModel;
    }

    public List<TbCatalogItem> GetCatalogAllItem(string table, string field, int companyId)
    {
        var objCompanyModel = _companyModel.GetRowByPk(companyId);
        var objElement = _elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA {table} DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = _subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO {field} DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO {table}");
        }

        var objComponent = _componentModel.GetRowByName("tb_catalog");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE tb_catalog DENTROS DE LOS REGISTROS DE Component ");
        }

        //obtener el catalogo
        if (objSubElement.CatalogId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA {field} NO EXISTE EL CATALOGO CONFIGURADO");
        }

        var objCatalog = _catalogModel.GetRowByCatalogId(Convert.ToInt32(objSubElement.CatalogId));
        if (objCatalog is null)
        {
            throw new Exception("NO EXISTE EL CATALOGO ");
        }

        //obtener flavor
        var objCompanyComponentFlavor = _componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(companyId,
            objComponent.ComponentId, objCatalog.CatalogId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE CATALOGO ");
        }

        var objCatalogItem =
            _catalogItemModel.GetRowByCatalogIdAndFlavorId(objCatalog.CatalogId, objCompanyModel.FlavorId);
        if (objCatalogItem.Count <= 0)
        {
            objCatalogItem = _catalogItemModel.GetRowByCatalogIdAndFlavorId(objCatalog.CatalogId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId));
        }

        return objCatalogItem;
    }

    public List<TbCatalogItem> GetCatalogAllItemByNameCatalogo(string name, int companyId)
    {
        var objCompanyModel = _companyModel.GetRowByPk(companyId);

        var objComponent = _componentModel.GetRowByName("tb_catalog");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_catalog' DENTROS DE LOS REGISTROS DE 'Component'");
        }

        var objCatalog = _catalogModel.GetRowByName(name);
        if (objCatalog is null)
        {
            throw new Exception("NO EXISTE EL CATALOGO ");
        }

        var objCompanyComponentFlavor =
            _componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId,
                objCatalog.CatalogId);
        if (objCatalog is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE CATALOGO ");
        }

        var objCatalogItem =
            _catalogItemModel.GetRowByCatalogIdAndFlavorId(objCatalog.CatalogId, objCompanyModel.FlavorId);
        if (objCatalogItem.Count <= 0)
        {
            objCatalogItem = _catalogItemModel.GetRowByCatalogIdAndFlavorId(objCatalog.CatalogId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId));
        }

        return objCatalogItem;
    }

    public List<TbCatalogItem> GetCatalogAllItemParent(string table, string field, int companyId,
        int parentCatalogItemId)
    {
        var objElement = _elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA {table} DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = _subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO {field} DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO {table} ");
        }

        var objComponent = _componentModel.GetRowByName("tb_catalog");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_catalog' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.CatalogId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL CATALOGO CONFIGURADO");
        }

        var objCatalog = _catalogModel.GetRowByCatalogId(Convert.ToInt32(objSubElement.CatalogId));
        if (objCatalog is null)
        {
            throw new Exception("NO EXISTE EL CATALOGO ");
        }

        var objCompanyComponentFlavor = _componentFlavor
            .GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId, objCatalog.CatalogId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE CATALOGO ");
        }

        return _catalogItemModel.GetRowByCatalogIdAndFlavorIdParent(
            objCatalog.CatalogId, Convert.ToInt32(objCompanyComponentFlavor.FlavorId), parentCatalogItemId);
    }

    public TbCatalogItem GetCatalogItem(string table, string field, int companyId, int catalogItemId)
    {
        var objElement = _elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA {table} DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = _subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO {field} DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO {table} ");
        }

        var objComponent = _componentModel.GetRowByName("tb_catalog");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_catalog' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.CatalogId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL CATALOGO CONFIGURADO");
        }

        var objCatalog = _catalogModel.GetRowByCatalogId(Convert.ToInt32(objSubElement.CatalogId));
        if (objCatalog is null)
        {
            throw new Exception("NO EXISTE EL CATALOGO ");
        }

        var objCompanyComponentFlavor = _componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(
            companyId, objComponent.ComponentId, objCatalog.CatalogId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE CATALOGO ");
        }

        return _catalogItemModel.GetRowByCatalogItemId(catalogItemId);
    }
}