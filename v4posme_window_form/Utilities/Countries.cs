namespace v4posme_window.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class Countries
    {
        static Countries()
        {
            var rows = Assets.Data.Rows;
            var all = new List<Info>(rows.Count);
            for (int i = 0; i < rows.Count; i++)
            {
                var info = new Info(rows[i]);
                mapByName.Add(info.Country, info);
                mapByFIPS.Add(info.FIPS, info);
                all.Add(info);
            }
            All = all.AsReadOnly();
            Asia = all.Where(x => x.Continent == nameof(Asia)).ToList().AsReadOnly();
            Africa = all.Where(x => x.Continent == nameof(Africa)).ToList().AsReadOnly();
            Antarctica = all.Where(x => x.Continent == nameof(Antarctica)).ToList().AsReadOnly();
            Europe = all.Where(x => x.Continent == nameof(Europe)).ToList().AsReadOnly();
            Oceania = all.Where(x => x.Continent == nameof(Oceania)).ToList().AsReadOnly();
            NorthAmerica = all.Where(x => x.Continent == "North America").ToList().AsReadOnly();
            SouthAmerica = all.Where(x => x.Continent == "South America").ToList().AsReadOnly();
        }
        public sealed class Info
        {
            internal Info(string[] parts)
                : this(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7])
            {
            }
            internal Info(string country, string iso2, string iso3, string fips, string phoneCode, string continent, string capital, string currency)
            {
                Country = country;
                ISO2 = iso2;
                ISO3 = iso3;
                FIPS = fips;
                PhoneCode = phoneCode;
                PhoneCodeDigits = phoneCode.Replace("-", string.Empty);
                Continent = continent;
                Capital = capital;
                Currency = currency;
            }
            public string Country { get; private set; }
            public string ISO2 { get; private set; }
            public string ISO3 { get; private set; }
            public string FIPS { get; private set; }
            public string PhoneCode { get; private set; }
            public string PhoneCodeDigits;
            public string Continent { get; private set; }
            public string Capital { get; private set; }
            public string Currency { get; private set; }
            public override string ToString()
            {
                return $"{Country}, {ISO2}/{ISO3}, {FIPS}, +{PhoneCode}, {Continent}, {Capital}, {Currency}";
            }
        }
        readonly static Dictionary<string, Info> mapByName = new Dictionary<string, Info>(StringComparer.OrdinalIgnoreCase);
        public static Info GetCountryByName(string countryName)
        {
            Info info;
            return mapByName.TryGetValue(countryName, out info) ? info : null;
        }
        readonly static Dictionary<string, Info> mapByFIPS = new Dictionary<string, Info>(StringComparer.OrdinalIgnoreCase);
        public static Info GetCountryByFIPS(string fips)
        {
            Info info;
            return mapByFIPS.TryGetValue(fips, out info) ? info : null;
        }
        public static bool IsValidPhoneCode(string phoneCodeDigits, out Info countryInfo)
        {
            countryInfo = null;
            foreach (Info info in All)
            {
                if (phoneCodeDigits.StartsWith(info.PhoneCodeDigits, StringComparison.Ordinal))
                {
                    countryInfo = info;
                    break;
                }
            }
            return countryInfo != null;
        }
        public static bool HasTheSamePhoneCode(Info info, string phoneNumberDigits)
        {
            if (info == null || string.IsNullOrEmpty(phoneNumberDigits))
                return false;
            return phoneNumberDigits.StartsWith(info.PhoneCodeDigits, StringComparison.Ordinal);
        }
        public static ReadOnlyCollection<Info> All
        {
            get;
            private set;
        }
        public static ReadOnlyCollection<Info> Asia
        {
            get;
            private set;
        }
        public static ReadOnlyCollection<Info> Africa
        {
            get;
            private set;
        }
        public static ReadOnlyCollection<Info> Antarctica
        {
            get;
            private set;
        }
        public static ReadOnlyCollection<Info> Oceania
        {
            get;
            private set;
        }
        public static ReadOnlyCollection<Info> Europe
        {
            get;
            private set;
        }
        public static ReadOnlyCollection<Info> NorthAmerica
        {
            get;
            private set;
        }
        public static ReadOnlyCollection<Info> SouthAmerica
        {
            get;
            private set;
        }
        #region Data
        static class Assets
        {
            public static CsvAsset Data = new CountryCodeCsvAsset();
            //
            sealed class CountryCodeCsvAsset : CsvAsset { }
        }
        #endregion Data
    }
}
