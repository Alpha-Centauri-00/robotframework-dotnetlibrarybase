// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

namespace RobotFramework.DotNetLibraryBase;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Text;

#pragma warning disable 1591
public static class ReflectionExtensions
{
    public static string? GetXmlDocumentation(this MethodInfo methodInfo)
    {
        try
        {
            // Get the assembly that contains the method
            var assembly = methodInfo.DeclaringType?.Assembly;
            if (assembly == null) return null;

            // Get the XML documentation file path
            var xmlPath = Path.ChangeExtension(assembly.Location, "xml");
            if (!File.Exists(xmlPath)) return null;

            // Load and parse the XML documentation
            var doc = XDocument.Load(xmlPath);
            
            // Build the member ID that matches the XML documentation format
            var parameters = methodInfo.GetParameters();
            var parameterTypes = parameters.Length == 0 
                ? string.Empty 
                : $"({string.Join(",", parameters.Select(p => p.ParameterType.FullName))})";
            
            var memberName = $"M:{methodInfo.DeclaringType?.FullName}.{methodInfo.Name}{parameterTypes}";
            
            // Find the member documentation
            var member = doc.Root?.Elements("members")
                           .Elements("member")
                           .FirstOrDefault(m => m.Attribute("name")?.Value == memberName);

            if (member == null) 
            {
                // Try without parameter types if not found (for backward compatibility)
                memberName = $"M:{methodInfo.DeclaringType?.FullName}.{methodInfo.Name}";
                member = doc.Root?.Elements("members")
                           .Elements("member")
                           .FirstOrDefault(m => m.Attribute("name")?.Value.StartsWith(memberName) == true);
                
                if (member == null) return null;
            }

            // Return just the summary content
            return member.Element("summary")?.Value.Trim();
        }
        catch
        {
            // If anything goes wrong, return null
            return null;
        }
    }
}