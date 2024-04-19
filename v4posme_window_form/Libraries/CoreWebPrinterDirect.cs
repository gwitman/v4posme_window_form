using System.Drawing.Printing;

namespace v4posme_window.Libraries;

public class CoreWebPrinterDirect
{
    public string? NombreImpresora { get; set; }

    public PrintDocument? ConfigurationPrinter(string printerName)
    {
        NombreImpresora = printerName;
        if (PrinterSettings.InstalledPrinters.Cast<string>().Any(printer => !printer.Equals(printerName, StringComparison.OrdinalIgnoreCase)))
        {
            return null;
        }
        var pd = new PrintDocument();
        pd.PrinterSettings.PrinterName = printerName;
        return pd;
    }
}