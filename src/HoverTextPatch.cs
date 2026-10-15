using System;
using HarmonyLib;

namespace GrowTime
{
    // Character implements Hoverable; Hud.UpdateCrosshair calls GetHoverText() only for the object under the crosshair,
    // so this runs for a single creature per frame.
    [HarmonyPatch(typeof(Character), nameof(Character.GetHoverText))]
    internal static class HoverTextPatch
    {
        private static void Postfix(Character __instance, ref string __result)
        {
            if (!Plugin.Enabled.Value || !ZNet.instance) return;

            Growup growup = __instance.GetComponent<Growup>();
            if (!growup || growup.m_growTime <= 0f) return;

            ZNetView nview = __instance.GetComponent<ZNetView>();
            if (!nview || !nview.IsValid()) return;

            // Read the synced spawn time directly. BaseAI.GetTimeSinceSpawned() would write one into the ZDO if missing,
            // and this mod must never change game data.
            long spawnTicks = nview.GetZDO().GetLong(ZDOVars.s_spawnTime, 0L);
            if (spawnTicks == 0L) return;

            // ZNet time is the server-synced world time, the same clock Growup uses.
            double elapsed = (ZNet.instance.GetTime().Ticks - spawnTicks) / (double)TimeSpan.TicksPerSecond;
            double remaining = growup.m_growTime - elapsed;

            Localization loc = Localization.instance;
            // Growup only checks every 10 s on the owner's client, so the animal may still be young briefly after 0.
            string time = remaining > 0 ? TimeFormat.Format(remaining) : loc.Localize("$growtime_soon");
            string line = loc.Localize("$growtime_grows_in") + ": " + time;
            if (Plugin.ShowPercentage.Value)
                line += " (" + TimeFormat.Percent(elapsed, growup.m_growTime) + "%)";

            // Offspring like Lox calves have no Tameable component, so vanilla returns "" for them; start with the name then.
            __result = string.IsNullOrEmpty(__result) ? __instance.GetHoverName() + "\n" + line : __result + "\n" + line;
        }
    }
}
