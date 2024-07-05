# Chapter 1: Hello, C#! Welcome, .NET!

## Setting up your development environment

### Visual Studio Code for cross-platform development

- It has strong support for web development, but weak support for mobile and desktop environments.

### Installing other extensions (Visual Studio Code)

```bash
code --install-extension ms-dotnettools.csdevkit
code --install-extension ms-dotnettools.dotnet-interactive-vscode
code --install-extension tintoy.msbuild-project-tools
code --install-extension humao.rest-client
code --install-extension icsharpcode.ilspy-vscode
```

## Understanding .NET

### Listing and removing versions of .NET

```bash
dotnet --list-sdks
dotnet --list-runtimes
dotnet --info
```

- On Windows, use the **Apps & features** to remove .NET SDKs.
- On Linux: [🔗](https://learn.microsoft.com/en-us/dotnet/core/install/remove-runtime-sdk-versions?pivots=os-linux)

### Understanding intermediate language

- The **C# compiler (Roslyn)** converts source code into intermediate language (IL) code and stores it in an assembly (DLL or EXE).
- IL code statements are executed by **.NET's virtual machine (CoreCLR)**.
- The **just-in-time (JIT) compiler** compiles it into native CPU instructions.

## Building console apps using Visual Studio 2022

### Compiling and running code using Visual Studio

- Attaching a **debugger** requires **more resources**.

### Requirements for top-level programs

- There can be **only one** file like this in a project.
- Any classes or other types must be at the **bottom** of the file.

## Building console apps using Visual Studio Code

```bash
dotnet new sln --name Chapter01

# Targets your latest .NET SDK version by default.
# Use the -f or --framework switch to specify a target framework.
dotnet new console --output HelloCS

dotnet sln add HelloCS
```

### Notes

- dotnet CLI executes the app from the <projectname> folder.
- Visual Studio 2022 executes the app from the <projectname>\bin\Debug\net8.0 folder.

### Summary of other project types

![Project template names for various code editors](images/project-template-names-for-various-code-editors.png)

- Summary of project template defaults, options, and switches: [🔗](https://githu6.com/markjprice/cs12dotnet8/blob/main/docs/ch01-project-options.md)

## Explore topics

- Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-1---hello-c-welcome-net)

# Chapter 2: Speaking C#

## Discovering your C# compiler version

### Enabling a specific language version compiler

To use the improvements in a **C# point release** like 7.1, 7.2, or 7.3, you had to add a \<LangVersion> configuration element to the project file.

```xml
<LangVersion>7.3</LangVersion>
```

## Understanding C# grammar and vocabulary

### Comments

/\* \*/ is useful for commenting in the middle of a statement.

```csharp
decimal totalPrice = subtotal /* for this item */ + salesTax;
```

### Implicitly and globally importing namespaces

- The `global using` keyword combination means you only need to import a namespace in one .cs file and it will be available throughout all .cs files.
- Use **.csproj** project file to control which namespaces are implicitly imported.
  ![Implicitly and globally importing namespaces](images/implicitly-and-globally-importing-namespaces.png)
- Modify the project file to change what is included in the auto-generated class file.

## Working with variables

### Storing text

- Some letter needs two System.Char values to represent it. So, do not always assume one char equals one letter.

### Verbatim strings

- Prefix string with @ symbol to prevent escape character (\\) evaluation.

### Raw string literals

- Start and end with three or more double-quote characters.

```csharp
string xml = """
            <person age="50">
                <first_name>Mark</first_name>
            </person>
            """;
```

### Comparing double and decimal types

- Never compare double values using ==
- `decimal` stores the number as a large integer and shifts the decimal point.

### Storing dynamic types

- Its flexibility comes at the cost of performance.
- The value stored in the variable can have its members invoked without an explicit cast.
- Dynamic types are most useful when interoperating with non-.NET systems.

### Formatting using interpolated strings

- They can't be read from resource files to be localized.

## Exploring more about console apps

### Custom number formatting

![Custom numeric format codes](images/custom-numeric-format-codes.png)

![Standard numeric format codes](images/standard-numeric-format-codes.png)

### Simplifying the usage of the console

- using statement can be used to import a static class.

### Passing arguments to a console app

- Command-line arguments are separated by **spaces**.
- To include spaces, enclose the argument value in **single or double quotes**.

## Explore topics

- Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-2---speaking-c)

# Chapter 3: Controlling Flow, Converting Types, and Handling Exceptions

## Operating on variables

### Exploring unary operators

