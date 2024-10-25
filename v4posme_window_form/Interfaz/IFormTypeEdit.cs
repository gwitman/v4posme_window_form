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
        void PreRender();
        void SaveInsert();
        void SaveUpdate();
        void LoadNew();
        void LoadEdit();
        void LoadRender(TypeRender typeRedner);
        void ComandDelete();
        void ComandPrinter();
        void CommandNew(object? sender, EventArgs e);
        void CommandSave(object? sender, EventArgs e);
        void CommandRegresar(object? sender, EventArgs e);

        void InitializeControl();
        // Manejador de excepciones global para excepciones no controladas en subprocesos de la interfaz de usuario
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e);

        // Manejador de excepciones global para excepciones no controladas en subprocesos no manejados
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e);

    }
}
