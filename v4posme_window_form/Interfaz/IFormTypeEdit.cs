using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Interfaz
{
    public interface IFormTypeEdit
    {
        void SaveInsert();
        void SaveUpdate();
        void LoadNew();
        void LoadEdit();
        void ComandDelete();
        void ComandPrinter();
    }
}
