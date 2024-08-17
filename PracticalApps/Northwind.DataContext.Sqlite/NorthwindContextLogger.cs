using static System.Environment;

namespace Northwind.EntityModels;

public class NorthwindContextLogger
{
    public static void WriteLine(string message)
    {
        string path = Path.Combine(GetFolderPath(SpecialFolder.Desktop), 
            "northwind-log.txt");

        using StreamWriter textFile = File.AppendText(path);
        textFile.WriteLine(message);
        // textFile.Close();
    }
}
