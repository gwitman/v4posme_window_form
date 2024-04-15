using DevExpress.Utils.Filtering.Internal;
using DevExpress.XtraReports.Design.ParameterEditor;
using DevExpress.XtraRichEdit.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Implementacion;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;


namespace v4posme_window.Views
{
    public partial class FormInvoiceBillingEdit : Form, IFormTypeEdit
    {
        private int CompanyId { get; set; }
        private int TransactionId { get; set; }
        private int TransactionMasterId { get; set; }
        private TypeOpenForm TypeOpen { get; set; }

        public FormInvoiceBillingEdit(TypeOpenForm typeOpen, int companyId, int transactionId, int transactionMasterId)
        {
            InitializeComponent();
            CompanyId = companyId;
            TransactionId = transactionId;
            TransactionMasterId = transactionMasterId;
            TypeOpen = typeOpen;
        }


        public void ComandDelete()
        {
            if (VariablesGlobales.Instance.User is null)
            {
                return;
            }

            try
            {
                //Libreria Window
                var objInterfazCoreWebRenderInView = new CoreWebRenderInView();
                //Helper Dll
                var objInterfazWebToolsHelper = new WebToolsHelper();

                //Libreria DLL
                var objInterfazCoreWebPermission =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
                var objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
                var objInterfazCoreWebParameter =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
                var objInterfazCoreWebAccounting =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAccounting>();
                var objInterfazCoreWebTransaction =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
                var objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();

                //Libreria Model Custom DLL
                var objInterfazTransactionMaster =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();
                var objInterfazTransactionMasterDetail =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailModel>();
                var objInterfazCustomerCreditDocument =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentModel>();
                var objInterfazCustomerCredit =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>();
                var objInterfazCustomerCreditLine =
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditLineModel>();
                var objInterfazCustomer = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();


                using DataContext objDataContext = new DataContext();
                var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
                var commandEliminable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"]);
                var commandAplicable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);
                var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
                var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
                var resultPermission = 0;

                if (appNeedAuthentication == "true")
                {
                    var permited = objInterfazCoreWebPermission.UrlPermited("app_invoice_billing", "delete", urlSuffix!,
                        VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                        VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                        VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (!permited)
                    {
                        objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Permisos",
                            "No tiene acceso a controlador", this);
                        return;
                    }

                    resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_invoice_billing", "delete",
                        urlSuffix!,
                        VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                        VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                        VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                        VariablesGlobales.Instance.ListMenuHiddenPopup);

                    if (resultPermission == permissionNone)
                    {
                        objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Permisos",
                            "No se encontraron permisos", this);
                        return;
                    }
                }

                //Obtener variables de autenticacion
                var objUser = VariablesGlobales.Instance.User;
                var objRole = VariablesGlobales.Instance.Role;

