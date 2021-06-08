using System.Diagnostics.CodeAnalysis;
using Harmony;
using Spacechase.Shared.Harmony;
using StardewModdingAPI;
using StardewValley.Menus;

namespace SpaceCore.Overrides
{
    /// <summary>Applies Harmony patches to <see cref="GameMenu"/>.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "The naming is determined by Harmony.")]
    internal class GameMenuPatcher : BasePatcher
    {
        /*********
        ** Public methods
        *********/
        /// <inheritdoc />
        public override void Apply(HarmonyInstance harmony, IMonitor monitor)
        {
            harmony.Patch(
                original: this.RequireMethod<GameMenu>(nameof(GameMenu.getTabNumberFromName)),
                postfix: this.GetHarmonyMethod(nameof(After_GetTabNumberFromName))
            );
        }


        /*********
        ** Private methods
        *********/
        /// <summary>The method to call after <see cref="GameMenu.getTabNumberFromName"/>.</summary>
        public static void After_GetTabNumberFromName(GameMenu __instance, string name, ref int __result)
        {
            foreach (var tab in Menus.extraGameMenuTabs)
            {
                if (name == tab.Value)
                    __result = tab.Key;
            }
        }
    }
}
