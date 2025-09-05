using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;

namespace Project1
{
    internal class Rat : MovingVisual
    {
        private Viewport window;
        public Rat(ContentManager content, Vector2 position, int topFrames, Viewport window, float scaleFactor = 1, float speed = 0) : base(position, content.Load<Texture2D>("rat1"), topFrames, scaleFactor, speed)
        {
            base.LoadTextures(
                content.Load<Texture2D>("rat1"), 
                content.Load<Texture2D>("rat2"),
                content.Load<Texture2D>("rat7"),
                content.Load<Texture2D>("rat8"),
                content.Load<Texture2D>("rat9"),
                content.Load<Texture2D>("rat10"));
            this.window = window;
        }

        public override void Update(Box box)
        {
            base.Update(box);
            if (float.Parse(ConfigurationManager.AppSettings["perspective"]) != 0)
            {
                this.scale = Helper.GetScale(this.position.Y, this.window, this.defaultScale, box.margin);
                //this.speed = Helper.GetSpeed(this.position.Y, this.window, this.defaultSpeed, box.margin);
            }
        }
    }
}
