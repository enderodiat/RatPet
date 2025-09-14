using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace RatPet.VisualControllers
{
    public class Box : Visual
    {
        public new Rectangle Rectangle { get; set; }
        private const int width = 1;
        private List<Visual> lines;
        private bool drawBorders;

        public Box(Vector2 position, Viewport window,  Texture2D texture, bool drawBorders, float scaleFactor = 1f) :  base(position, texture, scaleFactor)
        {
            int margin = (int)position.X;
            this.lines = new List<Visual>()
            {
                createLine(margin, margin, width, window.Height - 2 * margin, texture),
                createLine(margin, margin, window.Width - 2 * margin, width, texture),
                createLine(window.Width - margin, margin, width, window.Height - 2 * margin, texture),
                createLine(margin, window.Height - margin, window.Width - 2 * margin + width, width, texture)
            };
            this.Rectangle = new Rectangle(margin, margin, window.Width - (2*margin), window.Height - 2*margin);
            this.drawBorders = drawBorders;
        }

        private Visual createLine(int x, int y, int width, int height, Texture2D texture)
        {
            return new Visual(new Vector2(x, y), this.actualTexture, 1f, new Rectangle(x, y, width, height));
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

        public Vector2 Collision(Visual visual)
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
