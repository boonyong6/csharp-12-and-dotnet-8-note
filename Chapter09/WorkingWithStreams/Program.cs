using System.Xml; // To use XmlWriter and so on.
using Packt.Shared; // To use Viper.

#region Writing to text streams

SectionTitle("Writing to text streams");

// Define a file to write to.
string textile = Combine(CurrentDirectory, "streams.txt");

// Create a text file and return a helper writer.
StreamWriter text = File.CreateText(textile);

// Enumerate the string, writing each one to the stream on a separate line.
foreach (string item in Viper.CallSigns)
{
    text.WriteLine(item);
}
text.Close(); // Release unmanaged file resources.

OutputFileInfo(textile);

#endregion

#region Writing to XML streams

SectionTitle("Writing to XML streams");

// Define a file path to write to.
string xmlFile = Combine(CurrentDirectory, "streams.xml");

// Declare variables for the FileStream and XML writer.
FileStream? xmlFileStream = null;
XmlWriter? xmlWriter = null;

try
{
    xmlFileStream = File.Create(xmlFile);

    // Wrap the file stream in an XML writer helper and tell it
    // to automatically indent nested elements.
    xmlWriter = XmlWriter.Create(xmlFileStream, 
        new XmlWriterSettings { Indent = true });
    
    // Write the XML declaration.
    xmlWriter.WriteStartDocument();

    // Write a root element.
    xmlWriter.WriteStartElement("callsigns");

    // Enumerate the strings, writing each one to the stream.
    foreach (string item in Viper.CallSigns)
    {
        xmlWriter.WriteElementString("callsign", item);
    }

    // Write the close root element.
    xmlWriter.WriteEndElement();
}
catch (Exception ex)
{
    // If the path doesn't exist the exception will be caught.
    WriteLine($"{ex.GetType()} says {ex.Message}");
}
finally
{
    if (xmlWriter is not null)
    {
        xmlWriter.Close();
        WriteLine("The XML writer's unmanaged resources have been disposed.");
    }

    if (xmlFileStream is not null)
    {
        xmlFileStream.Close();
        WriteLine("The file stream's unmanaged resources have been disposed.");
    }
}

OutputFileInfo(xmlFile);

#endregion

#region Compressing streams

SectionTitle("Compressing streams");
Compress(algorithm: "gzip");
Compress(algorithm: "brotli");

#endregion