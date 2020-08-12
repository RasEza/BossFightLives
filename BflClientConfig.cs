using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BossFightLives
{
    public class BflClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Enable chat announcements")]
        [Tooltip("Messages in chat will indicate when lives are lost and more...")]
        [DefaultValue(false)]
        public bool EnableChatAnnouncements;

        [Header("Life Pool")]

        [Label("Enable")]
        [DefaultValue(true)]
        public bool EnableLifePoolUi;

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
        [Range(0f, 3840f)]
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
            ModContent.GetInstance<BossFightLives>().ConfigureUi(this);
        }
    }
}