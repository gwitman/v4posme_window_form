
using DevExpress.XtraGrid;

namespace v4posme_window.Interfaz
{
    public interface IFormTypeList
    {
        int? DataViewId { get; set; }
        DateTime? Fecha { get; set; }
        GridControl ObjGridControl { get; set; }
        void List();
        void Delete(object? sender, EventArgs? args);
        void Edit(object? sender, EventArgs? args);
        void New(object? sender, EventArgs? args);
        void PreRender();

        // Manejador de excepciones global para excepciones no controladas en subprocesos de la interfaz de usuario
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e);


        // Manejador de excepciones global para excepciones no controladas en subprocesos no manejados
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e);

    }
}
