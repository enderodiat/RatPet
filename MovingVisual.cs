using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class MovingVisual : Visual
    {
        private Texture2D textureD1;
        private Texture2D textureD2;
        private Texture2D textureS1;
        private Texture2D textureS2;
        private Texture2D textureW1;
        private Texture2D textureW2;
        private Texture2D textureX1;
        private Texture2D textureX2;
        private Texture2D textureX3;
        private Texture2D textureX4;
        private int topFramesAnimation;
        private int framesAnimation = 0;
        private char direction = 'd';
        private char lastdirection = 'd';
        internal float speed;
        internal float defaultSpeed;
        private bool lookingLeft = false;
        private bool idle = false;

        public MovingVisual(Vector2 position, Texture2D texture, int topframes, float scaleFactor = 1,  float speed = 0) : base(position, texture, scaleFactor)
        {
            this.speed = speed;
            this.defaultSpeed = speed;
            this.topFramesAnimation = topframes;
        }

        public void LoadTextures(Texture2D D1, Texture2D D2, Texture2D S1, Texture2D S2, Texture2D W1, Texture2D W2, Texture2D X1, Texture2D X2, Texture2D X3, Texture2D X4)
        {
            this.textureD2 = D2;
            this.textureD1 = D1;
            this.textureS1 = S1;
            this.textureS2 = S2;
            this.textureW1 = W1;
            this.textureW2 = W2;
            this.textureX1 = X1;
            this.textureX2 = X2;
            this.textureX3 = X3;
            this.textureX4 = X4;
        }

        public virtual void Update(Box box)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                UpdateBasics();
                this.direction = 'w';
                this.position.Y -= speed;
                this.idle = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                UpdateBasics();
                this.direction = 's';
                this.position.Y += speed;
                this.idle = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                UpdateBasics();
                this.direction = 'd';
                this.position.X += speed;
                this.lookingLeft = false;
                this.idle = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                UpdateBasics();
                this.position.X -= speed;
                this.direction = 'a';
                this.lookingLeft = true;
                this.idle = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space)){
                UpdateBasics();
                this.direction = 'x';
                this.idle = true;
            }
            else
            {
                this.framesAnimation = this.topFramesAnimation;
                if (this.direction != ' ')
                {
                    this.lastdirection = this.direction;
                }
                this.direction = ' ';
            }
            this.position = box.Collision(this.Rectangle);
        }

        private void UpdateBasics()
        {
            this.lastdirection = this.direction;
            this.framesAnimation++;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture = null)
        {
            if (this.textureD1 != null)
            {
                base.DrawCenter(spriteBatch, this.lookingLeft && !this.idle, getNextTexture());
            }
        }

        private Texture2D getNextTexture()
        {
            if (this.direction != this.lastdirection || this.framesAnimation == this.topFramesAnimation)
            {
                this.framesAnimation = 0;
                this.actualTexture = changeTextureDirection();
            }
            return this.actualTexture;
        }

        private Texture2D changeTextureDirection()
        {
            switch (this.direction)
            {
                case 'a':
                    return this.actualTexture == this.textureD1 ? this.textureD2 : this.textureD1;
                case 's':
                    return this.actualTexture == this.textureS1 ? this.textureS2 : this.textureS1;
                case 'd':
                    return this.actualTexture == this.textureD1 ? this.textureD2 : this.textureD1;
                case 'w':
                    return this.actualTexture == this.textureW1 ? this.textureW2 : this.textureW1;
                case 'x':
                    if (lookingLeft)
                    {
                        return this.actualTexture == this.textureX3 ? this.textureX4 : this.textureX3;
                    }
                    else
                    {
                        return this.actualTexture == this.textureX1 ? this.textureX2 : this.textureX1;
                    }
                        
            }
            return this.actualTexture;
        }
    }
}
