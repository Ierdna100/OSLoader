using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    [KeybindSetting("A keybind setting!", keybindConstraints: KeybindConstraints.NoFunctions | KeybindConstraints.NoExistingGameBinds, dissalowedKeys: new KeyCode[] { KeyCode.K, KeyCode.Alpha0 })]
    public KeyCode keybind = KeyCode.G;
}
