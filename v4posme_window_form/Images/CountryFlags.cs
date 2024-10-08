namespace DevExpress.UITemplates.Collection.Images {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using DevExpress.Skins;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Drawing;
    using DevExpress.Utils.Svg;

    public static class CountryFlags {
        const string assetsRoot = "v4posme_window.Assets."; 
        [ThreadStatic]
        static SvgImageCollection svgImagesCore;
        public static SvgImageCollection SvgImages {
            get { return svgImagesCore ?? (svgImagesCore = SvgImageCollection.FromResources(assetsRoot + "SVG.Countries", typeof(CountryFlags).Assembly)); }
        }
        // Some Countries with simple flags
        public static SvgImage Armenia {
            get { return SvgImages["AM"]; }
        }
        public static SvgImage Austria {
            get { return SvgImages["AT"]; }
        }
        public static SvgImage Belgium {
            get { return SvgImages["BE"]; }
        }
        public static SvgImage Bulgaria {
            get { return SvgImages["BG"]; }
        }
        public static SvgImage Chile {
            get { return SvgImages["CL"]; }
        }
        public static SvgImage Germany {
            get { return SvgImages["DE"]; }
        }
        public static SvgImage Estonia {
            get { return SvgImages["EE"]; }
        }
        public static SvgImage France {
            get { return SvgImages["FR"]; }
        }
        public static SvgImage Gabon {
            get { return SvgImages["GA"]; }
        }
        public static SvgImage Guinea {
            get { return SvgImages["GN"]; }
        }
        public static SvgImage Hungary {
            get { return SvgImages["HU"]; }
        }
        public static SvgImage Indonesia {
            get { return SvgImages["ID"]; }
        }
        public static SvgImage Ireland {
            get { return SvgImages["IE"]; }
        }
        public static SvgImage Italy {
            get { return SvgImages["IT"]; }
        }
        public static SvgImage Lithuania {
            get { return SvgImages["LT"]; }
        }
        public static SvgImage Luxembourg {
            get { return SvgImages["LU"]; }
        }
        public static SvgImage Monaco {
            get { return SvgImages["MC"]; }
        }
        public static SvgImage SaintMartin {
            get { return SvgImages["MF"]; }
        }
        public static SvgImage Mali {
            get { return SvgImages["ML"]; }
        }
        public static SvgImage Nigeria {
            get { return SvgImages["NG"]; }
        }
        public static SvgImage Netherlands {
            get { return SvgImages["NL"]; }
        }
        public static SvgImage Peru {
            get { return SvgImages["PE"]; }
        }
        public static SvgImage Poland {
            get { return SvgImages["PL"]; }
        }
        public static SvgImage Romania {
            get { return SvgImages["RO"]; }
        }
        public static SvgImage Russia {
            get { return SvgImages["RU"]; }
        }
        public static SvgImage SierraLeone {
            get { return SvgImages["SL"]; }
        }
        public static SvgImage Chad {
            get { return SvgImages["TD"]; }
        }
        public static SvgImage Ukraine {
            get { return SvgImages["UA"]; }
        }
        public static SvgImage USA {
            get { return SvgImages["US"]; }
        }
        public static SvgImage Yemen {
            get { return SvgImages["YE"]; }
        }
        #region Images
        [ThreadStatic]
        static Dictionary<int, Image> glyphCache;
        public static Image GetISO2GlyphOrFlag(ISkinProvider skinProvider, string iso2, Size glyphSize, ISvgPaletteProvider palette = null) {
            if(glyphCache == null)
                glyphCache = new Dictionary<int, Image>(Utilities.Countries.All.Count * 2);
            int key = HashCodeHelper.Calculate(glyphSize.Width, glyphSize.Height, iso2[0], iso2[1]);
            Image glyph;
            if(!glyphCache.TryGetValue(key, out glyph)) {
                glyph = new Bitmap(glyphSize.Width, glyphSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                UpdateISO2Glyph(iso2, glyph, skinProvider, palette);
                glyphCache.Add(key, glyph);
            }
            return glyph;
        }
        static void UpdateISO2Glyph(string iso2, Image glyph, ISkinProvider skinProvider, ISvgPaletteProvider palette) {
            using(Graphics g = Graphics.FromImage(glyph)) {
                g.Clear(Color.Transparent);
                var svgImage = SvgImages[iso2];
                if(svgImage != null)
                    DrawFlagSvgImage(glyph, palette, g, svgImage);
                else DrawFlagViaISO2Glyph(iso2, glyph, skinProvider, g);
            }
        }
        static void DrawFlagSvgImage(Image glyph, ISvgPaletteProvider palette, Graphics g, SvgImage svgImage) {
            double factor = (double)svgImage.Height / svgImage.Width;
            float offset = (int)Math.Ceiling((glyph.Height - factor * glyph.Width) / 2);
            g.TranslateTransform(0, offset);
            var svgBmp = SvgBitmap.Create(svgImage);
            svgBmp.RenderToGraphics(g, palette, glyph.Width / svgImage.Width);
        }
        static void DrawFlagViaISO2Glyph(string iso2, Image glyph, ISkinProvider provider, Graphics g) {
            var length = iso2.Length;
            if(length > 2 || length == 0)
                throw new ArgumentException();
            int width = glyph.Width;
            int height = (int)(2 * (width + 0.5f) / 3);
            int padding = width / 16;
            var family = AppearanceObject.DefaultFont.FontFamily;
            Size maxSize = new Size(width - padding, height - padding);
            int fontBestSize = 0;
            float emSize;
            SizeF bestSize;
            using(var path = new GraphicsPath()) {
                while(true) {
                    emSize = 96f * (fontBestSize + 1) / 72;
                    path.AddString("WW", family, 0, emSize, Point.Empty, null);
                    bestSize = path.GetBounds().Size;
                    if(bestSize.Width > maxSize.Width || bestSize.Height > maxSize.Height)
                        break;
                    fontBestSize++;
                    path.Reset();
                }
                path.Reset();
                emSize = 96f * fontBestSize / 72;
                path.AddString(iso2, family, 0, emSize, Point.Empty, null);
                var pathBounds = path.GetBounds();
                var colors = GetColors(provider, iso2[0] % 5, ObjectState.Normal);
                g.Clear(Color.Transparent);
                var offset = (glyph.Height - height) / 2f;
                using(var brush = new SolidBrush(colors.Item1))
                    g.FillRectangle(brush, new RectangleF(0, offset, width, height));
                g.TranslateTransform((width - pathBounds.Width) / 2f - pathBounds.X - 0.5f, (height - pathBounds.Height) / 2f - pathBounds.Y + offset);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                using(var brush = new SolidBrush(colors.Item2))
                    g.FillPath(brush, path);
            }
        }
        static Tuple<Color, Color> GetColors(ISkinProvider provider, int index, ObjectState state) {
            var skin = DevExpress.Skins.CommonSkins.GetSkin(provider);
            if(skin.SvgPalettes == null || skin.SvgPalettes.Count == 0)
                return new Tuple<Color, Color>(Color.Gray, Color.White);
            SvgPalette palette = skin.SvgPalettes[state];
            if(palette == null)
                palette = skin.SvgPalettes[ObjectState.Normal];
            if(palette == null)
                return new Tuple<Color, Color>(Color.Gray, Color.White);
            Color backColor = palette.GetColorByStyleName(backColorNameTable[index], "Black");
            Color foreColor = palette.GetColorByStyleName(foreColorNameTable[index], "White");
            return new Tuple<Color, Color>(backColor, foreColor);
        }
        readonly static string[] backColorNameTable = new string[] {
            "Black", "Red", "Green", "Blue", "Yellow"
        };
        readonly static string[] foreColorNameTable = new string[] {
            "White", "White", "White", "White", "White"
        };
        #endregion
    }
}
