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
