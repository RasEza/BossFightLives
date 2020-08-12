using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace BossFightLives
{
    class BflServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Shared lives")] [Tooltip("Number of deaths before respawning should be prevented.")]
        [DefaultValue(5)]
        public int SharedLives;

        [Label("Shared death")]
        [Tooltip("Checked: Kill remaining players when the shared life pool is empty." +
                 "\nUnchecked: Prevent respawning when the shared life pool is empty.")]
        [DefaultValue(true)]
        public bool SharedDeath;
    }
}