using BossFightLives.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossFightLives.UiConfiguration
{
    public class LifePool : UiConfiguration
    {
        private readonly UserInterface userInterface;
        private readonly LifePoolUi lifePoolUi;

        public LifePool()
        {
            lifePoolUi = new LifePoolUi();
            lifePoolUi.Activate();
            userInterface = new UserInterface();
        }

        public override void Disable()
        {
            base.Disable();
            Hide();
        }

        protected override void OnLifeLost(object sender, (int currentLives, int previousLives) e)
        {
            lifePoolUi.Update(new GameTime());
        }

        protected override void OnBossStateChanged(object sender, bool b)
        {
            if (b)
            {
                Show();
                lifePoolUi.Update(new GameTime());
            }
            else if (!ModContent.GetInstance<BflClientConfig>().ConfigurationMode)
                Hide();
        }

        public void UpdateProperties()
        {
            lifePoolUi?.UpdateProperties();
        }

        public void Draw()
        {
            userInterface.Draw(Main.spriteBatch, new GameTime());
        }

        public void Show()
        {
            if (userInterface?.CurrentState == null)
                userInterface?.SetState(lifePoolUi);
        }

        public void Hide()
        {
            userInterface?.SetState(null);
        }
    }
}