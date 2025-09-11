using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class CheesePool
    {
        private ContentManager contentManager;
        private Viewport window;
        private float scale;
        private float reduceFactorX;
        private float reduceFactorY;
        private List<Cheese> pool;
        private int padding;
        private int speed;
        private int topFrames;
        private int framesUntilNewCheese;
        
        public CheesePool(ContentManager content, Viewport window, int padding, float reduceFactorX, float reduceFactorY, int speed, int topFrames, float scaleFactor = 1f)
        {
            this.contentManager = content;
            this.window = window;
            this.padding = padding;
            this.reduceFactorX = reduceFactorX;
            this.reduceFactorY = reduceFactorY;
            this.speed = speed;
            this.scale = scaleFactor;
            this.topFrames = topFrames;
            this.pool = new List<Cheese>();
            this.framesUntilNewCheese = 0;
        }

        public void Update(Visual visual)
        {
            if(framesUntilNewCheese == 0)
            {
                Random random = new Random();
                this.framesUntilNewCheese = random.Next(this.topFrames);
                this.pool.Add(new Cheese(this.contentManager, new Vector2(0, 0), window, this.padding, this.reduceFactorX, this.reduceFactorY, this.speed, this.scale));
            }
            else
            {
                this.framesUntilNewCheese--;
            }
            deleteCheese(visual);
            updateCheese();
        }

        private void updateCheese()
        {
            foreach(var cheese in this.pool)
            {
                cheese.Update();
            }
        }

        private void deleteCheese(Visual visual)
        {
            List<Cheese> cheeseToDelete = new List<Cheese>();
            foreach(var cheese in this.pool)
            {
                if (cheese.Collision(visual))
                {
                    cheeseToDelete.Add(cheese);
                }
            }
            foreach(var cheese in cheeseToDelete)
            {
                this.pool.Remove(cheese);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var cheese in this.pool)
            {
                cheese.Draw(spriteBatch);
            }
        }
    }
}
