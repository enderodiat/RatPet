using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RatPet.VisualControllers
{
    public class CheeseScore : Visual
    {
        private Texture2D tinyCheeseTexture;
        private SpriteFont font;

        public CheeseScore(Viewport window, Texture2D boardTexture, Texture2D tinyCheeseTexture, SpriteFont font, int paddingTop, int paddingRight, float scaleFactor = 1) 
            : base(boardTexture, scaleFactor)
        {
            this.tinyCheeseTexture = tinyCheeseTexture;
            this.font = font;
            this.Position = new Vector2(window.Width - paddingRight - this.Rectangle.Width/2, paddingTop + this.Rectangle.Height/2);
        }
    }
}
