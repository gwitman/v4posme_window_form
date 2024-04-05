using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Libraries;

namespace v4posme_window.Views
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
            var company = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();
            var coreWebRender = new CoreWebRenderInView();
            var dto = new TableCompanyDataViewDto
            {
                Data = company.GetRows(),
                Config = new TbCompanyDataview()
            };
            coreWebRender.RenderGrid(dto, "grid", 0, this);
        }
    }
}