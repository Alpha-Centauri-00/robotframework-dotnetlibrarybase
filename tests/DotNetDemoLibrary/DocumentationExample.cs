// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

namespace DotNetDemoLibrary;

using System;

/// <summary>
/// Provides examples of using this class to see the docu
/// </summary>
public class DocumentationExample
{
    /// <summary>
    /// This is an example of XML documentation.
    /// The documentation will be shown in Robot Framework.
    /// </summary>
    /// <remarks>
    /// This keyword demonstrates XML documentation in IntelliSense.
    /// </remarks>
    public void XmlDocumentedKeyword()
    {
        Console.WriteLine("This keyword uses XML documentation");
    }

    /// <summary>
    /// Example with parameters
    /// </summary>
    /// <param name="text">The text to display</param>
    /// <param name="count">Number of times to repeat</param>
    public void AttributeDocumentedKeyword(string text, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(text);
        }
    }

    /// <summary>
    /// This keyword has both XML documentation...
    /// </summary>
    /// <param name="message">Optional message to display</param>
    /// <returns>The message that was displayed</returns>
    public string BothDocumentationTypesKeyword(string message = "Default message")
    {
        Console.WriteLine(message);
        return message;
    }

    /// <summary>
    /// No Docu keyword
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int adding(int a, int b)
    {
        return a + b;
    }
}
