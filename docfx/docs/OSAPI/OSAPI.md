# OSAPI Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: OSAPI.cs  

## Description
API that handles common tasks so mods don't have to. It is a static class you can reference from anywhere. See [Usage](#usage)

## Usage
```cs
using UnityEngine;
using OSLoader;

public class MyMod : Mod
{
    public void OnInitialize()
    {
        OSScene currentScene = OSAPI.CurrentScene;
        Debug.Log($"Current scene build index is :{(int)currentScene}");
    }
}
```