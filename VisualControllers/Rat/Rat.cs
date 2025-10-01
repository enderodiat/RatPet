using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RatPet.Helpers;
using System.Linq;
using System.Runtime.CompilerServices;
using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers.Rat
{
    public class Rat : Visual
    {
        public int speed;
        public float defaultSpeed;
        private RatBrain ratBrain;
        private RatState actualState;
        private int clickPadding;
        private ButtonState previousMouseState;

        public Rat(ContentManager content, Visual box, Parameters parameters, Texture2D texture = null) : base(texture, parameters.scale, box, box)
        {
            this.speed = parameters.defaultSpeed;
            this.defaultSpeed = parameters.defaultSpeed;
            this.clickPadding = parameters.clickMousePadding;
            this.ratBrain = new RatBrain(parameters.statesFileName, content, parameters.topFramesAnimation, parameters.clickMousePadding);
            this.actualState = this.ratBrain.States.Where(state => state.numState == RatStateID.goingRight).First();
            this.actualTexture = this.actualState.GetTexture();
        }

        public override void Update()
        {
            var mousePositionPressed = getValidMousePositionPressed();
            var keyPressed = this.ratBrain.GetAllowedKeyPressed(Keyboard.GetState().GetPressedKeys().ToList());
            this.actualState = this.ratBrain.GetNewState(this.Position, keyPressed, mousePositionPressed, this.actualState);
            this.actualTexture = this.actualState.GetTexture();
            move();
            this.scale = Helper.GetScale(this.Position.Y, this.defaultScale, this.collider.Rectangle);
        }

        private Rectangle? getValidMousePositionPressed()
        {
            var mouseState = Mouse.GetState().LeftButton;
            if (mouseState == ButtonState.Pressed && this.previousMouseState == ButtonState.Released)
            {
                this.previousMouseState = mouseState;
                Vector2 mousePositionPressed = Mouse.GetState().Position.ToVector2();
                if (this.container.Rectangle.Contains(mousePositionPressed))
                {
                    return new Rectangle((int)(mousePositionPressed.X - this.clickPadding/2),
                        (int)(mousePositionPressed.Y - this.clickPadding/2), 
                        this.clickPadding, 
                        this.clickPadding);
                }
                else
                {
                    return null;
                }
            }
            this.previousMouseState = mouseState;
            return null;
        }

        public override void Draw(SpriteBatch spriteBatch, bool flip = false, Vector2? position = null, float? layer = null, float transparencyFactor = 1)
        {
            base.Draw(spriteBatch, this.actualState.needToFlip);
        }

        private void move()
        {
            if(this.actualState.canMove && this.actualState.moving)
            {
                this.Position = nextPosition(this.actualState.direction, speed);
            }
            var colisionPosition = this.container.Collision(this);
            if (colisionPosition != Position)
            {
                this.Position = colisionPosition.Value;
                this.ratBrain.Collision();
            }
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
