using HarmonyLib;
using RimWorld;
using Verse;

namespace RaidLimiter;

[HarmonyPatch(typeof(StorytellerUtility), nameof(StorytellerUtility.DefaultParmsNow), typeof(IncidentCategoryDef),
    typeof(IIncidentTarget))]
internal static class StorytellerUtility_DefaultParmsNow
{
    public static bool Prefix(IncidentCategoryDef incCat, IIncidentTarget target, ref IncidentParms __result)
    {
        if (!LoadedModManager.GetMod<RaidLimiterMod>().GetSettings<RaidLimiterSettings>().Debug)
        {
            return true;
        }

        MyLog.Log("RL.LogTitle.label".Translate());
        if (incCat == null)
        {
            Log.Warning("Trying to get default parms for null incident category.");
        }

        var incidentParms = new IncidentParms
        {
            target = target
        };
        var needsParmsPoints = incCat is { needsParmsPoints: true };
        if (needsParmsPoints)
        {
            var typeFromHandle = typeof(StorytellerUtility);
            var method = typeFromHandle.GetMethod("DefaultThreatPointsNow");
            if (method is not null)
            {
                incidentParms.points = (float)method.Invoke(null, [
                    target
                ]);
            }

            MyLog.Log("RL.LogPoints.label".Translate(incidentParms.points));
        }

        __result = incidentParms;

        return false;
    }
}