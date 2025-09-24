using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RatPet.Helpers;
using System;

namespace RatPet.VisualControllers
{
    internal class Cheese : Visual
    {
        public new Rectangle Rectangle { 
            get
            {
                if (fallingPositionY < base.Position.Y)
                {
                    return Rectangle.Empty;
                }
                else
                {
                    return new Rectangle(
                        (int)(Position.X - base.Rectangle.Width / reduceFactorX / 2),
                        (int)(Position.Y - base.Rectangle.Height / reduceFactorY * 1.1),
                        (int)(base.Rectangle.Width / reduceFactorX),
                        (int)(base.Rectangle.Height / reduceFactorY));
                }   
            }
        }
        public new Vector2 Position
        {
            get { 
                if (fallingPositionY < base.Position.Y)
                {
                    return new Vector2(base.Position.X, fallingPositionY);
                } 
                else
                {
                    return base.Position;
                }
            }
        }
        private float reduceFactorX;
        private float reduceFactorY;
        private int speed;
        private int fallingPositionY;

        public Cheese(Vector2 position, Texture2D texture, Visual container, float reduceFactorX, float reduceFactorY, int speed, float scaleFactor = 1f) 
            : base(texture, scaleFactor, null, container, position) 
        {
            this.reduceFactorX = reduceFactorX;
            this.reduceFactorY = reduceFactorY;
            this.speed = speed;
            SetNewPosition();
        }

        private void SetNewPosition()
        {
            Random rdn = new Random();
            base.Position = new Vector2(
                rdn.Next(
                    (int)container.Rectangle.Location.X + (base.Rectangle.Width/2), 
                    (int)container.Rectangle.Location.X + container.Rectangle.Width - (base.Rectangle.Width/2)),
                rdn.Next(
                    (int)container.Rectangle.Location.Y + (base.Rectangle.Height/2), 
                    (int)container.Rectangle.Location.Y + container.Rectangle.Height - (base.Rectangle.Height/2)));
            scale = Helper.GetScale(base.Position.Y, defaultScale, container.Rectangle);
            fallingPositionY = 0;
        }

        public override Vector2? Collision(Visual visual)
        {
            return Rectangle.Intersects(visual.Rectangle) ? new Vector2(Rectangle.Center.X, Rectangle.Center.Y) : null;
        }

        public override void Update()
        {
            fallingPositionY += speed;
        }

        public override void Draw(SpriteBatch spriteBatch, bool flip = false, Vector2? position = null, float? layer = null)
        {
            base.Draw(spriteBatch, flip, this.Position);
        }
    }
}
