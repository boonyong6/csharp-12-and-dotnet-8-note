#region Looping with the while statement

int x = 0;
while (x < 10)
{
    WriteLine(x);
    x++;
}

#endregion

#region Looping with the do statement

WriteLine();

// string? actualPassword = "Pa$$w0rd";
// string? password;

// do 
// {
//     Write("Enter your password: ");
//     password = ReadLine();
// }
// while (password != actualPassword);

// WriteLine("Correct!");

/******************************************************************************/

// string? actualPassword = "Pa$$w0rd";
// string? password;
// int attempts = 0;

// do
// {
//     Write("Enter your password: ");
//     password = ReadLine();

//     attempts++;
// }
// while (password != actualPassword && attempts < 3);

// if (attempts == 3)
// {
//     WriteLine("You've entered the wrong password 3 times!");
// }
// else
// {
//     WriteLine("Correct!");
// }

#endregion

#region Looping with the for statement

for (int y = 1; y <= 10; y++)
{
    WriteLine(y);
}
WriteLine();

for (int y = 0; y <= 10; y += 3)
{
    WriteLine(y);
}

#endregion

#region Looping with the foreach statement

WriteLine();

string[] names = { "Adam", "Barry", "Charlie" };

foreach (string name in names)
{
    WriteLine($"{name} has {name.Length} characters.");
}

#endregion