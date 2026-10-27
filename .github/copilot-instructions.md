# GitHub Copilot Instructions for RimWorld Mod: Raid Limiter (Continued)

## Mod Overview and Purpose

The Raid Limiter (Continued) mod is an update to the Greeps mod, which aims to control and limit the size of raids in the RimWorld game. The mod allows players to edit the settings in-game, with the XML-based values being used only on the first run. It provides various configuration options to customize the raid size based on factors such as difficulty, number of colonists, and wealth.

## Key Features and Systems

### Settings
- **CapByDifficultySettings**: Determines if the raid system should pick a value based on the storyteller's difficulty.
- **CapByDifficultySettingsMultiplier**: Sets the base value multiplied by the difficulty.
- **RaidCap**: Manually sets the raid size cap.
- **RaidPointsMultiplier**: Applies a straight multiplier on top of the difficulty.
- **RaidCapPointsPerColonist**: Sets the raid cap based on the number of colonists.
- **Colonist Multiplier**: Adjusts the impact of colonists on raid points.
- **Combat Animal Multiplier**: Adjusts the impact of combat animals on raid points.
- **Wealth Multiplier**: Adjusts the impact of wealth on raid points.
- **Soft Caps and Adaptation**: Allows for soft caps and adaptation settings to control raid size increases.

### Debugging
- **Debug**: Controls the logging of warnings and other debug information.

### Logging
- **Logger**: Provides warning logs for active settings, showing the effects of the tags on the overall raid points.

## Coding Patterns and Conventions

### C# Files
- **MyLog.cs**: Contains the `MyLog` type with the `Log` member for logging warnings.
- **Main.cs**: Contains the `Main` type with the `Main` and `ResetSettings` members for initializing and resetting settings.
- **RaidLimiterMod.cs**: Contains the `RaidLimiterMod` type with methods for handling settings window contents, writing settings, and defining settings.
- **RaidLimiterSettings.cs**: Contains the `RaidLimiterSettings` type with the `ExposeData` member for exposing data.
- **RaidLimiterSettingsDef.cs**: Contains the `RaidLimiterSettingsDef` type for defining settings.

### XML Files
- **Settings.xml**: Contains the `RaidLimiter.RaidLimiterSettingsDef` definition for the settings.

## XML Integration

The mod uses XML files to define the settings structure and values. The `Settings.xml` file is located in the `Defs` directory and contains the `RaidLimiter.RaidLimiterSettingsDef` definition, which specifies the various settings and their default values.

xml
<RaidLimiter.RaidLimiterSettingsDef>
    <label>Raid Limiter Settings</label>
    <description>Configure raid size limits.</description>
    <settings>
        <setting>
            <label>Cap By Difficulty Settings</label>
            <description>Enable or disable cap by difficulty settings.</description>
            <defaultValue>YES</defaultValue>
        </setting>
        <!-- Additional settings -->
    </settings>
</RaidLimiter.RaidLimiterSettingsDef>


## Harmony Patching

The mod uses Harmony patching to modify the behavior of the game's code. Harmony is a powerful library that allows modders to patch existing methods and add new functionality without modifying the original codebase. The mod patches various methods related to raid size calculation to apply the custom settings.

csharp
using HarmonyLib;

public class RaidLimiterPatch
{
    [HarmonyPatch(typeof(Raid), nameof(Raid.InitPoints))]
    public static void Postfix(Raid __instance)
    {
        // Apply custom settings to raid points calculation
        // Example: __instance.points *= RaidLimiterMod.Settings.RaidPointsMultiplier;
    }
}


## Suggestions for Copilot

1. **Mod Structure**: Copilot should be familiar with the overall structure of the mod, including the C# and XML files, and their respective roles.
2. **Settings Management**: Copilot should understand how to manage and expose settings using the `ExposeData` method and the `RaidLimiterSettings` type.
3. **Harmony Patching**: Copilot should be proficient in using Harmony to patch existing methods and add new functionality.
4. **Logging and Debugging**: Copilot should be aware of the logging and debugging mechanisms provided by the `MyLog` type and how to use them for troubleshooting.
5. **XML Defs**: Copilot should be able to work with XML definitions and understand how they are integrated into the mod.

By following these guidelines, Copilot can effectively assist in the development and maintenance of the Raid Limiter (Continued) mod.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

