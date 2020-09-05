using Terraria.ModLoader;

namespace BossFightLives.UiConfiguration
{
    public abstract class UiConfiguration
    {
        public bool Enabled;

        public virtual void Enable()
        {
            if (Enabled)
                return;
            Enabled = true;

            ModContent.GetInstance<BflWorld>().BossActiveStateChanged += OnBossStateChanged;
            BflPlayer.OnLifeLost += OnLifeLost;
        }

        public virtual void Disable()
        {
            Enabled = false;
            ModContent.GetInstance<BflWorld>().BossActiveStateChanged -= OnBossStateChanged;
            BflPlayer.OnLifeLost -= OnLifeLost;
        }

        protected abstract void OnBossStateChanged(object sender, bool b);
        protected abstract void OnLifeLost(object sender, (int currentLives, int previousLives) e);
    }
}