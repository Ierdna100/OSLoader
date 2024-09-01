# OSAPI.GameVersion Property

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: OSAPI.cs  

## Declaration
public static [Version](../Utilities/Version.md) **GameVersion**;

## Description
Returns the current game version as a [Version](../Utilities/Version.md).

## Usage
```cs
using UnityEngine;
using OSLoader;

public class MyMod : Mod
{
    public bool IsGameVersionSufficient()
    {
        return OSAPI.GameVersion >= this.info.GameVersion;
    }
}
```
