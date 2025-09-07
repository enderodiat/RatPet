using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Rat : Visual
    {
        internal float speed;
        internal float defaultSpeed;
        private RatStates ratStates;
        private Viewport window;
        private RatState actualState;

        //Nota: debería ver que propiedades publicas uso de state para delegar esa lógica a la clase state
        //también debería hacer algo con las direcciones (clase enumerada o algo)
        //y con la strings que uso para acceder al appsettings

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
            var keyPressed = Keyboard.GetState().GetPressedKeys().FirstOrDefault(Keys.X); // TODO: detectar tecla sobrepulsada
            bool wasMoving = this.actualState.moving;
            this.actualState = this.ratStates.States.Where(state => state.keyToActivate == keyPressed).FirstOrDefault(this.actualState);
            this.actualState.moving = !(keyPressed == Keys.X);
            this.actualState.wasMoving = wasMoving;
            this.actualTexture = this.actualState.moving ? this.actualState.GetTexture() : this.actualTexture;
            this.position = this.actualState.canMove && this.actualState.moving ? getPosition(this.actualState.direction, speed) : this.position;
            this.position = box.Collision(this.Rectangle);
            this.scale = Helper.GetScale(this.position.Y, this.window, this.defaultScale, box.margin); 
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture = null)
        {
            base.DrawCenter(spriteBatch, this.actualState.needToFlip, this.actualTexture);
        }

        private Vector2 getPosition(char direction, float speed)
        {
            switch (direction) // TODO: pasar directo a enum
            {
                case 'w':
                    return new Vector2(this.position.X, this.position.Y - speed);
                case 's':
                    return new Vector2(this.position.X, this.position.Y + speed);
                case 'd':
                    return new Vector2(this.position.X + speed, this.position.Y);
                case 'a':
                    return new Vector2(this.position.X - speed, this.position.Y);
            }
            return new Vector2(this.position.X, this.position.Y);
        }
    }
}
