﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.ComponentModel;
using System.IO;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Dto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using ComboBoxItem = v4posme_window.Libraries.ComboBoxItem;
using CommandLine;

namespace v4posme_window.Views
{
    public partial class FormInventoryItemEdit : FormTypeHeadEdit, IFormTypeEdit
    {
        #region Campos

        private TypeRender _typeRender;
        private int _itemId;
        private BackgroundWorker _backgroundWorker;
        private int _txtEmployerId = 0;
        private bool _findValueWarehouse;
        private bool _findValueSku;
        private string? _warehouseDefault;
        private int _objParameterTypePreiceDefault;
        private RenderFileGridControl _renderGridFiles;

        #endregion

        #region Modelos

        public List<TbCatalogItem> ObjListTypePrice { get; set; }
        public List<TbCatalogItem> ObjListDisplayGerenciaExcl { get; set; }
        public List<TbCatalogItem> ObjListDisplayUnitMeasure { get; set; }
        public List<TbCatalogItem> ObjListCity { get; set; }
        public List<TbCatalogItem> ObjListState { get; set; }
        public List<TbCatalogItem> ObjListCountry { get; set; }
        public TbComponent? ObjComponentItem { get; private set; }
        public TbComponent? ObjComponentEmployer { get; set; }
        public TbComponent? ObjComponentProvider { get; set; }
        public TbItem? ObjItem { get; private set; }
        public List<TbWorkflowStage> ObjListWorkflowStage { get; private set; }
        public List<TbCatalogItem> ObjListFamily { get; private set; }
        public List<TbItemCategory> ObjListInventoryCategory { get; private set; }
        public List<TbCatalogItem> ObjListUnitMeasure { get; private set; }
        public List<TbCatalogItem> ObjListDisplay { get; private set; }
        public List<TbWarehouse> ObjListWarehouse { get; private set; }
        public List<TbCompanyCurrencyDto> ObjListCurrency { get; private set; }
        public TbEmployeeDto? ObjEmployer { get; set; }
        public TbNaturale ObjEmployerNatural { get; set; }
        public TbLegal? ObjEmployerLegal { get; set; }
        public BindingList<TbProviderItemDto> ObjListProvider { get; set; }
        public BindingList<FormInventoryItemEditWarehouseDTO> WarehouseDtoBindingList { get; set; }
        public BindingList<FormInventoryItemEditConceptDTO> ObjListConcept { get; set; }
        public BindingList<TbPriceDto> ObjListPriceItem { get; set; }
        public BindingList<TbItemSkuDto> ObjItemSku { get; set; }
        public decimal ObjListPriceItemFirst { get; set; }
        public string? ObjParameterMasive { get; set; }

        #endregion

        #region Librerias

        private readonly ICoreWebAuditoria _objInterfazCoreWebAuditoria = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>();
        private readonly ICoreWebCatalog _objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
        private readonly ICoreWebParameter _objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        private readonly ICoreWebAccounting _objInterfazCoreWebAccounting = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAccounting>();
        private readonly ICoreWebTransaction _objInterfazCoreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
        private readonly ICoreWebCurrency _objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();
        private readonly ICoreWebTools _objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebCounter _objInterfazCoreWebCounter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCounter>();
        private readonly ICoreWebWorkflow _objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
        private readonly CoreWebRenderInView _objInterfazCoreWebRenderInView = new CoreWebRenderInView();
        private readonly ICoreWebPermission _objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly IItemCategoryModel _itemCategoryModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemCategoryModel>();
        private readonly IItemModel _objItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemModel>();
        private readonly IWarehouseModel _warehouseModel = VariablesGlobales.Instance.UnityContainer.Resolve<IWarehouseModel>();
        private readonly IItemWarehouseModel _itemWarehouseModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemWarehouseModel>();
        private readonly ICompanyCurrencyModel _companyCurrencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyCurrencyModel>();
        private readonly ICompanyComponentConceptModel _companyComponentConceptModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyComponentConceptModel>();
        private readonly IEmployeeModel _employeeModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEmployeeModel>();
        private readonly ILegalModel _legalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ILegalModel>();
        private readonly INaturalModel _naturalModel = VariablesGlobales.Instance.UnityContainer.Resolve<INaturalModel>();
        private readonly IProviderItemModel _priProviderItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IProviderItemModel>();
        private readonly IPriceModel _priceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPriceModel>();
        private readonly IItemSkuModel _itemSkuModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemSkuModel>();

        #endregion

        #region Init

        public FormInventoryItemEdit()
        {
            InitializeComponent();
        }