```csharp
int a = 3;
// The ++ operator executes after the assignment (aka postfix operator).
int b = a++; // b is 3
```

- **Good Practice:** Never combine the use of the ++ and -- operators with an assignment operator, =. Perform the operations as separate statements.

### Exploring logical operators (&, |, ^)

- Operate on **Boolean** values.
- For the **XOR ^** logical operator, either operand can be true **(but not both)** for the result to be true.

### Exploring conditional logical operators (&&, ||)

- **Good Practice:** It is safest to avoid conditional logical operators when used in combination with functions that cause side effects.

### Exploring bitwise and binary shift operators (&, |, <<, >>)

- Binary shift operators **(<<, >>)** can perform some common arithmetic calculations (e.g. x \* 2, x / 2) much faster than traditional operators.

## Understanding selection statements

### Pattern matching with the if statement

- The `if` statement can use the `is` **keyword** in combination with declaring a local variable.

```csharp
object o = 3;

if (o is int i)
{
    // ...
}
```

### Branching with the switch statement

- **Good Practice:** The `goto` keyword can be a good solution to code logic in some scenarios. But, you **should use it sparingly**.

### Pattern matching with the switch statement

- The `switch` statement supports pattern matching.

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

- Switch expressions can be used where **all cases return a value to set a single variables**. It uses a **lambda**, **=>**, to indicate a **return value**.

## Understanding iteration statements

### Looping with the foreach statement

- If the sequence structure of the `foreach` statement is modified during iteration, for example, by adding or removing an item, then **an exception will be thrown**.

### Understanding how foreach works internally

- `foreach` statement will work on any type that implements `IEnumerable` interface.

## Storing multiple values in an array

### Working with single-dimensional arrays

![Visualization of an array of four string values](images/visualization-of-an-array-of-four-string-values.png)

- Arrays are always of a fixed size at the time of memory allocation.

### Working with multi-dimensional arrays

![Visualization of a two dimensional array](images/visualization-of-a-two-dimensional-array.png)

- To store **a grid of values**.
- Helpful methods that can discover the lower and upper bounds.
- You can use a `foreach` statement to enumerate all the items in a multi-dimensional array.

```csharp
// To get the first index of the first dimension.
int firstIndex = mdArray.GetLowerBound(0);
// To get the last index of the first dimension.
int lastIndex = mdArray.GetUpperBound(0);
```

### Working with jagged arrays

![Visualization of a jagged array](images/visualization-of-a-jagged-array.png)

- Use jagged arrays (aka **array of arrays**, e.g. `jagged[][]`) when the number of items stored in each dimension is different.

### List pattern matching with arrays

![Examples of list pattern matching](images/examples-of-list-pattern-matching.png)

### Summarizing arrays

- **Arrays** are useful for **temporarily** storing multiple items. **Collections** are more **flexible** option when adding and removing items dynamically.
- Any sequence of items can convert into an array using the `ToArray` extension method.

## Casting and converting between types

### How negative numbers are represented in binary

- Negative aka signed numbers use the **first bit** to represent negativity.

### Converting with the System.Convert type

- An alternative to using the cast operator is to use the `System.Convert` type.

```csharp
double g = 9.8;
// int h = (int)g; // Using the cast operator.
int h = Convert.ToInt32(g); // Using the System.Convert.
```

- Important differences between casting and converting:
  - Converting rounds the double value 9.8 up to 10 instead of trimming the part after the decimal point.
  - Casting can allow overflows while converting will throw an exception.

### Rounding numbers and the default rounding rules

- **System.Convert**'s rounding rule is the **Banker's rounding**. This rule reduces bias by alternating when it rounds toward or away from zero.

### Converting from a binary object to a string

- **Base64** encoding is used to represent binary object (e.g. image, video) in a text-based format.

### Parsing from strings to numbers or dates and times

- The opposite of `ToString` is `Parse`.
- **Good Practice:** Use the standard date and time format specifiers: [🔗](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#table-of-format-specifiers)

## Checking for overflow

### Throwing overflow exceptions with the checked statement

- `checked` statement tells .NET to **throw an exception** when an overflow happens.
- `checked` statement is used to change the default overflow behavior at **runtime**.

### Disabling compiler overflow checks with the unchecked statement

- `unchecked` statement is used to change the default overflow behavior at **compile-time**.

## Explore topics

- Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-3---controlling-flow-converting-types-and-handling-exceptions)

# Chapter 4: Writing, Debugging, and Testing Functions

## Writing functions

### What is automatically generated for a local function?

