using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project1
{
    internal class Cheese : Visual
    {
        
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
        private new Rectangle Rectangle { 
            get
            {
                return new Rectangle(
                    (int)(this.position.X - base.Rectangle.Width / reduceFactorX / 2),
                    (int)(this.position.Y - base.Rectangle.Height / reduceFactorY * 1.1),
                    (int)(base.Rectangle.Width / reduceFactorX),
                    (int)(base.Rectangle.Height / reduceFactorY));
            }
        }
        private float reduceFactorX;
        private float reduceFactorY;
        private int padding;
        private Viewport window;

        public Cheese(ContentManager content, Vector2 position, Viewport window, int padding, float reduceFactorX, float reduceFactorY, float scaleFactor = 1f) 
            : base(position, content.Load<Texture2D>("cheese2"), scaleFactor) 
        {
            this.window = window;
            this.padding = padding;
            this.reduceFactorX = reduceFactorX;
            this.reduceFactorY = reduceFactorY;
        }

        public void SetNewPosition()
        {
            Random rdn = new Random();
            this.position.X = rdn.Next(this.Padding + base.Rectangle.Width/2, this.window.Width - (this.Padding + base.Rectangle.Width/2));
            this.position.Y = rdn.Next(this.Padding + base.Rectangle.Height/2, this.window.Height - (this.Padding + base.Rectangle.Height/2));
            this.scale = Helper.GetScale(this.position.Y, this.window, this.defaultScale, this.Padding);
        }

        public bool Collision(Visual visual)
        {
            return this.Rectangle.Intersects(visual.Rectangle);
        }
    }
}
