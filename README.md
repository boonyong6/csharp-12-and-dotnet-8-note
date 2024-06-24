# Chapter 1: Hello, C#! Welcome, .NET!

## Setting up your development environment
### Visual Studio Code for cross-platform development
* It has strong support for web development, but weak support for mobile and desktop environments.
### Installing other extensions (Visual Studio Code)
```bash
code --install-extension ms-dotnettools.csdevkit
code --install-extension ms-dotnettools.dotnet-interactive-vscode
code --install-extension tintoy.msbuild-project-tools
code --install-extension humao.rest-client
code --install-extension icsharpcode.ilspy-vscode
```

## Understanding .NET
### Understanding intermediate language
* The **C# compiler (Roslyn)** converts source code into intermediate language (IL) code and stores it in an assembly (DLL or EXE).
* IL code statements are executed by **.NET's virtual machine (CoreCLR)**.
* The **just-in-time (JIT) compiler** compiles it into native CPU instructions.

## Building console apps using Visual Studio 2022
### Compiling and running code using Visual Studio
* Attaching a **debugger** requires **more resources**.
### Requirements for top-level programs
* There can be **only one** file like this in a project.
* Any classes or other types must be at the **bottom** of the file.

## Building console apps using Visual Studio Code
```bash
dotnet new sln --name Chapter01

# Targets your latest .NET SDK version by default.
# Use the -f or --framework switch to specify a target framework.
dotnet new console --output HelloCS

dotnet sln add HelloCS
```
### Notes
* dotnet CLI executes the app from the <projectname> folder.
* Visual Studio 2022 executes the app from the <projectname>\bin\Debug\net8.0 folder.
### Summary of other project types
![Project template names for various code editors](images/project-template-names-for-various-code-editors.png)
* Summary of project template defaults, options, and switches: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/ch01-project-options.md)

# Chapter 2: Speaking C#
## Discovering your C# compiler version
### Enabling a specific language version compiler
To use the improvements in a **C# point release** like 7.1, 7.2, or 7.3, you had to add a \<LangVersion> configuration element to the project file.
```xml
<LangVersion>7.3</LangVersion>
```

## Understanding C# grammar and vocabulary
### Comments
/* */ is useful for commenting in the middle of a statement.
```csharp
decimal totalPrice = subtotal /* for this item */ + salesTax;
```
### Implicitly and globally importing namespaces
* The `global using` keyword combination means you only need to import a namespace in one .cs file and it will be available throughout all .cs files.
* Use **.csproj** project file to control which namespaces are implicitly imported.
![Implicitly and globally importing namespaces](images/implicitly-and-globally-importing-namespaces.png)
* Modify the project file to change what is included in the auto-generated class file.

## Working with variables
### Storing text
* Some letter needs two System.Char values to represent it. So, do not always assume one char equals one letter.
### Verbatim strings
* Prefix string with @ symbol to prevent escape character (\\) evaluation.
### Raw string literals
* Start and end with three or more double-quote characters.
```csharp
string xml = """
            <person age="50">
                <first_name>Mark</first_name>
            </person>
            """;
```
### Comparing double and decimal types
* Never compare double values using ==
* `decimal` stores the number as a large integer and shifts the decimal point.
### Storing dynamic types
* Its flexibility comes at the cost of performance.
* The value stored in the variable can have its members invoked without an explicit cast.
* Dynamic types are most useful when interoperating with non-.NET systems.
### Formatting using interpolated strings
* They can't be read from resource files to be localized.
## Exploring more about console apps
### Custom number formatting
![Custom numeric format codes](images/custom-numeric-format-codes.png)

![Standard numeric format codes](images/standard-numeric-format-codes.png)
### Simplifying the usage of the console
* using statement can be used to import a static class.
### Passing arguments to a console app
* Command-line arguments are seperated by **spaces**.
* To include spaces, enclose the argument value in **single or double quotes**.

# Chapter 3: Controlling Flow, Converting Types, and Handling Exceptions
## Operating on variables
### Exploring unary operators
```csharp
int a = 3;
// The ++ operator executes after the assignment (aka postfix operator).
int b = a++; // b is 3
```
* **Good Practice:** Never combine the use of the ++ and -- operators with an assignement operator, =. Perform the operations as separate statements.
### Exploring logical operators (&, |, ^)
* Operate on **Boolean** values.
* For the **XOR ^** logical operator, either operand can be true **(but not both)** for the result to be true.
### Exploring conditional logical operators (&&, ||)
* **Good Practice:** It is safest to avoid conditional logical operators when used in combination with functions that cause side effects.
### Exploring bitwise and binary shift operators (&, |, <<, >>)
* Binary shift operators **(<<, >>)** can perform some common arithmetic calculations (e.g. x * 2, x / 2) much faster than traditional operators.

