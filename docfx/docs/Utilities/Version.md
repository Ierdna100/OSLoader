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
```cs
using UnityEngine;
using OSLoader;

public class MyMod : Mod
{
    public void OnModInitialize() 
    {
        Version version1 = new Version("1.0.0");
        Version version2 = new Version("1.0.1");

        // All comparison operators are implemented
        // Prints: false
        Debug.Log(version1 > version2);
    }
}
```