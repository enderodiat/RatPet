using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RatPet.Helpers;

namespace RatPet.VisualControllers
{
    public class CheeseScore : Visual
    {
        public int score = 0;
        public Visual tinyCheese;
        private SpriteFont font;
        private int textPaddingRight;
        private int textPaddingTop;

        public CheeseScore(Viewport window, Texture2D boardTexture, Texture2D tinyCheeseTexture, SpriteFont font, Parameters parameters)
            : base(boardTexture, parameters.uiScale)
        {
            this.textPaddingTop = parameters.scoreCheeseTextPaddingTop;
            this.textPaddingRight = parameters.scoreCheeseTextPaddingRight;  
            this.font = font;
            this.Position = new Vector2(window.Width - parameters.scorePaddingRight - this.Rectangle.Width/2, parameters.scorePaddingTop + this.Rectangle.Height/2);
            this.tinyCheese = new Visual(tinyCheeseTexture,
                parameters.uiScale,
                null,
                this,
                new Vector2(this.Rectangle.Left + parameters.tinyCheesePaddingLeft + (tinyCheeseTexture.Width * parameters.uiScale / 2), this.Rectangle.Center.Y));
        }

        public override void Draw(SpriteBatch spriteBatch, bool flipTexture = false, Vector2? alternativePosition = null, float? layer = null)
        {            
            base.Draw(spriteBatch, flipTexture, alternativePosition, 0.9999f);

            this.tinyCheese.Draw(spriteBatch, false, null, 0.999f);

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
                0.999f);
        }
    }
}
