# OSLoader
Mod loader for Obenseuer based on Unity Doorstop

## Projects present in the repository
### OSLoader
Actually loads mods found in the `.../Obenseuer/OSLoader/mods/` folder in the game making use of Doorstop. 
Doorstop initializes with a library called `winhttp.dll`, which is loaded instead of the one in your Windows installation if present
in the game files, which then searches for the DLL with `Doorstop.Entrypoint::Start()` (OSLoader).

### TestMod
Testing if mods work. I don't know why you'd want to use this, but it's there. Mods can currently not access
private/internal variables, will come with a future version.

## Installation
>  (you will have to build it yourself until version 1.0.0), I am not shipping this in its current incomplete state.

1. Download Unity `2019.4.40f1`
2. Create an empty project with it
3. Compile project
4. Copy everything from the compiled project's `Project/Managed/` folder to `Obenseuer/Managed/` $^1$
5. Only then copy all of the required assemblies to each project's `Depdenencies` folder as instructed in the `instructions.txt` of each.
6. You should now be able to build the project (Note that a Post-Build script is present, you will have to configure it to run on your machine (paths are absolute) or disable it all together)

7. Download the custom `winhttp.dll` library from [here](https://github.com/NeighTools/UnityDoorstop/releases/tag/v4.3.0) (release version contains no Doorstop logs, verbose does)
8. Put `winhttp.dll` in the game's root directory
9. Create a folder with the name `OSLoader` in the game's root directory
10. Put `OSLoader.dll`, `UnityEngine.CoreModule.dll` and `Newtonsoft.Json.dll` in `Obenseuer/OSLoader/`
11. You can put mods in a folder named `mods`, such that the directory looks like this: `Obenseuer/OSLoader/mods/`
12. Create a file in the game's root named `doorstop_config.ini`
13. Put the contents present in the section below in the file
14. Launch the game, and enjoy your mods!

## `doorstop_config.ini`
```ini
# General options for Unity Doorstop
[General]

# Enable Doorstop?
enabled=true
# Path to the assembly to load and execute 
# DO NOT TOUCH THIS
target_assembly=OSLoader/OSLoader.dll
# If true, Unity's output log is redirected to <current folder>\output_log.txt
redirect_output_log=false

# Options specific to running under Unity Mono runtime
[UnityMono]
# If true, Mono debugger server will be enabled
# You can use any debugger (such as the one present in dnSpy)
debug_enabled=false
# When debug_enabled is true, specifies the address to use for the debugger server
debug_address=127.0.0.1:10000
```

## References
$^1$ Obenseuer assemblies come stripped, we need clean ones