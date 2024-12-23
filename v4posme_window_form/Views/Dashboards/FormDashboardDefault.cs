using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraCharts;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_window.Libraries;
using Unity;
using v4posme_library.ModelsDto;
using System.Diagnostics;
using System.Globalization;

namespace v4posme_window.Views.Dashboards
{
    public partial class FormDashboardDefault : DevExpress.XtraEditors.XtraForm
    {
        #region Campos

        private BackgroundWorker backgroundWorker;
        private DateTime firstDateYear = new(DateTime.Now.Year, 1, 1);
        private DateTime lastDateYear = new(DateTime.Now.Year, 12, 31);
        private DateTime firstDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
        private DateTime lastDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

        #endregion

        #region Modelos

        public string? ObjParameterLogo { get; set; }

        public decimal ObjExchangeRateCordobaDolar { get; set; }

        public decimal ObjExchangeRateDolarACordoba { get; set; }

        public string? ObjParameterPriceByInvoice { get; set; }

        public string? ObjParameterPrice { get; set; }

        public string? ObjParameterVersion { get; set; }

        public string? ObjParameterCreditos { get; set; }

        public DateTime ObjParameterExpiredLicense { get; set; }

        public DateTime ParameterFechaExpiration { get; set; }

        public string? ObjParameterTipoPlan { get; set; }

        public string? ObjParameterISleep { get; set; }

        public string? ObjParameterMAX_USER { get; set; }

        public List<TbTransactionMasterDetailDto> ObjListVentaContadoMensuales { get; set; }

        public List<TbTransactionMasterDetailDto> ObjListVentasContadoMesActual { get; set; }

        public List<TbTransactionMasterDetailDto> ObjListVentasCreditoMensuales { get; set; }

        public List<TbTransactionMasterDetailDto> ObjPagosMensuales { get; set; }

        public string ObjUserEmail { get; set; } = string.Empty;

        public string ObjUserNickname { get; set; } = string.Empty;

        #endregion

        #region Librerias

        private CoreWebRenderInView coreWebRender = new CoreWebRenderInView();
        private readonly ICoreWebParameter coreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        private readonly ICoreWebCurrency coreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();
        private readonly ICoreWebPermission coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebTools coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebView coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
        private readonly ITransactionMasterDetailModel transactionMasterDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailModel>();
        private static readonly string? AppNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        private static readonly int? permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        private static readonly string? urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
        private static readonly string? userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];

        #endregion

        #region Init

        public FormDashboardDefault()
        {
            InitializeComponent();
            ObjListVentaContadoMensuales = new();
            ObjListVentasCreditoMensuales = new();
            ObjPagosMensuales = new();
        }

        private void FormDashboardDefault_Load(object sender, EventArgs e)
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (ob, ev) => { Index(); };
            backgroundWorker.RunWorkerCompleted += (obb, evb) =>
            {
                // Ocultar el mensaje de carga
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }

