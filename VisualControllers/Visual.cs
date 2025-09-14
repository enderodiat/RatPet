using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RatPet.VisualControllers
{
    public class Visual
    {

        public Vector2 Position { 
            get
            {
                return position;
            } 
            set
            {
                position = value;
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
            Position = position;
            scale = scaleFactor;
            defaultScale = scaleFactor;
            actualTexture = texture;
            if (rectangle.HasValue)
            {
                _rectangle = rectangle.Value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, bool flipTexture = false, Vector2? alternativePosition = null)
        {
            var bottomPosition = this.Position.Y + this.Rectangle.Height/2;
            var layer = 1 / bottomPosition;
            spriteBatch.Draw(
                this.actualTexture,
                alternativePosition.HasValue ? alternativePosition.Value : this.Position,
                null,
                Color.White,
                0f,
                new Vector2(this.actualTexture.Width/2, this.actualTexture.Height/2),
                scale,
                flipTexture ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                layer);
        }

        public virtual void Update() { }
    }
}
