// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

namespace RobotFramework.DotNetLibraryBase;

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
            var memberName = $"M:{methodInfo.DeclaringType?.FullName}.{methodInfo.Name}";

            // Find the member documentation
            var member = doc.Root?.Elements("members")
                           .Elements("member")
                           .FirstOrDefault(m => m.Attribute("name")?.Value == memberName);

            if (member == null) return null;

            // Build complete documentation including summary, params, and returns
            var documentation = new StringBuilder();

            // Add summary
            var summary = member.Element("summary");
            if (summary != null)
            {
                documentation.AppendLine(summary.Value.Trim());
                documentation.AppendLine();
            }

            // Add parameters
            var parameters = member.Elements("param");
            if (parameters.Any())
            {
                documentation.AppendLine("Parameters:");
                foreach (var param in parameters)
                {
                    var name = param.Attribute("name")?.Value;
                    var description = param.Value.Trim();
                    documentation.AppendLine($"- {name}: {description}");
                }
                documentation.AppendLine();
            }

            // Add return value
            var returns = member.Element("returns");
            if (returns != null)
            {
                documentation.AppendLine("Returns:");
                documentation.AppendLine(returns.Value.Trim());
            }

            return documentation.ToString().Trim();
        }
        catch
        {
            // If anything goes wrong, return null
            return null;
        }
    }
}
