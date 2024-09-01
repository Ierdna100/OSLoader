# OSAPI.CurrentScene Property

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: OSAPI.cs  

## Declaration
public static [OSScene](../Utilities/OSScene.md) **CurrentScene**;

## Description
Returns the current scene of the game. In a loading screen, the scene that is being loaded will be returned.
Return value is a value of [OSScene](../Utilities/OSScene.md), if you cast it to an integer, you will obtain the build index of the scene.

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
