using System.Diagnostics;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_window.Properties;
namespace v4posme_window_form.Views
{
    public partial class PrincipalForm : FluentDesignForm
    {
        private readonly ICompanyModel _companyModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();
        private readonly IBranchModel _branchModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchModel>();
        private readonly IMembershipModel _membershipModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipModel>();
        private readonly ICoreWebMenu _coreWebMenu = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebMenu>();

        public PrincipalForm()
        {
            InitializeComponent();
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            Debug.Assert(VariablesGlobales.Instance.User != null, "VariablesGlobales.Instance.User != null");
            VariablesGlobales.Instance.Company = _companyModel.GetRowByPk(VariablesGlobales.Instance.User.CompanyId);
            VariablesGlobales.Instance.Branch = _branchModel.GetRowByPk(VariablesGlobales.Instance.User.BranchId, VariablesGlobales.Instance.User.CompanyId);
            VariablesGlobales.Instance.Membership = _membershipModel.GetRowByCompanyIdBranchIdUserId(VariablesGlobales.Instance.User.CompanyId,
                VariablesGlobales.Instance.User.BranchId, VariablesGlobales.Instance.User.UserId);
            barCompanyNane.Caption = Resources.PrincipalForm_Compañía_Titulo + VariablesGlobales.Instance.Company!.Name + "-" + VariablesGlobales.Instance.Branch.Name;
            foreach (var item in _coreWebMenu.RenderMenuLeft(VariablesGlobales.Instance.Company, VariablesGlobales.Instance.ListMenuLeft))
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
