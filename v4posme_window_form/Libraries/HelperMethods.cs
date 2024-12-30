﻿using System.Collections;
using DevExpress.XtraEditors;
using System.IO;
using ESC_POS_USB_NET.EpsonCommands;

namespace v4posme_window.Libraries;

public class HelperMethods
{
    public static void OnlyNumberDecimals(TextEdit textEdit)
    {
        textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        textEdit.Properties.Mask.EditMask = @"n2";
        textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
    }

    public static void OnlyNumberInt(TextEdit textEdit)
    {
        textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        textEdit.Properties.Mask.EditMask = @"n0";
        textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
    }

    public static void FnClearTextEdits(Control control)
    {
        foreach (Control c in control.Controls)
        {
            if (c is DevExpress.XtraEditors.TextEdit textEdit)
            {
                textEdit.Text = string.Empty;
            }
            else if (c.HasChildren)
            {
                // Recursively clear TextEdit controls in child containers
                FnClearTextEdits(c);
            }
        }
    }

    private static BitmapData GetBitmapData(Bitmap bmp)
    {
        var threshold = 127;
        var index = 0;
        double multiplier = 380; // this depends on your printer model.
        double scale = multiplier / bmp.Width;
        var xheight = (int)(bmp.Height * scale);
        var xwidth = (int)(bmp.Width * scale);
        var dimensions = xwidth * xheight;
        var dots = new BitArray(dimensions);

        for (var y = 0; y < xheight; y++)
        {
            for (var x = 0; x < xwidth; x++)
            {
                var i = (int)(x / scale);
                var j = (int)(y / scale);
                var color = bmp.GetPixel(i, j);
                var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                dots[index] = (luminance < threshold);
                index++;
            }
        }

        return new BitmapData()
        {
            Dots = dots,
            Height = (int)(bmp.Height * scale),
            Width = (int)(bmp.Width * scale)
        };
    }

    public static byte[] Print(Bitmap image)
    {
        var data = GetBitmapData(image);
        var dots = data.Dots;
        var width = BitConverter.GetBytes(data.Width);

        int offset = 0;
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        bw.Write((char)0x1B);
        bw.Write('@');

        bw.Write((char)0x1B);
        bw.Write('3');
        bw.Write((byte)24);

        while (offset < data.Height)
        {
            bw.Write((char)0x1B);
            bw.Write('*'); // bit-image mode
            bw.Write((byte)33); // 24-dot double-density
            bw.Write(width[0]); // width low byte
            bw.Write(width[1]); // width high byte

            for (int x = 0; x < data.Width; ++x)
            {
                for (int k = 0; k < 3; ++k)
                {
                    byte slice = 0;
                    for (int b = 0; b < 8; ++b)
                    {
                        int y = (((offset / 8) + k) * 8) + b;
                        // Calculate the location of the pixel we want in the bit array.
                        // It'll be at (y * width) + x.
                        int i = (y * data.Width) + x;

                        // If the image is shorter than 24 dots, pad with zero.
                        bool v = false;
                        if (i < dots.Length)
                        {
                            v = dots[i];
                        }

                        slice |= (byte)((v ? 1 : 0) << (7 - b));
                    }

                    bw.Write(slice);
                }
            }

            offset += 24;
            bw.Write((char)0x0A);
        }

        // Restore the line spacing to the default of 30 dots.
        bw.Write((char)0x1B);
        bw.Write('3');
        bw.Write((byte)30);

        bw.Flush();
        byte[] bytes = stream.ToArray();
        bw.Dispose();
        return bytes;
    }
}