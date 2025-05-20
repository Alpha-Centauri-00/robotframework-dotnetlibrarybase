*** Settings ***
Library    DotNetLibraryBase    DotNetDemoLibrary.DocumentationExample, DotNetDemoLibrary

*** Test Cases ***
Test Documentation Types
    XML Documented Keyword
    Attribute Documented Keyword    Hello from Robot    count=3
    ${message}=    Both Documentation Types Keyword    Custom message
    Log    ${message}
    ${result}=    Adding    5    5
    Log    ${result}
    