## Understanding selection statements
### Pattern matching with the if statement
* The `if` statement can use the `is` **keyword** in combination with declaring a local variable.
```csharp
object o = 3;

if (o is int i)
{
    // ...
}
```
### Branching with the switch statement
* **Good Practice:** The `goto` keyword can be a good solution to code logic in some scenarios. But, you **should use it sparingly**.
### Pattern matching with the switch statement
* The `switch` statement supports pattern matching.
```csharp
switch(animal)
{
    // case Cat fourLeggedCat when fourLeggedCat.Legs == 4:

    // Alternative: A more concise pattern-matching syntax.
    case Cat { Legs: 4 } fourLeggedCat:
        // ...
        break;
    case Spider spider:
        // ...
        break;
    default:
        // ...
        break;
}
```
### Simplifying switch statements with switch expressions
* Switch expressions can be used where **all cases return a value to set a single variables**. It uses a **lambda**, **=>**, to indicate a **return value**.

## Understanding iteration statements
### Looping with the foreach statement
* If the sequence structure of the `foreach` statement is modified during iteration, for example, by adding or removing an item, then **an exception will be thrown**.
### Understanding how foreach works internally
* `foreach` statement will work on any type that implements `IEnumerable` interface.

## Storing multiple values in an array
### Working with single-dimensional arrays
![Visualization of an array of four string values](images/visualization-of-an-array-of-four-string-values.png)
* Arrays are always of a fixed size at the time of memory allocation.
### Working with multi-dimensional arrays
![Visualization of a two dimensional array](images/visualization-of-a-two-dimensional-array.png)
* To store **a grid of values**.
* Helpful methods that can discover the lower and upper bounds.
* You can use a `foreach` statement to enumerate all the items in a multi-dimensional array.
```csharp
// To get the first index of the first dimension.
int firstIndex = mdArray.GetLowerBound(0);
// To get the last index of the first dimension.
int lastIndex = mdArray.GetUpperBound(0);
```
### Working with jagged arrays
![Visualization of a jagged array](images/visualization-of-a-jagged-array.png)
* Use jagged arrays (aka **array of arrays**, e.g. `jagged[][]`) when the number of items stored in each dimension is different.
### List pattern matching with arrays
![Examples of list pattern matching](images/examples-of-list-pattern-matching.png)
### Summarizing arrays
* **Arrays** are useful for **temporarily** storing multiple items. **Collections** are more **flexible** option when adding and removing items dynamically.
* Any sequence of items can convert into an array using the `ToArray` extension method.

## Casting and converting between types
### How negative numbers are represented in binary
* Negative aka signed numbers use the **first bit** to represent negativity.
### Converting with the System.Convert type
* An alternative to using the cast operator is to use the `System.Convert` type.
```csharp
double g = 9.8;
// int h = (int)g; // Using the cast operator.
int h = Convert.ToInt32(g); // Using the System.Convert.
```
* Important differences betweem casting and converting:
    * Converting rounds the double value 9.8 up to 10 instead of trimming the part after the decimal point.
    * Casting can allow overflows while converting will throw an exception.
### Rounding numbers and the default rounding rules
* **System.Convert**'s rounding rule is the **Banker's rounding**. This rule reduces bias by alternating when it rounds toward or away from zero.
### Converting from a binary object to a string
* **Base64** encoding is used to represent binary object (e.g. image, video) in a text-based format.
### Parsing from strings to numbers or dates and times
* The opposite of `ToString` is `Parse`.
* **Good Practice:** Use the standard date and time format specifiers: [🔗](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#table-of-format-specifiers)

## Checking for overflow
### Throwing overflow exceptions with the checked statement
* `checked` statement tells .NET to **throw an exception** when an overflow happens.
* `checked` statement is used to change the default overflow behavior at **runtime**.
### Disabling compiler overflow checks with the unchecked statement
* `unchecked` statement is used to change the default overflow behavior at **compile-time**.
    