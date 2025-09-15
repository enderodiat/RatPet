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
        private List<Visual> pool;
        private int speed;
        private int topFrames;
        private int framesUntilNewCheese;
        
        public CheesePool(Texture2D texture, Visual box, Visual rat, float reduceFactorX, float reduceFactorY, int speed, int topFrames, float scaleFactor = 1f) 
            : base(texture, scaleFactor, rat, box, null, box.Rectangle)
        {
            this.reduceFactorX = reduceFactorX;
            this.reduceFactorY = reduceFactorY;
            this.speed = speed;
            this.topFrames = topFrames;
            this.pool = new List<Visual>();
            this.framesUntilNewCheese = 0;
        }

        public override void Update()
        {
            if(framesUntilNewCheese == 0)
            {
                Random random = new Random();
                framesUntilNewCheese = random.Next(topFrames);
                pool.Add(new Cheese(new Vector2(0, 0), this.actualTexture, this.container, reduceFactorX, reduceFactorY, speed, scale));
            }
            else
            {
                framesUntilNewCheese--;
            }
            deleteCheese(this.collider);
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
            List<Visual> cheeseToDelete = new List<Visual>();
            foreach(var cheese in pool)
            {
                if (cheese.Collision(visual).HasValue)
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
