using UnityEngine;
using OSLoader;

public class MyMod : Mod
{
    public static TestMod instance;

    public override void InitializeMod()
    {
        Version version1 = new Version("1.0.0");
        Version version2 = new Version("1.0.1");

        // All comparison operators are implemented
        // Prints: false
        Debug.Log(version1 > version2)
    }
}