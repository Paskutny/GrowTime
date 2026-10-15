# GrowTime

A client-side Valheim mod (BepInEx) that adds a line to the hover text of young animals showing how long they still need to grow up:

```
Grows in: 12m 34s (58%)
```

The player-facing description is in [thunderstore/README.md](thunderstore/README.md) (also in German: [Deutsch](thunderstore/README.md#deutsch)).

## How it works

| Valheim code | Used for |
|---|---|
| `Growup.m_growTime` | Total grow time in seconds (the creature turns into its adult prefab once its age exceeds this) |
| ZDO value `ZDOVars.s_spawnTime` | Spawn time in ticks, set by `BaseAI` and synced to all clients |
| `ZNet.instance.GetTime()` | Server-synced world time, the same clock `Growup` uses |
| `Character.GetHoverText()` | Postfix-patched to append the line (`Character` is the `Hoverable` the HUD calls) |
| `Localization.SetupLanguage()` | Postfix-patched to inject our translation keys (`AddWord` is private and is called via reflection) |

The mod only reads data and never writes to the ZDO. That's why it reads `s_spawnTime` directly instead of calling `BaseAI.GetTimeSinceSpawned()`, which writes a spawn time if one is missing.

## Project layout

```
src/                     Plugin source (net462)
  Plugin.cs              BepInEx entry point, config
  HoverTextPatch.cs      Hover text postfix and grow time calculation
  Translations.cs        Loads Translations/*.txt into Valheim's Localization
  TimeFormat.cs          "12m 34s" / "1h 05m" formatting
  Translations/          <Language>.txt (English, German, Spanish, French, Russian, Portuguese_*)
tests/                   Unit tests for TimeFormat (net8.0, xunit)
thunderstore/            manifest.json, README.md, icon.png (package files)
CHANGELOG.md             Also shipped in the package
Environment.props.example  Template for your local paths
```

## Building

Requirements: [.NET SDK 8](https://dotnet.microsoft.com/download) or newer, Valheim, and BepInEx (installed manually or through a mod manager profile).

1. Copy `Environment.props.example` to `Environment.props` and set `ValheimDir` and `BepInExDir`. This file is git-ignored. Game and BepInEx DLLs are only referenced from your install, never copied into the repo.
2. Build:
   ```
   dotnet build src -c Release
   ```
   This produces:
   - `src/bin/Release/GrowTime.dll`
   - `dist/Paskutny-GrowTime-<version>.zip`, the finished Thunderstore package
   - if `DeployDir` is set: a copy of the DLL and translations in that folder, for testing
3. Run the tests:
   ```
   dotnet test tests
   ```

The version lives only in `thunderstore/manifest.json`. The build reads it for the assembly, the `[BepInPlugin]` attribute and the zip name.

## Testing in game

- **With `DeployDir`:** build, then start Valheim modded from your mod manager profile.
- **Without it:** in the mod manager, use *Settings → Import local mod* and pick the zip from `dist/`.

Check `BepInEx/LogOutput.log` for `GrowTime` loading without errors. Then hover over a young animal. To test the German text, switch the game language in the settings.

## Releasing

1. Bump `version_number` in `thunderstore/manifest.json` and add an entry to `CHANGELOG.md`.
2. `dotnet build src -c Release`
3. Upload `dist/Paskutny-GrowTime-<version>.zip` at https://thunderstore.io/c/valheim/create/ (team `Paskutny`).

## Adding a language

Add `src/Translations/<Language>.txt`, where `<Language>` is Valheim's language name (`English`, `German`, `French`, `Spanish`, `Russian`, `Polish`, `Portuguese_Brazilian`, `Chinese`, ...). Copy `English.txt` and translate the values. No code change is needed. Missing keys fall back to English.

## License

MIT
