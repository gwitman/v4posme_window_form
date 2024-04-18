using System.IO;

namespace v4posme_window.Libraries;

public class CsvReader
{
    public char Separator { get; set; } = ',';
    public string SeparatorLine { get; set; } = "\n";

    public void WriteFile(string filePath, List<Dictionary<string, object>>? data)
    {
        if (data is null || data.Count <= 0) return;
        // Obtener el encabezado
        var row1 = data[0];
        var values = row1.Keys.ToList();
        values.Add("CANTIDAD");
        // Crear el archivo
        using var writer = new StreamWriter(filePath);
        // Escribir el encabezado
        writer.WriteLine(string.Join(Separator.ToString(), values));
        // Escribir la información
        foreach (var row in data)
        {
            // Obtener la fila
            values.AddRange(row.Values.Select(val => val.ToString()));

            // Escribir la fila
            values.Add("0");
            writer.WriteLine(string.Join(Separator.ToString(), values));
        }
    }
    public List<Dictionary<string, string>> ParseFile(string filePath)
    {
        var content = new List<Dictionary<string, string>>();

        // Abrir el archivo
        using var reader = new StreamReader(filePath);
        // Leer las líneas del archivo
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line != null)
            {
                var data = line.Split(Separator);
                if (content.Count == 0)
                {
                    // Si es la primera línea, establecer los nombres de las columnas
                    for (int i = 0; i < data.Length; i++)
                    {
                        content.Add(new Dictionary<string, string>());
                        content[0][$"Column{i + 1}"] = data[i];
                    }
                }
                else
                {
                    // Para las siguientes líneas, asignar los valores a las columnas correspondientes
                    var item = new Dictionary<string, string>();
                    for (int i = 0; i < data.Length && i < content[0].Count; i++)
                    {
                        var column = content[0][$"Column{i + 1}"];
                        if (!string.IsNullOrEmpty(column))
                        {
                            item[column] = data[i];
                        }
                    }
                    content.Add(item);
                }
            }
        }

        return content;
    }
}