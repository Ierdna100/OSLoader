# Requirements to build

-   `NodeJS ^v20.13.1`
-   `typescript ^5.3.3` - Installed globally
-   `.NET standard 2.0`
-   `.NET 8.0`
-   `Unity 2019.4.40f1`

# Instructions

> NB: `root` refers to the same directory as this file is in.

## Setup

1. Download [Doorstop](https://github.com/NeighTools/UnityDoorstop/releases) and put `winhttp.dll` in the `doorstop` directory (which already contains `doorstop_config.ini`)
2. Create a directory called `steam_api64` and download [Facepunch](), then copy the `steam_api64.dll` that is found in the Unity project with the Facepunch download (in the redistributable subfolder)
3. Create a directory called `unity2019.4.40f1` and put all of the DLLs from the ProjectReference's `Managed` directory (after you build it) in it.
4. Run `npm install`
5. Run `tsc` (if TS is installed globally) on the `Builder` project.

## Building

Run `npm run build` on the `Builder` project.
