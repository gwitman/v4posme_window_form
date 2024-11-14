using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Html;
using DevExpress.Utils.Layout;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts.Sankey;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using ESC_POS_USB_NET.Printer;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Dto;
using v4posme_window.Template;
using Control = System.Windows.Forms.Control;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
using Image = System.Drawing.Image;

namespace v4posme_window.Libraries;

public class CoreWebRenderInView
{
    private readonly ICoreWebTools _objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
    private readonly ICoreWebParameter _objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
    private static readonly string SvgPath = Path.Combine(Application.StartupPath, "Resources", "list.svg");
    private byte[] Avanza(int puntos)
    {
        return [27, 74, (byte)puntos]; //8puntos = 1mm
    }

    public void PrintBarCodeItem(TbItem item, int cantidadImprimir)
    {
        var user = VariablesGlobales.Instance.User;
        if (user is null)
        {
            throw new Exception("Usuario no logeado");
        }

        var spacing = 0.5;
        var objComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
        if (objComponentItem is null)
        {
            throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
        }

        // Imprimir el documento               
        var printerName = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", user.CompanyID);
        var printer = new Printer(printerName!.Value);

        printer.AlignCenter();

        printer.Code128(item.BarCode);
        printer.Append(item.Name);
        printer.Append(item.BarCode);
        printer.Append(item.Cost.ToString("N2"));
        printer.Append("-");
        printer.Append(Avanza(45) /*8puntos = 1mm*/);
        for (int i = 1; i <= cantidadImprimir * 2; i++)
        {
            printer.PrintDocument();
        }
    }

    public static void MostrarArchivoGrid(ArchivoDto selectedValue)
    {
        var extension = selectedValue.FileType;
        switch (extension)
        {
            case ".jpg":
            case ".jpeg":
            case ".png":
            case ".gif":
            case ".bmp":
            case ".tiff":
                MostrarDialogImagen(selectedValue.PathFile);
                break;
            case ".pdf":
                MostrarPdf(selectedValue.PathFile, selectedValue.FileName);
                break;
        }
    }

    public static void MostrarDialogImagen(string pathFile)
    {
        var layoutControl = new StackPanel();
        layoutControl.Width = 500;
        layoutControl.Height = 350;
        var pictureEdit = new PictureEdit();
        pictureEdit.Image = Image.FromFile(pathFile);
        pictureEdit.Width = 500;
        pictureEdit.Height = 350;
        pictureEdit.Dock = DockStyle.Fill;
        pictureEdit.Properties.ShowMenu = false;
        pictureEdit.Properties.SizeMode = PictureSizeMode.Zoom;
        layoutControl.Controls.Add(pictureEdit);
        XtraDialog.Show(layoutControl, "Imagen Producto", MessageBoxButtons.OK);
    }

    public static void MostrarPdf(string pathFile, string fileName)
    {
        var pdfViewer = new FormTypePdfViewer();
        pdfViewer.pdfViewer.LoadDocument(pathFile);
        pdfViewer.Text = fileName;
        pdfViewer.Show();
    }

    public static void LlenarComboBoxSetIndex(ComboBoxEdit comboBox, int indexValue)
    {
        for (var i = 0; i < comboBox.Properties.Items.Count; i++)
        {
            var comboboxIem = (ComboBoxItem)comboBox.Properties.Items[i];
            if (i == indexValue)
            {
                comboBox.SelectedItem = comboboxIem;
            }
        }
    }

    public static void LlenarComboBoxSetItem(ComboBoxEdit comboBox, string value)
    {
        for (var i = 0; i < comboBox.Properties.Items.Count; i++)
        {
            var comboboxIem = (ComboBoxItem)comboBox.Properties.Items[i];
            if (comboboxIem.Key == value)
            {
                comboBox.SelectedItem = comboboxIem;
            }
        }
    }

