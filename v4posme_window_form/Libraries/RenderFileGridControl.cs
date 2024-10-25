using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using v4posme_library.Libraries;
using v4posme_window.Dto;

namespace v4posme_window.Libraries;

public class RenderFileGridControl
{
    public RenderFileGridControl(int companyId, int componentId, int componentItemId)
    {
        _companyId = companyId;
        _componentId = componentId;
        _componentItemId = componentItemId;
        ArchivoList = new BindingList<ArchivoDto>();
    }

    private int _companyId;
    private int _componentId;
    private int _componentItemId;
    private readonly string? _rootPath = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];

    public BindingList<ArchivoDto> ArchivoList { get; }

    public GridView? GridView { get; set; }

    public void RenderGridControl(GridControl gridControl)
    {
        GridView = gridControl.MainView as GridView ?? new GridView(gridControl);
        GridView.Columns.Clear();
        GridView.Columns.AddVisible("PathFile", "Ruta del Archivo");
        GridView.Columns.AddVisible("FileName", "Nombre del Archivo");
        GridView.Columns.AddVisible("FileType", "Tipo de Archivo");

        GridView.Columns[0].Visible = false;

        var previewButtonColumn = GridView.Columns.AddVisible("Preview", "Vista Previa");
        previewButtonColumn.MaxWidth = 75;
        previewButtonColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;

        var downloadButtonColumn = GridView.Columns.AddVisible("Download", "Descargar");
        downloadButtonColumn.MaxWidth = 75;
        downloadButtonColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;

        var deleteButtonColumn = GridView.Columns.AddVisible("Delete", "Eliminar");
        deleteButtonColumn.MaxWidth = 75;
        deleteButtonColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;

        var buttonPreview = new RepositoryItemButtonEdit();
        buttonPreview.TextEditStyle = TextEditStyles.HideTextEditor;
        buttonPreview.Buttons[0].Caption = "Preview";
        buttonPreview.Buttons[0].Kind = ButtonPredefines.Glyph;
        buttonPreview.Buttons[0].ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "preview.svg"));
        buttonPreview.ButtonClick += ButtonEdit_ButtonClick;
        gridControl.RepositoryItems.Add(buttonPreview);
        previewButtonColumn.ColumnEdit = buttonPreview;

        var buttonDownload = new RepositoryItemButtonEdit();
        buttonDownload.TextEditStyle = TextEditStyles.HideTextEditor;
        buttonDownload.Buttons[0].Caption = "Descargar";
        buttonDownload.Buttons[0].Kind = ButtonPredefines.Glyph;
        buttonDownload.Buttons[0].ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "download.svg"));
        buttonDownload.ButtonClick += ButtonDownload_ButtonClick;
        gridControl.RepositoryItems.Add(buttonDownload);
        downloadButtonColumn.ColumnEdit = buttonDownload;

        var deleteDownload = new RepositoryItemButtonEdit();
        deleteDownload.TextEditStyle = TextEditStyles.HideTextEditor;
        deleteDownload.Buttons[0].Caption = "Eliminar";
        deleteDownload.Buttons[0].Kind = ButtonPredefines.Glyph;
        deleteDownload.Buttons[0].ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "delete.svg"));
        deleteDownload.ButtonClick += ButtonDelete_ButtonClick;
        gridControl.RepositoryItems.Add(deleteDownload);
        deleteButtonColumn.ColumnEdit = deleteDownload;

        gridControl.MainView = GridView;
        gridControl.DataSource = ArchivoList;
    }

    public void AddRow(string archivo)
    {
        var companyPath = $"company_{_companyId}";
        var componentPath = $"component_{_componentId}";
        var componentItemPath = $"component_item_{_componentItemId}";
        var path = Path.Combine(_rootPath, companyPath, componentPath, componentItemPath);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var pathFile = Path.Combine(path, archivo);
        File.Copy(archivo, pathFile,true);
        string extensionArchivo = Path.GetExtension(archivo);
        ArchivoList.Add(new ArchivoDto(pathFile, archivo, extensionArchivo));
        ArchivoList.ResetBindings();
    }

    public void LoadFiles()
    {
        if (string.IsNullOrEmpty(_rootPath))
        {
            return;
        }

        var companyFolder = FindFolderByPattern(_rootPath, "company", _companyId);

        if (!string.IsNullOrEmpty(companyFolder))
        {
            var componentFolder = FindFolderByPattern(companyFolder, "component", _componentId);

            if (!string.IsNullOrEmpty(componentFolder))
            {
                var componentItemFolder = FindFolderByPattern(componentFolder, "component_item", _componentItemId);

                if (!string.IsNullOrEmpty(componentItemFolder))
                {
                    GetFilesFromFolder(componentItemFolder);
                }
                else
                {
                    Debug.WriteLine("Carpeta 'component_item' no encontrada.");
                }
            }
            else
            {
                Debug.WriteLine("Carpeta 'component' no encontrada.");
            }
        }
        else
        {
            Debug.WriteLine("Carpeta 'company' no encontrada.");
        }
    }

    private string? FindFolderByPattern(string parentDirectory, string prefix, int id)
    {
        try
        {
            string?[] directories = Directory.GetDirectories(parentDirectory);

            foreach (var directory in directories)
            {
                var folderName = Path.GetFileName(directory);
                if (string.IsNullOrEmpty(folderName)) return null;
                var parts = folderName.Split('_');

                if (parts.Length == 2 && parts[0] == prefix && int.TryParse(parts[1], out var folderId) && folderId == id)
                {
                    return directory;
                }

                if (parts.Length == 3 && int.TryParse(parts[2], out folderId) && folderId == id)
                {
                    return directory;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($@"Error al buscar la carpeta: {ex.Message}");
        }

        return null;
    }

    private void GetFilesFromFolder(string folderPath)
    {
        try
        {
            var fileEntries = Directory.GetFiles(folderPath);

            foreach (var filePath in fileEntries)
            {
                var fileName = Path.GetFileName(filePath);
                var fileType = Path.GetExtension(filePath);
                ArchivoList.Add(new ArchivoDto(filePath, fileName, fileType));
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($@"Error al leer archivos: {ex.Message}");
        }
    }

    private void ButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
        if (GridView?.GetFocusedRow() is ArchivoDto selectedValue)
        {
           CoreWebRenderInView.MostrarArchivoGrid(selectedValue);
        }
    }

    private void ButtonDownload_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
        if (GridView?.GetFocusedRow() is ArchivoDto selectedValue)
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"Todos los archivos (*.*)|*.*";
            saveFileDialog.Title = @"Guardar archivo";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                File.Copy(selectedValue.PathFile,$"{filePath}{selectedValue.FileType}",true);
            }
        }
    }

    private void ButtonDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
        if (GridView?.GetFocusedRow() is ArchivoDto selectedValue)
        {
            var dialogResult = XtraMessageBox.Show( @"¿Seguro desea eliminar el archivo seleccionado?",@"Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                ArchivoList.Remove(selectedValue);
                ArchivoList.ResetBindings();
                File.Delete(selectedValue.PathFile);
            }
        } 
    }
}