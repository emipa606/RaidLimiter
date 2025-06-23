using System;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace RaidLimiter;

[HarmonyPatch(typeof(StorytellerUtility), nameof(StorytellerUtility.DefaultThreatPointsNow))]
internal class StorytellerUtility_DefaultThreatPointsNow
{
    private static bool Prefix(IIncidentTarget target, ref float __result)
    {
        var simpleCurve = new SimpleCurve
        {
            new CurvePoint(0f, 0f),
            new CurvePoint(300000f, 1800f),
            new CurvePoint(600000f, 3000f),
            new CurvePoint(900000f, 3600f)
        };
        var simpleCurve2 = new SimpleCurve
        {
            new CurvePoint(0f, 40f),
            new CurvePoint(300000f, 110f)
        };
        var simpleCurve3 = new SimpleCurve
        {
            new CurvePoint(0f, 35f),
            new CurvePoint(100f, 35f),
            new CurvePoint(1000f, 700f),
            new CurvePoint(2000f, 1400f),
            new CurvePoint(3000f, 2100f),
            new CurvePoint(4000f, 2800f),
            new CurvePoint(5000f, 3500f),
            new CurvePoint(6000f, 4000f)
        };
        var playerWealthForStoryteller = target.PlayerWealthForStoryteller;
        var settingsWealthMultiplier = simpleCurve.Evaluate(playerWealthForStoryteller);
        MyLog.Log($"Player Wealth Contribution: {settingsWealthMultiplier}");
        settingsWealthMultiplier *= RaidLimiterMod.Instance.Settings.WealthMultiplier;
        MyLog.Log($"Player Wealth After Multiplier: {settingsWealthMultiplier}");
        var multiplier = 0f;
        var num3 = 0;
        foreach (var pawn in target.PlayerPawnsForStoryteller)
        {
            var colonistMultiplier = 0f;
            var isFreeColonist = pawn.IsFreeColonist;
            if (isFreeColonist)
            {
                colonistMultiplier = simpleCurve2.Evaluate(playerWealthForStoryteller) *
                                     RaidLimiterMod.Instance.Settings.ColonistMultiplier;
                num3++;
            }
            else
            {
                if (pawn.RaceProps.Animal && pawn.Faction == Faction.OfPlayer && !pawn.Downed &&
                    pawn.training.CanAssignToTrain(TrainableDefOf.Release).Accepted)
                {
                    colonistMultiplier = 0.09f * pawn.kindDef.combatPower;
                    if (target is Caravan)
                    {
                        colonistMultiplier *= 0.5f;
                    }

                    colonistMultiplier *= RaidLimiterMod.Instance.Settings.CombatAnimalMultiplier;
                }
            }

            if (!(colonistMultiplier > 0f))
            {
                continue;
            }

            if (pawn.ParentHolder is Building_CryptosleepCasket)
            {
                colonistMultiplier *= 0.3f;
            }

            colonistMultiplier = Mathf.Lerp(colonistMultiplier,
                colonistMultiplier * pawn.health.summaryHealth.SummaryHealthPercent, 0.65f);
            multiplier += colonistMultiplier;
        }

        var scale = settingsWealthMultiplier + multiplier;
        scale *= target.IncidentPointsRandomFactorRange.RandomInRange;
        scale = simpleCurve3.Evaluate(scale);
        var totalThreatPointsFactor = Find.StoryWatcher.watcherAdaptation.TotalThreatPointsFactor;
        if (RaidLimiterMod.Instance.Settings.AdaptationTapering > 0f &&
            totalThreatPointsFactor > RaidLimiterMod.Instance.Settings.AdaptationTapering)
        {
            MyLog.Log($"adaptation Before AdaptationTapering: {totalThreatPointsFactor}");
            totalThreatPointsFactor = RaidLimiterMod.Instance.Settings.AdaptationTapering =
                (float)Math.Pow(totalThreatPointsFactor - RaidLimiterMod.Instance.Settings.AdaptationTapering,
                    RaidLimiterMod.Instance.Settings.AdaptationExponent);
            MyLog.Log($"adaptation after AdaptationTapering: {totalThreatPointsFactor}");
        }

        if (RaidLimiterMod.Instance.Settings.AdaptationCap > 0f &&
            totalThreatPointsFactor > RaidLimiterMod.Instance.Settings.AdaptationCap)
        {
            MyLog.Log($"adaptation Before AdaptationCap: {totalThreatPointsFactor}");
            totalThreatPointsFactor = RaidLimiterMod.Instance.Settings.AdaptationCap;
            MyLog.Log($"adaptation Before AdaptationCap: {totalThreatPointsFactor}");
        }

        scale *= totalThreatPointsFactor;
        scale *= Find.Storyteller.difficulty.threatScale;
        MyLog.Log($"Before RaidPointsMultiplier: {scale}");
        scale *= RaidLimiterMod.Instance.Settings.RaidPointsMultiplier;
        MyLog.Log($"After RaidPointsMultiplier: {scale}");
        if (RaidLimiterMod.Instance.Settings.SoftCapBeginTapering > 0f &&
            scale > RaidLimiterMod.Instance.Settings.SoftCapBeginTapering)
        {
            MyLog.Log($"Before SoftCapBeginTapering: {scale}");
            scale = RaidLimiterMod.Instance.Settings.SoftCapBeginTapering =
                (float)Math.Pow(scale - RaidLimiterMod.Instance.Settings.SoftCapBeginTapering,
                    RaidLimiterMod.Instance.Settings.SoftCapExponent);
            MyLog.Log($"After SoftCapBeginTapering: {scale}");
        }

        if (RaidLimiterMod.Instance.Settings.RaidCapPointsPerColonist > 0f)
        {
            MyLog.Log($"Before RaidCapPointsPerColonist: {scale}");
            scale = Math.Min(scale, RaidLimiterMod.Instance.Settings.RaidCapPointsPerColonist * num3);
            MyLog.Log($"After RaidCapPointsPerColonist: {scale}");
        }

        if (RaidLimiterMod.Instance.Settings.RaidCap > 0f)
        {
            MyLog.Log($"Before RaidCap: {scale}");
            scale = Math.Min(RaidLimiterMod.Instance.Settings.RaidCap, scale);
            MyLog.Log($"After RaidCap: {scale}");
        }

        if (RaidLimiterMod.Instance.Settings.CapByDifficultySettings)
        {
            MyLog.Log($"Before CapByDifficultySettings: {scale}");
            scale = Math.Min(
                scale * RaidLimiterMod.Instance.Settings.CapByDifficultySettingsMultiplier *
                Find.Storyteller.difficulty.threatScale,
                scale);
            MyLog.Log($"After CapByDifficultySettings: {scale}");
        }

        __result = scale;
        return false;
    }
}