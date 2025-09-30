# CardVentureTrainer by redleaf

## Features

This is a simple mod using **BepInEx 6** to modify the game **CardVenture:JustAHead** (demo**0924**), which includes following features:

* Enable test version features, the debug menu.
* Seal some of the ability pools.
* Disable negative damage of ability Hadouken.
* Always allow parrying from sides.
* Allow parrying even if perviously in the attack range.
* Delay resetting oldPos to make parrying easier.
* Disable friend unit spawn limit.
* Reduce animation time to make parrying spiders easier.
* Show oldPos and unitPos of the player.
* Show possible targets of the demon sword

## How to use

1. Download from [releases](https://github.com/redleaf1221/CardVentureTrainer/releases/latest) or build it on your own.
2. Download latest [BepInEx 6 Bleeding Edge Build](https://builds.bepinex.dev/projects/bepinex_be) (BepInEx Unity (Mono) for Windows (x64) games)
3. Unzip and place it in the game folder.
4. Move `CardVentureTrainer.dll` to `BepInEx\plugins`.
5. Use F12 to open the in-game GUI, you may change the hotkey in `BepInEx\config\CardVentureTrainer.cfg`
6. Enjoy~

## TODO

- [x] Massive refactor to allow dynamic config.
- [x] Implement GUI for cheating and toggling configs.
- [x] More features:
- [x] Render oldPos in realtime.
- [ ] Delete abilities and change cooltime of abilities.

## Misc

<img src="misc/I Patched 100 Methods.png">

Conditional Compile? Interesting.

Well actually no conditional compilation, I suspect that the author don't know about it.
