---
_layout: landing
---

# Welcome to the OSLoader Documentation!

## What is OSLoader?

Good question! `OSLoader` (shortened occasionally in this documentation as `"OSL"`) is a mod loader for the Unity game 
[`Obenseuer`](https://store.steampowered.com/app/951240/Obenseuer/), by `Loiste Interactive`. While OSLoader specifically refers to the program that loads mods into memory, this documentation goes over the whole modding environement (denoted as OS Modding Environement in this documentation), meaning the `OS Mod Provider (OSMP)`, which handles mod downloads, installs and updates; as well as the [`BepInEx`](https://docs.bepinex.dev/articles/user_guide/installation/index.html) alternative
to the environment. 

## Getting Started

### ... as a user

The installation guide can be found [here](https://ierdna100.github.io/OSLoader/user/Getting%20Started.html).

If you have further questions, the official Discord server can be found [here](https://discord.gg/GFS5CvrfzK).

### ... as a mod developer

Looking for the API documentation? Look no further than [this link here](https://ierdna100.github.io/OSLoader/docs/Mod/Mod%20Structure.html).

Looking to contribute or report a bug? The Github repository can be found [here](https://github.com/Ierdna100/OSLoader).

## Why OSLoader (over BepInEx)?

> [!IMPORTANT]
> While OSLoader's modloader is separate from BepInEx, both are supported by the OS Modding Provider application and both's entrypoints are easily interchangeable in code if you wish to make your code available for both (details on differences below). More about this in the API documentation for the modding reference found [here](https://ierdna100.github.io/OSLoader/docs/Mod/Mod%20Structure.html)

OSLoader fulfills the same role as BepInEx, in the sense that they can be used interchangeably if you wanted to. BepInEx is nonetheless a "generic" modloader, made for any Unity game, which means it cannot integrate specific features to Obenseuer. A dependency in BepInEx could solve this, but the OSLoader modloader guarantees the features exist at all times on all mods.

Additionally, the community is closely knit in Obenseuer, including active developers in the Obenseuer Discord server. A custom modloader (OSLoader) gives us the ability to coordinate with the game developers for features they may desire or not. It also pushes us ever closer to Steam Workshop integration, should modding become popular.

It is nonetheless important to note the OSLoader modloader does not support very esoteric features of BepInEx, which I believe are not needed (or will come later if they're requested), but may be a part of existing codebases. Such features include modifying the C# assembly before it is loaded in memory, referred to as "preloader patchers" in [BepInEx documentation](https://docs.bepinex.dev/articles/dev_guide/preloader_patchers.html).

Finally, note that this documentation is relevant for both modding systems in Obenseuer. It will cover topics agnostically of modloader for the user-facing documentation, and the API documentation is neatly split between BepInEx documentation and OSLoader documentation. Your mod should ideally support both (your entrypoint will be similar, the major difference will be settings and API features, which are for the moment only available for OSLoader, where most of my efforts will be spent).