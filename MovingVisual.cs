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
        private int topFramesAnimation;
        private int framesAnimation = 0;
        private char direction = 'd';
        private char lastdirection = 'd';
        internal float speed;
        internal float defaultSpeed;

        public MovingVisual(Vector2 position, Texture2D texture, int topframes, float scaleFactor = 1,  float speed = 0) : base(position, texture, scaleFactor)
        {
            this.speed = speed;
            this.defaultSpeed = speed;
            this.topFramesAnimation = topframes;
        }

        public void LoadTextures(Texture2D D1, Texture2D D2, Texture2D S1, Texture2D S2, Texture2D W1, Texture2D W2)
        {
            this.textureD2 = D2;
            this.textureD1 = D1;
            this.textureS1 = S1;
            this.textureS2 = S2;
            this.textureW1 = W1;
            this.textureW2 = W2;
        }

        public virtual void Update(Box box)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                UpdateBasics();
                this.direction = 'w';
                position.Y -= speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                UpdateBasics();
                this.direction = 's';
                position.Y += speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                UpdateBasics();
                this.direction = 'd';
                position.X += speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                UpdateBasics();
                position.X -= speed;
                this.direction = 'a';
            }
            else
            {
                framesAnimation = topFramesAnimation;
                if (this.direction != ' ')
                {
                    this.lastdirection = this.direction;
                }
                this.direction = ' ';
            }
            position = box.Collision(Rectangle);
        }

        private void UpdateBasics()
        {
            this.lastdirection = this.direction;
            framesAnimation++;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture = null)
        {
            if (textureD1 != null)
            {
                bool flipTexture = (direction == 'a' || (lastdirection == 'a' && direction == ' '));
                base.DrawCenter(spriteBatch, flipTexture, getNextTexture());
            }
        }

        private Texture2D getNextTexture()
        {
            if (direction != lastdirection || framesAnimation == topFramesAnimation)
            {
                framesAnimation = 0;
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
            }
            return this.actualTexture;
        }
    }
}
