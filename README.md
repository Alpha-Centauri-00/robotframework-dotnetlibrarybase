<!--
SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>

SPDX-License-Identifier: Apache-2.0
-->

# robotframework-DotNetLibraryBase.NET

[![PyPI - Version](https://img.shields.io/pypi/v/robotframework-dotnetlibrarybase.svg)](https://pypi.org/project/robotframework-dotnetlibrarybase)
[![PyPI - Python Version](https://img.shields.io/pypi/pyversions/robotframework-dotnetlibrarybase.svg)](https://pypi.org/project/robotframework-dotnetlibrarybase)
[![.NET 8](https://img.shields.io/badge/.NET-8-blueviolet?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![License](https://img.shields.io/github/license/imbus/robotframework-dotnetlibrarybase.svg)](https://github.com/imbus/robotframework-dotnetlibrarybase/blob/main/LICENSE)
[![Downloads](https://img.shields.io/pypi/dm/robotframework-dotnetlibrarybase.svg)](https://pypi.org/project/robotframework-dotnetlibrarybase)

# Introduction
`robotframework-DotNetLibraryBase` is a library designed to extend Robot Framework by enabling the use of custom C# functions as test keywords. It allows the creation of new C# classes, where multiple functions can be defined and then exposed as keywords for test automation within Robot Framework. This approach facilitates direct integration of .NET features into test suites, supporting a wide range of .NET functionalities with straightforward configuration and usage.

For a complete list of changes and releases history, refer to [Change Log](CHANGELOG.md).

## Features

- Seamless integration with .NET libraries.
- Easy to use and configure.
- Supports various .NET functionalities out-of-the-box.

## Prerequisites

- .NET [download page](https://dotnet.microsoft.com/download).
- Python [download page](https://www.python.org/).
- Required VS Code Extensions
  - [RobotCode](https://marketplace.visualstudio.com/items?itemName=d-biehl.robotcode)
  - [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) (optional)
  - [C# Dev Kit Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) (optional)

# Installation

Create a new virtual environment and activate it (recommended)

## Install dotnetlibrarybase

```console
pip install robotframework-dotnetlibrarybase
```

# .NET Setup Guide

## Create a Solution

```sh
dotnet new sln -o . -n libraryname
```

The template "Solution File" will be created successfully.

## Create .NET Project
Create a new folder called `src` and navigate into it:

```sh
mkdir src
cd src
dotnet new classlib -n libraryname.example
```

## Add Projects to solution
Navigate back and add the new project to the solution:

```sh
dotnet sln add .\src\libraryname.example\
```

## Create C# function
In `src/library.name/Class1.cs`, add this function:

```c#
namespace libraryname.example;

public class Class1
{
    public void HelloFromCS()
    {
        System.Console.WriteLine("Hello Mars, from C#");
    }
}
```

## Build .NET
Navigate to `.\src\libraryname.example\` and run:

```sh
dotnet build
```

## Create robot.toml
Create a new `robot.toml` file with the following content:

```toml
extend-python-path = ["src/libraryname.example/bin/Debug/net8.0"]

[env]
PYTHONNET_RUNTIME = "coreclr"
```

## Create Robot Framework Test
Create a new `test.robot` file:

```robotframework
*** Settings ***
Library    DotNetLibraryBase    libraryname.example.Class1, libraryname.example
#info      DotNetLibraryBase    [namespace].[classname], [Assemblyname]

*** Test Cases ***
New test
    Hello From CS
```

# Reporting Issues

If you encounter any bugs, have questions, or want to suggest improvements, please don't hesitate to open an [Issues](https://github.com/imbus/robotframework-dotnetlibrarybase/issues). Your feedback is valuable and helps make this project better for everyone.

We appreciate your contribution to improving this project!
