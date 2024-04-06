using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using v4posme_library.Libraries;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Views;
using Control = System.Windows.Forms.Control;
using Guid = System.Guid;
using Image = System.Drawing.Image;

namespace v4posme_window.Libraries
{
    public class CoreWebRenderInView
    {
        public void RenderGrid(TableCompanyDataViewDto dataViewDto, string nameGridView, int displayLength,
            System.Windows.Forms.Control form)
        {
            if (dataViewDto.Config is null)
            {
                throw new Exception("Dto is null");
            }

            var gridControl = new GridControl()
            {
                Name = nameGridView,
                Parent = form,
                Dock = DockStyle.Fill
            };
            var viewData = dataViewDto.Data;
            gridControl.DataSource = viewData;
            var gridView = gridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            var summaryColumns = dataViewDto.Config.SummaryColumns!.Split(",");
            var formatColumns = dataViewDto.Config.FormatColumns!.Split("");
            var noVisibleColumns = dataViewDto.Config.NonVisibleColumns!.Split(",");
            var visibleColumns = dataViewDto.Config.VisibleColumns!.Split(",");
            var columns = noVisibleColumns.Concat(visibleColumns).ToList();
            if (summaryColumns.Length > 0)
            {
                gridView.OptionsView.ShowFooter = true;
            }

            var agregarLineaSumaRow = summaryColumns;
            for (int i = 0; i < summaryColumns.Length; i++)
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

            var columnsGrid = gridView.Columns.ToList();
            var aux = 0;
            var noVisibleLowerCase = noVisibleColumns.Select(s => s.ToLowerInvariant()).ToList();
            foreach (var column in columnsGrid)
            {
                if (noVisibleLowerCase.Contains(column.Name.ToLowerInvariant()))
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

        public void GetMessageAlert(TypeMessage type, string title, string body, Form form)
        {
            var alert = new AlertControl();
            var image = type switch
            {
                TypeMessage.Informacion => Image.FromFile(
                    VariablesGlobales.ConfigurationBuilder["ICON_INFORMACION_PATH"]!),
                TypeMessage.Error => Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_ERROR_PATH"]!),
                TypeMessage.Warning => Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_WARNING_PATH"]!),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            alert.Show(form, title, body, image);
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
                        accordionElement.ImageOptions.ImageLayoutMode=ImageLayoutMode.Stretch;
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
                        subElement.ImageOptions.Image = Image.FromFile(subItem.IconWindowForm);
                        subElement.ImageOptions.ImageLayoutMode = ImageLayoutMode.Stretch;
                        //subElement.ImageLayoutMode = ImageLayoutMode.Stretch;
                        //subElement.Image = Image.FromFile(subItem.IconWindowForm!);
                    }

                    if (!string.IsNullOrEmpty(subItem.FormRedirectWindowForm))
                    {
                        subElement.Name = subItem.FormRedirectWindowForm;
                    }
                }
            }
        }

        public static void RenderMenuTop(List<TbMenuElement> data, MenuStrip menu)
        {
            // Llamada inicial a la función recursiva para agregar elementos al AccordionControl
            foreach (var menuItem in data)
            {
                if (menuItem.ParentMenuElementId is null)
                {
                    var toolStripMenuItem = new ToolStripMenuItem();
                    toolStripMenuItem.Text = menuItem.Display;
                    if (!string.IsNullOrEmpty(menuItem.IconWindowForm))
                    {
                        toolStripMenuItem.Image = Image.FromFile(menuItem.IconWindowForm);
                        toolStripMenuItem.Size = new Size(32, 32);
                    }
                    menu.Items.Add(toolStripMenuItem);
                    RenderItemTop(toolStripMenuItem,
                        data.Where(k => k.ParentMenuElementId == menuItem.MenuElementId).ToList());
                }
            }
        }

        private static void RenderItemTop(ToolStripMenuItem parentElement, List<TbMenuElement> subItems)
        {
            foreach (var subItem in subItems)
            {
                var subElement = new ToolStripMenuItem();
                subElement.Text = subItem.Display;
                parentElement.DropDownItems.Add(subElement);
                var subItemsInner = subItems.Where(k => k.ParentMenuElementId == subItem.MenuElementId).ToList();
                // Si el elemento tiene subelementos, llamamos recursivamente a esta función
                if (subItemsInner.Count > 0)
                {
                    RenderItemTop(subElement, subItemsInner);
                }
                else
                {
                    if (!string.IsNullOrEmpty(subItem.IconWindowForm))
                    {
                        subElement.Image = Image.FromFile(subItem.IconWindowForm);
                        subElement.Size = new Size(32, 32);
                    }

                    if (!string.IsNullOrEmpty(subItem.FormRedirectWindowForm))
                    {
                        subElement.Name = subItem.FormRedirectWindowForm;
                    }
                }
            }
        }

        public static void RenderMenuTop(List<TbMenuElement> data, DevExpress.XtraBars.Ribbon.RibbonControl menu)
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
    }
}