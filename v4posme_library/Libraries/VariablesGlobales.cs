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
            _unityContainer.RegisterType<IParameterModel, ParameterModel>();

            #endregion

            #region CDI_LIBRARIES

            _unityContainer.RegisterType<IMembershipService, MembershipService>();
            _unityContainer.RegisterType<IRoleService, RoleService>();
            _unityContainer.RegisterType<IElementSevice, ElementService>();
            _unityContainer.RegisterType<ICoreMenuService, CoreMenuService>();
            _unityContainer.RegisterType<IUserPermissionService, UserPermissionService>();
            _unityContainer.RegisterType<IMenuElementModelService, MenuElementModelService>();
            _unityContainer.RegisterType<ICoreWebAuthentication, CoreWebAuthentication>();
            _unityContainer.RegisterType<ICompanyService, CompanyService>();
            _unityContainer.RegisterType<ICoreWebParameter, CoreWebParameter>();
            _unityContainer.RegisterType<ICoreWebPermission, CoreWebPermission>();

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

        public void sendEmail()
        {
            var fromAddress = new MailAddress("from@gmail.com", "From Name");
            var toAddress = new MailAddress("to@example.com", "To Name");
            const string fromPassword = "fromPassword";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress))
            {
                message.Subject = subject;
                message.Body = body;
                smtp.Send(message);
            }
        }
    }
}