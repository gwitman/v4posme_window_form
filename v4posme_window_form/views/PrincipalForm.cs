using DevExpress.Utils.MVVM;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using Unity;
using v4posme_window_form.Domain;
using v4posme_window_form.Domain.Services;

namespace v4posme_window_form.Views
{
    public partial class PrincipalForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private ICompanyService companyService = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyService>();
        private IBranchService branchService = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchService>();
        private IMembershipService membershipService = VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipService>();
        private readonly ICoreMenuService coreMenuService = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreMenuService>();
        
        public PrincipalForm()
        {
            InitializeComponent();
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            VariablesGlobales.Instance.Company = companyService.findById(VariablesGlobales.Instance.User.CompanyID);
            VariablesGlobales.Instance.Branch = branchService.findById(VariablesGlobales.Instance.User.BranchID);
            VariablesGlobales.Instance.Membership =
                membershipService.getRowByCompanyIDBranchIDUserID(VariablesGlobales.Instance.User.CompanyID, 
                VariablesGlobales.Instance.User.BranchID, VariablesGlobales.Instance.User.UserID);
            barCompanyNane.Caption = "Compañía: " + VariablesGlobales.Instance.Company.Name + "-" + VariablesGlobales.Instance.Branch.Name;
            foreach(var item in coreMenuService.RenderMenuLeft(VariablesGlobales.Instance.Company, VariablesGlobales.Instance.ListMenuLeft))
            {
                menuElement.Elements.Add(new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Text = item,
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
                });
            }
            
        }

    }
}
