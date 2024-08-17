using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Serialization;
using CsvSerializer = ServiceStack.Text.CsvSerializer;

partial class Program
{
    private static void SerializeToXmlFile<T>(T instance, string outputPath) where T : class
    {
        using FileStream fs = File.Create(outputPath);

        XmlSerializer serializer = new(instance.GetType());
        serializer.Serialize(fs, instance);
    }

    private static void SerializeToJsonFile<T>(T instance, string outputPath) where T : class
    {
        using FileStream fs = File.Create(outputPath);

        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
        };
        JsonSerializer.Serialize(fs, instance, options);
    }

    private static void SerializeToCsvFile<T>(ICollection<T> collection, string outputPath) where T : class
    {
        string csv = CsvSerializer.SerializeToCsv(collection);

        File.WriteAllText(outputPath, csv);
    }

    private static void SerializeToFiles<T>(ICollection<T> collection, string outputDirectory, 
        [CallerArgumentExpression(nameof(collection))] string collectionName = null!) where T : class
    {
        string fileNameWithoutExtension = Path.Combine(outputDirectory, $"{collectionName}");

        SerializeToXmlFile(collection, $"{fileNameWithoutExtension}.xml");
        SerializeToJsonFile(collection, $"{fileNameWithoutExtension}.json");
        SerializeToCsvFile(collection, $"{fileNameWithoutExtension}.csv");
    }
}
