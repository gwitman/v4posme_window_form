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
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views
{
    public partial class FormCustomerEdit : FormTypeHeadEdit, IFormTypeEdit
    {
        private TypeRender _typeRender;
        private int _entityId;

        public FormCustomerEdit()
        {
            InitializeComponent();
        }

        public FormCustomerEdit(TypeRender typeRender, int entityId)
        {
            InitializeComponent();
            _typeRender = typeRender;
            _entityId = entityId;
        }

        public void PreRender()
        {
            throw new NotImplementedException();
        }

        public void SaveInsert()
        {
            throw new NotImplementedException();
        }

        public void SaveUpdate()
        {
            throw new NotImplementedException();
        }

        public void LoadNew()
        {
            throw new NotImplementedException();
        }

        public void LoadEdit()
        {
            throw new NotImplementedException();
        }

        public void LoadRender(TypeRender typeRedner)
        {
            throw new NotImplementedException();
        }

        public void ComandDelete()
        {
            throw new NotImplementedException();
        }

        public void ComandPrinter()
        {
            throw new NotImplementedException();
        }

        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}