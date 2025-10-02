using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RatPet.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using System.Xml.Linq;
using System;
using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers.Rat
{
    public class Rat : Visual
    {
        public int speed;
        public float defaultSpeed;
        private RatBrain ratBrain;
        private RatState actualState;
        private List<RatState> ratStates;
        private Gamepad controller;
        private int topFramesAnimation;

        public Rat(ContentManager content, Visual box, Parameters parameters, Texture2D texture = null) : base(texture, parameters.scale, box, box)
        {
            this.topFramesAnimation = parameters.topFramesAnimation;
            this.speed = parameters.defaultSpeed;
            this.defaultSpeed = parameters.defaultSpeed;
            this.ratStates = loadRatStates(parameters.statesFileName, content);
            this.ratBrain = new RatBrain(this.ratStates);
            this.actualState = this.ratBrain.States.Where(state => state.numState == RatStateID.goingRight).First();
            this.actualTexture = this.actualState.GetTexture();
            this.controller = new Gamepad(parameters);
        }

        public override void Update()
        {
            var mousePositionPressed = this.controller.GetValidMousePositionPressed(this.container);
            var keyPressed = this.controller.GetAllowedKeyPressed(this.ratStates);
            this.actualState = this.ratBrain.GetNewState(this.Position, keyPressed, mousePositionPressed, this.actualState);
            this.actualTexture = this.actualState.GetTexture();
            move();
            this.scale = Helper.GetScale(this.Position.Y, this.defaultScale, this.collider.Rectangle);
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

        private List<RatState> loadRatStates(string fileName, ContentManager content)
        {
            var ratStates = new List<RatState>();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                XDocument doc = XDocument.Load(stream);
                var states = doc.Descendants(typeof(RatState).Name);
                foreach (var element in states)
                {
                    ratStates.Add(new RatState(element, content, this.topFramesAnimation));
                }
            }
            return ratStates;
        }
    }
}
