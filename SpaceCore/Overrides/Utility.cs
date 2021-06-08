using System.Diagnostics.CodeAnalysis;
using Harmony;
using Spacechase.Shared.Harmony;
using SpaceCore.Events;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Events;

namespace SpaceCore.Overrides
{
    /// <summary>Applies Harmony patches to <see cref="Utility"/>.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "The naming is determined by Harmony.")]
    internal class UtilityPatcher : BasePatcher
    {
        /*********
        ** Public methods
        *********/
        /// <inheritdoc />
        public override void Apply(HarmonyInstance harmony, IMonitor monitor)
        {
            harmony.Patch(
                original: this.RequireMethod<Utility>(nameof(Utility.pickFarmEvent)),
                postfix: this.GetHarmonyMethod(nameof(After_PickFarmEvent))
            );
        }


        /*********
        ** Private methods
        *********/
        /// <summary>The method to call after <see cref="Utility.pickFarmEvent"/>.</summary>
        private static void After_PickFarmEvent(ref FarmEvent __result)
        {
            __result = SpaceEvents.InvokeChooseNightlyFarmEvent(__result);
        }
    }
}
