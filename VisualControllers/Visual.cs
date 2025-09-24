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
        public virtual Rectangle Rectangle
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
            set
            {
                _rectangle = value;
            }
        }
        public float scale;
        public float defaultScale;
        public Texture2D actualTexture;
        public Visual collider;
        public Visual container;
        private Rectangle? _rectangle;
        private Vector2 position;
        

        public Visual(Texture2D texture, float scaleFactor = 1f,  Visual collider = null, Visual container = null, Vector2? position = null, Rectangle? rectangle = null)
        {
            this.scale = scaleFactor;
            this.defaultScale = scaleFactor;
            this.actualTexture = texture;
            this._rectangle = rectangle.HasValue ? rectangle.Value : null;
            this.collider = collider;
            this.container = container;
            if (position.HasValue)
            {
                this.position = position.Value;
            }
            else if(!position.HasValue && container != null)
            {
                this.position = new Vector2(container.Rectangle.Center.X, container.Rectangle.Center.Y);
            }
            else
            {
                position = new Vector2(0, 0);
            } 
        }

        public virtual void Draw(SpriteBatch spriteBatch, bool flipTexture = false, Vector2? alternativePosition = null, float? alternativeLayer = null)
        {
            var bottomPosition = this.Position.Y + this.Rectangle.Height/2;
            var layer = alternativeLayer.HasValue ? alternativeLayer.Value : 1 / bottomPosition;
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
        public virtual Vector2? Collision(Visual visual) { 
            return null; 
        }
    }
}
