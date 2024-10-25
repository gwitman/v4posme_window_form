using v4posme_window.Dto;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views
{
    public partial class FormInventoryInputEditMasInfo : FormTypeHeadModal
    {
        private readonly FormInventoryInputTransactionMasterDetailDto _transactionMasterDetailDto;

        public FormInventoryInputEditMasInfo()
        {
            InitializeComponent();
            _transactionMasterDetailDto = new();
        }

        public FormInventoryInputEditMasInfo(FormInventoryInputTransactionMasterDetailDto selectedValue)
        {
            InitializeComponent();
            _transactionMasterDetailDto = selectedValue;
            btnAceptar.Click += BtnAceptar_Click;
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            _transactionMasterDetailDto.Lote = txtLote.Text;
            if (txtVencimiento.EditValue is DateTime fechaSeleccionada)
            {
                _transactionMasterDetailDto.Vencimiento = fechaSeleccionada;
            }

            _transactionMasterDetailDto.Precio2 = decimal.Parse(txtPrecio1.Text);
            _transactionMasterDetailDto.Precio3 = decimal.Parse(txtPrecio2.Text);
            _transactionMasterDetailDto.Reference4 = txtReference4TransactionMasterDetail.Text;
            DialogResult = DialogResult.OK;
        }

        private void FormInventoryInputEditMasInfo_Load(object sender, EventArgs e)
        {
            PreRender();
            LoadValues();
        }

        private void PreRender()
        {
            HelperMethods.OnlyNumberDecimals(txtPrecio1);
            HelperMethods.OnlyNumberDecimals(txtPrecio2);
            txtVencimiento.EditValue = null;
        }

        private void LoadValues()
        {
            txtVencimiento.EditValue = _transactionMasterDetailDto.Vencimiento;
            txtLote.Text = _transactionMasterDetailDto.Lote;
            txtPrecio1.EditValue = _transactionMasterDetailDto.Precio2;
            txtPrecio2.EditValue = _transactionMasterDetailDto.Precio3;
            txtReference4TransactionMasterDetail.Text = _transactionMasterDetailDto.Reference4;
        }
    }
}