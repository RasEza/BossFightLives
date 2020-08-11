using System.Collections.Generic;
using BossFightLives.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossFightLives
{
	public class BossFightLives : Mod
	{
        private UserInterface userInterface;
        private LifePoolUi lifePoolUi;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                lifePoolUi = new LifePoolUi();
                lifePoolUi.Activate();
                userInterface = new UserInterface();
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                userInterface = null;
                lifePoolUi = null;
            }
        }
        
        public override void UpdateUI(GameTime gameTime)
        {
            var modInstance = ModContent.GetInstance<BossFightLives>();
            if (ModContent.GetInstance<BflClientConfig>().ConfigurationMode || BflWorld.IsBossActive)
            {
                modInstance.ShowLifePoolUi();
            }
            else if (!BflWorld.IsBossActive && !ModContent.GetInstance<BflClientConfig>().ConfigurationMode)
            {
                modInstance.HideLifePoolUi();
            }

            if (userInterface?.CurrentState != null)
                userInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    $"{nameof(BossFightLives)}: UI",
                    () =>
                    {
                        if (userInterface?.CurrentState != null)
                        {
                            userInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }

        public void UpdateLifePoolUiProperties()
        {
            lifePoolUi?.UpdateProperties();
        }

        private void ShowLifePoolUi()
        {
            if (userInterface?.CurrentState == null)
                userInterface?.SetState(lifePoolUi);
        }

        private void HideLifePoolUi()
        {
            userInterface?.SetState(null);
        }
    }
}