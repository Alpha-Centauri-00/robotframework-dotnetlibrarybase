// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

namespace RobotFramework.DotNetLibraryBase;

using System;

[AttributeUsage(AttributeTargets.Method)]
#pragma warning disable 1591
public class RobotKeywordDocumentationAttribute : Attribute
{
    public string Documentation { get; }

    public RobotKeywordDocumentationAttribute(string documentation)
    {
        Documentation = documentation;
    }
}