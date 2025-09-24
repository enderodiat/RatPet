using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace RatPet.VisualControllers
{
    public class Box : Visual
    {
        private const int thickness = 1;
        private List<Visual> lines;
        private bool drawBorders;

        public Box(Viewport window, Texture2D texture, bool drawBorders, int paddingTop, int paddingRight, int paddingLeft, int paddingBottom, float scaleFactor = 1f) 
            : base(texture, scaleFactor, null, null, new Vector2(paddingLeft, paddingTop))
        {
            this.lines = new List<Visual>()
            {
                createLine(paddingLeft, paddingTop, thickness, window.Height - paddingTop - paddingBottom, texture),
                createLine(paddingLeft, paddingTop, window.Width - paddingRight - paddingLeft, thickness, texture),
                createLine(window.Width - paddingRight, paddingTop, thickness, window.Height - paddingTop - paddingBottom, texture),
                createLine(paddingLeft, window.Height - paddingBottom, window.Width - paddingLeft - paddingRight + thickness, thickness, texture)
            };
            this.Rectangle = new Rectangle(paddingLeft, paddingTop, window.Width - paddingLeft - paddingRight, window.Height - paddingTop - paddingBottom);
            this.drawBorders = drawBorders;
        }

        private Visual createLine(int x, int y, int width, int height, Texture2D texture)
        {
            return new Visual(this.actualTexture, 1f, null, null, new Vector2(x, y), new Rectangle(x, y, width, height));
        }

        public override void Draw(SpriteBatch spritebatch, bool flip = false, Vector2? position = null)
        {
            if (drawBorders)
            {
                foreach (var line in lines)
                {
                    spritebatch.Draw(line.actualTexture, line.Rectangle, Color.White);
                }
            }
        }

        public override Vector2? Collision(Visual visual)
        {
            int lineNumber = 1;
            Rectangle rectangle = visual.Rectangle;
            foreach (var line in lines)
            {
                if (line.Rectangle.Intersects(rectangle))
                {
                    switch (lineNumber)
                    {
                        case 1: return new Vector2(line.Position.X + rectangle.Width / 2 + 1, rectangle.Center.Y);
                        case 2: return new Vector2(rectangle.Center.X, line.Position.Y + rectangle.Height / 2 + 1);
                        case 3: return new Vector2(line.Position.X - rectangle.Width / 2 - 1, rectangle.Center.Y);
                        case 4: return new Vector2(rectangle.Center.X, line.Position.Y - rectangle.Height / 2 - 1);
                    }
                }
                lineNumber++;
            }
            return new Vector2(rectangle.Center.X, rectangle.Center.Y);
        }
    }
}