- Local functions have limitations, like they **cannot have XML comments** to document them.

### What is automatically generated for a static function?

- When you use a separate file to define a `partial Program` class with `static` functions, the compiler merges your function as a member of the `Program` class **at the same level as** the \<Main>$method.

### A brief aside about arguments and parameters

- **Parameter** is a **variable** in a function definition.
- **Argument** is the **data** you pass into the method's parameters.

### Documenting functions with XML comments

- This feature is **primarily** designed to be used with a tool that converts the comments into documentation, like **Sandcastle**.

## Hot reloading during development

### Hot reloading using Visual Studio Code and dotnet watch

```bash
# Command to activate Hot Reload.
dotnet watch [run]
```

## Logging during development and runtime

### Understanding logging options

- **Common logging frameworks:** Apache log4net, NLog, **Serilog**

### Instrumenting with Debug and Trace

- `Debug` class is used to add logging that gets written only during **development**.
- `Trace` class is used to add logging that gets written during **both development and runtime**.
- `Debug` and `Trace` classes write to any trace listener.
- Trace listener is a type that can be configured to write output anywhere you like.

### Configuring trace listeners

```bash
# With the Debug configuration, both Debug and Trace are active and will write to any trace listeners.
# With the Release configuration, only Trace will write to any trace listeners.
dotnet run --configuration (Debug|Release)
```

### Adding packages to a project in Visual Studio Code

```bash
# Add a reference to a NuGet package to your project file.
dotnet add package <PACKAGE_NAME>

# Add a project-to-project reference to your project file.
dotnet add reference <PROJECT_NAME>
```

### Logging information about your source code

- In C# 10 and later, you can get more logging information from the compiler by decorating function parameters with special attributes.
  ![Attributes to get information about the method caller](images/attributes-to-get-information-about-the-method-caller.png)

## Unit testing

- **xUnit** is more extensible and has better community support.

## Throwing and catching exceptions in functions

- You should only catch and handle an exception if you have enough information to mitigate the issue.

### Throwing exceptions using guard clauses

![Common guard clauses](images/common-guard-clauses.png)

### Rethrowing exceptions

- 3 ways to rethrow:
  1. `throw`
  2. `throw ex` - This is **usually poor practice**, but can be useful when you want to deliberately remove that information when it contains sensitive data.
  3. throw a new exception, and pass the caught exception as the `innerException` parameter.

## Explore topic

- Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-4---writing-debugging-and-testing-functions)

# Chapter 5: Building Your Own Types with Object-Oriented Programming

## Building class libraries

### Creating a class library

- Although we can use a newer C# 12 compiler on older frameworks, some modern compiler features require a modern .NET runtime. For example, we cannot use default implementations in an interface and the `required` keyword (needs an attribute introduced in .NET 7).
- By default, targeting .NET Standard 2.0 uses the C# 7 compiler, but this can be overriden.

### Understanding type access modifier

- **Good Practice:** Always **explicitly specify** the access modifier for a class.
- The **default** access modifier for a type is `internal`.

### Instantiating a class

- `new` keyword allocates memory for the object and initializes any internal data.

## Storing data in fields

### Member access modifiers

![Six member access modifier](images/six-member-access-modifier.png)

- The **default** access modifier for members is `private`.
- **Good Practice:** Explicitly apply one of the access modifiers to all type members. Fields should usually be `private` or `protected`, and you should then create `public` properties to get or set the field values.

### Storing multiple values using an enum type

- We can combine multiple choices into a single value using `enum` flags.
- Use the | operator (the bitwise logical OR) to combine the `enum` values.

### Making a field static

- Fields, constructors, methods, properties and other members can all be static.

### Making a field constant

- Constants are not always the best choice.
- It must be expressible as a literal string, Boolean, or number value.
- It is replaced with the literal value at compile time, which will, therefore, not be reflected if the value changes in a future version.

### Making a field read-only

- A better choice for fields that should not change is to mark them as read-only.
- Use read-only fields over constant fields.
- It can be expressed using any executable statement.
- It is a live reference.

### Requiring fields to be set during instantiation

- Setting a property or field to be required does not mean that it cannot be null. It just means that it must be explicitly set to null.

## Working with methods and tuples

### Naming parameter values when calling methods

- Use named parameters to skip over optional parameters.

### Controlling how parameters are passed

- By value (**default**): in-only.
- `out` parameter: out-only. They must be set inside the method.
- `ref` parameter (By reference): in-and-out.
- `in` parameter: Think of these as being a reference parameter that is _read-only_.

