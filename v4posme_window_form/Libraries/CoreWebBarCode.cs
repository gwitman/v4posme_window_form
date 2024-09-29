using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace v4posme_window.Libraries;

public class CoreWebBarCode
{
    public static void Generate(string filePath = "", string text = "0", int height = 100, int width = 300, int margin = 10)
    {
        if (string.IsNullOrEmpty(filePath)) return;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.CODE_128,
            Options = new EncodingOptions
            {
                Height = height,
                Width = width, 
                Margin = margin
            },
        };
        using (var bitmap = writer.Write(text))
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
            }
        }
    }
}