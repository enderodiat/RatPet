using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class Visual
    {

        public Vector2 Position { 
            get
            {
                return this.position;
            } 
            set
            {
                this.position = value;
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                if (!_rectangle.HasValue)
                {
                    int width = (int)(actualTexture.Width * scale);
                    int height = (int)(actualTexture.Height * scale);
                    return new Rectangle(
                        (int)Position.X - width / 2,
                        (int)Position.Y - height / 2,
                        width,
                        height);
                }
                else
                {
                    return _rectangle.Value;
                }
            }
        }
        public float scale;
        public float defaultScale;
        public Texture2D actualTexture;
        private Rectangle? _rectangle;
        private Vector2 position;
        

        public Visual(Vector2 position, Texture2D texture, float scaleFactor = 1f, Rectangle? rectangle = null)
        {
            this.Position = position;
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

        public void DrawCenter(SpriteBatch spriteBatch, bool flipTexture, Vector2? position = null)
        {
            var bottomPosition = this.Position.Y + this.Rectangle.Height/2;
            var layer = (1 / bottomPosition);
            spriteBatch.Draw(
                this.actualTexture,
                position.HasValue ? position.Value : this.Position,
                null,
                Color.White,
                0f,
                new Vector2(this.actualTexture.Width/2, this.actualTexture.Height/2),
                this.scale,
                flipTexture ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                layer);
        }
    }
}
