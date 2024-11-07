using v4posme_window.Dto;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views.Inventory.Inputunpost
{
    public partial class FormInventoryInputEditMasInfo : FormTypeHeadModal
    {
        private readonly FormInventoryInputTransactionMasterDetailDto transactionMasterDetailDto;

        public FormInventoryInputEditMasInfo()
        {
            InitializeComponent();
            transactionMasterDetailDto = new();
        }

        public FormInventoryInputEditMasInfo(FormInventoryInputTransactionMasterDetailDto selectedValue)
        {
            InitializeComponent();
            transactionMasterDetailDto = selectedValue;
            btnAceptar.Click += BtnAceptar_Click;
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            transactionMasterDetailDto.Lote = txtLote.Text;
            if (txtVencimiento.EditValue is DateTime fechaSeleccionada)
            {
                transactionMasterDetailDto.Vencimiento = fechaSeleccionada;
            }

            transactionMasterDetailDto.Precio2 = decimal.Parse(txtPrecio1.Text);
            transactionMasterDetailDto.Precio3 = decimal.Parse(txtPrecio2.Text);
            transactionMasterDetailDto.Reference4 = txtReference4TransactionMasterDetail.Text;
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
            txtVencimiento.EditValue = transactionMasterDetailDto.Vencimiento;
            txtLote.Text = transactionMasterDetailDto.Lote;
            txtPrecio1.EditValue = transactionMasterDetailDto.Precio2;
            txtPrecio2.EditValue = transactionMasterDetailDto.Precio3;
            txtReference4TransactionMasterDetail.Text = transactionMasterDetailDto.Reference4;
        }
    }
}