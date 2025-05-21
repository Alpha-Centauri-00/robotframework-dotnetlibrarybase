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
            
            // Try to find the member with exact match first
            var member = TryFindMember(doc, methodInfo, exact: true);
            
            // If not found, try a more lenient search
            member ??= TryFindMember(doc, methodInfo, exact: false);
            
            if (member == null) return null;

            // Return just the summary content
            return member.Element("summary")?.Value.Trim();
        }
        catch
        {
            // If anything goes wrong, return null
            return null;
        }
    }

    private static XElement? TryFindMember(XDocument doc, MethodInfo methodInfo, bool exact)
    {
        var typeName = methodInfo.DeclaringType?.FullName ?? string.Empty;
        var methodName = methodInfo.Name;
        
        // Get all member elements
        var members = doc.Root?.Elements("members")
                       .Elements("member")
                       .Where(m => m.Attribute("name")?.Value.StartsWith($"M:{typeName}.{methodName}") == true)
                       .ToList();
        
        if (members == null || !members.Any()) 
            return null;
            
        if (exact)
        {
            // Try to find exact match with parameters
            var parameters = methodInfo.GetParameters();
            var parameterTypes = parameters.Length == 0 
                ? string.Empty 
                : $"({string.Join(",", parameters.Select(GetTypeNameForXmlDoc))})";
                
            var exactName = $"M:{typeName}.{methodName}{parameterTypes}";
            return members.FirstOrDefault(m => 
                string.Equals(m.Attribute("name")?.Value, exactName, StringComparison.Ordinal));
        }
        
        // Return first match (most specific one)
        return members.FirstOrDefault();
    }
    
    private static string GetTypeNameForXmlDoc(ParameterInfo parameter)
    {
        return GetTypeNameForXmlDoc(parameter.ParameterType);
    }
    
    private static string GetTypeNameForXmlDoc(Type type)
    {
        // Handle array types
        if (type.IsArray)
        {
            return $"{GetTypeNameForXmlDoc(type.GetElementType()!)}[]";
        }
        
        // Handle generic types
        if (type.IsGenericType)
        {
            var name = type.Name.Split('`')[0];
            var args = string.Join(",", type.GetGenericArguments().Select(t => GetTypeNameForXmlDoc(t)));
            return $"{name}{{{args}}}";
        }
        
        // Handle nullable value types
        var underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
        {
            return $"{GetTypeNameForXmlDoc(underlyingType)}?";
        }
        
        // Default case - use full name
        return type.FullName ?? type.Name;
    }
}