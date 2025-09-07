using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class Visual
    {
        internal Vector2 position;
        internal float scale;
        internal float defaultScale;
        internal Texture2D actualTexture;
        private Rectangle? _rectangle;
        public Rectangle Rectangle
        {
            get
            {
                if(!_rectangle.HasValue)
                {
                    int width = (int)(actualTexture.Width * scale);
                    int height = (int)(actualTexture.Height * scale);
                    return new Rectangle(
                        (int)position.X - width/2,
                        (int)position.Y - height/2,
                        width,
                        height);
                }
                else
                {
                    return _rectangle.Value;
                }
            }
        }

        public Visual(Vector2 position, Texture2D texture, float scaleFactor = 1f, Rectangle? rectangle = null)
        {
            this.position = position;
            this.scale = scaleFactor;
            this.defaultScale = scaleFactor;
            this.actualTexture = texture;
            if (rectangle.HasValue)
            {
                this._rectangle = rectangle.Value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture = null) {
            spriteBatch.Draw(texture != null ? texture : this.actualTexture, Rectangle, Color.White);
        }

        public void DrawCenter(SpriteBatch spriteBatch, bool flipTexture, Texture2D texture = null)
        {
            Texture2D texture2D = texture != null ? texture : this.actualTexture;
            spriteBatch.Draw(
                texture2D,
                this.position,
                null,
                Color.White,
                0f,
                new Vector2(texture2D.Width/2, texture2D.Height/2),
                this.scale,
                flipTexture ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);
        }
    }
}
