using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project1
{
    internal class Cheese : Visual
    {

        public new Rectangle Rectangle { 
            get
            {
                if (this.fallingPositionY < base.Position.Y)
                {
                    return Rectangle.Empty;
                }
                else
                {
                    return new Rectangle(
                        (int)(this.Position.X - base.Rectangle.Width / reduceFactorX / 2),
                        (int)(this.Position.Y - base.Rectangle.Height / reduceFactorY * 1.1),
                        (int)(base.Rectangle.Width / reduceFactorX),
                        (int)(base.Rectangle.Height / reduceFactorY));
                }   
            }
        }
        public new Vector2 Position
        {
            get { 
                if (this.fallingPositionY < base.Position.Y)
                {
                    return new Vector2(base.Position.X, this.fallingPositionY);
                } 
                else
                {
                    return base.Position;
                }
            }
        }
        private int Padding
        {
            get
            {
                return this.padding + (base.Rectangle.Width > base.Rectangle.Height ? base.Rectangle.Width : base.Rectangle.Height);
            }
            set
            {
                this.padding = value;
            }
        }
        private float reduceFactorX;
        private float reduceFactorY;
        private int padding;
        private Viewport window;
        private int speed;
        private int fallingPositionY;

        public Cheese(ContentManager content, Vector2 position, Viewport window, int padding, float reduceFactorX, float reduceFactorY, int speed, float scaleFactor = 1f) 
            : base(position, content.Load<Texture2D>("cheese2"), scaleFactor) 
        {
            this.window = window;
            this.padding = padding;
            this.reduceFactorX = reduceFactorX;
            this.reduceFactorY = reduceFactorY;
            this.speed = speed;
        }

        public void SetNewPosition()
        {
            Random rdn = new Random();
            base.Position = new Vector2(rdn.Next(this.Padding + base.Rectangle.Width / 2, this.window.Width - (this.Padding + base.Rectangle.Width / 2)),
                rdn.Next(this.Padding + base.Rectangle.Height / 2, this.window.Height - (this.Padding + base.Rectangle.Height / 2)));
            this.scale = Helper.GetScale(base.Position.Y, this.window, this.defaultScale, this.Padding);
            this.fallingPositionY = 0;
        }

        public bool Collision(Visual visual)
        {
            return this.Rectangle.Intersects(visual.Rectangle);
        }

        public void Update()
        {
            this.fallingPositionY += speed;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture = null)
        {
            base.DrawCenter(spriteBatch, false, this.Position);
        }
    }
}
