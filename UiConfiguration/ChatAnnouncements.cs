using Terraria;
using Terraria.ModLoader;

namespace BossFightLives.UiConfiguration
{
    public class ChatAnnouncements : UiConfiguration
    {
        protected override void OnBossStateChanged(object sender, bool b)
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

        protected override void OnLifeLost(object sender, (int currentLives, int previousLives) e)
        {
            Main.NewText($"Lives: {e.currentLives} -> {e.previousLives}");
        }
    }
}