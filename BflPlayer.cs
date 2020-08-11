using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BossFightLives
{
    public class BflPlayer : ModPlayer
    {
        public override void PreUpdate()
        {
            if (ModContent.GetInstance<BflServerConfig>().SharedDeath &&
                BflWorld.Lives < 1 &&
                BflWorld.IsBossActive)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} disintegrated."), 0, 0);
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (BflWorld.IsBossActive && BflWorld.Lives > 0)
            {
                BflWorld.Lives--;
            }
        }

        public override void UpdateDead()
        {
            if (!ModContent.GetInstance<BflServerConfig>().SharedDeath &&
                BflWorld.Lives < 1 && 
                BflWorld.IsBossActive)
            {
                player.respawnTimer++;
            }
        }
    }
}
