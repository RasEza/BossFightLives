using Terraria;
using Terraria.ModLoader;

namespace BossFightLives.UiConfiguration
{
    public class ChatAnnouncements : UiConfiguration
    {
        public override void Enable()
        {
            if (Enabled)
                return;
            Enabled = true;
            ModContent.GetInstance<BflWorld>().BossActiveStateChanged += OnModWorldOnBossActiveStateChanged;
            BflPlayer.OnLifeLost += BflPlayer_OnLifeLost;
        }

        public override void Disable()
        {
            Enabled = false;
            ModContent.GetInstance<BflWorld>().BossActiveStateChanged -= OnModWorldOnBossActiveStateChanged;
            BflPlayer.OnLifeLost -= BflPlayer_OnLifeLost;
        }

        private void OnModWorldOnBossActiveStateChanged(object sender, bool b)
        {
            if (!b)
                return;

            var message = $"You have {BflWorld.Lives} lives. When you run out ";
            if (ModContent.GetInstance<BflServerConfig>().SharedDeath)
            {
                message += "all remaining players will be killed.";
            }
            else
            {
                message += "you will no longer be able to respawn.";
            }

            Main.NewText(message);
        }

        private void BflPlayer_OnLifeLost(object sender, (int currentLives, int previousLives) e)
        {
            Main.NewText($"Lives: {e.currentLives} -> {e.previousLives}");
        }
    }
}