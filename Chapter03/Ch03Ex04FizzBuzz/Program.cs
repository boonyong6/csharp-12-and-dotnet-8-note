using System.Text;

StringBuilder stringBuilder = new();

for (int i = 1; i <= 100; i++)
{
    stringBuilder.Clear();

    if (i % 3 == 0)
    {
        stringBuilder.Append("Fizz");
    }

    if (i % 5 == 0)
    {
        stringBuilder.Append("Buzz");
    }

    if (stringBuilder.Length == 0) {
        stringBuilder.Append(i);
    }

    Write(stringBuilder);

    if (i != 100)
    {
        Write(", ");
    }
}
