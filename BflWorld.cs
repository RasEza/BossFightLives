using System;
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

        public event EventHandler<bool> BossActiveStateChanged;

        public override void PostUpdate()
        {
            var anyBosses = IsAnyBossActive();
            if (anyBosses && !IsBossActive)
            {
                IsBossActive = true;
                Lives = ModContent.GetInstance<BflServerConfig>().SharedLives;
                NetMessage.SendData(MessageID.WorldData);
                BossActiveStateChanged?.Invoke(this, IsBossActive);
            }
            else if (!anyBosses && IsBossActive)
            {
                IsBossActive = false;
                NetMessage.SendData(MessageID.WorldData);
                BossActiveStateChanged?.Invoke(this, IsBossActive);
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

            var newBossState = reader.ReadBoolean();
            if (newBossState != IsBossActive)
            {
                BossActiveStateChanged?.Invoke(this, newBossState);
            }

            IsBossActive = newBossState;
        }

        private static bool IsAnyBossActive() => Main.npc.Any(x => x != null && x.active && x.boss);
    }
}