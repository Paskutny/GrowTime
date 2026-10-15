using System.IO;
using System.Reflection;
using HarmonyLib;

namespace GrowTime
{
    // Adds our keys to Valheim's own Localization, so "$growtime_..." follows the in-game language setting.
    // Files: Translations/<Language>.txt, where <Language> is Valheim's name (English, German, French, ...).
    [HarmonyPatch(typeof(Localization), nameof(Localization.SetupLanguage))]
    internal static class Translations
    {
        private static readonly MethodInfo AddWord = AccessTools.Method(typeof(Localization), "AddWord");

        // SetupLanguage runs on startup and on every language change (after the old words were cleared).
        private static void Postfix(Localization __instance, string language) => Inject(__instance, language);

        internal static void Inject(Localization localization, string language)
        {
            // English first, so keys missing in the selected language fall back to English.
            Load(localization, "English");
            if (language != "English") Load(localization, language);
        }

        // Format: one "key = value" per line, lines starting with # are comments.
        private static void Load(Localization localization, string language)
        {
            string path = Path.Combine(Plugin.TranslationsDir, language + ".txt");
            if (!File.Exists(path)) return;

            foreach (string line in File.ReadAllLines(path))
            {
                int eq = line.IndexOf('=');
                if (eq <= 0 || line.TrimStart().StartsWith("#")) continue;
                AddWord.Invoke(localization, new object[] { line.Substring(0, eq).Trim(), line.Substring(eq + 1).Trim() });
            }
        }
    }
}
