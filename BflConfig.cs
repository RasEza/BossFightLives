using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BossFightLives
{
    class BflServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Shared lives")]
        [Tooltip("Number of deaths before respawning should be prevented.")]
        [DefaultValue(5)]
        public int SharedLives;

        [Label("Shared death")]
        [Tooltip("Checked: Kill remaining players when the shared life pool is empty." +
                 "\nUnchecked: Prevent respawning when the shared life pool is empty.")]
        [DefaultValue(true)]
        public bool SharedDeath;
    }

    class BflClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Lock life pool display")]
        [DefaultValue(false)]
        public bool Lock;

        [Label("Configuration mode")]
        [Tooltip("Use this setting to configure life pool display outside of a boss fight.")]
        [DefaultValue(false)]
        public bool ConfigurationMode;

        [Label("Life Pool X Position")]
        [Tooltip("The X position of the life pool display.")]
        [Slider]
        [Range(0f,3840f)]
        [DefaultValue(500f)]
        public float LifePoolPosX;

        [Label("Life Pool Y Position")]
        [Tooltip("The Y position of the life pool display.")]
        [Slider]
        [Range(0f, 2160f)]
        [DefaultValue(50f)]
        public float LifePoolPosY;

        public override void OnChanged()
        {
            ModContent.GetInstance<BossFightLives>().UpdateLifePoolUiProperties();
        }
    }
}
