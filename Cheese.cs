using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;
using System;

namespace Project1
{
    internal class Cheese : Visual
    {
        private Viewport window;
        private int padding;
        public Cheese(ContentManager content, Vector2 position, Viewport window, int padding, float scaleFactor = 1f) 
            : base(position, content.Load<Texture2D>("cheese2"), scaleFactor) 
        {
            this.window = window;
            this.padding = padding;
        }

        public void SetNewPosition()
        {
            Random rdn = new Random();
            this.position.X = rdn.Next(this.padding + this.Rectangle.Width/2, this.window.Width - (this.padding + this.Rectangle.Width/2));
            this.position.Y = rdn.Next(this.padding + this.Rectangle.Height/2, this.window.Height - (this.padding + this.Rectangle.Height/2));
            this.scale = Helper.GetScale(this.position.Y, this.window, this.defaultScale, this.padding);
        }

        public bool Colision(Visual visual)
        {
            return this.Rectangle.Intersects(visual.Rectangle);
        }
    }
}
