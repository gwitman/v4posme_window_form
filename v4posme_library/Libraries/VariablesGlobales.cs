using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Implementacion;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries
{
    public class VariablesGlobales
    {
        private readonly IUnityContainer _unityContainer;

        private VariablesGlobales()
        {
            _unityContainer = new UnityContainer();

            #region CDI_MODELS

            _unityContainer.RegisterType<IAccountingBalanceModel, AccountingBalanceModel>();
            _unityContainer.RegisterType<IAccountLevelModel, AccountLevelModel>();
            _unityContainer.RegisterType<IAccountModel, AccountModel>();
            _unityContainer.RegisterType<IAccountTypeModel, AccountTypeModel>();
            _unityContainer.RegisterType<IBibliaModel, BibliaModel>();
            _unityContainer.RegisterType<IBiometricUserModel, BiometricUserModel>();
            _unityContainer.RegisterType<ICenterCostModel, CenterCostModel>();
            _unityContainer.RegisterType<ICompanyComponentConceptModel, CompanyComponentConceptModel>();
            _unityContainer.RegisterType<ICompanyCurrencyModel, CompanyCurrencyModel>();
            _unityContainer.RegisterType<ICompanyParameterModel, CompanyParameterModel>();
            _unityContainer.RegisterType<IComponentCycleModel, ComponentCycleModel>();
            _unityContainer.RegisterType<IComponentPeriodModel, ComponentPeriodModel>();
            _unityContainer.RegisterType<ICreditLineModel, CreditLineModel>();
            _unityContainer.RegisterType<ICustomerConsultasSinRiesgoModel, CustomerConsultasSinRiesgoModel>();
            _unityContainer.RegisterType<ICustomerCreditAmortizationModel, CustomerCreditAmortizationModel>();
            _unityContainer
                .RegisterType<ICustomerCreditDocumentEntityRelatedModel, CustomerCreditDocumentEntityRelatedModel>();
            _unityContainer.RegisterType<ICustomerCreditDocumentModel, CustomerCreditDocumentModel>();
            _unityContainer.RegisterType<ICustomerCreditLineModel, CustomerCreditLineModel>();
            _unityContainer.RegisterType<ICustomerCreditModel, CustomerCreditModel>();
            _unityContainer.RegisterType<ICustomerModel, CustomerModel>();
            _unityContainer.RegisterType<IEmployeeCalendarPayDetailModel, EmployeeCalendarPayDetailModel>();
            _unityContainer.RegisterType<IEmployeeCalendarPayModel, EmployeeCalendarPayModel>();
            _unityContainer.RegisterType<IEmployeeModel, EmployeeModel>();
            _unityContainer.RegisterType<IEntityAccountModel, IEntityAccountModel>();
            _unityContainer.RegisterType<IEntityEmailModel, EntityEmailModel>();
            _unityContainer.RegisterType<IEntityModel, EntityModel>();
            _unityContainer.RegisterType<IEntityPhoneModel, EntityPhoneModel>();
            _unityContainer.RegisterType<IErrorModel, ErrorModel>();
            _unityContainer.RegisterType<IFixedAssentModel, FixedAssentModel>();
            _unityContainer.RegisterType<IItemDataSheetDetailModel, ItemDataSheetDetailModel>();
            _unityContainer.RegisterType<IItemDataSheetModel, ItemDataSheetModel>();
            _unityContainer.RegisterType<IItemModel, ItemModel>();
            _unityContainer.RegisterType<IItemSkuModel, ItemSkuModel>();
            _unityContainer.RegisterType<IItemWarehouseExpiredModel, ItemWarehouseExpiredModel>();
            _unityContainer.RegisterType<IItemCategoryModel, ItemCategoryModel>();
            _unityContainer.RegisterType<IItemWarehouseModel, ItemWarehouseModel>();
            _unityContainer.RegisterType<IJournalEntryDetailModel, JournalEntryDetailModel>();
            _unityContainer.RegisterType<IJournalEntryModel, JournalEntryModel>();
            _unityContainer.RegisterType<ILegalModel, LegalModel>();
            _unityContainer.RegisterType<IListPriceModel, ListPriceModel>();
            _unityContainer.RegisterType<INaturalModel, NaturalModel>();
            _unityContainer.RegisterType<INotificationModel, NotificationModel>();
            _unityContainer.RegisterType<IPriceModel, PriceModel>();
            _unityContainer.RegisterType<IProviderModel, ProviderModel>();
            _unityContainer.RegisterType<IProviderItemModel, ProviderItemModel>();
            _unityContainer.RegisterType<IPublicCatalogDetailModel, PublicCatalogDetailModel>();
            _unityContainer.RegisterType<IRelationshipModel, RelationshipModel>();
            _unityContainer.RegisterType<IRememberModel, RememberModel>();
            _unityContainer.RegisterType<ITagModel, TagModel>();
            _unityContainer.RegisterType<ITransactionCausalModel, TransactionCausalModel>();
            _unityContainer.RegisterType<ITransactionMasterConceptModel, TransactionMasterConceptModel>();
            _unityContainer.RegisterType<ITransactionMasterDenominationModel, TransactionMasterDenominationModel>();
            _unityContainer.RegisterType<ITransactionMasterDetailCreditModel, TransactionMasterDetailCreditModel>();
            _unityContainer.RegisterType<ITransactionMasterDetailModel, TransactionMasterDetailModel>();
            _unityContainer.RegisterType<ITransactionMasterInfoModel, TransactionMasterInfoModel>();
            _unityContainer.RegisterType<ITransactionMasterModel, TransactionMasterModel>();
            _unityContainer.RegisterType<ITransactionProfileDetailModel, TransactionProfileDetailModel>();
            _unityContainer.RegisterType<IUserTagModel, UserTagModel>();
            _unityContainer.RegisterType<IUserWarehouseModel, UserWarehouseModel>();
            _unityContainer.RegisterType<IWarehouseModel, WarehouseModel>();
            _unityContainer.RegisterType<IBankModel, BankModel>();
            _unityContainer.RegisterType<IBranchModel, BranchModel>();
            _unityContainer.RegisterType<ICatalogItemConvertionModel, CatalogItemConvertionModel>();
            _unityContainer.RegisterType<ICatalogItemModel, CatalogItemModel>();
            _unityContainer.RegisterType<ICatalogModel, CatalogModel>();
            _unityContainer.RegisterType<ICompanyComponentFlavorModel, CompanyComponentFlavorModel>();
            _unityContainer.RegisterType<ICompanyComponentModel, CompanyComponentModel>();
            _unityContainer.RegisterType<ICompanyDataViewModel, CompanyDataViewModel>();
            _unityContainer.RegisterType<ICompanyDefaultDataViewModel, CompanyDefaultDataViewModel>();
            _unityContainer.RegisterType<ICompanyModel, CompanyModel>();
            _unityContainer.RegisterType<ICompanySubElementAuditModel, CompanySubElementAuditModel>();
            _unityContainer.RegisterType<IComponentAuditDetailModel, ComponentAuditDetailModel>();
            _unityContainer.RegisterType<IComponentAuditModel, ComponentAuditModel>();
            _unityContainer.RegisterType<IComponentAutorizationModel, ComponentAutorizationModel>();
            _unityContainer.RegisterType<IComponentModel, ComponentModel>();
            _unityContainer.RegisterType<ICounterModel, CounterModel>();
            _unityContainer.RegisterType<ICurrencyModel, CurrencyModel>();
            _unityContainer.RegisterType<IDataViewModel, DataViewModel>();
            _unityContainer.RegisterType<IElementModel, ElementModel>();
            _unityContainer.RegisterType<IExchangerateModel, ExchangerateModel>();
            _unityContainer.RegisterType<ILogModel, LogModel>();
            _unityContainer.RegisterType<IMembershipModel, MembershipModel>();
            _unityContainer.RegisterType<IRoleAutorizationModel, RoleAutorizationModel>();
            _unityContainer.RegisterType<ISubElementModel, SubElementModel>();
            _unityContainer.RegisterType<ITransactionConceptModel, TransactionConceptModel>();
            _unityContainer.RegisterType<ITransactionModel, TransactionModel>();
            _unityContainer.RegisterType<IUserModel, UserModel>();
            _unityContainer.RegisterType<IRoleModel, RoleModel>();
            _unityContainer.RegisterType<IUserPermissionModel, UserPermissionModel>();
            _unityContainer.RegisterType<IMenuElementModel, MenuElementModel>();
            _unityContainer.RegisterType<IWorkflowModel, WorkflowModel>();
            _unityContainer.RegisterType<IWorkflowStageModel, WorkflowStageModel>();
            _unityContainer.RegisterType<IWorkflowStageRelationModel, WorkflowStageRelationModel>();
            _unityContainer.RegisterType<IParameterModel, ParameterModel>();

            #endregion

            #region CDI_LIBRARIES

            _unityContainer.RegisterType<ICoreWebMenu, CoreWebMenu>();
            _unityContainer.RegisterType<ICoreWebAuthentication, CoreWebAuthentication>();
            _unityContainer.RegisterType<ICoreWebAccounting, CoreWebAccounting>();
            _unityContainer.RegisterType<ICoreWebParameter, CoreWebParameter>();
            _unityContainer.RegisterType<ICoreWebPermission, CoreWebPermission>();
            _unityContainer.RegisterType<ICoreWebCatalog, CoreWebCatalog>();
            _unityContainer.RegisterType<ICoreWebWorkflow, CoreWebWorkflow>();
            _unityContainer.RegisterType<ICoreWebTools, CoreWebTools>();
            _unityContainer.RegisterType<ICoreWebTransaction, CoreWebTransaction>();
            _unityContainer.RegisterType<IBdModel, BdModel>();
            _unityContainer.RegisterType<ICoreWebInventory, CoreWebInventory>();
            _unityContainer.RegisterType<ICoreWebCurrency, CoreWebCurrency>();
            _unityContainer.RegisterType<ICoreWebCounter, CoreWebCounter>();
            _unityContainer.RegisterType<ICoreWebConvertion, CoreWebConvertion>();
            _unityContainer.RegisterType<ICoreWebConcept, CoreWebConcept>();
            _unityContainer.RegisterType<ICoreWebAuditoria, CoreWebAuditoria>();
            _unityContainer.RegisterType<ICoreWebFinancialAmort, CoreWebFinancialAmort>();
            _unityContainer.RegisterType<ICoreWebAmortization, CoreWebAmortization>();
            _unityContainer.RegisterType<ICoreWebTransactionMasterDetail, CoreWebTransactionMasterDetail>();
            _unityContainer.RegisterType<ICoreWebWhatsap, CoreWebWhatsap>();

            #endregion
        }

        public static VariablesGlobales Instance { get; } = new();

        public static string? ConnectionString
        {
            get
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                return builder.GetConnectionString("ConnectionString");
            }
        }

        public static IConfigurationSection ConfigurationBuilder => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("globals");

        public IUnityContainer UnityContainer
        {
            get { return _unityContainer; }
        }

        public TbUser? User { get; set; }

        public TbCompany? Company { get; set; }

        public TbBranch? Branch { get; set; }

        public TbMembership? Membership { get; set; }

        public TbRole? Role { get; set; }
        public List<TbMenuElement>? ListMenuTop { get; set; }
        public List<TbMenuElement>? ListMenuLeft { get; set; }
        public List<TbMenuElement>? ListMenuBodyReport { get; set; }
        public List<TbMenuElement>? ListMenuHiddenPopup { get; set; }
        public string? MessageLogin { get; set; }
        public string? ParameterLabelSystem { get; set; }
        public List<string>? SubMenu { get; set; }

        
    }
}