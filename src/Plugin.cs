using System.IO;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace GrowTime
{
    [BepInPlugin(Guid, Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "paskutny.growtime";
        public const string Name = "GrowTime";

        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<bool> ShowPercentage;
        internal static string TranslationsDir;

        private void Awake()
        {
            Enabled = Config.Bind("General", "Enabled", true,
                "Show the remaining grow time when hovering over a young animal.");
            ShowPercentage = Config.Bind("General", "ShowPercentage", false,
                "Also show the growth progress in percent, e.g. \"Grows in: 12m 34s (58%)\".");

            TranslationsDir = Path.Combine(Path.GetDirectoryName(Info.Location), "Translations");

            new Harmony(Guid).PatchAll();

            // Normally Localization is created after plugins load and our SetupLanguage postfix handles it.
            // If another mod already created it, inject our words now.
            if (AccessTools.Field(typeof(Localization), "m_instance").GetValue(null) != null)
                Translations.Inject(Localization.instance, Localization.instance.GetSelectedLanguage());
        }
    }
}
