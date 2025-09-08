using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using static Project1.Enums;

namespace Project1
{
    public class Rat : Visual
    {
        internal float speed;
        internal float defaultSpeed;
        private RatStates ratStates;
        private Viewport window;
        private RatState actualState;

        public Rat(ContentManager content, Vector2 position, Viewport window, float scaleFactor = 1, float speed = 0, Texture2D texture = null) : base(position, texture, scaleFactor)
        {
            this.speed = speed;
            this.defaultSpeed = speed;
            this.ratStates = new RatStates();
            this.ratStates.Load(ConfigurationManager.AppSettings["statesFileName"], content);
            this.actualState = this.ratStates.States.Where(state => state.numState == Enums.State.goingRight).First();
            this.actualTexture = this.actualState.GetTexture();
            this.window = window;
        }

        public virtual void Update(Box box)
        {
            Keys keyPressed = Keyboard.GetState().GetPressedKeys().FirstOrDefault();
            this.actualState = this.ratStates.GetNewState(keyPressed, this.actualState);
            this.actualTexture = this.actualState.GetTexture();
            this.position = updatePosition();
            this.position = box.Collision(this.Rectangle);
            this.scale = Helper.GetScale(this.position.Y, this.window, this.defaultScale, box.margin); 
        }

        private Vector2 updatePosition()
        {
            if(this.actualState.canMove && this.actualState.moving)
            {
                return getNextPosition(this.actualState.direction, speed);
            }
            return this.position;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture = null)
        {
            base.DrawCenter(spriteBatch, this.actualState.needToFlip, this.actualTexture);
        }

        private Vector2 getNextPosition(Direction direction, float speed)
        {
            switch (direction)
            {
                case Enums.Direction.up:
                    return new Vector2(this.position.X, this.position.Y - speed);
                case Enums.Direction.down:
                    return new Vector2(this.position.X, this.position.Y + speed);
                case Enums.Direction.right:
                    return new Vector2(this.position.X + speed, this.position.Y);
                case Enums.Direction.left:
                    return new Vector2(this.position.X - speed, this.position.Y);
            }
            return new Vector2(this.position.X, this.position.Y);
        }
    }
}
