using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Project1
{
    public class Box
    {
        public int margin;
        private const int width = 1;
        private List<Visual> lines;
        public Box(ContentManager content, int margin, Viewport window) {
            this.margin = margin;
            Texture2D texture = content.Load<Texture2D>("blackpixel");
            lines = new List<Visual>()
            {
                createLine(margin, margin, width, window.Height - (2 * margin), content),
                createLine(margin, margin, window.Width - (2 * margin), width, content),
                createLine(window.Width - margin, margin, width, window.Height - (2 * margin), content),
                createLine(margin, window.Height - margin, window.Width - (2 * margin) + width, width, content)
            };
        }

        private Visual createLine(int x, int y, int width, int height, ContentManager content)
        {
            return new Visual(new Vector2(x, y), content.Load<Texture2D>("blackpixel"), 1f, new Rectangle(x, y, width, height));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (var line in lines)
            {
                line.Draw(spritebatch);
            }
        }

        public Vector2 Collision(Rectangle rectangle)
        {
            int lineNumber = 1;
            foreach (var line in lines)
            {
                if (line.Rectangle.Intersects(rectangle))
                {
                    switch (lineNumber)
                    {
                        case 1: return new Vector2(line.position.X + rectangle.Width / 2 + 1, rectangle.Center.Y);
                        case 2: return new Vector2(rectangle.Center.X, line.position.Y + rectangle.Height / 2 + 1);
                        case 3: return new Vector2(line.position.X - rectangle.Width / 2 - 1, rectangle.Center.Y);
                        case 4: return new Vector2(rectangle.Center.X, line.position.Y - rectangle.Height / 2 - 1);
                    }
                }
                lineNumber++;
            }
            return new Vector2(rectangle.Center.X, rectangle.Center.Y);
        }
    }
}
