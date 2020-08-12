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

        public override void Enable()
        {
            if (Enabled)
                return;
            Enabled = true;

            var modWorld = ModContent.GetInstance<BflWorld>();
            modWorld.BossActiveStateChanged += ToggleLifePoolUi;
        }

        public override void Disable()
        {
            Enabled = false;
            var modWorld = ModContent.GetInstance<BflWorld>();
            modWorld.BossActiveStateChanged -= ToggleLifePoolUi;
            Hide();
        }

        public void UpdateProperties()
        {
            lifePoolUi?.UpdateProperties();
        }

        public void Update(GameTime gameTime)
        {
            if (userInterface?.CurrentState != null)
                userInterface?.Update(gameTime);
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

        private void ToggleLifePoolUi(object sender, bool b)
        {
            if (b)
            {
                Show();
            }
            else
                Hide();
        }
    }
}