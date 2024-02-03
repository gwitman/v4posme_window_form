using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using v4posme_window_form.Domain;
using v4posme_window_form.Domain.Services;

namespace v4posme_window_form.Views
{
    public partial class PrincipalForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private ICompanyService companyService = new CompanyService();
        private IBranchService branchService = new BranchService();

        public PrincipalForm()
        {
            InitializeComponent();
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            VariablesGlobales.Instance.Company = companyService.findById(VariablesGlobales.Instance.User.CompanyID);
            VariablesGlobales.Instance.Branch = branchService.findById(VariablesGlobales.Instance.User.BranchID);
            barCompanyNane.Caption = "Compañía: " + VariablesGlobales.Instance.Company.Name + "-" + VariablesGlobales.Instance.Branch.Name;
        }
    }
}