    public static void LlenarComboBoxRemoveItem(ComboBoxEdit comboBox, string value)
    {
        // Iteramos sobre todos los elementos del ComboBoxEdit
        for (int i = 0; i < comboBox.Properties.Items.Count; i++)
        {
            // Verificamos si el elemento es una instancia de la clase que tiene la propiedad Key
            if (comboBox.Properties.Items[i] is ComboBoxItem)
            {
                // Si la clave del elemento coincide con la clave proporcionada, lo eliminamos
                if (((ComboBoxItem)comboBox.Properties.Items[i]).Key == value)
                {
                    comboBox.Properties.Items.RemoveAt(i);
                    return; // Terminamos el bucle después de eliminar el elemento
                }
            }
        }
    }

    public static void LlenarComboBoxAddItem<T>(T itemElement, ComboBoxEdit comboBox, string keyField, string descripcionField)
    {
        var key = itemElement!.GetType().GetProperty(keyField)?.GetValue(itemElement)?.ToString();
        var value = itemElement.GetType().GetProperty(descripcionField)?.GetValue(itemElement);
        var comboBoxItem = new ComboBoxItem(key, value);
        comboBox.Properties.Items.Add(comboBoxItem);
    }

    public static void LlenarComboBox<T>(List<T>? lista, ComboBoxEdit comboBox, string keyField, string descripcionField, object? defaultValue)
    {
        // Limpiar el combobox
        comboBox.Properties.Items.Clear();

        // Agregar los elementos de la lista al combobox
        foreach (var item in lista)
        {
            // Obtener los valores de key y value de cada objeto
            var key = item!.GetType().GetProperty(keyField)?.GetValue(item)?.ToString();
            var value = item.GetType().GetProperty(descripcionField)?.GetValue(item);
            var comboBoxItem = new ComboBoxItem(key, value);
            comboBox.Properties.Items.Add(comboBoxItem);
            if (defaultValue is null) continue;

            if (Convert.ToInt32(key) == Convert.ToInt32(defaultValue))
            {
                comboBox.SelectedItem = comboBoxItem;
            }
        }
    }

    public static void LlenarComboBoxGridControl<T>(IList<T>? lista, RepositoryItemComboBox comboBox, string keyField, string descripcionField)
    {
        // Limpiar el combobox
        comboBox.Items.Clear();
        // Agregar los elementos de la lista al combobox
        foreach (var item in lista)
        {
            // Obtener los valores de key y value de cada objeto
            var key = item!.GetType().GetProperty(keyField)?.GetValue(item)?.ToString();
            var value = item.GetType().GetProperty(descripcionField)?.GetValue(item);
            var comboBoxItem = new ComboBoxItem(key, value);
            comboBox.Items.Add(comboBoxItem);
        }
    }

