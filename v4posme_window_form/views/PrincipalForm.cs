using System.Diagnostics;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraEditors;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_window.Libraries;
using v4posme_window.Properties;

namespace v4posme_window.Views
{
    public partial class PrincipalForm : XtraForm
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
            var coreWebRender = new CoreWebRenderInView();
            if (VariablesGlobales.Instance.User is null)
            {
                coreWebRender.GetMessageAlert(TypeMessage.Error, "Error",
                    "NO se pudo cargar los datos de usuario, inicie sesion nuevamente", this);
                return;
            }

            VariablesGlobales.Instance.Company = _companyModel.GetRowByPk(VariablesGlobales.Instance.User.CompanyId);
            VariablesGlobales.Instance.Branch = _branchModel.GetRowByPk(VariablesGlobales.Instance.User.BranchId,
                VariablesGlobales.Instance.User.CompanyId);
            VariablesGlobales.Instance.Membership = _membershipModel.GetRowByCompanyIdBranchIdUserId(
                VariablesGlobales.Instance.User.CompanyId, VariablesGlobales.Instance.User.BranchId,
                VariablesGlobales.Instance.User.UserId);
            barStaticItemTitulo.Caption = VariablesGlobales.Instance.Company!.Name + "-" +
                                          VariablesGlobales.Instance.Branch.Name +
                                          $@"(Usuario: {VariablesGlobales.Instance.User.Nickname})";
            var menuElementModel = VariablesGlobales.Instance.UnityContainer.Resolve<IMenuElementModel>();

            if (VariablesGlobales.Instance.ListMenuLeft is not null)
            {
                CoreWebRenderInView.RenderMenuLeft(VariablesGlobales.Instance.ListMenuLeft, accordionControl1);
                accordionControl1.ElementClick += accordionControl1_ElementClick;
            }

            if (VariablesGlobales.Instance.ListMenuTop is not null)
            {
                CoreWebRenderInView.RenderMenuTop(VariablesGlobales.Instance.ListMenuTop, ribbonControl1);
            }

            //Abrir un formulario por defecto
            var formularioDefaultValue = CoreFormList
                .Formularios()
                .FirstOrDefault(c => c.Key == VariablesGlobales.Instance.Role!.UrlDefault).Value;
            if (formularioDefaultValue is null) return;
            formularioDefaultValue.MdiParent = this;
            formularioDefaultValue.Show();
        }

        private void accordionControl1_ElementClick(object sender,
            DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
            if (e.Element.Name == null) return;
            var filterForm = (from keyValuePair in CoreFormList.Formularios()
                where e.Element.Name == keyValuePair.Key
                select keyValuePair.Value).FirstOrDefault();
            if (filterForm is null) return;
            if (IsFormOpen(filterForm.GetType())) return;
            filterForm.MdiParent = this;
            filterForm.Show();
        }

        private static bool IsFormOpen(Type formType)
        {
            return Application.OpenForms.Cast<Form>().Any(form => form.GetType() == formType);
        }
    }
}