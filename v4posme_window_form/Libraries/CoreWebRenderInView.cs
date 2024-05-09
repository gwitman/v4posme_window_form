using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Html;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts.Sankey;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using Control = System.Windows.Forms.Control;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
using Image = System.Drawing.Image;

namespace v4posme_window.Libraries;

public class CoreWebRenderInView
{
    private AlertControl _alert=new();

    public CoreWebRenderInView()
    {
        _alert.AutoHeight = true;
        _alert.AutoFormDelay = 2000;
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

    public static void LlenarComboBox<T>(List<T> lista, ComboBoxEdit comboBox, string keyField, string descripcionField, object? defaultValue)
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
                comboBox.EditValue = comboBoxItem;
            }
        }
    }

    public static void RenderGrid(TableCompanyDataViewDto dataViewDto, string nameGridView, GridControl gridControl)
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
        var gridView = gridControl.MainView as GridView;
        if (gridView is not null)
        {
            gridView.BestFitColumns();
            gridView.OptionsView.ShowGroupPanel = false;
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

        return;
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
                row[kvp.Key] = kvp.Value;
            }

            table.Rows.Add(row);
        }

        return table;
    }

    public void GetMessageAlert(TypeError type, string title, string body, Form form)
    {
        var templateHtml = new HtmlTemplate();
        var image = type switch
        {
            TypeError.Informacion => Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_INFORMACION_PATH"]!),
            TypeError.Error => Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_ERROR_PATH"]!),
            TypeError.Warning => Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_WARNING_PATH"]!),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        templateHtml.Template = Load("Alert.html");
        templateHtml.Styles = Load("Alert.css");
        var svgImageItemCollection = new SvgImageCollection();
        svgImageItemCollection.Add("message_close",Properties.Resources.message_close);
        svgImageItemCollection.Add("message_icon",Properties.Resources.Glyph_Message);
        svgImageItemCollection.ImageSize = new Size(16, 16);
        _alert.HtmlTemplates.Add(templateHtml);
        _alert.HtmlImages = svgImageItemCollection;
        _alert.HtmlElementMouseClick += AlertControl1_HtmlElementMouseClick;
        _alert.Show(form, title, body,"", image);
    }

    public static void RenderMenuLeft(List<TbMenuElement> data, AccordionControl menu)
    {
        // Llamada inicial a la función recursiva para agregar elementos al AccordionControl
        foreach (var menuItem in data)
        {
            if (menuItem.ParentMenuElementId is null)
            {
                var accordionElement = new AccordionControlElement();
                accordionElement.Text = menuItem.Display;
                accordionElement.Style = ElementStyle.Group;
                if (!string.IsNullOrEmpty(menuItem.IconWindowForm))
                {
                    accordionElement.ImageOptions.Image = Image.FromFile(menuItem.IconWindowForm);
                    accordionElement.ImageOptions.ImageLayoutMode = ImageLayoutMode.Stretch;
                }

                menu.Elements.Add(accordionElement);
                RenderItemLeft(accordionElement,
                    data.Where(k => k.ParentMenuElementId == menuItem.MenuElementId).ToList());
            }
        }
    }

    private static void RenderItemLeft(AccordionControlElement parentElement, List<TbMenuElement> subItems)
    {
        foreach (var subItem in subItems)
        {
            var subElement = new AccordionControlElement();
            subElement.Text = subItem.Display;
            parentElement.Elements.Add(subElement);
            var subItemsInner = subItems.Where(k => k.ParentMenuElementId == subItem.MenuElementId).ToList();
            // Si el elemento tiene subelementos, llamamos recursivamente a esta función
            if (subItemsInner.Count > 0)
            {
                subElement.Style = ElementStyle.Group;
                RenderItemLeft(subElement, subItemsInner);
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
            if (menuItem.ParentMenuElementId is null)
            {
                var ribbonPage = new RibbonPage();
                ribbonPage.Text = menuItem.Display;
                menu.Pages.Add(ribbonPage);
                RenderItemTop(ribbonPage,
                    data.Where(k => k.ParentMenuElementId == menuItem.MenuElementId).ToList());
            }
        }
    }

    private static void RenderItemTop(RibbonPage parentElement, List<TbMenuElement> subItems)
    {
        foreach (var subItem in subItems)
        {
            var subItemsInner = subItems.Where(k => k.ParentMenuElementId == subItem.MenuElementId).ToList();
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
    private static string Load(string fileName)
    {
        var directoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        var filePath = Path.Combine(directoryPath, "Libraries/Style", fileName);
        Debug.WriteLine(File.ReadAllText(filePath));
        return File.Exists(filePath) ? File.ReadAllText(filePath) : "";
    }
    private void AlertControl1_HtmlElementMouseClick(object sender, AlertHtmlElementMouseEventArgs e) {
        if(e.ElementId == "closeButton" || e.ParentHasId("closeButton") ||
           e.ElementId == "okButton" || e.ParentHasId("okButton"))
            e.HtmlPopup.Close();
        else
            e.HtmlPopup.Pinned = !e.HtmlPopup.Pinned;
    }
}