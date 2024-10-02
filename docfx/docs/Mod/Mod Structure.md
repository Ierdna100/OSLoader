# Mod Structure

The file structure of a mod is important to be followed so that the modloader and other mods requiring this structure to be set can properly reference files.

## Structure
```txt
Obenseuer/OSLoader/mods
  | -  ModFolder1
  |  | - info.json
  |  | - settings.json
  |  \ - Entrypoint.dll
  |
  \ -  ModFolder2
     | - info.json
     | - settings.json
     \ - Entrypoint.dll
```

## Notes
Only the info.json is actually required, and it needs to be exactly as described above. Settings will also always be saved in the same location. The DLL of your mod is referenced via the `info.json`'s `dllFilepath` property, but it is still recommended to be kept in the root of the mod's folder for simplicity and standardization purposes.

> [!IMPORTANT]
> In order to zip a mod to either upload it to the mod downloader or to share with someone directly, you may want to zip it into a compressed file. It is important NOT to zip `ModFolder1` or `ModFolder2`, but rather the actual internal contents of the folder. The mod downloader should give an error if this is done improperly, but the modloader will simply not load a mod if it does not find the `info.json`, if you instead sent a mod manually to someone.