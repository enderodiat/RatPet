using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RatPet.VisualControllers
{
    public class CheeseScore : Visual
    {
        public int score = 0;
        private Visual tinyCheese;
        private SpriteFont font;
        private int textPaddingRight;
        private int textPaddingTop;

        public CheeseScore(Viewport window, Texture2D boardTexture, Texture2D tinyCheeseTexture, SpriteFont font, int paddingTop, int paddingRight, int tinyCheesePaddingLeft, int textPaddingRight, int textPaddingTop, float scaleFactor = 1) 
            : base(boardTexture, scaleFactor)
        {
            this.textPaddingTop = textPaddingTop;
            this.textPaddingRight = textPaddingRight;  
            this.font = font;
            this.Position = new Vector2(window.Width - paddingRight - this.Rectangle.Width/2, paddingTop + this.Rectangle.Height/2);
            this.tinyCheese = new Visual(tinyCheeseTexture,
                scaleFactor,
                null,
                this,
                new Vector2(this.Rectangle.Left + tinyCheesePaddingLeft + (tinyCheeseTexture.Width * scaleFactor / 2), this.Rectangle.Center.Y));
        }

        public override void Draw(SpriteBatch spriteBatch, bool flipTexture = false, Vector2? alternativePosition = null, float? layer = null)
        {            
            base.Draw(spriteBatch, flipTexture, alternativePosition);

            this.tinyCheese.Draw(spriteBatch, false, null, 0f);

            Vector2 size = font.MeasureString(score.ToString());
            spriteBatch.DrawString(
                this.font,
                score.ToString(),
                new Vector2(this.Rectangle.Right - this.textPaddingRight, this.Rectangle.Top + this.textPaddingTop),
                new Color(Color.Black, 1f),
                0,
                new Vector2(size.X, 0),
                this.scale,
                SpriteEffects.None,
                0f);
        }
    }
}
