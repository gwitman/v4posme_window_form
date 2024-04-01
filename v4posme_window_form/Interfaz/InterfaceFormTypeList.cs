using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace v4posme_window.Interfaz
{
    internal interface InterfaceFormTypeList
    {
        GridView objControlGridView { get; set; }

        void List();
    }
}
