﻿/* Visual Studio 2022: run the app, change the message, click Hot Reload.
 * Visual Studio Code: run the app using dotnet watch, change the message. */

while (true)
{
    WriteLine("Goodbye, Hot Reload!");
    await Task.Delay(2000);
}

// Issue "dotnet watch" command to activate Hot Reload.