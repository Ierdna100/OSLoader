# Attempt 1

Version: 0.3.20, 25th nov:

- Decompile with default settings
- Delete AuxiliaryFiles folder in ripped folder
- Pulled out contents of ExportedProjects in root
- Deleted LightingDataAssets folder in Assets (needs to be deleted to avoid crashes)
- Create "Assets/Plugins/"
- Drag the following DLLs from compiled game data the new folder
    - Assembly-CSHarp-firstpass
    - AstarPathfindingProject
    - BehaviourDesignerRuntime
    - Pathfinding.ClipperLib
    - Pathfinding.Ionic.Zip.Reduced
    - Pathfinding.Poly2Tri
    - SALSA-LipSync
- Opened project with unity 2019.4.40f1

- Change .NET compat level to 4.x

- ??? Delete hidden_ stuff? (don't mind this, it's to remind me to test something in the future)

- Packages to install: 
    - TMPro
    - Post Processing
    - Mathematics
    - Addressables
    - Multiplayer HLAPI
    - PREVIEW PACKAGE: Install Entities
    - PREVIEW PACKAGE: Install Animation Rigging

- Fix code errors

Other possible dependencies:
- [Bakery](https://assetstore.unity.com/packages/tools/level-design/bakery-gpu-lightmapper-122218)