### Aliasing tuples

```csharp
// Use the title case naming convention for tuple's parameters.
using Fruit = (string Name, int Number);
```

### Splitting classes using partial

- E.g. split the class into an autogenerated code file and a manually edited code file.

## Controlling access with properties and indexers

- A property is simply a method.

### Defining indexers

- Use the array syntax to access a property.
- Indexers can be overloaded.

```csharp
public Person this[int index]
{
    get
    {
        return Children[index];
    }
    set
    {
        Children[index] = value;
    }
}
```

## Working with record types

### Init-only properties

- Use the `init` keyword to make a property immutable. It can be used in place of the `set` keyword in a property definition.

### Defining record types

- Record types are defined by using the `record` keyword.
- It makes the whole object **immutable**.
- It **acts like a value** when compared.
- To change any state after instantiation, you create new records from existing ones using the `with` keyword.

```csharp
ImmutableVehicle repaintedCar = car with
{
    Color = "Polymetal Grey Metallic"
};
```

### Positional data members in records

```csharp
// Simpler syntax to define a record that auto-generates the
// properties, constructor, and deconstructor.
// The class keyword is optional.
public record class ImmutableAnimal(string Name, string Species);
```

### Defining a primary constructor for a class

- Parameters of a class type with a primary constructor don't become public properties automatically.

```csharp
public class Headset(string manufacturer, string productName)
{
    public string Manufacturer { get; set; } = manufacturer;
    public string ProductName { get; set; } = productName;

    // Default parameterless constructor calls the primary constructor.
    public Headset() : this("Microsoft", "HoloLens") { }
}
```

## Explore topics

Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-5---building-your-own-types-with-object-oriented-programming)

# Chapter 6: Implementing Interfaces and Inheriting Classes

## Static methods and overloading operators

- Instance methods are actions that an object does to itself.
- Static methods are actions the type does.

### Implementing functionality using methods

- Use a different method name for related static and instance methods, for example, `Compare(x, y)` and `x.CompareTo(y)`.
- **Good Practice:** A method that creates a new object, or modifies an existing object, should return a reference to that object.

### Implementing functionality using operators

```csharp
// The return type does not need to match th types passed as parameters, but it cannot be void.
public static bool operator +(Person p1, person p2)
{
    // ...
}
```

- Operators do not appear in IntelliSense lists, so make a method for it as well.

## Making types safely reusable with generics

### Working with non-generic types

- Avoid types in the `System.Collections` namespace. Use types in the `System.Collections.Generics` and related namespaces instead.
- `System.Collections.Generic` is implicitly and globally imported by default.

## Raising and handling events

- Events provide a way of exchanging messages between objects.
- Events are built on **delegates**.

### Calling methods using delegates

```csharp
delegate int DelegateWithMatchingSignature(string s);
```

- Think of a delegate as being a **type-safe method pointer**.
- A `delegate` is a reference type like a class, so if you define one in Program.cs then it must be at the bottom of the file.

### Examples of delegate use

- To create a queue of methods.
- To execute multiple actions in parallel.
- To implement events to send messages between different objects.

### Defining and handling delegates

```csharp
// 2 predefined delegates for use as events.
public delegate void EventHandler(object? sender, EventArgs e);
public delegate void EventHandler<TEventArgs>(object? sender, TEventArgs e);
```

### Defining and handling events

```csharp
// Use event keyword to enforce the use of either the += operator or the -= operator to assign and remove methods.
public event EventHandler? Shout;

// To raise/trigger the event.
Shout?.Invoke(this, EventArgs.Empty);
```

## Implementing interfaces

### Common interfaces

![Some common interfaces that your types might implement](images/some-common-interfaces-that-your-types-might-implement.png)

### Comparing objects when sorting

- If a type implements one of the `IComparable` interfaces, then arrays and collections containing instances of that type can be sorted.

### Comparing objects using a separate class

- Useful when you don't have access to the source code for a type.

### Implicit and explicit interface implementations

- Only necessary if a type must have multiple methods with the same name and signature.
- The members of an interface are always and automatically `public`.

### Defining interfaces with default implementations

- This feature **breaks the clean separation** between interfaces that define a contract and classes and other types that implement them.
- Only supported with C# if the target framework is .NET 5 or later, .NET Core 3 or later, or .NET Standard 2.1.

## Managing memory with reference and value types

### Understanding stack and heap memory

- **Stack memory** is **faster**, but **limited in size**. It is managed directly by the CPU.
- **Heap memory** is **slower** but much more **plentiful**.

