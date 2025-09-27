using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RatPet.Helpers;
using System;
using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers
{
    internal class Cheese : Visual
    {
        public new Rectangle Rectangle { 
            get
            {
                if(this.state == CheeseStateID.chilling)
                {
                    return new Rectangle(
                        (int)(Position.X - base.Rectangle.Width / reduceFactorX / 2),
                        (int)(Position.Y - base.Rectangle.Height / reduceFactorY * 1.1),
                        (int)(base.Rectangle.Width / reduceFactorX),
                        (int)(base.Rectangle.Height / reduceFactorY));
                } 
                else if (this.state == CheeseStateID.going)
                {
                    return base.Rectangle;
                }
                else {
                    return Rectangle.Empty;
                }   
            }
        }
        public new Vector2 Position
        {
            get {
                return new Vector2(_positionX, _positionY);
            }
        }
        public CheeseStateID state;
        private float reduceFactorX;
        private float reduceFactorY;
        private int fallingSpeed;
        private int goingSpeed;
        private float _positionY;
        private float _positionX;
        private float uiScale;

        public Cheese(Vector2 position, Texture2D texture, Visual container, Visual collaider, float reduceFactorX, float reduceFactorY, int fallingSpeed, int goingSpeed, float scaleFactor, float uiScale) 
            : base(texture, scaleFactor, collaider, container, position) 
        {
            this.reduceFactorX = reduceFactorX;
            this.reduceFactorY = reduceFactorY;
            this.fallingSpeed = fallingSpeed;
            this.goingSpeed = goingSpeed;
            this.uiScale = uiScale;
            SetNewPosition();
            SetState(CheeseStateID.falling);
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
        }

        public void SetState(CheeseStateID state)
        {
            this.state = state;
            switch (state)
            {
                case CheeseStateID.falling:
                    _positionY = 0;
                    _positionX = base.Position.X;
                    break;
                case CheeseStateID.chilling:
                    _positionY = base.Position.Y;
                    _positionX = base.Position.X;
                    break;
            }
            
        }

        public override Vector2? Collision(Visual visual)
        {
            return Rectangle.Intersects(visual.Rectangle) ? new Vector2(Rectangle.Center.X, Rectangle.Center.Y) : null;
        }

        public override void Update()
        {
            switch (this.state)
            {
                case CheeseStateID.falling:
                    if (_positionY < base.Position.Y)
                    {
                        _positionY += fallingSpeed;
                    }
                    else
                    {
                        SetState(CheeseStateID.chilling);
                    }
                    break;
                case CheeseStateID.going:
                    this.actualTexture = this.collider.actualTexture;
                    this.scale = this.uiScale;
                    Vector2 destinationPoint = new Vector2(this.collider.Position.X, this.collider.Position.Y);
                    float m = (destinationPoint.Y - this.Position.Y)/(destinationPoint.X - this.Position.X);
                    float x = this.Position.X < this.collider.Position.X ? 
                        (float)(this.Position.X + (this.goingSpeed / Math.Sqrt(1 + m * m))) : (float)(this.Position.X - (this.goingSpeed / Math.Sqrt(1 + m * m)));
                    float y = m * (x - this.Position.X) + this.Position.Y;
                    _positionX = x;
                    _positionY = y;
                    if (this.Position.Y < destinationPoint.Y) {
                        SetState(CheeseStateID.deleted);
                    }
                    break;
            } 
        }

        public override void Draw(SpriteBatch spriteBatch, bool flip = false, Vector2? position = null, float? layer = null)
        {
            base.Draw(spriteBatch, flip, this.Position);
        }
    }
}
