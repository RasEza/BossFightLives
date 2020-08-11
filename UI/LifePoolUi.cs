using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace BossFightLives.UI
{
    class LifePoolUi : UIState
    {
        private UIText text;
        private DraggableUiPanel uiPanel;

        public override void OnInitialize()
        {
            var texture = ModContent.GetTexture("BossFightLives/UI/head");
            var image = new UIImage(texture)
            {
                VAlign = 0.5f
            };

            text = new UIText($"{BflWorld.Lives} / {ModContent.GetInstance<BflServerConfig>().SharedLives}")
            {
                VAlign = 0.5f,
                MarginLeft = image.Width.Pixels * 1.5f
            };

            var clientConfig = ModContent.GetInstance<BflClientConfig>();
            uiPanel = new DraggableUiPanel(UpdateOffsetConfiguration, !ModContent.GetInstance<BflClientConfig>().Lock);

            var panelWidth = (text.MinWidth.Pixels + image.Width.Pixels) * 1.5f;
            uiPanel.Width.Set(panelWidth, 0);
            uiPanel.Height.Set(image.Height.Pixels * 2, 0);

            uiPanel.MarginLeft = clientConfig.LifePoolPosX;
            uiPanel.MarginTop = clientConfig.LifePoolPosY;

            Append(uiPanel);
            uiPanel.Append(image);
            uiPanel.Append(text);
        }

        public override void Update(GameTime gameTime) =>
            text.SetText($"{BflWorld.Lives} / {ModContent.GetInstance<BflServerConfig>().SharedLives}");

        public void UpdateProperties()
        {
            var clientConfig = ModContent.GetInstance<BflClientConfig>();
            uiPanel.MarginLeft = clientConfig.LifePoolPosX;
            uiPanel.MarginTop = clientConfig.LifePoolPosY;
            uiPanel.Draggable = !clientConfig.Lock;
        }

        private static void UpdateOffsetConfiguration(float x, float y)
        {
            var methodInfo = typeof(ConfigManager)
                .GetMethod("Save", BindingFlags.NonPublic | BindingFlags.Static);

            var clientConfig = ModContent.GetInstance<BflClientConfig>();
            clientConfig.LifePoolPosY = y;
            clientConfig.LifePoolPosX = x;

            if (methodInfo != null)
                methodInfo.Invoke(null, new object[] {clientConfig});
            else
                ModContent.GetInstance<BossFightLives>().Logger
                    .Warn("In-game SaveConfig failed, code update required");
        }
    }
}