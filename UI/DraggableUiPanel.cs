using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BossFightLives.UI
{
    /// <summary>
    /// From https://github.com/tModLoader/tModLoader/blob/master/ExampleMod/UI/DragableUIPanel.cs
    /// </summary>
    internal class DraggableUiPanel : UIPanel
    {
        private Vector2 offset;
        private bool dragging;
        private readonly Action<float,float> onOffsetChanged;

        public bool Draggable;

        public DraggableUiPanel(Action<float, float> onOffsetChanged = null, bool draggable = true)
        {
            this.onOffsetChanged = onOffsetChanged;
            Draggable = draggable;
        }

        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);
            DragStart(evt);
        }

        public override void MouseUp(UIMouseEvent evt)
        {
            base.MouseUp(evt);
            DragEnd();
        }

        private void DragStart(UIMouseEvent evt)
        {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        private void DragEnd()
        {
            var newPosition = GetDimensions();
            onOffsetChanged?.Invoke(newPosition.X, newPosition.Y);
            dragging = false;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (dragging && Draggable)
            {
                Left.Set(Main.MouseScreen.X - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same.
                Top.Set(Main.MouseScreen.Y - offset.Y, 0f);
                Recalculate();
            }

            base.DrawSelf(spriteBatch);
        }
    }
}