        public FormInventoryItemEdit(TypeRender typeRender, int itemId)
        {
            InitializeComponent();
            _typeRender = typeRender;
            _itemId = itemId;
            MdiParent = CoreFormList.Principal();
            btnRegresar.Click += CommandRegresar;
            btnGuardar.Click += CommandSave;
            btnEliminar.Click += (s, e) => ComandDelete();
            btnNuevo.Click += CommandNew;
        }
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }
        private void FormInventoryItemEdit_Load(object sender, EventArgs e)
        {
            _backgroundWorker = new BackgroundWorker();
            if (!progressPanel.Visible)
            {
                progressPanel.Width = Width;
                progressPanel.Height = Height;
                progressPanel.Visible = true;
            }

            _backgroundWorker.DoWork += (ob, ev) =>
            {
                switch (_typeRender)
                {
                    case TypeRender.Edit:
                        LoadEdit();
                        break;
                    case TypeRender.New:
                        LoadNew();
                        break;
                }
            };

            _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                if (ev.Error is not null)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", ev.Error.Message, this);
                }
                else if (ev.Cancelled)
                {
                    //se canceló por el usuario
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Error", "Operación cancelada por el usuario", this);
                }
                else
                {
                    PreRender();
                    LoadRender(_typeRender);

                    if (progressPanel.Visible)
                    {
                        progressPanel.Visible = false;
                    }
                }
            };

            if (!progressPanel.Visible)
            {
                progressPanel.Size = Size;
                progressPanel.Visible = true;
            }

            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        #endregion
        
        #region Metodos
        public void ComandDelete()
        {
            var result = _objInterfazCoreWebRenderInView.XtraMessageBoxArgs(TypeError.Error, "Eliminar", "¿Seguro desea eliminar el articulo seleccionado? Esta acción no se puede revertir.");

            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
                var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
                var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
                var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
                var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
                var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
                var user = VariablesGlobales.Instance.User;
                var pathFileApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
                if (user is null)
                {
                    throw new Exception(userNotAutenticated);
                }

                var role = VariablesGlobales.Instance.Role;
                if (role is null)
                {
                    throw new Exception("ROL NO VALIDO");
                }

                if (ObjItem is null)
                {
                    throw new Exception("ITEM NO VALIDO");
                }

                int resultPermission = 0;
                if (appNeedAuthentication == "true")
                {
                    var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_item", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (!permited)
                    {
                        throw new Exception(notAccessControl);
                    }

                    resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_item", "delete", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (resultPermission == permissionNone)
                    {
                        throw new Exception(notAllEdit);
                    }
                }

                var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
                if (resultPermission == permissionMe && ObjItem.CreatedBy != user.UserID)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
                }

                var command = VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"];
                var commandEditableTotal = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_item", "statusID", ObjItem.StatusID!.Value, Convert.ToInt32(command), user.CompanyID, user.BranchID, role.RoleID);
                if (!commandEditableTotal!.Value)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
                }

                ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
                if (ObjComponentItem is null)
                {
                    throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
                }

                //VALIDAR CANTIDAD
                if (VariablesGlobales.Instance.Company is not null && VariablesGlobales.Instance.Company.Type != "luciaralstate")
                {
                    if (ObjItem.Quantity > 0)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Eliminar", "EL REGISTRO NO PUEDE SER ELIMINADO, SU CANTIDAD ES MAYOR QUE  0", this);
                        return;
                    }
                }

                _objItemModel.DeleteAppPosme(user.CompanyID, _itemId);
                XtraMessageBox.Show("Se ha eliminado el articulo de forma correcta", "Eliminar");
                Close();
            }
            catch (Exception exception)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"No fue posible eliminar el articulo, debido al siguiente error: {exception.Message}", this);
            }
        }

        public void ComandPrinter()
        {
        }

        public void LoadEdit()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception(userNotAutenticated);
            }

            var role = VariablesGlobales.Instance.Role;
            if (appNeedAuthentication == "true")
            {
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_item", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_item", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            ObjComponentEmployer = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_employee");
            if (ObjComponentEmployer is null)
            {
                throw new Exception("EL COMPONENT 'tb_employee' NO EXISTE...");
            }

            ObjComponentProvider = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_provider");
            if (ObjComponentProvider is null)
            {
                throw new Exception("EL COMPONENTE 'tb_provider' NO EXISTE...");
            }

            var objParameterListPreiceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyID)!.Value;

            ObjItem = _objItemModel.GetRowByPk(user.CompanyID, _itemId);
            if (ObjItem is null)
            {
                throw new Exception("EL ITEM NO EXISTE...");
            }

            ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_item", "statusID", ObjItem.StatusID!.Value, user.CompanyID, user.BranchID, role.RoleID);
            ObjListInventoryCategory = _itemCategoryModel.GetByCompany(user.CompanyID);
            ObjListFamily = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "familyID", user.CompanyID);
            ObjListUnitMeasure = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "unitMeasureID", user.CompanyID);
            ObjListDisplay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "displayID", user.CompanyID);
            ObjListWarehouse = _warehouseModel.GetByCompany(user.CompanyID);
            ObjListCurrency = _companyCurrencyModel.GetByCompany(user.CompanyID);
            var listaBodegas = _itemWarehouseModel.GetRowByItemId(ObjItem.CompanyID, ObjItem.ItemID);
            FnWarehouseBindingList(listaBodegas);

            var listProvider = _priProviderItemModel.GetRowByItemId(ObjItem.CompanyID, ObjItem.ItemID);
            FnProviderBindingList(listProvider);

            var listConcept = _companyComponentConceptModel.GetRowByComponentItemId(user.CompanyID, ObjComponentItem.ComponentID, ObjItem.ItemID);
            FnConceptBindingList(listConcept);

            var pricesItem = _priceModel.GetRowByItemId(user.CompanyID, Convert.ToInt32(objParameterListPreiceDefault), ObjItem.ItemID);
            FnPriceItemBindingList(pricesItem);

            var skuLista = _itemSkuModel.GetRowByItemId(ObjItem.ItemID);
            FnItemSkuBindingList(skuLista);

            //Obtener colaborador
            ObjEmployer = _employeeModel.GetRowByEntityId(user.CompanyID, ObjItem.RealStateEmployerAgentID!.Value);
            if (ObjEmployer is not null)
            {
                ObjEmployerNatural = _naturalModel.GetRowByPk(ObjItem.CompanyID, ObjItem.BranchID, ObjEmployer.EntityId);
                ObjEmployerLegal = _legalModel.GetRowByPk(ObjItem.CompanyID, ObjItem.BranchID, ObjEmployer.EntityId);
            }

            ObjParameterMasive = _objInterfazCoreWebParameter.GetParameter("ITEM_PRINTER_BARCODE_MASIVE", user.CompanyID)!.Value;
            ObjListCountry = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "realStateCountryID", user.CompanyID);
            ObjListState = _objInterfazCoreWebCatalog.GetCatalogAllItemParent("tb_item", "realStateStateID", user.CompanyID, ObjItem.RealStateCountryID!.Value);
            ObjListCity = _objInterfazCoreWebCatalog.GetCatalogAllItemParent("tb_item", "realStateCityID", user.CompanyID, ObjItem.RealStateStateID!.Value);
        }

        public void LoadNew()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception(userNotAutenticated);
            }

            var role = VariablesGlobales.Instance.Role;
            if (appNeedAuthentication == "true")
            {
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_item", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_item", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            _warehouseDefault = _objInterfazCoreWebParameter.GetParameter("INVENTORY_ITEM_WAREHOUSE_DEFAULT", user.CompanyID)!.Value;
            _objParameterTypePreiceDefault = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyID)!.Value);
            var objParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyID)!.Value;

            ObjComponentEmployer = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_employee");
            if (ObjComponentEmployer is null)
            {
                throw new Exception("EL COMPONENTE 'tb_employee' NO EXISTE..");
            }

            ObjListCountry = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "realStateCountryID", user.CompanyID);
            var objParameterListPreiceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyID);
            ObjListWarehouse = _warehouseModel.GetByCompany(user.CompanyID);
            ObjListInventoryCategory = _itemCategoryModel.GetByCompany(user.CompanyID);
            ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_item", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            ObjListFamily = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "familyID", user.CompanyID);
            ObjListUnitMeasure = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "unitMeasureID", user.CompanyID);
            ObjListDisplay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "displayID", user.CompanyID);
            ObjListDisplayUnitMeasure = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "displayUnitMeasureID", user.CompanyID);
            ObjListDisplayGerenciaExcl = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "realStateGerenciaExclusive", user.CompanyID);
            ObjListTypePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyID);
            ObjListCurrency = _companyCurrencyModel.GetByCompany(user.CompanyID);
            var warehouseDefault = ObjListWarehouse.SingleOrDefault(warehouse => warehouse.Number == _warehouseDefault);
            List<TbItemWarehouseDto> listaBodegas = [];
            if (warehouseDefault is not null)
            {
                listaBodegas.Add(
                    new TbItemWarehouseDto
                    {
                        Number = warehouseDefault.Number,
                        WarehouseId = warehouseDefault.WarehouseID,
                        WarehouseName = warehouseDefault.Name,
                        Quantity = 0,
                        QuantityMax = 1000,
                        QuantityMin = decimal.Zero,
                        BranchId = warehouseDefault.BranchID,
                        CompanyId = warehouseDefault.CompanyID,
                    }
                );
            }

            FnWarehouseBindingList(listaBodegas);

            if (ObjListTypePrice.Count > 0)
            {
                var pricesItem = ObjListTypePrice.Select(tbCatalogItem => new TbPriceDto
                    {
                        CompanyId = user.CompanyID,
                        TypePriceId = tbCatalogItem.CatalogItemID,
                        ListPriceId = Convert.ToInt32(objParameterListPreiceDefault!.Value),
                        NameTypePrice = tbCatalogItem.Name,
                        Price = decimal.Zero,
                        PercentageCommision = decimal.Zero
                    })
                    .ToList();

                FnPriceItemBindingList(pricesItem);
            }
            else
            {
                ObjListPriceItem = new BindingList<TbPriceDto>();
                gridControlPrecios.DataSource = ObjListPriceItem;
            }


            if (ObjListUnitMeasure.Count > 0)
            {
                var skuLista = new List<TbItemSkuDto>();
                var tbCatalogItem = ObjListUnitMeasure.First();
                skuLista.Add(new TbItemSkuDto
                {
                    Value = decimal.One,
                    ItemId = 0,
                    CatalogItemId = tbCatalogItem.CatalogItemID,
                    SkuId = 0,
                    Sku = tbCatalogItem.Name
                });
                FnItemSkuBindingList(skuLista);
            }
            else
            {
                ObjItemSku = new BindingList<TbItemSkuDto>();
                gridControlSku.DataSource = ObjItemSku;
            }
        }

        public void SaveInsert()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception(userNotAutenticated);
            }

            var role = VariablesGlobales.Instance.Role;
            if (appNeedAuthentication == "true")
            {
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_item", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_item", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            _objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_inventory_item");
            var paisDefault = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameterValue("CXC_PAIS_DEFAULT", user.CompanyID));
            var departamentoDefault = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameterValue("CXC_DEPARTAMENTO_DEFAULT", user.CompanyID));
            var municipioDefault = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameterValue("CXC_MUNICIPIO_DEFAULT", user.CompanyID));
            var validateBarCode = _objInterfazCoreWebParameter.GetParameterValue("INVENTORY_BAR_CODE_UNIQUE", user.CompanyID);
            var realStateGerenciaExclusive = _objInterfazCoreWebParameter.GetParameterValue("ITEM_REAL_STATE_GERENCIA_EXCLUSIVE", user.CompanyID);
            ObjItem = new TbItem
            {
                CompanyID = user.CompanyID,
                BranchID = user.CompanyID,
                QuantityInvoice = decimal.Zero,
                DateLastUse = DateTime.Now
            };
            if (txtInventoryCategoryID.SelectedItem is ComboBoxItem inventoryCategoryId)
            {
                ObjItem.InventoryCategoryID = Convert.ToInt32(inventoryCategoryId.Key);
            }

            var nameProducto = txtName.Text;
            nameProducto = nameProducto.Replace("\\", "").Replace("'", "");
            nameProducto = nameProducto.Trim();
            nameProducto = nameProducto.Replace("\"", "");
            if (txtFamilyID.SelectedItem is ComboBoxItem familyId)
            {
                ObjItem.FamilyID = Convert.ToInt32(familyId.Key);
            }

            ObjItem.ItemNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_item", 0)!;
            ObjItem.BarCode = string.IsNullOrWhiteSpace(txtBarCode.Text) ? $"B{ObjItem.ItemNumber}" : txtBarCode.Text;
            ObjItem.BarCode = ObjItem.BarCode.Trim().Replace(Environment.NewLine, ",").Replace(",,", ",").Replace("\n\r", "").Replace("\n", "").Replace("\r", "");
            var objItemValidBarCode = _objItemModel.GetRowByCodeBarra(user.CompanyID, ObjItem.BarCode);
            if (!string.IsNullOrEmpty(validateBarCode) && validateBarCode.ToUpper().Equals("true".ToUpper()))
            {
                if (objItemValidBarCode is not null)
                {
                    throw new Exception("Codigo de barra ya existe");
                }

                var objItemValidBarCodeList = _objItemModel.GetRowByCodeBarraSimilar(user.CompanyID, ObjItem.BarCode);
                foreach (var tbItem in objItemValidBarCodeList)
                {
                    var codeTemp = tbItem.BarCode!.Split(",");
                    foreach (var arrayCode in codeTemp)
                    {
                        if (arrayCode.Equals(ObjItem.BarCode))
                        {
                            throw new Exception("Codigo de barra ya existe");
                        }
                    }
                }
            }

            ObjItem.Name = nameProducto;
            if (txtDescription.InvokeRequired)
            {
                Invoke(() => ObjItem.Description = txtDescription.Text);
            }
            else
            {
                ObjItem.Description = txtDescription.Text;
            }

            var unitMeasureId = txtUnitMeasureID.SelectedItem as ComboBoxItem;
            ObjItem.UnitMeasureID = unitMeasureId is null ? "" : unitMeasureId.Key;
            var displayId = txtDisplayID.SelectedItem as ComboBoxItem;
            ObjItem.DisplayID = displayId is null ? 0 : Convert.ToInt32(displayId.Key);
            ObjItem.Capacity = string.IsNullOrWhiteSpace(txtCapacity.Text) ? 0 : Convert.ToInt32(double.Parse(txtCapacity.Text));
            var displayUnitMeasureId = txtDisplayUnitMeasureID.SelectedItem as ComboBoxItem;
            ObjItem.DisplayUnitMeasureID = displayUnitMeasureId is null ? 0 : Convert.ToInt32(displayUnitMeasureId.Key);
            var defaultWarehouseId = txtDefaultWarehouseID.SelectedItem as ComboBoxItem;
            ObjItem.DefaultWarehouseID = defaultWarehouseId is null ? 0 : Convert.ToInt32(defaultWarehouseId.Key);
            ObjItem.Quantity = decimal.Zero;
            ObjItem.QuantityMax = string.IsNullOrWhiteSpace(txtQuantityMax.Text) ? decimal.Parse("1000") : decimal.Parse(txtQuantityMax.Text);
            ObjItem.QuantityMin = string.IsNullOrWhiteSpace(txtQuantityMin.Text) ? decimal.One : decimal.Parse(txtQuantityMin.Text);
            ObjItem.Cost = decimal.Zero;
            ObjItem.Reference1 = txtReference1.Text;
            ObjItem.Reference2 = txtReference2.Text;
            ObjItem.Reference3 = txtReference3.Text;
            var statusId = txtStatusID.SelectedItem as ComboBoxItem;
            ObjItem.StatusID = statusId is null ? 0 : Convert.ToInt32(statusId.Key);
            ObjItem.IsPerishable = txtIsPerishable.Checked;
            ObjItem.IsServices = (sbyte)(txtIsServices.Checked ? 1 : 0);
            ObjItem.IsInvoiceQuantityZero = (sbyte)(txtIsInvoiceQuantityZero.Checked ? 1 : 0);
            ObjItem.IsInvoice = txtIsInvoice.Checked ? 1 : 0;
            ObjItem.FactorBox = string.IsNullOrWhiteSpace(txtFactorBox.Text) ? decimal.One : decimal.Parse(txtFactorBox.Text);
            ObjItem.FactorProgram = string.IsNullOrWhiteSpace(txtFactorProgram.Text) ? decimal.One : decimal.Parse(txtFactorProgram.Text);
            var currencyId = txtCurrencyID.SelectedItem as ComboBoxItem;
            ObjItem.CurrencyID = Convert.ToInt32(currencyId!.Key);
            ObjItem.RealStateRoomBatchServices = (sbyte)(txtRealStateRoomBatchServices.Checked ? 1 : 0);
            ObjItem.RealStateRoomServices = (sbyte)(txtRealStateRoomServices.Checked ? 1 : 0);
            ObjItem.RealStateWallInCloset = (sbyte)(txtRealStateWallInCloset.Checked ? 1 : 0);
            ObjItem.RealStatePiscinaPrivate = (sbyte)(txtRealStatePiscinaPrivate.Checked ? 1 : 0);
            ObjItem.RealStateClubPiscina = (sbyte)(txtRealStateClubPiscina.Checked ? 1 : 0);
            ObjItem.RealStateAceptanMascota = (sbyte)(txtRealStateAceptanMascota.Checked ? 1 : 0);
            ObjItem.RealStateRooBatchVisit = (sbyte)(txtRealStateRooBatchVisit.Checked ? 1 : 0);
            ObjItem.RealStateContractCorrentaje = (sbyte)(txtRealStateContractCorrentaje.Checked ? 1 : 0);
            ObjItem.RealStatePlanReference = (sbyte)(txtRealStatePlanReference.Checked ? 1 : 0);
            ObjItem.RealStateLinkYoutube = txtRealStateLinkYoutube.Text;
            ObjItem.RealStateLinkPaginaWeb = txtRealStateLinkPaginaWeb.Text;
            ObjItem.RealStateLinkPhontos = txtRealStateLinkPhontos.Text;
            ObjItem.RealStateLinkGoogleMaps = txtRealStateLinkGoogleMaps.Text;
            ObjItem.RealStateLinkOther = txtRealStateLinkOther.Text;
            ObjItem.RealStateStyleKitchen = txtRealStateStyleKitchen.Text;
            ObjItem.RealStateReferenceUbicacion = txtRealStateReferenceUbicacion.Text;
            ObjItem.RealStateReferenceCondominio = txtRealStateReferenceCondominio.Text;
            ObjItem.RealStateReferenceZone = txtRealStateReferenceZone.Text;
            ObjItem.RealStateGerenciaExclusive = Convert.ToInt32(realStateGerenciaExclusive);
            ObjItem.RealStateCountryID = paisDefault;
            ObjItem.RealStateStateID = departamentoDefault;
            ObjItem.RealStateCityID = municipioDefault;
            ObjItem.ModifiedOn = DateTime.Now;
            ObjItem.RealStateEmployerAgentID = _txtEmployerId;
            ObjItem.RealStateEmail = txtRealStateEmail.Text;
            ObjItem.RealStatePhone = txtRealStatePhone.Text;
            _objInterfazCoreWebAuditoria.SetAuditCreated(ObjItem, user, "");
            ObjItem.IsActive = true;
            //guardar item
            _itemId = _objItemModel.InsertAppPosme(ObjItem);

            var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
            var pathItem = $"{pathFileOfApp}/company_{user.CompanyID}/component_{ObjComponentItem.ComponentID}/component_item_{_itemId}";
            if (!Directory.Exists(pathItem))
            {
                Directory.CreateDirectory(pathItem);
            }

            if (ObjListUnitMeasure.Count == 0)
            {
                ObjListUnitMeasure = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "unitMeasureID", user.CompanyID);
            }

            //Obtener la unidad del producto
            var objUnitMeasure = "";
            foreach (var tbCatalogItem in ObjListUnitMeasure)
            {
                if (tbCatalogItem.CatalogItemID == Convert.ToInt32(ObjItem.UnitMeasureID))
                {
                    objUnitMeasure = tbCatalogItem.Name;
                }
            }

            //Guardar el Detalle de las Bodegas
            if (WarehouseDtoBindingList.Count > 0)
            {
                foreach (var warehouseDto in WarehouseDtoBindingList)
                {
                    var itemWarehouse = new TbItemWarehouse
                    {
                        CompanyID = user.CompanyID,
                        BranchID = user.BranchID,
                        WarehouseID = warehouseDto.WarehouseId,
                        ItemID = _itemId,
                        Quantity = decimal.Zero,
                        QuantityMax = warehouseDto.QuantityMax,
                        QuantityMin = warehouseDto.QuantityMin
                    };
                    _itemWarehouseModel.InsertAppPosme(itemWarehouse);
                }
            }

            //Agregar las bodegas que no esten
            var objListWarehouse = _warehouseModel.GetByCompany(user.CompanyID);
            if (objListWarehouse.Count > 0)
            {
                foreach (var tbWarehouse in objListWarehouse)
                {
                    var existWarehouse = _itemWarehouseModel.GetByPk(user.CompanyID, _itemId, tbWarehouse.WarehouseID);
                    if (existWarehouse is not null)
                    {
                        continue;
                    }

                    var itemWarehouse = new TbItemWarehouse
                    {
                        CompanyID = user.CompanyID,
                        BranchID = user.BranchID,
                        WarehouseID = tbWarehouse.WarehouseID,
                        ItemID = _itemId,
                        Quantity = decimal.Zero,
                        QuantityMax = decimal.Parse("1000"),
                        QuantityMin = decimal.Zero
                    };
                    _itemWarehouseModel.InsertAppPosme(itemWarehouse);
                }
            }

            //Guardar Detalle de sku
            if (ObjItemSku.Count > 0)
            {
                foreach (var tbItemSkuDto in ObjItemSku)
                {
                    var objSku = new TbItemSku
                    {
                        CatalogItemID = tbItemSkuDto.CatalogItemId,
                        Value = tbItemSkuDto.Value,
                        ItemID = _itemId
                    };
                    _itemSkuModel.InsertAppPosme(objSku);
                }
            }

            var objSkuExist = _itemSkuModel.GetByPk(_itemId, Convert.ToInt32(ObjItem.UnitMeasureID));
            if (objSkuExist is null)
            {
                var objSku = new TbItemSku
                {
                    CatalogItemID = Convert.ToInt32(ObjItem.UnitMeasureID),
                    Value = decimal.One,
                    ItemID = _itemId
                };
                _itemSkuModel.InsertAppPosme(objSku);
            }

            //Guardar proveedor por defecto
            var objParameterProviderDefault = _objInterfazCoreWebParameter.GetParameter("INVENTORY_ITEM_PROVIDER_DEFAULT", user.CompanyID)!.Value;
            var objTmpProvider = new TbProviderItem
            {
                CompanyID = user.CompanyID,
                BranchID = user.BranchID,
                EntityID = Convert.ToInt32(objParameterProviderDefault),
                ItemID = _itemId
            };
            _priProviderItemModel.InsertAppPosme(objTmpProvider);

            //Ingresar la configuracion de precios
            //por defecto con 0% de utilidad
            if (ObjListPriceItem.Count > 0)
            {
                foreach (var priceDto in ObjListPriceItem)
                {
                    var price = new TbPrice
                    {
                        CompanyID = user.CompanyID,
                        ItemID = _itemId,
                        ListPriceID = priceDto.ListPriceId,
                        TypePriceID = priceDto.TypePriceId,
                        Percentage = decimal.Zero,
                        PercentageCommision = priceDto.PercentageCommision,
                        Price = priceDto.Price
                    };
                    _priceModel.InsertAppPosme(price);
                }
            }

            //Generar la Imagen del Codigo de Barra
            var pathFileCodeBarra = $"{pathFileOfApp}/company_{user.CompanyID}/component_{ObjComponentItem.ComponentID}/component_item_{_itemId}/barcode.jpg";
            if (ObjItem.BarCode!.IndexOf(",", StringComparison.Ordinal) > 0)
            {
            }
            else
            {
                CoreWebBarCode.Generate(pathFileCodeBarra, ObjItem.BarCode);
            }
        }

        public void SaveUpdate()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            var user = VariablesGlobales.Instance.User;
            var pathFileApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
            if (user is null)
            {
                throw new Exception(userNotAutenticated);
            }

            var role = VariablesGlobales.Instance.Role;
            if (role is null)
            {
                throw new Exception("ROL NO VALIDO");
            }

            if (ObjItem is null)
            {
                throw new Exception("ITEM NO VALIDO");
            }

            int resultPermission = 0;
            if (appNeedAuthentication == "true")
            {
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_item", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_item", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
            if (resultPermission == permissionMe && ObjItem.CreatedBy != user.UserID)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_EDIT"]);
            }

            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            var command = VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"];
            var commandEditableTotal = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_item", "statusID", ObjItem.StatusID!.Value, Convert.ToInt32(command), user.CompanyID, user.BranchID, role.RoleID);
            if (!commandEditableTotal!.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"]);
            }

            _objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_inventory_item");

            var directoryItem = $"{pathFileApp}/company_{ObjItem.CompanyID}/component_{ObjComponentItem.ComponentID}/component_item_{ObjItem.ItemID}";
            if (!Directory.Exists(directoryItem))
            {
                Directory.CreateDirectory(directoryItem);
            }

            var paisDefault = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameterValue("CXC_PAIS_DEFAULT", user.CompanyID));
            var departamentoDefault = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameterValue("CXC_DEPARTAMENTO_DEFAULT", user.CompanyID));
            var municipioDefault = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameterValue("CXC_MUNICIPIO_DEFAULT", user.CompanyID));
            command = VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"];
            var commandEditable = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_item", "statusID", ObjItem.StatusID!.Value, Convert.ToInt32(command), user.CompanyID, user.BranchID, role.RoleID);
            var realStateGerenciaExclusive = _objInterfazCoreWebParameter.GetParameterValue("ITEM_REAL_STATE_GERENCIA_EXCLUSIVE", user.CompanyID);
            if (!commandEditable!.Value)
            {
                var objNewItem = new TbItem();
                objNewItem = ObjItem;
                objNewItem.CompanyID = user.CompanyID;
                objNewItem.BranchID = user.CompanyID;
                if (txtInventoryCategoryID.SelectedItem is ComboBoxItem inventoryCategoryId)
                {
                    objNewItem.InventoryCategoryID = Convert.ToInt32(inventoryCategoryId.Key);
                }

                var nameProducto = txtName.Text;
                nameProducto = nameProducto.Replace("\\", "").Replace("'", "");
                nameProducto = nameProducto.Trim();
                nameProducto = nameProducto.Replace("\"", "");
                if (txtFamilyID.SelectedItem is ComboBoxItem familyId)
                {
                    objNewItem.FamilyID = Convert.ToInt32(familyId.Key);
                }

                objNewItem.ItemNumber = ObjItem.ItemNumber;
                objNewItem.BarCode = string.IsNullOrWhiteSpace(txtBarCode.Text) ? $"B{objNewItem.ItemNumber}" : txtBarCode.Text;
                objNewItem.BarCode = objNewItem.BarCode.Trim().Replace(Environment.NewLine, ",").Replace(",,", ",").Replace("\n\r", "").Replace("\n", "").Replace("\r", "");
                objNewItem.Name = nameProducto;
                if (txtDescription.InvokeRequired)
                {
                    Invoke(() => objNewItem.Description = txtDescription.Text);
                }
                else
                {
                    objNewItem.Description = txtDescription.Text;
                }

                var unitMeasureId = txtUnitMeasureID.SelectedItem as ComboBoxItem;
                objNewItem.UnitMeasureID = unitMeasureId is null ? "" : unitMeasureId.Key;
                var displayId = txtDisplayID.SelectedItem as ComboBoxItem;
                objNewItem.DisplayID = displayId is null ? 0 : Convert.ToInt32(displayId.Key);
                objNewItem.Capacity = string.IsNullOrWhiteSpace(txtCapacity.Text) ? 0 : Convert.ToInt32(double.Parse(txtCapacity.Text));
                var displayUnitMeasureId = txtDisplayUnitMeasureID.SelectedItem as ComboBoxItem;
                objNewItem.DisplayUnitMeasureID = displayUnitMeasureId is null ? 0 : Convert.ToInt32(displayUnitMeasureId.Key);
                var defaultWarehouseId = txtDefaultWarehouseID.SelectedItem as ComboBoxItem;
                objNewItem.DefaultWarehouseID = defaultWarehouseId is null ? 0 : Convert.ToInt32(defaultWarehouseId.Key);
                objNewItem.Quantity = decimal.Zero;
                objNewItem.QuantityMax = string.IsNullOrWhiteSpace(txtQuantityMax.Text) ? decimal.Parse("1000") : decimal.Parse(txtQuantityMax.Text);
                objNewItem.QuantityMin = string.IsNullOrWhiteSpace(txtQuantityMin.Text) ? decimal.One : decimal.Parse(txtQuantityMin.Text);
                objNewItem.Cost = decimal.Zero;
                objNewItem.Reference1 = txtReference1.Text;
                objNewItem.Reference2 = txtReference2.Text;
                objNewItem.Reference3 = txtReference3.Text;
                var statusId = txtStatusID.SelectedItem as ComboBoxItem;
                objNewItem.StatusID = statusId is null ? 0 : Convert.ToInt32(statusId.Key);
                objNewItem.IsPerishable = txtIsPerishable.Checked;
                objNewItem.IsServices = (sbyte)(txtIsServices.Checked ? 1 : 0);
                objNewItem.IsInvoiceQuantityZero = (sbyte)(txtIsInvoiceQuantityZero.Checked ? 1 : 0);
                objNewItem.IsInvoice = txtIsInvoice.Checked ? 1 : 0;
                objNewItem.FactorBox = string.IsNullOrWhiteSpace(txtFactorBox.Text) ? decimal.One : decimal.Parse(txtFactorBox.Text);
                objNewItem.FactorProgram = string.IsNullOrWhiteSpace(txtFactorProgram.Text) ? decimal.One : decimal.Parse(txtFactorProgram.Text);
                var currencyId = txtCurrencyID.SelectedItem as ComboBoxItem;
                objNewItem.CurrencyID = Convert.ToInt32(currencyId!.Key);
                objNewItem.RealStateRoomBatchServices = (sbyte)(txtRealStateRoomBatchServices.Checked ? 1 : 0);
                objNewItem.RealStateRoomServices = (sbyte)(txtRealStateRoomServices.Checked ? 1 : 0);
                objNewItem.RealStateWallInCloset = (sbyte)(txtRealStateWallInCloset.Checked ? 1 : 0);
                objNewItem.RealStatePiscinaPrivate = (sbyte)(txtRealStatePiscinaPrivate.Checked ? 1 : 0);
                objNewItem.RealStateClubPiscina = (sbyte)(txtRealStateClubPiscina.Checked ? 1 : 0);
                objNewItem.RealStateAceptanMascota = (sbyte)(txtRealStateAceptanMascota.Checked ? 1 : 0);
                objNewItem.RealStateRooBatchVisit = (sbyte)(txtRealStateRooBatchVisit.Checked ? 1 : 0);
                objNewItem.RealStateContractCorrentaje = (sbyte)(txtRealStateContractCorrentaje.Checked ? 1 : 0);
                objNewItem.RealStatePlanReference = (sbyte)(txtRealStatePlanReference.Checked ? 1 : 0);
                objNewItem.RealStateLinkYoutube = txtRealStateLinkYoutube.Text;
                objNewItem.RealStateLinkPaginaWeb = txtRealStateLinkPaginaWeb.Text;
                objNewItem.RealStateLinkPhontos = txtRealStateLinkPhontos.Text;
                objNewItem.RealStateLinkGoogleMaps = txtRealStateLinkGoogleMaps.Text;
                objNewItem.RealStateLinkOther = txtRealStateLinkOther.Text;
                objNewItem.RealStateStyleKitchen = txtRealStateStyleKitchen.Text;
                objNewItem.RealStateReferenceUbicacion = txtRealStateReferenceUbicacion.Text;
                objNewItem.RealStateReferenceCondominio = txtRealStateReferenceCondominio.Text;
                objNewItem.RealStateReferenceZone = txtRealStateReferenceZone.Text;
                objNewItem.RealStateCountryID = paisDefault;
                objNewItem.RealStateStateID = departamentoDefault;
                objNewItem.RealStateCityID = municipioDefault;
                objNewItem.ModifiedOn = DateTime.Now;
                objNewItem.RealStateEmployerAgentID = _txtEmployerId;
                objNewItem.RealStateEmail = txtRealStateEmail.Text;
                objNewItem.RealStatePhone = txtRealStatePhone.Text;
                objNewItem.ModifiedOn = DateTime.Now;
                objNewItem.RealStateGerenciaExclusive = Convert.ToInt32(realStateGerenciaExclusive);
                objNewItem.DateLastUse = DateTime.Now;
                //Actualizar Objeto
                _objItemModel.UpdateAppPosme(user.CompanyID, ObjItem.ItemID, objNewItem);

                //Guardar el detalle de Conceptos
                _companyComponentConceptModel.DeleteWhereComponentItemId(user.CompanyID, ObjComponentItem.ComponentID, _itemId);
                if (ObjListConcept.Count > 0)
                {
                    foreach (var itemConceptDto in ObjListConcept)
                    {
                        var concept = new TbCompanyComponentConcept
                        {
                            Name = itemConceptDto.NameConcept,
                            ValueIn = itemConceptDto.ValueIn,
                            ValueOut = itemConceptDto.ValueOut,
                            CompanyID = user.CompanyID,
                            ComponentID = ObjComponentItem.ComponentID,
                            ComponentItemID = _itemId
                        };
                        _companyComponentConceptModel.InsertAppPosme(concept);
                    }
                }

                //Guardar el detalle de Proveedores
                _priProviderItemModel.DeleteWhereItemId(user.CompanyID, _itemId);
                if (ObjListProvider.Count > 0)
                {
                    foreach (var tbProviderItemDto in ObjListProvider)
                    {
                        var providerItem = new TbProviderItem
                        {
                            CompanyID = user.CompanyID,
                            ItemID = _itemId,
                            BranchID = user.BranchID,
                            EntityID = tbProviderItemDto.EntityId
                        };
                        _priProviderItemModel.InsertAppPosme(providerItem);
                    }
                }

                //Guardar Detalle de sku
                _itemSkuModel.DeleteAppPosme(_itemId);
                if (ObjItemSku.Count > 0)
                {
                    foreach (var tbItemSkuDto in ObjItemSku)
                    {
                        var itemSku = new TbItemSku
                        {
                            CatalogItemID = tbItemSkuDto.CatalogItemId,
                            ItemID = _itemId,
                            Value = tbItemSkuDto.Value
                        };
                        _itemSkuModel.InsertAppPosme(itemSku);
                    }
                }

                var objSkuExist = _itemSkuModel.GetByPk(_itemId, Convert.ToInt32(objNewItem.UnitMeasureID));
                if (objSkuExist is null)
                {
                    var itemSku = new TbItemSku
                    {
                        CatalogItemID = Convert.ToInt32(objNewItem.UnitMeasureID),
                        ItemID = _itemId,
                        Value = decimal.One
                    };
                    _itemSkuModel.InsertAppPosme(itemSku);
                }

                //Guardar el Detalle las Bodegas
                if (WarehouseDtoBindingList.Count > 0)
                {
                    _itemWarehouseModel.DeleteWhereIdNotIn(user.CompanyID, _itemId, WarehouseDtoBindingList.Select(dto => dto.WarehouseId).ToList());
                    foreach (var warehouseDto in WarehouseDtoBindingList)
                    {
                        var findWarehouse = _itemWarehouseModel.GetByPk(user.CompanyID, _itemId, warehouseDto.WarehouseId);
                        if (findWarehouse is not null)
                        {
                            var warehouse = new TbItemWarehouse
                            {
                                CompanyID = user.CompanyID,
                                ItemID = _itemId,
                                BranchID = user.BranchID,
                                Quantity = findWarehouse.Quantity,
                                QuantityMax = warehouseDto.QuantityMax,
                                QuantityMin = warehouseDto.QuantityMin,
                                WarehouseID = warehouseDto.WarehouseId
                            };
                            _itemWarehouseModel.UpdateAppPosme(user.CompanyID, _itemId, warehouseDto.WarehouseId, warehouse);
                        }
                        else
                        {
                            var warehouse = new TbItemWarehouse
                            {
                                CompanyID = user.CompanyID,
                                ItemID = _itemId,
                                BranchID = user.BranchID,
                                Quantity = decimal.Zero,
                                QuantityMax = warehouseDto.QuantityMax,
                                QuantityMin = warehouseDto.QuantityMin,
                                WarehouseID = warehouseDto.WarehouseId
                            };
                            _itemWarehouseModel.InsertAppPosme(warehouse);
                        }
                    }
                }

                //Ingresar la configuracion de precios
                //por defecto con 0% de utilidad
                if (ObjListPriceItem.Count > 0)
                {
                    foreach (var tbPriceDto in ObjListPriceItem)
                    {
                        var findPrice = _priceModel.GetRowByPk(user.CompanyID, tbPriceDto.ListPriceId, _itemId, tbPriceDto.TypePriceId);
                        if (findPrice is not null)
                        {
                            findPrice.PercentageCommision = tbPriceDto.PercentageCommision;
                            findPrice.Price = tbPriceDto.Price;
                            _priceModel.UpdateAppPosme(user.CompanyID, tbPriceDto.ListPriceId, _itemId, tbPriceDto.TypePriceId, findPrice);
                        }
                        else
                        {
                            var price = new TbPrice
                            {
                                CompanyID = user.CompanyID,
                                ItemID = _itemId,
                                ListPriceID = tbPriceDto.ListPriceId,
                                Percentage = decimal.Zero,
                                PercentageCommision = tbPriceDto.PercentageCommision,
                                Price = tbPriceDto.Price,
                                TypePriceID = tbPriceDto.TypePriceId
                            };
                            _priceModel.InsertAppPosme(price);
                        }
                    }
                }
            }
            else
            {
                var statusId = txtStatusID.SelectedItem as ComboBoxItem;
                ObjItem.StatusID = Convert.ToInt32(statusId!.Key);
                _objItemModel.UpdateAppPosme(user.CompanyID, _itemId, ObjItem);
            }

            //Generar la Imagen del Codigo de Barra
            var pathFileCodeBarra = $"{pathFileApp}/company_{user.CompanyID}/component_{ObjComponentItem.ComponentID}/component_item_{_itemId}/barcode.jpg";
            if (ObjItem.BarCode!.IndexOf(",", StringComparison.Ordinal) > 0)
            {
            }
            else
            {
                if (txtCodigoBarra.InvokeRequired)
                {
                    Invoke(() =>
                    {
                        if (txtCodigoBarra.Image != null)
                        {
                            txtCodigoBarra.Image.Dispose();
                            txtCodigoBarra.Image = null;
                        }
                    });
                }
                else
                {
                    if (txtCodigoBarra.Image != null)
                    {
                        txtCodigoBarra.Image.Dispose();
                        txtCodigoBarra.Image = null;
                    }
                }

                CoreWebBarCode.Generate(pathFileCodeBarra, ObjItem.BarCode);
            }
        }

        private void CommandNew(object? sender, EventArgs e)
        {
            Close();
            var nuevo = new FormInventoryItemEdit(TypeRender.New, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            nuevo.Show();
        }

        private void CommandSave(object? sender, EventArgs e)
        {
            if (FnValidateFormAndSubmit())
            {
                _backgroundWorker = new BackgroundWorker();
                if (!progressPanel.Visible)
                {
                    progressPanel.Width = Width;
                    progressPanel.Height = Height;
                    progressPanel.Visible = true;
                }

                _backgroundWorker.DoWork += (ob, ev) =>
                {
                    switch (_typeRender)
                    {
                        case TypeRender.New:
                            SaveInsert();
                            break;
                        default:
                            SaveUpdate();
                            break;
                    }
                };
                _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
                {
                    if (ev.Error is not null)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Registrar", $"No se registraron los valores. {ev.Error.Message}", this);
                    }
                    else if (ev.Cancelled)
                    {
                        //cancelado por el usuario   
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Registrar", "Se ha cancelado la operación actual. Linea 3424", this);
                    }
                    else
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se han registrdo los datos de forma correcta", this);
                        if (ObjItem is not null && _itemId > 0)
                        {
                            LoadEdit();
                            LoadRender(TypeRender.Edit);
                        }
                    }

                    if (progressPanel.Visible)
                    {
                        progressPanel.Visible = false;
                    }
                };

                if (!_backgroundWorker.IsBusy)
                {
                    _backgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void CommandRegresar(object? sender, EventArgs e)
        {
            Close();
        }

        public void PreRender()
        {
            HelperMethods.OnlyNumberDecimals(txtCapacity);
            HelperMethods.OnlyNumberDecimals(txtQuantity);
            HelperMethods.OnlyNumberDecimals(txtQuantityMin);
            HelperMethods.OnlyNumberDecimals(txtQuantityMax);
            HelperMethods.OnlyNumberDecimals(txtCost);
        }

        public void LoadRender(TypeRender typeRedner)
        {
            _typeRender = typeRedner;
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Usuario", "No existe el usuario", this);
                return;
            }

            btnImprmir.Visible = false;
            switch (typeRedner)
            {
                case TypeRender.New:
                    HelperMethods.FnClearTextEdits(this);
                    lblTitulo.Text = @"Editar Producto Número: 0000000";
                    btnEliminar.Visible = false;
                    btnNuevo.Visible = false;
                    tabPageProveedores.PageVisible = false;
                    tabPageConceptos.PageVisible = false;
                    tabPageArchivos.PageVisible = false;
                    _renderGridFiles = new RenderFileGridControl(user.CompanyID, ObjComponentItem!.ComponentID, 0);
                    gridControlArchivos.DataSource = null;
                    layoutControlCodigoBarra.Visible = false;
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.ElementAt(0).WorkflowStageID);
                    CoreWebRenderInView.LlenarComboBox(ObjListInventoryCategory, txtInventoryCategoryID, "InventoryCategoryID", "Name", ObjListInventoryCategory.ElementAt(0).InventoryCategoryID);
                    CoreWebRenderInView.LlenarComboBox(ObjListFamily, txtFamilyID, "CatalogItemID", "Name", ObjListFamily.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListUnitMeasure, txtUnitMeasureID, "CatalogItemID", "Name", ObjListUnitMeasure.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListDisplay, txtDisplayID, "CatalogItemID", "Name", ObjListDisplay.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListUnitMeasure, txtDisplayUnitMeasureID, "CatalogItemID", "Name", ObjListUnitMeasure.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListWarehouse, txtDefaultWarehouseID, "WarehouseID", "Name", ObjListWarehouse.Single(warehouse => warehouse.Number == _warehouseDefault).WarehouseID);
                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjListCurrency.ElementAt(0).CurrencyId);
                    CoreWebRenderInView.LlenarComboBoxGridControl(ObjListWarehouse, cmbWarehouseNameGridControl, "WarehouseID", "Name");
                    CoreWebRenderInView.LlenarComboBoxGridControl(ObjListUnitMeasure, cmbSku, "CatalogItemID", "Name");
                    txtCapacity.Text = "1";
                    txtQuantityMin.Text = "1";
                    txtQuantityMax.Text = "1000";
                    txtFactorBox.Text = "1";
                    txtFactorProgram.Text = "1";
                    break;
                case TypeRender.Edit:
                    Text = @"Editar Producto";
                    lblTitulo.Text = $@"Editar Producto Número: {ObjItem!.ItemNumber}";
                    btnEliminar.Visible = true;
                    btnNuevo.Visible = true;
                    tabPageProveedores.PageVisible = true;
                    tabPageConceptos.PageVisible = true;
                    tabPageArchivos.PageVisible = true;
                    layoutControlCodigoBarra.Visible = true;
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjItem.StatusID);
                    CoreWebRenderInView.LlenarComboBox(ObjListInventoryCategory, txtInventoryCategoryID, "InventoryCategoryID", "Name", ObjItem.InventoryCategoryID);
                    CoreWebRenderInView.LlenarComboBox(ObjListFamily, txtFamilyID, "CatalogItemID", "Name", ObjItem.FamilyID);
                    CoreWebRenderInView.LlenarComboBox(ObjListUnitMeasure, txtUnitMeasureID, "CatalogItemID", "Name", ObjItem.UnitMeasureID);
                    CoreWebRenderInView.LlenarComboBox(ObjListDisplay, txtDisplayID, "CatalogItemID", "Name", ObjItem.DisplayID);
                    CoreWebRenderInView.LlenarComboBox(ObjListUnitMeasure, txtDisplayUnitMeasureID, "CatalogItemID", "Name", ObjItem.DisplayUnitMeasureID);
                    CoreWebRenderInView.LlenarComboBox(ObjListWarehouse, txtDefaultWarehouseID, "WarehouseID", "Name", ObjItem.DefaultWarehouseID);
                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjItem.CurrencyID);
                    CoreWebRenderInView.LlenarComboBoxGridControl(ObjListWarehouse, cmbWarehouseNameGridControl, "WarehouseID", "Name");
                    CoreWebRenderInView.LlenarComboBoxGridControl(ObjListUnitMeasure, cmbSku, "CatalogItemID", "Name");
                    _renderGridFiles = new RenderFileGridControl(user.CompanyID, ObjComponentItem!.ComponentID, ObjItem.ItemID);
                    _renderGridFiles.RenderGridControl(gridControlArchivos);
                    _renderGridFiles.LoadFiles();
                    var rootPath = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
                    if (!string.IsNullOrWhiteSpace(rootPath))
                    {
                        var companyPath = $"company_{ObjItem.CompanyID}";
                        var componentPath = $"component_{ObjComponentItem.ComponentID}";
                        var componentItemPath = $"component_item_{ObjItem.ItemID}";
                        var path = Path.Combine(rootPath, companyPath, componentPath, componentItemPath);
                        var pathFile = Path.Combine(path, "barcode.jpg");
                        if (File.Exists(pathFile))
                        {
                            txtCodigoBarra.Image = Image.FromFile(pathFile);
                        }
                    }

                    txtName.EditValue = ObjItem.Name;
                    txtBarCode.EditValue = ObjItem.BarCode;
                    if (ObjItem.IsPerishable is not null)
                    {
                        txtIsPerishable.Checked = ObjItem.IsPerishable.Value;
                    }

                    if (ObjItem.IsInvoiceQuantityZero is not null)
                    {
                        txtIsInvoiceQuantityZero.Checked = ObjItem.IsInvoiceQuantityZero.Value == 1;
                    }

                    txtIsInvoice.Checked = ObjItem.IsInvoice == 1;
                    if (ObjItem.IsServices is not null)
                    {
                        txtIsServices.Checked = ObjItem.IsServices.Value == 1;
                    }

                    txtCapacity.EditValue = ObjItem.Capacity;
                    txtQuantity.EditValue = ObjItem.Quantity;
                    txtQuantityMin.EditValue = ObjItem.QuantityMin;
                    txtQuantityMax.EditValue = ObjItem.QuantityMax;
                    txtCost.EditValue = ObjItem.Cost;
                    txtFactorBox.EditValue = ObjItem.FactorBox;
                    txtFactorProgram.EditValue = ObjItem.FactorProgram;
                    txtReference1.EditValue = ObjItem.Reference1;
                    txtReference2.EditValue = ObjItem.Reference2;
                    txtReference3.EditValue = ObjItem.Reference3;
                    txtRealStatePhone.EditValue = ObjItem.RealStatePhone;
                    txtRealStateEmail.EditValue = ObjItem.RealStateEmail;
                    txtRealStateRoomBatchServices.Checked = ObjItem.RealStateRoomBatchServices!.Value == 1;
                    txtRealStateRooBatchVisit.Checked = ObjItem.RealStateRooBatchVisit!.Value == 1;
                    txtRealStateRoomServices.Checked = ObjItem.RealStateRoomServices!.Value == 1;
                    txtRealStateWallInCloset.Checked = ObjItem.RealStateWallInCloset!.Value == 1;
                    txtRealStatePiscinaPrivate.Checked = ObjItem.RealStatePiscinaPrivate!.Value == 1;
                    txtRealStateClubPiscina.Checked = ObjItem.RealStateClubPiscina!.Value == 1;
                    txtRealStateAceptanMascota.Checked = ObjItem.RealStateAceptanMascota!.Value == 1;
                    txtRealStateContractCorrentaje.Checked = ObjItem.RealStateContractCorrentaje!.Value == 1;
                    txtRealStatePlanReference.Checked = ObjItem.RealStatePlanReference!.Value == 1;
                    txtRealStateLinkYoutube.EditValue = ObjItem.RealStateLinkYoutube;
                    txtRealStateLinkPaginaWeb.EditValue = ObjItem.RealStateLinkPaginaWeb;
                    txtRealStateLinkPhontos.EditValue = ObjItem.RealStateLinkPhontos;
                    txtRealStateLinkGoogleMaps.EditValue = ObjItem.RealStateLinkGoogleMaps;
                    txtRealStateLinkOther.EditValue = ObjItem.RealStateLinkOther;
                    txtRealStateStyleKitchen.EditValue = ObjItem.RealStateStyleKitchen;
                    txtRealStateReferenceUbicacion.EditValue = ObjItem.RealStateReferenceUbicacion;
                    txtRealStateReferenceCondominio.EditValue = ObjItem.RealStateReferenceCondominio;
                    txtRealStateReferenceZone.EditValue = ObjItem.RealStateReferenceZone;
                    txtEmployerDescription.EditValue = ObjEmployer is not null ? $@"{ObjEmployer.EmployeNumber} / {ObjEmployer.FirstName} {ObjEmployer.LastName}" : "";
                    txtDescription.Text = ObjItem.Description;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeRedner), typeRedner, null);
            }
        }
        #endregion

        #region Funciones
        
        private bool FnValidateFormAndSubmit()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                dxErrorProvider.SetError(txtName, "Debe especificar este campo para continuar");
                return false;
            }

            if (txtStatusID.SelectedItem is null)
            {
                dxErrorProvider.SetError(txtStatusID, "Establecer Estado");
                return false;
            }

            if (txtInventoryCategoryID.SelectedItem is null)
            {
                dxErrorProvider.SetError(txtInventoryCategoryID, "Seleccione una categoria");
                return false;
            }

            if (txtDefaultWarehouseID.SelectedItem is null)
            {
                dxErrorProvider.SetError(txtDefaultWarehouseID, "Seleccione una bodega por defecto");
                return false;
            }

            if (txtUnitMeasureID.SelectedItem is null)
            {
                dxErrorProvider.SetError(txtUnitMeasureID, "Seleccione la unidad de medida");
                return false;
            }

            var defaultWarehouSelectedItem = txtDefaultWarehouseID.SelectedItem as ComboBoxItem;
            if (defaultWarehouSelectedItem is null)
            {
                dxErrorProvider.SetError(txtDefaultWarehouseID, "Debe seleccionar una bodega para continuar");
                return false;
            }

            if (WarehouseDtoBindingList.Count <= 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Guardar", "Debe especificar una bodega para continuar", this);
                return false;
            }

            if (WarehouseDtoBindingList.All(dto => dto.WarehouseId != Convert.ToInt32(defaultWarehouSelectedItem.Key)))
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Guardar", "La bodega que esta pordefecto debe de estar en el detalle de Bodegas", this);
                return false;
            }

            dxErrorProvider.ClearErrors();
            return true;
        }
        private void FnOnCompleteNewEmployerPopPub(dynamic mensaje)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            diccionario.Add("entityID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
            diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", ""));
            diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", ""));

            FnOnClompeteNewEmployer(diccionario);
        }

        private void FnOnClompeteNewEmployer(Dictionary<string, string> diccionario)
        {
            Invoke(() =>
            {
                txtEmployerDescription.EditValue = $@"{diccionario["Codigo"]} / {diccionario["Nombre"]}";
                _txtEmployerId = Convert.ToInt32(diccionario["entityID"]);
            });
        }

        private void FnItemSkuBindingList(List<TbItemSkuDto> itemsSkuDtos)
        {
            ObjItemSku = new BindingList<TbItemSkuDto>
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true
            };
            if (itemsSkuDtos.Count > 0)
            {
                foreach (var tbItemSkuDto in itemsSkuDtos)
                {
                    ObjItemSku.Add(tbItemSkuDto);
                }
            }

            gridControlSku.DataSource = ObjItemSku;
        }

        private void FnPriceItemBindingList(List<TbPriceDto> priceDtos)
        {
            ObjListPriceItem = new BindingList<TbPriceDto>
            {
                AllowEdit = true,
                AllowNew = false,
                AllowRemove = false
            };
            if (priceDtos.Count > 0)
            {
                foreach (var tbPriceDto in priceDtos)
                {
                    ObjListPriceItem.Add(tbPriceDto);
                }

                ObjListPriceItemFirst = priceDtos.Select(dto => dto.Price).First();
            }

            gridControlPrecios.DataSource = ObjListPriceItem;
        }

        private void FnConceptBindingList(List<TbCompanyComponentConcept> listConcept)
        {
            ObjListConcept = new BindingList<FormInventoryItemEditConceptDTO>
            {
                AllowEdit = false,
                AllowNew = false,
                AllowRemove = true
            };
            if (listConcept.Count > 0)
            {
                foreach (var tbCompanyComponentConcept in listConcept)
                {
                    var concept = new FormInventoryItemEditConceptDTO(tbCompanyComponentConcept.Name, tbCompanyComponentConcept.ValueIn!.Value, tbCompanyComponentConcept.ValueOut!.Value);
                    ObjListConcept.Add(concept);
                }
            }

            gridControlConcepts.DataSource = ObjListConcept;
        }

        private void FnProviderBindingList(List<TbProviderItemDto> listProvider)
        {
            ObjListProvider = new BindingList<TbProviderItemDto>
            {
                AllowEdit = false,
                AllowNew = false,
                AllowRemove = true
            };
            if (listProvider.Count > 0)
            {
                foreach (var tbProviderItemDto in listProvider)
                {
                    ObjListProvider.Add(tbProviderItemDto);
                }
            }

            gridControlProvider.DataSource = ObjListProvider;
        }

        private void FnWarehouseBindingList(List<TbItemWarehouseDto> listaBodegas)
        {
            WarehouseDtoBindingList = new BindingList<FormInventoryItemEditWarehouseDTO>
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true
            };
            if (listaBodegas.Count > 0)
            {
                foreach (var tbItemWarehouseDto in listaBodegas)
                {
                    var warehouse = new FormInventoryItemEditWarehouseDTO(tbItemWarehouseDto.WarehouseId, tbItemWarehouseDto.WarehouseName!, tbItemWarehouseDto.Quantity, tbItemWarehouseDto.QuantityMin, tbItemWarehouseDto.QuantityMax);
                    WarehouseDtoBindingList.Add(warehouse);
                }
            }

            gridControlBodegas.DataSource = WarehouseDtoBindingList;
        }

        private void FnOnCompleteNewProviderPopPub(dynamic mensaje)
        {
            var diccionario = new Dictionary<string, string>();
            var objWebToolsHelper = new WebToolsHelper();
            diccionario.Add("entityID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
            diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", ""));
            diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", ""));

            FnOnClompeteNewProvider(diccionario);
        }

        private void FnOnClompeteNewProvider(Dictionary<string, string> diccionario)
        {
            Invoke(() =>
            {
                var entityId = Convert.ToInt32(diccionario["entityID"]);
                var findValue = ObjListProvider.Any(dto => dto.EntityId == entityId);
                if (findValue)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Provedor", "Ya existe el proveedor en la lista, agregue uno nuevo", this);
                    return;
                }

                var providerNumber = diccionario["Codigo"];
                var nombre = diccionario["Nombre"];
                var providerDto = new TbProviderItemDto
                {
                    EntityId = entityId,
                    ProviderNumber = providerNumber,
                    FirstName = nombre
                };
                ObjListProvider.Add(providerDto);
            });
        }

        #endregion

        #region Eventos

         private void cmbWarehouseNameGridControl_ValueChanged(object? sender, EventArgs e)
        {
            if (sender is ComboBoxEdit editor)
            {
                var item = editor.SelectedItem as ComboBoxItem;
                if (item is null) return;
                var itemKey = Convert.ToInt32(item.Key);
                if (WarehouseDtoBindingList.Count > 0)
                {
                    _findValueWarehouse = WarehouseDtoBindingList.Any(dto => dto.WarehouseId == itemKey);
                    if (_findValueWarehouse)
                    {
                        gridViewBodegas.SetColumnError(colWarehouseName, "Ya está seleccionada la bodega");
                    }
                    else
                    {
                        gridViewBodegas.SetFocusedRowCellValue(colDetailWarehouseID, itemKey);
                    }
                }
            }
        }

        /// <summary>
        /// Esto es para cuando se selecciona del combobox de bodegas, pueda convertir al valor y no mande error
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ConvertEditValueEventArgs</param>
        private void cmbWarehouseNameGridControl_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                var isItem = e.Value is ComboBoxItem;
                if (isItem)
                {
                    var item = (ComboBoxItem)e.Value;
                    e.Value = item.Value;
                }
                else
                {
                    e.Value = e.Value.ToString();
                }
            }

            e.Handled = true;
        }

        private void btnDeleteDetailWarehouse_Click(object sender, EventArgs e)
        {
            var rowSelecteds = gridViewBodegas.GetSelectedRows();
            if (rowSelecteds.Length <= 0) return;
            gridViewBodegas.DeleteSelectedRows();
            WarehouseDtoBindingList.ResetBindings();
        }

        private void btnSearchEmployer_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch("Lista de Colaboradores", ObjComponentEmployer!.ComponentID,
                "SELECCIONAR_EMPLOYEE", true, @"", false, "", 0, 5, "", true);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarEmployer;
            formTypeListSearch.ShowDialog(this);
        }

        private void btnClearEmployer_Click(object sender, EventArgs e)
        {
            txtEmployerDescription.EditValue = "";
            _txtEmployerId = 0;
        }

        private void EventoCallBackAceptarEmployer(dynamic mensaje)
        {
            FnOnCompleteNewEmployerPopPub(mensaje);
        }

        private void gridViewBodegas_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            var view = sender as ColumnView;
            if (view is null) return;
            var column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column != colWarehouseName) return;
            if (_findValueWarehouse)
                e.Valid = false;
        }

        private void gridViewBodegas_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            var view = sender as ColumnView;
            if (view == null) return;
            if (view.FocusedColumn == colWarehouseName)
            {
                e.ExceptionMode = ExceptionMode.DisplayError;
                e.WindowCaption = "Error";
                e.ErrorText = "Ya está seleccionada la bodega";
            }
        }

        private void btnNewDetailProvider_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch("Lista de Proveedores", ObjComponentProvider!.ComponentID,
                "SELECCIONAR_PROVEEDOR", true, @"", false, "", 0, 5, "", true);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarProvider;
            formTypeListSearch.ShowDialog(this);
        }

        private void EventoCallBackAceptarProvider(dynamic mensaje)
        {
            FnOnCompleteNewProviderPopPub(mensaje);
        }

        private void btnDeleteDetailProvider_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewProvider.SelectedRowsCount;
            if (selectedRows <= 0) return;
            gridViewProvider.DeleteSelectedRows();
            ObjListProvider.ResetBindings();
        }

        private void btnNewDetailConcept_Click(object sender, EventArgs e)
        {
            var formConceptos = new FormInventoryItemEditConcepts();
            var result = formConceptos.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var concept = new FormInventoryItemEditConceptDTO(formConceptos.NameConcept!, formConceptos.ValueIn, formConceptos.ValueOut);
                var findValue = ObjListConcept.Any(dto => dto.NameConcept.ToUpper().Equals(concept.NameConcept.ToUpper()));
                if (!findValue)
                {
                    ObjListConcept.Add(concept);
                }
                else
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Concepto", "Ya existe el concepto en la lista", this);
                }
            }
        }

        private void btnDeleteDetailConcept_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewConcepts.SelectedRowsCount;
            if (selectedRows <= 0) return;
            gridViewConcepts.DeleteSelectedRows();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new XtraOpenFileDialog();
            openFileDialog.Title = @"Seleccionar archivo";
            var dialogResult = openFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                var file = openFileDialog.SafeFileName;
                _renderGridFiles.AddRow(file);
            }
        }

        private void cmbSku_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                var isItem = e.Value is ComboBoxItem;
                if (isItem)
                {
                    var item = (ComboBoxItem)e.Value;
                    e.Value = item.Value;
                }
                else
                {
                    e.Value = e.Value.ToString();
                }
            }

            e.Handled = true;
        }

        private void cmbSku_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit editor)
            {
                var item = editor.SelectedItem as ComboBoxItem;
                if (item is null) return;
                var itemKey = Convert.ToInt32(item.Key);
                _findValueSku = ObjItemSku.Any(dto => dto.CatalogItemId == itemKey);
                if (_findValueSku)
                {
                    gridViewSku.SetColumnError(colSkuDisplay, "Ya está seleccionado el SKU");
                }
                else
                {
                    gridViewSku.SetFocusedRowCellValue(colDetailSkuCatalogItemId, itemKey);
                }
            }
        }

        private void gridViewSku_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            var view = sender as ColumnView;
            if (view == null) return;
            if (view.FocusedColumn == colSkuDisplay)
            {
                e.ExceptionMode = ExceptionMode.DisplayError;
                e.WindowCaption = "Error";
                e.ErrorText = "Ya está seleccionado el SKu";
            }
        }

        private void gridViewSku_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            var view = sender as ColumnView;
            if (view is null) return;
            var column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column != colSkuDisplay) return;
            if (_findValueSku)
                e.Valid = false;
        }

        private void btnEliminarSkuGrid_Click(object sender, EventArgs e)
        {
            gridViewSku.DeleteSelectedRows();
        }

        private void xtraScrollableControl1_Click(object sender, EventArgs e)
        {
        }

        #endregion
    }
}