    public static void RenderGrid(TableCompanyDataViewDto dataViewDto, string nameGridView, GridControl gridControl, bool multiSelect = false, bool searchPanel = false)
    {
        if (dataViewDto.Config is null)
        {
            throw new Exception("Dto is null");
        }

        if (dataViewDto.Data is null)
        {
            return;
        }

        var viewData = (List<Dictionary<string, object>>)dataViewDto.Data;
        var table = FillGridControl(viewData);
        gridControl.DataSource = table;
        // Ajustar la configuración del GridView
        if (gridControl.MainView is not GridView gridView)
        {
            gridView = new GridView(gridControl);
        }

        gridView.BestFitColumns();
        gridView.OptionsSelection.MultiSelect = multiSelect;
        gridView.OptionsView.ShowGroupPanel = searchPanel;
        gridView.OptionsCustomization.AllowSort = true;
        if (searchPanel)
        {
            gridView.OptionsFind.AlwaysVisible = true;
        }

        var summaryColumns = dataViewDto.Config.SummaryColumns is null
            ? []
            : dataViewDto.Config.SummaryColumns.Split(",");

        var formatColumns = dataViewDto.Config.FormatColumns is null
            ? []
            : dataViewDto.Config.FormatColumns.Split("");

        var noVisibleColumns = dataViewDto.Config.NonVisibleColumns is null
            ? []
            : dataViewDto.Config.NonVisibleColumns.Split(",");

        var visibleColumns = dataViewDto.Config.VisibleColumns is null
            ? []
            : dataViewDto.Config.VisibleColumns.Split(",");

        var columns = noVisibleColumns.Concat(visibleColumns).ToList();

        if (summaryColumns.Length > 0)
        {
            gridView.OptionsView.ShowFooter = true;
        }

        var agregarLineaSumaRow = summaryColumns;
        for (var i = 0; i < summaryColumns.Length; i++)
        {
            if (summaryColumns[i] == "true")
            {
                agregarLineaSumaRow.SetValue(0, i);
            }
            else if (i == 0)
            {
                agregarLineaSumaRow.SetValue("Total", i);
            }
            else
            {
                agregarLineaSumaRow.SetValue("Total", i);
            }
        }

        noVisibleColumns = noVisibleColumns.Select(s => $"col{s}").ToArray();

        var columnsGrid = gridView.Columns.ToList();
        var aux = 0;
        foreach (var column in columnsGrid)
        {
            column.OptionsColumn.AllowSort = DefaultBoolean.True;
            if (noVisibleColumns.Contains(column.Name))
            {
                column.Visible = false;
            }

            if (summaryColumns.Length > 0)
            {
                if (summaryColumns[aux] == "true")
                {
                    var summary = new GridColumnSummaryItem
                    {
                        SummaryType = SummaryItemType.Sum,
                        DisplayFormat = agregarLineaSumaRow[aux] + @": {0:#.#}"
                    };
                    column.Summary.Add(summary);
                }
            }

            aux++;
        }
    }

    public static DataTable? FillGridControl(List<Dictionary<string, object>>? data)
    {
        if (data == null || data.Count == 0)
            return null;

        var table = new DataTable();

        // Añadir columnas al DataTable basadas en las claves del primer diccionario
        foreach (var key in data.First().Keys)
        {
            table.Columns.Add(key, typeof(object));
        }

        // Añadir filas al DataTable
        foreach (var dict in data)
        {
            var row = table.NewRow();
            foreach (var kvp in dict)
            {
                var content = kvp.Value;

                if (content is string strContent)
                {
                    strContent = Regex.Replace(strContent, "<.*?>", string.Empty);
                    strContent = HttpUtility.HtmlDecode(strContent);
                    strContent = Regex.Replace(strContent, @"\b(Nota|Detalle)\b", string.Empty, RegexOptions.IgnoreCase).Trim();
                    content = strContent;
                }

                row[kvp.Key] = content;
            }

            table.Rows.Add(row);
        }

        return table;
    }

