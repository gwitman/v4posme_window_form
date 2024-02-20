using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_window.Properties;
namespace v4posme_window_form.Views
{
    public partial class PrincipalForm : FluentDesignForm
    {
        private readonly ICompanyService _companyService = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyService>();
        private readonly IBranchModel _branchModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchModel>();
        private readonly IMembershipService _membershipService = VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipService>();
        private readonly ICoreMenuService _coreMenuService = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreMenuService>();

        public PrincipalForm()
        {
            InitializeComponent();
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            VariablesGlobales.Instance.Company = _companyService.GetRowByPk(VariablesGlobales.Instance.User.CompanyId);
            VariablesGlobales.Instance.Branch = _branchModel.findById(VariablesGlobales.Instance.User.BranchId);
            VariablesGlobales.Instance.Membership = _membershipService.GetRowByCompanyIdBranchIdUserId(VariablesGlobales.Instance.User.CompanyId,
                VariablesGlobales.Instance.User.BranchId, VariablesGlobales.Instance.User.UserId);
            barCompanyNane.Caption = Resources.PrincipalForm_Compañía_Titulo + VariablesGlobales.Instance.Company.Name + "-" + VariablesGlobales.Instance.Branch.Name;
            foreach (var item in _coreMenuService.RenderMenuLeft(VariablesGlobales.Instance.Company, VariablesGlobales.Instance.ListMenuLeft))
            {
                menuElement.Elements.Add(new AccordionControlElement()
                {
                    Text = item,
                    Style = ElementStyle.Item
                });
            }

        }

    }
}
