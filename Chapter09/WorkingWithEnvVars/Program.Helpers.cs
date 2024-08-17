// null namespace to merge with auto-generated Program.

using System.Text;

partial class Program
{
    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"*** {title} ***");
        ForegroundColor = previousColor;
    }

    private static void DictionaryToTable(IDictionary dictionary)
    {
        Table table = new();
        table.AddColumn("Key");
        table.AddColumn("Value");

        StringBuilder sb = new();

        foreach (string key in dictionary.Keys)
        {
            sb.Clear();
            sb.Append(dictionary[key]!.ToString());
            sb.Replace('[', '{').Replace(']', '}');
            
            table.AddRow(key, sb.ToString());
        }

        AnsiConsole.Write(table);
    }
}