                // Verificar si hubo algún error durante la carga de datos
                if (evb.Error is not null)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Error", $@"Error al cargar datos: {evb.Error.Message}", this);
                    return;
                }

                if (evb.Cancelled)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Error", $@"Operación cancelada por el usuario", this);
                    return;
                }

                // Actualizar la interfaz de usuario con los datos cargados
                PreRender();
                Render();
            };
            progressPanel.Size = Size;
            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }

            backgroundWorker.RunWorkerAsync();
        }

        #endregion

        #region Metodos

        private void Index()
        {
            var resultPermission = 0;
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception(userNotAutenticated);
            }

            if (AppNeedAuthentication!.Equals("true"))
            {
                var permited = coreWebPermission.UrlPermited("core_dashboards", "index", urlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"]);
                }

                resultPermission = coreWebPermission.UrlPermissionCmd("core_dashboards", "index", urlSuffix!,
                    VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_FUNCTION"]);
                }
            }

            // Obtener las fechas iniciales
            var objFirstYearDate = firstDateYear;
            var objFirstDate = firstDate;

            ObjParameterMAX_USER = coreWebParameter.GetParameterValue("CORE_CUST_PRICE_MAX_USER", user.CompanyID);
            var priceLicensesExpired = coreWebParameter.GetParameterValue("CORE_CUST_PRICE_LICENCES_EXPIRED", user.CompanyID);
            ParameterFechaExpiration = string.IsNullOrWhiteSpace(priceLicensesExpired) ? DateTime.MinValue : Convert.ToDateTime(priceLicensesExpired);
            ObjParameterExpiredLicense = string.IsNullOrWhiteSpace(priceLicensesExpired) ? DateTime.MinValue : Convert.ToDateTime(priceLicensesExpired);
            ObjParameterISleep = coreWebParameter.GetParameterValue("CORE_CUST_PRICE_SLEEP", user.CompanyID);
            ObjParameterTipoPlan = coreWebParameter.GetParameterValue("CORE_CUST_PRICE_TIPO_PLAN", user.CompanyID);
            ObjParameterCreditos = coreWebParameter.GetParameterValue("CORE_CUST_PRICE_BALANCE", user.CompanyID);
            ObjParameterVersion = coreWebParameter.GetParameterValue("CORE_CUST_PRICE_VERSION", user.CompanyID);
            ObjParameterPrice = coreWebParameter.GetParameterValue("CORE_CUST_PRICE", user.CompanyID);
            ObjParameterPriceByInvoice = coreWebParameter.GetParameterValue("CORE_CUST_PRICE_BY_INVOICE", user.CompanyID);
            ObjParameterLogo = coreWebParameter.GetParameterValue("CORE_COMPANY_LOGO", user.CompanyID);
            var objCurrency = coreWebCurrency.GetCurrencyDefault(user.CompanyID);
            var targetCurrency = coreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjExchangeRateDolarACordoba = coreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
            ObjExchangeRateCordobaDolar = coreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objCurrency.CurrencyID, targetCurrency.CurrencyID);

            ObjListVentasContadoMesActual = transactionMasterDetailModel.DefaultVentasDeContadoMesActual(user.CompanyID, objFirstDate.Date, DateTime.Now.Date);

            while (objFirstYearDate <= objFirstDate)
            {
                var objLastDayMont = objFirstYearDate.AddMonths(1).AddDays(-1);
                var objListVentaContadoMensualTemporal = transactionMasterDetailModel.DefaultVentasDeContadoMensuales(user.CompanyID, objFirstYearDate.Date, objLastDayMont.Date);
                if (objListVentaContadoMensualTemporal.Count > 0)
                {
                    ObjListVentaContadoMensuales.Add(objListVentaContadoMensualTemporal[0]);
                }

                objFirstYearDate = objFirstYearDate.AddMonths(1);
            }

            objFirstYearDate = firstDateYear;
            objFirstDate = firstDate;
            while (objFirstYearDate <= objFirstDate)
            {
                var objLastDayMont = objFirstYearDate.AddMonths(1).AddDays(-1);
                var objListVentaCreditoMensualTemporal = transactionMasterDetailModel.DefaultVentasDeCreditoMesActual(user.CompanyID, objFirstYearDate.Date, objLastDayMont.Date);
                if (objListVentaCreditoMensualTemporal.Count > 0)
                {
                    ObjListVentasCreditoMensuales.Add(objListVentaCreditoMensualTemporal[0]);
                }

                objFirstYearDate = objFirstYearDate.AddMonths(1);
            }

            objFirstYearDate = firstDateYear;
            objFirstDate = firstDate;
            while (objFirstYearDate <= objFirstDate)
            {
                var objLastDayMont = objFirstYearDate.AddMonths(1).AddDays(-1);
                var objListCapitalMensualTemporal = transactionMasterDetailModel.DefaultPagossMensuales(user.CompanyID, objFirstYearDate.Date, objLastDayMont.Date);
                if (objListCapitalMensualTemporal.Count > 0)
                {
                    ObjPagosMensuales.Add(objListCapitalMensualTemporal[0]);
                }

                objFirstYearDate = objFirstYearDate.AddMonths(1);
            }

            ObjUserNickname = user.Nickname;
            ObjUserEmail = user.Email;
        }

        private void PreRender()
        {
            // Abrir todos los elementos del AccordionControl
            ExpandAllAccordionElements(accordionControl1);
            ExpandAllAccordionElements(accordionControl2);
        }

        private void Render()
        {
            var pathOfLogo = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP_ROOT"];
            var company = VariablesGlobales.Instance.Company;

            //Grupo compania
            accordionControlElementCompany.Text = company.Name;
            if (ObjParameterLogo is not null)
            {
                var imagePath = $"{pathOfLogo}/img/logos/{ObjParameterLogo!}";
                if (File.Exists(imagePath))
                {
                    var logoCompany = new Bitmap(Image.FromFile(imagePath));
                    picLogo.Image = logoCompany;
                }
            }

            //Parametros
            lblCORE_CUST_PRICE_SLEEP.Text = ObjParameterISleep ?? "";
            lblCORE_CUST_PRICE_TIPO_PLAN.Text = ObjParameterTipoPlan ?? "";
            lblCORE_CUST_PRICE_LICENCES_EXPIRED.Text = ObjParameterTipoPlan != "PERPETUA" ? ObjParameterExpiredLicense.ToShortDateString() : "PERPETUA";
            lblCORE_CUST_PRICE_BALANCE.Text = ObjParameterCreditos ?? "";
            lblCORE_CUST_PRICE_BY_INVOICE.Text = ObjParameterPriceByInvoice ?? "";
            lblCORE_CUST_PRICE_MAX_USER.Text = ObjParameterMAX_USER ?? "";
            lblCORE_CUST_PRICE_VERSION.Text = ObjParameterVersion ?? "";
            lblCORE_CUST_PRICE.Text = ObjParameterPrice ?? "";

            //Tipo de cambio
            lblDolarCordoba.Text = $@"VALOR: {ObjExchangeRateCordobaDolar:F4}";
            lblCordobaDolar.Text = $@"VALOR: {ObjExchangeRateDolarACordoba:F4}";

            DrawChartBarraVentasContadoMesActual();
            DrawChartPastelVentasContadoMensuales();
            DrawChartPastelVentasCreditoMensuales();
            DrawChartBarraCapitalMensual();

            lblNickname.Text = ObjUserNickname;
            lblEmail.Text = ObjUserEmail;
        }

        #endregion

        #region Funciones

        private void DrawChartBarraVentasContadoMesActual()
        {
            // Create a pie series.
            var series = new Series(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month), ViewType.Bar);
            // Bind the series to data.
            foreach (var dato in ObjListVentasContadoMesActual)
            {
                series.Points.Add(new SeriesPoint(dato.Dia, dato.Venta));
            }

            // Add the series to the chart.
            chartControlVentasContadoMesActual.Series.Add(series);
            series.Label.TextPattern = "{VP:p0} ({V:C})";
            // Configura el patrón de texto para las leyendas.
            series.LegendTextPattern = "{A}";
        }

        private void DrawChartPastelVentasContadoMensuales()
        {
            // Create a pie series.
            var series = new Series("Venta Contado Mes Actual", ViewType.Pie);
            // Bind the series to data.
            foreach (var dato in ObjListVentaContadoMensuales)
            {
                var mes = dato.Mes ?? 1;
                if (mes is < 1 or > 12)
                {
                    mes = 1;
                }

                series.Points.Add(new SeriesPoint(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes), dato.Venta));
            }

            // Add the series to the chart.
            chartControlVentasContadoMensuales.Series.Add(series);
            series.Label.TextPattern = "{VP:p0} ({V:C})";
            // Configura el patrón de texto para las leyendas.
            series.LegendTextPattern = "{A}";
        }

        private void DrawChartPastelVentasCreditoMensuales()
        {
            // Create a pie series.
            var series = new Series("Venta Credito Mensuales", ViewType.Pie);
            // Bind the series to data.
            foreach (var dato in ObjListVentasCreditoMensuales)
            {
                var mes = dato.Mes ?? 1;
                if (mes is < 1 or > 12)
                {
                    mes = 1;
                }

                series.Points.Add(new SeriesPoint(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes), dato.Venta));
            }

            // Add the series to the chart.
            chartControlVentasCreditoMensuales.Series.Add(series);
            series.Label.TextPattern = "{VP:p0} ({V:C})";
            // Configura el patrón de texto para las leyendas.
            series.LegendTextPattern = "{A}";
        }

        private void DrawChartBarraCapitalMensual()
        {
            // Create a pie series.
            var series = new Series("Pagos del Mes", ViewType.ScatterLine);
            // Bind the series to data.
            foreach (var dato in ObjPagosMensuales)
            {
                var mes = dato.Mes ?? 1;
                if (mes is < 1 or > 12)
                {
                    mes = 1;
                }

                series.Points.Add(new SeriesPoint(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes), dato.Pagos));
            }

            // Add the series to the chart.
            chartControlPagosMensuales.Series.Add(series);
            series.Label.TextPattern = "{VP:p0} ({V:C})";
            // Configura el patrón de texto para las leyendas.
            series.LegendTextPattern = "{A}";
        }

        // Método para expandir todos los elementos
        private void ExpandAllAccordionElements(AccordionControl accordion)
        {
            foreach (var element in accordion.Elements)
            {
                ExpandElement(element);
            }
        }

        // Expande un elemento y sus subelementos (recursivamente)
        private void ExpandElement(AccordionControlElement element)
        {
            if (element.Style == ElementStyle.Group)
            {
                element.Expanded = true; // Expandir el grupo
            }

            // Expandir subelementos si los hay
            foreach (var subElement in element.Elements)
            {
                ExpandElement(subElement);
            }
        }

        #endregion

        #region Eventos

        private void FormDashboardDefault_Resize(object sender, EventArgs e)
        {
            var nuevoAncho = this.ClientSize.Width / 2;
            panelControl1.Width = nuevoAncho;
        }

        private void btnWhatsApp_Click(object sender, EventArgs e)
        {
            var userEmail = VariablesGlobales.Instance.User.Email;
            var message = "Buenos días, le saluda " + userEmail + ": ";
            var url = $"https://wa.me/50587125827?text={Uri.EscapeDataString(message)}";
            // Abrir el enlace en el navegador predeterminado
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        #endregion
        
    }
}