### Defining reference and value types

- A type using `record` or `class` is a **reference type**. The object itself is allocated on the heap, and only the memory address of the object is stored on the stack.
- A type using `record struct` or `struct` is a **value type**. The object itself is allocated to the stack.
- You cannot inherit from a `struct`.
- `struct` objects are compared for equality using values instead of memory addresses.

### How reference and value types are stored in memory

![How value and reference types are allocated in the stack and heap](images/how-value-and-reference-types-are-allocated-in-the-stack-and-heap.png)

- **Heap memory** could still be allocated after a method returns. It is the .NET runtime **garbage collector**'s responsibility to release this memory at a future date.

### Understanding boxing

- Boxing is when a **value type** is moved to **heap memory** and wrapped inside a **System.Object** instance.
- Boxing can take **up to 20 times longer** than without boxing.

### Equality of types

- **Good Practice:** Use a `record class` as it implements the value equality behavior for you.

### Defining struct types

- You cannot compare struct variables using ==.
- Use `struct` when
  - The total memory used by all the fields is **16 bytes or less**.
  - Only uses value types for its fields.
  - Never want to derive from it.
- Use `class` when
  - Your type uses **more than 16 bytes of stack memory**.
  - Uses reference types for its fields
  - Want to inherit from it.

### Defining record struct types

- `record struct` is not immutable.
- A `struct` does not implement the == and != operators, but they are automatically implemented with a `record struct`.

### Releasing unmanaged resources

- It must be manually released.
- Each type can have a single finalizer to release unmanaged resources.

```csharp
public class ObjectWithUnmanagedResources : IDisposable
{
    public ObjectWithUnmanagedResources() // Constructor.
    {
        // Allocate unmanaged resource.
    }

    ~ObjectWithUnmanagedResources() // Finalizer aka destructor.
    {
        Dispose(false);
    }

    bool _disposed = false; // Indicates if resources have been released.

    public void Dispose()
    {
        Dispose(true);

        // Tell garbage collector it does not need to call the finalizer.
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        // Deallocate the *unmanaged* resource.
        // ...

        if (disposing)
        {
            // Deallocate any other *managed* resources.
            // ...
        }
        _disposed = true;
    }
}
```

## Working with null values

### Controlling the nullability warning check feature

```csharp
// To disable the feature at the file level, add the following to the top of a code file.
#nullable disable

// To enable the feature at the file level, add the following to the top of a code file.
#nullable enable
```

### Disabling null and other compiler warnings

```xml
<!-- To disable compile warnings for a whole project -->
<NoWarn>CS8600;CS8602</NoWarn>
```

```csharp
// To disable compiler warnings at the statement level.
#pragma warning disable CS8602
WriteLine(firstName.Length);
WriteLine(lastName.Length);
#pragma warning restore CS8602
```

### Declaring non-nullable variables and parameters

- Suffixing a reference type with ? does not change the type. This is different from suffixing a value type with ?, which changes its type to Nullable<T>.

## Inheriting from classes

### Overriding members

- Rather than hiding a method, it is usually better to **override** it.
- Use the `virtual` keyword to any methods that should allow overriding.

### Choosing between an interface and abstract class

- Every member of an interface must be public.
- An abstract class has more flexibility in its members' access modifiers.

### Understanding polymorphism

- **Good Practice:** You should use `virtual` and `override` rather than `new` to change the implementation of an inherited method whenever possible.

## Casting within inheritance hierarchies

### Using is to check and cast a type

```csharp
if (aliceInPerson is Employee explicitAlice)
{
    // ...
}
```

### Using as to cast a type

```csharp
// as keyword returns null if the type cannot be cast.
Employee? aliceAsEmployee = aliceToPerson as Employee;
```

## Inheriting and extending .NET types

### Inheriting exceptions

- Constructors are not inherited.
- **Good Practice:** When defining your own exceptions, give them the same three constructors that explicitly call the built-in ones in `System.Exception`.

### Using extension methods to reuse functionality

- Extension methods cannot replace or override existing instance methods. Instance method will be called in preference.

### Mutability and records

- The mutability of a record type depends on how the record is defined.

## Writing better code

Featured **StyleCop**: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/ch06-writing-better-code.md)

## Explore topics

Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-6---implementing-interfaces-and-inheriting-classes)

# Chapter 7: Packaging and Distributing .NET Types

## The road to .NET 8

- .NET 5 removed the need for .NET Standard because all project types can now target a single version of .NET.

### Checking your .NET SDKs for updates

