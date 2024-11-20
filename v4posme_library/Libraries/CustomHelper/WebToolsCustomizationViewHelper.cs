using System.Xml;

namespace v4posme_library.Libraries.CustomHelper
{
    public class WebToolsCustomizationViewHelper
    {
        private readonly string? _xmlFilePath  = VariablesGlobales.ConfigurationBuilder["PATH_CUSTOM_FILE"];

        public string? GetBehavior(string? typeCompany, string? keyController, string? keyElement, string? defaultValue)
        {
            var divs = ReadXmlFile();
            if (keyController != "core_web_menu")
            {
                var key = typeCompany.ToLower() + "_" + keyController.ToLower() + "_" + keyElement;
                if (!divs.ContainsKey(key))
                {
                    // Si el key no existe, buscar el key para la empresa por defecto
                    key = "default" + "_" + keyController.ToLower() + "_" + keyElement.ToLower();
                    if (!divs.ContainsKey(key))
                    {
                        return defaultValue;
                    }
                    else
                    {
                        return divs[key];
                    }
                }
                else
                {
                    // Si el key existe, buscar el valor del key
                    return divs[key];
                }
            }
            // Menú
            // Si el key no existe, regresa el mismo valor
            else
            {
                // Lenguaje
                var key = typeCompany.ToLower() + "_" + keyController.ToLower() + "_" + keyElement!.ToLower();
                if (divs.TryGetValue(key, out var value))
                {
                    // Si el key no existe, regresar el elemento
                    return keyElement;
                }
                else
                {
                    // Si el key existe, retornar valor
                    return value;
                }
            }
        }

        public Dictionary<string?, string?> ReadXmlFile()
        {
            if (_xmlFilePath is null)
            {
                throw new Exception("Debe especificar la ruta para lerr el archivo");
            }

            var xmlDoc = new XmlDocument();
            var xmlData = new Dictionary<string?, string?>();
            try
            {
                xmlDoc.Load(_xmlFilePath);
                var root = xmlDoc.DocumentElement;
                if (root is null)
                {
                    throw new Exception("No se pudo leer el archivo");
                }

                foreach (XmlNode node in root.ChildNodes)
                {
                    xmlData.Add(node.Name, node.InnerText);
                }

                return xmlData;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}