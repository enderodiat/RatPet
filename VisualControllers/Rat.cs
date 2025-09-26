using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RatPet.Helpers;
using System.Linq;
using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers
{
    public class Rat : Visual
    {
        internal int speed;
        internal float defaultSpeed;
        private RatStates ratStates;
        private RatState actualState;

        public Rat(ContentManager content, Visual box, Parameters parameters, Texture2D texture = null) : base(texture, parameters.scale, box, box)
        {                
            this.speed = parameters.defaultSpeed;
            defaultSpeed = parameters.defaultSpeed;
            ratStates = new RatStates(parameters.statesFileName, content, parameters.topFramesAnimation);
            actualState = ratStates.States.Where(state => state.numState == RatStateID.goingRight).First();
            actualTexture = actualState.GetTexture();
        }

        public override void Update()
        {
            Keys keyPressed = ratStates.GetAllowedKeyPressed(Keyboard.GetState().GetPressedKeys().ToList());
            actualState = ratStates.GetNewState(keyPressed, actualState);
            actualTexture = actualState.GetTexture();
            Position = updatePosition();
            Position = this.container.Collision(this).Value;
            scale = Helper.GetScale(Position.Y, defaultScale, this.collider.Rectangle); 
        }

        public override void Draw(SpriteBatch spriteBatch, bool flip = false, Vector2? position = null, float? layer = null)
        {
            base.Draw(spriteBatch, actualState.needToFlip);
        }

        private Vector2 updatePosition()
        {
            if(actualState.canMove && actualState.moving)
            {
                return nextPosition(actualState.direction, speed);
            }
            return Position;
        }

        private Vector2 nextPosition(Direction direction, float speed)
        {
            switch (direction)
            {
                case Direction.up:
                    return new Vector2(Position.X, Position.Y - speed);
                case Direction.down:
                    return new Vector2(Position.X, Position.Y + speed);
                case Direction.right:
                    return new Vector2(Position.X + speed, Position.Y);
                case Direction.left:
                    return new Vector2(Position.X - speed, Position.Y);
            }
            return new Vector2(Position.X, Position.Y);
        }
    }
}
