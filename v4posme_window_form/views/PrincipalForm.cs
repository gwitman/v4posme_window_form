﻿using System.Diagnostics;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
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
        private readonly ICompanyModel _companyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();
        private readonly IBranchModel _branchModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchModel>();
        private readonly IMembershipModel _membershipModel = VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipModel>();
        private readonly ICoreWebMenu _coreWebMenu = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebMenu>();

        public PrincipalForm()
        {
            InitializeComponent();
        }

        private async void PrincipalForm_Load(object sender, EventArgs e)
        {
            var coreWebRender = new CoreWebRenderInView();
            if (VariablesGlobales.Instance.User is null)
            {
                coreWebRender.GetMessageAlert(TypeError.Error, "Error", "NO se pudo cargar los datos de usuario, inicie sesion nuevamente", this);
                return;
            }

            VariablesGlobales.Instance.Company = _companyModel.GetRowByPk(VariablesGlobales.Instance.User.CompanyID);
            VariablesGlobales.Instance.Branch = _branchModel.GetRowByPk(VariablesGlobales.Instance.User.BranchID, VariablesGlobales.Instance.User.CompanyID);
            VariablesGlobales.Instance.Membership = _membershipModel.GetRowByCompanyIdBranchIdUserId(VariablesGlobales.Instance.User.CompanyID, VariablesGlobales.Instance.User.BranchID, VariablesGlobales.Instance.User.UserID);
            barStaticItemTitulo.Caption = VariablesGlobales.Instance.Company!.Name + @"-" + VariablesGlobales.Instance.Branch.Name + $@"(Usuario: {VariablesGlobales.Instance.User.Nickname})";
            var menuElementModel = VariablesGlobales.Instance.UnityContainer.Resolve<IMenuElementModel>();
            splashScreenManager.ShowWaitForm();
            await Task.Run(() =>
            {
                if (VariablesGlobales.Instance.ListMenuLeft is not null)
                {
                    accordionControl1.Invoke((MethodInvoker)delegate
                    {
                        CoreWebRenderInView.RenderMenuLeft(VariablesGlobales.Instance.ListMenuLeft, accordionControl1, svgImageCollection);
                        accordionControl1.ElementClick += accordionControl1_ElementClick;
                    });
                }

                if (VariablesGlobales.Instance.ListMenuTop is not null)
                {
                    ribbonControl1.Invoke((MethodInvoker)delegate { CoreWebRenderInView.RenderMenuTop(VariablesGlobales.Instance.ListMenuTop, ribbonControl1); });
                }
            });

            //Abrir un formulario por defecto
            var formularioDefaultValue = CoreFormList
                .GetFormulario(VariablesGlobales.Instance.Role!.UrlDefault);

            if (formularioDefaultValue is null) return;
            formularioDefaultValue.MdiParent = this;
            formularioDefaultValue.Show();
            splashScreenManager.CloseWaitForm();
        }

        private void accordionControl1_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
            if (e.Element.Name == null) return;

            var filterForm = CoreFormList.GetFormulario(e.Element.Name);
            if (filterForm is null) return;
            filterForm.MdiParent = this;
            if (IsFormOpen(filterForm.Name))
            {
                foreach (var frm in this.MdiChildren)
                {
                    if (frm.Name != filterForm.Name) continue;
                    frm.BringToFront();
                    frm.WindowState = FormWindowState.Maximized;
                    return;

                }
            }
            
            filterForm.Show();
        }

        public bool IsFormOpen(string formType)
        {
            return Application.OpenForms.Cast<Form>().Any(form => form.Name == formType);
        }
    }
}