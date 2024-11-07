namespace v4posme_window.Utilities
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public abstract class CsvAsset
    {
        public static string AssetsRoot = "v4posme_window.Assets.";
        public static System.Reflection.Assembly ResourcesAssembly = typeof(CsvAsset).Assembly;
        //
        readonly string csvName;
        protected CsvAsset(string csvName = null)
        {
            if (string.IsNullOrEmpty(csvName))
            {
                var typeName = GetType().Name;
                this.csvName = typeName.Substring(0, typeName.Length - nameof(CsvAsset).Length);
            }
            else this.csvName = csvName;
        }
        string[] headersCore;
        public string[] Headers
        {
            get
            {
                if (headersCore == null)
                    ReadCsv(ref headersCore, ref rowsCore);
                return headersCore;
            }
        }
        List<string[]> rowsCore;
        public IReadOnlyList<string[]> Rows
        {
            get
            {
                if (rowsCore == null)
                    ReadCsv(ref headersCore, ref rowsCore);
                return rowsCore;
            }
        }
        void ReadCsv(ref string[] headers, ref List<string[]> rows)
        {
            string resourceName = AssetsRoot + $"CSV.{csvName}.csv";
            using (var stream = GetResourceStream(resourceName, ResourcesAssembly))
            {
                var reader = stream != null ? new StreamReader(stream) : StreamReader.Null;
                using (var lineReader = new CsvLineReader(reader))
                {
                    headers = lineReader.ReadLine();
                    rows = new List<string[]>();
                    string[] line = null;
                    while ((line = lineReader.ReadLine(headers.Length)) != null)
                        rows.Add(line);
                }
            }
        }
        #region ReadCSV
        readonly static ConcurrentDictionary<string, string> mappings =
            new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        static Stream GetResourceStream(string key, System.Reflection.Assembly resourcesAssembly)
        {
            string resourceName;
            if (!mappings.TryGetValue(key, out resourceName))
            {
                var resourceNames = resourcesAssembly.GetManifestResourceNames();
                TryAddResourceNameMappings(resourceNames, key, out resourceName);
            }
            return !string.IsNullOrEmpty(resourceName) ? resourcesAssembly.GetManifestResourceStream(resourceName) : null;
        }
        static void TryAddResourceNameMappings(string[] names, string key, out string resourceName)
        {
            resourceName = string.Empty;
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    if (mappings.TryAdd(names[i], names[i]) && StringComparer.OrdinalIgnoreCase.Compare(key, names[i]) == 0)
                        resourceName = names[i];
                }
            }
        }
        protected sealed class CsvLineReader : IDisposable
        {
            readonly TextReader textReader;
            char next;
            readonly List<string> _fields = new List<string>();
            readonly StringBuilder fieldBuilder = new StringBuilder();
            //
            const char Eof = '\0';
            const char CarriageReturn = '\r';
            const char LineFeed = '\n';
            const char TextQualifier = '"';
            const char Delimiter = ',';
            //
            public CsvLineReader(TextReader textReader)
            {
                this.textReader = textReader;
                next = ToChar(textReader.Peek());
            }
            void IDisposable.Dispose()
            {
                textReader.Dispose();
            }
            public string[] ReadLine(int count = -1)
            {
                if (next == Eof)
                    return null;
                _fields.Clear();
                string field = ReadField();
                while (field != null)
                {
                    _fields.Add(field);
                    field = ReadField();
                }
                if (_fields.Count < count)
                    _fields.Add(string.Empty);
                return _fields.ToArray();
            }
            string ReadField()
            {
                if (next == CarriageReturn)
                {
                    Read();
                    if (next == LineFeed)
                        Read();
                    return null;
                }
                if (next == LineFeed)
                {
                    Read();
                    return null;
                }
                ReadWhitespace();
                if (next == Eof)
                    return null;
                if (next == TextQualifier)
                    return ReadQualifiedField();
                else
                    return ReadUnqualifiedField();
            }
            void ReadWhitespace()
            {
                while (char.IsWhiteSpace(next) &&
                    next != Delimiter &&
                    next != CarriageReturn &&
                    next != LineFeed &&
                    next != Eof)
                {
                    Read();
                }
            }
            string ReadQualifiedField()
            {
                Read(); // Skip first quote
                fieldBuilder.Clear();
                char c = Read();
                while (c != Eof)
                {
                    if (c == TextQualifier)
                    {
                        if (next == TextQualifier)
                            Read();
                        else break;
                    }
                    fieldBuilder.Append(c);
                    c = Read();
                }
                string field = fieldBuilder.ToString();
                ReadUnqualifiedField();
                return field;
            }
            string ReadUnqualifiedField()
            {
                fieldBuilder.Clear();
                while (
                    next != Delimiter &&
                    next != CarriageReturn &&
                    next != LineFeed &&
                    next != Eof)
                {
                    fieldBuilder.Append(next);
                    Read();
                }
                if (next == Delimiter)
                    Read();
                return fieldBuilder.ToString();
            }
            char Read()
            {
                char current = ToChar(textReader.Read());
                next = ToChar(textReader.Peek());
                return current;
            }
            static char ToChar(int c)
            {
                return c < 0 ? Eof : (char)c;
            }
        }
    }
    #endregion ReadCSV
}
