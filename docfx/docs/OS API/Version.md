# Version Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: Version.cs  

## Declaration

public **Version**(string rawVersion)

## Parameters
rawVersion | Raw string of the version to parse, format is `x.y.z` as defined by the SemVer standard.
-- | -

## Description
Represents a version partially compliant with [SemVer](https://semver.org/). 

## Usage
[!code-csharp[](../Code Examples/Version.cs)]