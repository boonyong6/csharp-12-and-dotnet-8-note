# Chapter 1: Hello, C#! Welcome, .NET!

## Setting up your development environment

### Visual Studio Code for cross-platform development

- It has **strong support for web development**, but weak support for mobile and desktop environments.

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

- dotnet CLI executes the app from the <ProjectName> folder.
- Visual Studio 2022 executes the app from the <ProjectName>\bin\Debug\net8.0 folder.

### Summary of other project types

![Project template names for various code editors](../images/project-template-names-for-various-code-editors.png)

- Summary of project template defaults, options, and switches: [🔗](https://githu6.com/markjprice/cs12dotnet8/blob/main/docs/ch01-project-options.md)

## Practicing and exploring

### Explore topics

- Learn more: [🔗](https://github.com/markjprice/cs12dotnet8/blob/main/docs/book-links.md#chapter-1---hello-c-welcome-net)
