using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BossFightLives
{
    public class BflWorld : ModWorld
    {
        public static int Lives;
        public static bool IsBossActive;

        public override void PostUpdate()
        {
            var anyBosses = IsAnyBossActive();
            if (anyBosses && !IsBossActive)
            {
                IsBossActive = true;
                Lives = ModContent.GetInstance<BflServerConfig>().SharedLives;
                NetMessage.SendData(MessageID.WorldData);
            }
            else if (!anyBosses && IsBossActive)
            {
                IsBossActive = false;
                NetMessage.SendData(MessageID.WorldData);
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(Lives);
            writer.Write(IsBossActive);
        }

        public override void NetReceive(BinaryReader reader)
        {
            Lives = reader.ReadInt32();
            IsBossActive = reader.ReadBoolean();
        }

        private static bool IsAnyBossActive() => Main.npc.Any(x => x != null && x.active && x.boss);
    }
}