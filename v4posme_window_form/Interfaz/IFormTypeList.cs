
using DevExpress.XtraGrid;

namespace v4posme_window.Interfaz
{
    public interface IFormTypeList
    {
        int? DataViewId { get; set; }
        DateTime? Fecha { get; set; }
        GridControl ObjGridControl { get; set; }
        // Manejador de excepciones global para excepciones no controladas en subprocesos de la interfaz de usuario
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e);


        // Manejador de excepciones global para excepciones no controladas en subprocesos no manejados
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e);
        void List();
        void RefreshData();
        void Delete(object? sender, EventArgs? args);
        void Edit(object? sender, EventArgs? args);
        void New(object? sender, EventArgs? args);
        void Print(object? sender, EventArgs? args);
        void PreRender();        
        void SearchTransactionMaster(object? sender, EventArgs? args);
    }
}