                //Validacion
                if (CompanyId == 0 && TransactionId == 0 && TransactionMasterId == 0)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"]);
                }

                //Obtener registros
                var objTm = objInterfazTransactionMaster.GetRowByPk(CompanyId, TransactionId, TransactionMasterId);
                var objCustomerCreditDocument =
                    objInterfazCustomerCreditDocument.GetRowByDocument(objTm.CompanyId, objTm.EntityId!.Value,
                        objTm.TransactionNumber!);

                //Validaciones
                if (resultPermission == permissionNone && (objTm.CreatedBy != objUser.UserId))
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
                }

                if (objInterfazCoreWebAccounting.CycleIsCloseByDate(objUser.CompanyId, objTm.CreatedOn!.Value))
                {
                    throw new Exception("EL DOCUMENTO NO PUEDE SE ELIMINADO, EL CICLO CONTABLE ESTA CERRADO");
                }

                if (!objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing", "statusID",
                            objTm.StatusId!.Value, commandEliminable, objUser.CompanyId, objUser.BranchId,
                            objRole.RoleId)!
                        .Value)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
                }

                ////Validar si la factura es de credito y esta aplicada y tiene abono	
                var parameterCausalTypeCredit =
                    objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", objUser.CompanyId);
                var causalIdTypeCredit = parameterCausalTypeCredit!.Value.Split(",");
                var exisCausalInCredit =
                    causalIdTypeCredit.Any(elemento => elemento == objTm.TransactionCausalId.ToString());

                var validateWorkflowStage = objInterfazCoreWebWorkflow.ValidateWorkflowStage(
                    "tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, commandAplicable,
                    objUser.CompanyId, objUser.BranchId, objRole.RoleId
                )!.Value;
                if (
                    validateWorkflowStage &&
                    exisCausalInCredit &&
                    objCustomerCreditDocument.Amount != objCustomerCreditDocument.Balance &&
                    objCustomerCreditDocument.Balance > 0
                )
                {
                    throw new Exception("Factura con abonos y balance mayor que 1");
                }

                if (validateWorkflowStage)
                {
                    //Actualizar fecha en la transacciones oroginal
                    TbTransactionMaster dataNewTM = new TbTransactionMaster();
                    dataNewTM = objDataContext.TbTransactionMasters.First(u =>
                        u.CompanyId == objTm.CompanyId && u.TransactionId == objTm.TransactionId &&
                        u.TransactionMasterId == objTm.TransactionMasterId);
                    dataNewTM.StatusIdchangeOn = DateTime.Now;
                    objInterfazTransactionMaster.UpdateAppPosme(dataNewTM.CompanyId, dataNewTM.TransactionId,
                        dataNewTM.TransactionMasterId, dataNewTM);

                    var transactionIDRevert =
                        objInterfazCoreWebParameter.GetParameter("INVOICE_TRANSACTION_REVERSION_TO_BILLING",
                            objUser.CompanyId);
                    var transactionIDRevertValue = Convert.ToInt32(transactionIDRevert.Value);
                    objInterfazCoreWebTransaction.CreateInverseDocumentByTransaccion(objTm.CompanyId,
                        objTm.TransactionId, objTm.TransactionMasterId, transactionIDRevertValue, 0);


                    if (exisCausalInCredit)
                    {
                        //Valores de tasa de cambio          
                        var objCurrencyDolares = objInterfazCoreWebCurrency.GetCurrencyExternal(objTm.CompanyId);
                        var objCurrencyCordoba = objInterfazCoreWebCurrency.GetCurrencyDefault(objTm.CompanyId);
                        var dateOn = DateOnly.FromDateTime(DateTime.Now);
                        var exchangeRate = objInterfazCoreWebCurrency.GetRatio(objTm.CompanyId, dateOn, 1,
                            objCurrencyDolares.CurrencyId, objCurrencyCordoba.CurrencyId);

                        //cancelar el documento de credito					
                        var shareDocumentAnuladoStatusID = Convert.ToInt32(objInterfazCoreWebParameter
                            .GetParameter("SHARE_DOCUMENT_ANULADO", objUser.CompanyId).Value);
                        var objCustomerCredotDocumentNew = objDataContext.TbCustomerCreditDocuments.First(u =>
                            u.CustomerCreditDocumentId == objCustomerCreditDocument.CustomerCreditDocumentId);
                        objCustomerCredotDocumentNew.StatusId = shareDocumentAnuladoStatusID;
                        objInterfazCustomerCreditDocument.UpdateAppPosme(
                            objCustomerCreditDocument.CustomerCreditDocumentId!.Value, objCustomerCredotDocumentNew);

                        var amountDol = objCustomerCreditDocument.Balance / exchangeRate;
                        var amountCor = objCustomerCreditDocument.Balance;


                        //aumentar el blance de la linea
                        var objCustomerCreditLineNew = objDataContext.TbCustomerCreditLines.First(u =>
                            u.CustomerCreditLineId == objCustomerCreditDocument.CustomerCreditLineId);
                        objCustomerCreditLineNew.Balance = objCustomerCreditLineNew.Balance +
                                                           (objCustomerCreditLineNew.CurrencyId ==
                                                            objCurrencyDolares.CurrencyId
                                                               ? amountDol!.Value
                                                               : amountCor!.Value);
                        objInterfazCustomerCreditLine.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditLineId,
                            objCustomerCreditLineNew);


                        //aumentar el balance de credito
                        var objCustomerCredit = objDataContext.TbCustomerCredits.First(u =>
                            u.CompanyId == objTm.CompanyId && u.BranchId == objTm.BranchId &&
                            u.EntityId == objTm.EntityId);
                        objCustomerCredit.BalanceDol = objCustomerCredit.BalanceDol + amountDol!.Value;
                        objInterfazCustomerCredit.UpdateAppPosme(objCustomerCredit.CompanyId,
                            objCustomerCredit.BranchId, objCustomerCredit.EntityId, objCustomerCredit);
                    }
                }
                else
                {
                    //	//Eliminar el Registro			
                    objInterfazTransactionMaster.DeleteAppPosme(objUser.CompanyId, objTm.TransactionId,
                        objTm.TransactionMasterId);
                    objInterfazTransactionMasterDetail.DeleteWhereTm(objUser.CompanyId, objTm.TransactionId,
                        objTm.TransactionMasterId);
                }
            }
            catch (Exception ex)
            {
                var objCoreWebRenderInView = new CoreWebRenderInView();
                Console.WriteLine(ex);
                objCoreWebRenderInView.GetMessageAlert(TypeError.Error, @"Error eliminando", ex.ToString(), this);
            }
        }

        public void ComandPrinter()
        {
            throw new NotImplementedException();
        }

        public void LoadEdit()
        {
            var c = 0;
        }

        public void LoadNew()
        {
            var c = 0;
        }

        public void SaveInsert()
        {
            throw new NotImplementedException();
        }

        public void SaveUpdate()
        {
            throw new NotImplementedException();
        }


        public void PreRender()
        {
            var imagenInvoice = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_POSME"];
            if (imagenInvoice is not null)
            {
                if (File.Exists(imagenInvoice))
                {
                    pictureEdit2.Image = Image.FromFile(imagenInvoice);
                }
            }

            var imageCustomer = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_CUSTOMER"];
            if (imageCustomer is not null)
            {
                if (File.Exists(imageCustomer))
                {
                    pictureEdit1.Image = Image.FromFile(imageCustomer);
                }
            }
        }


        private void EventoCallBackAceptar(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            var objWebToolsHelper = new WebToolsHelper();
            MessageBox.Show("Evento en el formulario hijo: " +
                            objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
        }


        private void FormInvoiceBillingEdit_Load(object sender, EventArgs e)
        {
            if (TypeOpen == TypeOpenForm.Init)
            {
                PreRender();
            }

            if (TypeOpen == TypeOpenForm.Init && TransactionMasterId > 0)
            {
                LoadEdit();
            }

            if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0)
            {
                LoadNew();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch("Lista de Productos", 33,
                "SELECCIONAR_ITEM_BILLING_POPUP_INVOICE", true,
                "{warehouseID:4,listPriceID:12,typePriceID:154,currencyID:1}", false, "", 0, 5, "");
            formTypeListSearch.EventoCallBackAceptar_ += EventoCallBackAceptar;
            formTypeListSearch.ShowDialog(this);
        }


        private void lblTitulo_Click(object sender, EventArgs e)
        {
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tablePanel2_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}