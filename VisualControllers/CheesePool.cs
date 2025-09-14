using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RatPet.VisualControllers
{
    public class CheesePool : Visual
    {
        private float reduceFactorX;
        private float reduceFactorY;
        private List<Cheese> pool;
        private int speed;
        private int topFrames;
        private int framesUntilNewCheese;
        private Rat rat;
        private Rectangle area;
        
        public CheesePool(Vector2 position, Texture2D texture, Rectangle area, Rat rat, float reduceFactorX, float reduceFactorY, int speed, int topFrames, float scaleFactor = 1f) 
            : base(position, texture, scaleFactor)
        {
            this.reduceFactorX = reduceFactorX;
            this.reduceFactorY = reduceFactorY;
            this.speed = speed;
            this.rat = rat;
            this.topFrames = topFrames;
            this.pool = new List<Cheese>();
            this.framesUntilNewCheese = 0;
            this.area = area;
        }

        public override void Update()
        {
            if(framesUntilNewCheese == 0)
            {
                Random random = new Random();
                framesUntilNewCheese = random.Next(topFrames);
                pool.Add(new Cheese(new Vector2(0, 0), this.actualTexture, this.area, reduceFactorX, reduceFactorY, speed, scale));
            }
            else
            {
                framesUntilNewCheese--;
            }
            deleteCheese(this.rat);
            updateCheese();
        }

        public override void Draw(SpriteBatch spritebatch, bool flip = false, Vector2? position = null)
        {
            foreach (var cheese in pool)
            {
                cheese.Draw(spritebatch);
            }
        }

        private void updateCheese()
        {
            foreach(var cheese in pool)
            {
                cheese.Update();
            }
        }

        private void deleteCheese(Visual visual)
        {
            List<Cheese> cheeseToDelete = new List<Cheese>();
            foreach(var cheese in pool)
            {
                if (cheese.Collision(visual))
                {
                    cheeseToDelete.Add(cheese);
                }
            }
            foreach(var cheese in cheeseToDelete)
            {
                pool.Remove(cheese);
            }
        }
    }
}