```bash
# To check the versions of .NET SDKs and runtimes.
dotnet sdk check
```

## Understanding .NET components

### Dependent assemblies

- Circular references are often a warning sign of poor code design.

### Namespaces and types in assemblies

- Many common .NET types are in the `System.Runtime.dll` assembly.

### Relating C# keywords to .NET types

- All C# keywords that represent types are aliases for a .NET type.

### Understanding defaults for class libraries with different SDKs

- **Good Practice:** Always check the target framework of a class library and then manually change it to something more appropriate if necessary.

### Controlling the .NET SDK

- You can control the .NET SDK used by default for a solution or project by using a `global.json` file.

```bash
# To create a global.json file that forces the use of the latest .NET Core 6.0 SDK.
dotnet new globaljson --sdk-version 6.0.314
```

## Publishing your code for deployment

### Three ways to publish and deploy a .NET application

1. **Framework-dependent deployment (FDD)** - You deploy a DLL that must be executed by the dotnet cli.
2. **Framework-dependent executable (FDE)** - You deploy an EXE that can be run directly from the command line.
3. **Self-contained**

### Creating a console app to publish

- **Runtime identifier (RID) values:** `win-x64`, `osx-x64`, `osx-arm64`, `linux-x64`, `linux-arm64`.

### Getting information about .NET and its environment

```bash
dotnet --info
```

### Publishing a self-contained app

```bash
# To build and publish the self-contained release version of the application.
dotnet publish -c Release -r <RID> --self-contained
```

### Publishing a single-file app

```bash
# Prerequisite: .NET must be already installed on the host.
# To publish the application as a single file (if possible). The .pdb file is a program debug database file that stores debugging information.
dotnet publish -r <RID> -c Release --no-self-contained -p:PublishSingleFile=true
```

```xml
<!-- In .csproj file -->

<!-- To embed the .pdb file in the .exe file -->
<DebugType>embedded</DebugType>
```

### Reducing the size of apps using app trimming

- **Partial trimming:** By not packaging unused assemblies (aka assembly-level trimming).
- **Full trimming:** By not packaging unused types and members (aka Type-level and member-level trimming). It's the **default** trim mode in **.NET 6 or later**.
- **Limitation:** If your code is **dynamic**, perhaps using **reflection**, then it **might not work correctly**.

```xml
<!-- First way, using .csproj file -->

<!-- Enable trimming. -->
<PublishTrimmed>true</PublishTrimmed>
<!-- Set trim mode to either "full" or "partial". -->
<TrimMode>partial</TrimMode>
```

```bash
# Second way, using dotnet cli

dotnet publish -r <RID> -c Release -p:PublishTrimmed=True
dotnet publish -r <RID> -c Release -p:PublishTrimmed=True -p:TrimMode=partial
```

### Controlling where build artifact are created

```bash
# To control where build artifacts are created. (Introduced with .NET 8)
dotnet new buildprops --use-artifacts
```

## Native ahead-of-time (AOT) compilation

- Compiles IL code to native code at **the time of publishing**.
- AOT assemblies must target a specific runtime environment like Windows x64 or Linux Arm.

### Limitations of native AOT

- No dynamic loading of assemblies.
- No runtime code generation.
- Requires trimming.
- Must be self-contained (larger file size).

### Requirements for native AOT

- On Windows, you must install the Visual Studio 2022 Desktop development with C++ workload with all default components.
- **Warning:** Cross-platform native AOT publishing is not supported. You must run the publish on the operating system that you will deploy to.

### Enabling native AOT for a project

```xml
<!-- In .csproj -->
<PublishAot>true</PublishAot>
```

### Publishing a native AOT project

- If your project does not produce any AOT warnings at publish time, you can then be confident that your service will work.

## Decompiling .NET assemblies

### Viewing source links with Visual Studio 2022

- A feature that allow you to view the original source code using source links.
- Not available in Visual Studio Code.

## Packaging your libraries for NuGet distribution

### Packaging a library for NuGet

- Project file's property (aka MSBuild Property) reference: [🔗](https://learn.microsoft.com/en-us/nuget/reference/msbuild-targets#pack-target)

```bash
# To create a package on build (Assuming <GeneratePackageOnBuild> is set to true in the project file).
dotnet build -c Release

# To create a package manually.
dotnet pack -c Release
```

### Publishing a package to a private NuGet feed

- Learn more: [🔗](https://learn.microsoft.com/en-us/nuget/hosting-packages/overview)

## Explore topics

- Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-7---packaging-and-distributing-net-types)
