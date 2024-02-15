using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using System;
using Unity;
using v4posme_window_form.Domain;
using v4posme_window_form.Domain.Services;
using v4posme_window_form.Properties;
namespace v4posme_window_form.Views
{
    public partial class PrincipalForm : FluentDesignForm
    {
        private readonly ICompanyService _companyService = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyService>();
        private readonly IBranchService _branchService = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchService>();
        private readonly IMembershipService _membershipService = VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipService>();
        private readonly ICoreMenuService _coreMenuService = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreMenuService>();

        public PrincipalForm()
        {
            InitializeComponent();
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            VariablesGlobales.Instance.Company = _companyService.findById(VariablesGlobales.Instance.User.CompanyID);
            VariablesGlobales.Instance.Branch = _branchService.findById(VariablesGlobales.Instance.User.BranchID);
            VariablesGlobales.Instance.Membership = _membershipService.getRowByCompanyIDBranchIDUserID(VariablesGlobales.Instance.User.CompanyID,
                VariablesGlobales.Instance.User.BranchID, VariablesGlobales.Instance.User.UserID);
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
