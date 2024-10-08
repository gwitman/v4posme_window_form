namespace DevExpress.UITemplates.Collection.Images {
    using System;
    using DevExpress.Utils;
    using DevExpress.Utils.Svg;

    public static class CardLogos {
        const string assetsRoot = "v4posme_window.Assets."; 
        [ThreadStatic]
        static SvgImageCollection svgImagesCore;
        public static SvgImageCollection SvgImages {
            get { return svgImagesCore ?? (svgImagesCore = CreateAllImages()); }
        }
        static SvgImageCollection CreateAllImages() {
            var rootAsm = typeof(CardLogos).Assembly;
            var imgCollection = SvgImageCollection.FromResources(assetsRoot + "SVG.CardLogos", rootAsm);
            var dinnersClubBase = imgCollection[nameof(DinersClub)];
            imgCollection.Add("DinersClubInternational", dinnersClubBase);
            imgCollection.Add("DinersClubCarteBlanche", dinnersClubBase);
            imgCollection.Add("DinersClubNorthAmerica", dinnersClubBase);
            return imgCollection;
        }
        public static SvgImage Visa {
            get { return SvgImages[nameof(Visa)]; }
        }
        public static SvgImage MasterCard {
            get { return SvgImages[nameof(MasterCard)]; }
        }
        public static SvgImage Maestro {
            get { return SvgImages[nameof(Maestro)]; }
        }
        public static SvgImage AmericanExpress {
            get { return SvgImages[nameof(AmericanExpress)]; }
        }
        public static SvgImage DinersClub {
            get { return SvgImages[nameof(DinersClub)]; }
        }
        public static SvgImage Discover {
            get { return SvgImages[nameof(Discover)]; }
        }
        public static SvgImage JCB {
            get { return SvgImages[nameof(JCB)]; }
        }
    }
}
