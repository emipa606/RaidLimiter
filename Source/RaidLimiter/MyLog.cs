namespace RaidLimiter;

internal static class MyLog
{
    public static void Log(string message)
    {
        if (RaidLimiterMod.instance.Settings.Debug)
        {
            Verse.Log.Message($"[RaidLimiter]: {message}");
        }
    }
}