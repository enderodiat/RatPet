using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RatPet.Helpers;
using System.Collections.Generic;
namespace RatPet.VisualControllers
{
    public class Box : Visual
    {
        private const int thickness = 1;
        private List<Visual> lines;
        private bool drawBorders;

        public Box(Viewport window, Texture2D texture, Parameters parameters) 
            : base(texture, parameters.scale, null, null, new Vector2(parameters.ratAreaPaddingLeft, parameters.ratAreaPaddingTop))
        {
            this.lines = new List<Visual>()
            {
                createLine(parameters.ratAreaPaddingLeft, 
                    parameters.ratAreaPaddingTop, thickness, 
                    window.Height - parameters.ratAreaPaddingTop - parameters.ratAreaPaddingBottom, 
                    texture),
                createLine(parameters.ratAreaPaddingLeft, 
                    parameters.ratAreaPaddingTop, 
                    window.Width - parameters.ratAreaPaddingRight - parameters.ratAreaPaddingLeft, 
                    thickness, 
                    texture),
                createLine(window.Width - parameters.ratAreaPaddingRight, 
                    parameters.ratAreaPaddingTop, 
                    thickness, 
                    window.Height - parameters.ratAreaPaddingTop - parameters.ratAreaPaddingBottom, 
                    texture),
                createLine(parameters.ratAreaPaddingLeft, 
                    window.Height - parameters.ratAreaPaddingBottom, 
                    window.Width - parameters.ratAreaPaddingLeft - parameters.ratAreaPaddingRight + thickness, 
                    thickness, 
                    texture)
            };
            this.Rectangle = new Rectangle(parameters.ratAreaPaddingLeft, 
                parameters.ratAreaPaddingTop, 
                window.Width - parameters.ratAreaPaddingLeft - parameters.ratAreaPaddingRight, 
                window.Height - parameters.ratAreaPaddingTop - parameters.ratAreaPaddingBottom);
            this.drawBorders = parameters.boxBorders;
        }

        private Visual createLine(int x, int y, int width, int height, Texture2D texture)
        {
            return new Visual(this.actualTexture, 1f, null, null, new Vector2(x, y), new Rectangle(x, y, width, height));
        }

        public override void Draw(SpriteBatch spritebatch, bool flip = false, Vector2? position = null, float? layer = null, float transparencyFactor = 1)
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