    public DialogResult XtraMessageBoxArgs(TypeError type, string title, string body)
    {
        var args = new XtraMessageBoxArgs()
        {
            Caption = title,
            Text = body,
            Buttons = [DialogResult.Yes, DialogResult.No],
        };
        switch (type)
        {
            case TypeError.Informacion:
                args.ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "info_svg.svg"));
                break;
            case TypeError.Warning:
                args.ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "warning_svg.svg"));
                break;
            case TypeError.Error:
                args.ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "error_svg.svg"));
                break;
        }

        args.ImageOptions.SvgImageSize = new Size(48, 48);
        args.Showing += Args_Showing;
        return XtraMessageBox.Show(args);
    }

    private void Args_Showing(object? sender, XtraMessageShowingArgs e)
    {
        e.Form.Appearance.FontSizeDelta = 2;
        foreach (var control in e.MessageBoxForm.Controls)
        {
            // Checks if a control is a SimpleButton.
            var button = control as SimpleButton;
            if (button != null)
            {
                button.ImageOptions.SvgImageSize = new Size(22, 22);
                button.Appearance.FontSizeDelta = 2;
                switch (button.DialogResult.ToString())
                {
                    case ("Yes"):
                        button.ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "check_svg.svg"));
                        break;
                    case ("No"):
                        button.ImageOptions.SvgImage = SvgImage.FromFile(Path.Combine(Application.StartupPath, "Resources", "cancel_svg.svg"));
                        break;
                    default:
                        button.ImageOptions.SvgImage = button.ImageOptions.SvgImage;
                        break;
                }
            }
        }
    }

    public void GetMessageAlert(TypeError type, string title, string body, Form form)
    {
        var templateHtml = new HtmlTemplate();
        templateHtml.Template = Load("Alert.html");
        templateHtml.Styles = string.Empty;
        Image? image;
        switch (type)
        {
            case TypeError.Informacion:
                image = Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_INFORMACION_PATH"]!);
                templateHtml.Styles = Load("AlertSuccess.css");
                break;
            case TypeError.Error:
                image = Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_ERROR_PATH"]!);
                templateHtml.Styles = Load("AlertError.css");
                break;
            case TypeError.Warning:
                image = Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_WARNING_PATH"]!);
                templateHtml.Styles = Load("AlertWarning.css");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        var svgImageItemCollection = new SvgImageCollection
        {
            { "message_close", Properties.Resources.message_close },
            { "message_icon", Properties.Resources.Glyph_Message }
        };
        svgImageItemCollection.ImageSize = new Size(16, 16);
        var alert = new AlertControl
        {
            AutoHeight = true,
            AutoFormDelay = 2000,
            HtmlImages = svgImageItemCollection
        };
        alert.HtmlTemplates.Add(templateHtml);
        alert.HtmlElementMouseClick += AlertControl1_HtmlElementMouseClick;
        alert.Show(form, title, body, "", image);
    }

    public static void RenderMenuLeft(List<TbMenuElement> data, AccordionControl menu, SvgImageCollection svgImageCollection)
    {
        var templateHtml = new HtmlTemplate();
        templateHtml.Template = Load("MenuItem.html");
        templateHtml.Styles = Load("MenuItemStyle.css");
        menu.HtmlTemplates.Item.Assign(templateHtml);
        // Llamada inicial a la función recursiva para agregar elementos al AccordionControl
        foreach (var menuItem in data)
        {
            if (menuItem.ParentMenuElementID is null)
            {
                var accordionElement = new AccordionControlElement();
                accordionElement.Text = menuItem.Display;
                accordionElement.Style = ElementStyle.Group;
                if (!string.IsNullOrEmpty(menuItem.IconWindowForm))
                {
                    accordionElement.ImageOptions.Image = Image.FromFile(menuItem.IconWindowForm);
                    accordionElement.ImageOptions.ImageLayoutMode = ImageLayoutMode.Stretch;
                }
                else
                {
                    accordionElement.ImageOptions.SvgImage = svgImageCollection[0];
                }

                menu.Elements.Add(accordionElement);
                RenderItemLeft(accordionElement, data.Where(k => k.ParentMenuElementID == menuItem.MenuElementID).ToList(),svgImageCollection);
            }
        }
    }

    private static void RenderItemLeft(AccordionControlElement parentElement, List<TbMenuElement> subItems, SvgImageCollection svgImageCollection)
    {
        foreach (var subItem in subItems)
        {
            var subElement = new AccordionControlElement();
            subElement.Text = subItem.Display;
            parentElement.Elements.Add(subElement);
            var subItemsInner = subItems.Where(k => k.ParentMenuElementID == subItem.MenuElementID).ToList();
            // Si el elemento tiene subelementos, llamamos recursivamente a esta función
            if (subItemsInner.Count > 0)
            {
                subElement.Style = ElementStyle.Group;
                RenderItemLeft(subElement, subItemsInner,svgImageCollection);
            }
            else
            {
                subElement.Style = ElementStyle.Item;
                if (!string.IsNullOrEmpty(subItem.IconWindowForm))
                {
                    if (File.Exists(subItem.IconWindowForm))
                    {
                        subElement.ImageOptions.Image = Image.FromFile(subItem.IconWindowForm);
                        subElement.ImageOptions.ImageLayoutMode = ImageLayoutMode.Stretch;
                    }
                }
                else
                {
                    subElement.ImageOptions.SvgImage = svgImageCollection[1];
                }

                if (!string.IsNullOrEmpty(subItem.FormRedirectWindowForm))
                {
                    subElement.Name = subItem.FormRedirectWindowForm;
                }
            }
        }
    }


    public static void RenderMenuTop(List<TbMenuElement> data, RibbonControl menu)
    {
        // Llamada inicial a la función recursiva para agregar elementos al AccordionControl
        foreach (var menuItem in data)
        {
            if (menuItem.ParentMenuElementID is null)
            {
                var ribbonPage = new RibbonPage();
                ribbonPage.Text = menuItem.Display;
                menu.Pages.Add(ribbonPage);
                RenderItemTop(ribbonPage,
                    data.Where(k => k.ParentMenuElementID == menuItem.MenuElementID).ToList());
            }
        }
    }

    private static void RenderItemTop(RibbonPage parentElement, List<TbMenuElement> subItems)
    {
        foreach (var subItem in subItems)
        {
            var subItemsInner = subItems.Where(k => k.ParentMenuElementID == subItem.MenuElementID).ToList();
            var subElement = new RibbonPageGroup();
            // Si el elemento tiene subelementos, llamamos recursivamente a esta función
            if (subItemsInner.Count > 0)
            {
                subElement.Text = subItem.Display;
                parentElement.Groups.Add(subElement);
                RenderItemTop(parentElement, subItems);
            }
            else
            {
                subElement.Text = "";
                var barButtonItem = new BarLargeButtonItem();
                barButtonItem.Caption = subItem.Display;
                if (!string.IsNullOrEmpty(subItem.IconWindowForm))
                {
                    barButtonItem.ImageOptions.Image = Image.FromFile(subItem.IconWindowForm);
                    barButtonItem.ImageToTextAlignment = BarItemImageToTextAlignment.AfterText;
                }

                if (!string.IsNullOrEmpty(subItem.FormRedirectWindowForm))
                {
                    barButtonItem.Name = subItem.FormRedirectWindowForm;
                }

                parentElement.Ribbon.Items.AddRange([parentElement.Ribbon.ExpandCollapseItem, barButtonItem]);
                subElement.ItemLinks.Add(barButtonItem);
                parentElement.Groups.Add(subElement);
            }
        }
    }

    public static void RenderListViewReport(ImageListBoxControl listBox, List<TbMenuElement>? items, int parentMenuElement, SvgImageCollection svgImageCollection)
    {
        if (items is null || items.Count <= 0) return;
        var listBoxDataSource = items.Where(item => item.ParentMenuElementID == parentMenuElement).ToList();
        foreach (var tbMenuElement in listBoxDataSource)
        {
            if (string.IsNullOrWhiteSpace(tbMenuElement.IconWindowForm))
            {
                svgImageCollection.Add(tbMenuElement.Orden,SvgImage.FromFile(SvgPath));
            }
            else
            {
                svgImageCollection.Add(tbMenuElement.Orden,SvgImage.FromFile(tbMenuElement.IconWindowForm));
            }
            
        }
        var templateHtml = new HtmlTemplate();
        templateHtml.Template = Load("ReportItem.html");
        templateHtml.Styles = Load("ReportItemStyle.css");
        listBox.HtmlImages = svgImageCollection;
        listBox.HtmlTemplates.Add(templateHtml);
        listBox.DataSource = listBoxDataSource;
    }

    private static string Load(string fileName)
    {
        var directoryPath = Application.StartupPath;
        var filePath = $"{directoryPath}/Libraries/Style/{fileName}" ; //Path.Combine(directoryPath, "Libraries/Style", fileName);
        Debug.WriteLine(File.ReadAllText(filePath));
        return File.Exists(filePath) ? File.ReadAllText(filePath) : "";
    }

    private void AlertControl1_HtmlElementMouseClick(object sender, AlertHtmlElementMouseEventArgs e)
    {
        if (e.ElementId == "closeButton" || e.ParentHasId("closeButton") ||
            e.ElementId == "okButton" || e.ParentHasId("okButton"))
            e.HtmlPopup.Close();
        else
            e.HtmlPopup.Pinned = !e.HtmlPopup.Pinned;
    }
}