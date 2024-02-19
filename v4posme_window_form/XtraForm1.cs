using DevExpress.XtraEditors;
namespace v4posme_window
{
    public partial class XtraForm1 : XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            using (var dbContext = new v4posme_library.Models.DataContext())
            {
                foreach (var tbCompany in dbContext.TbCompanies.ToList())
                {
                    MessageBox.Show(tbCompany.Name);
                }
            }
        }
    }
}
