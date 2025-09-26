using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RatPet.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using static RatPet.Helpers.Enums;
using static System.Net.Mime.MediaTypeNames;

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
        private int eatenCheese = 0;
        private float uiScale;
        private CheeseScore scoreBoard;
        
        public CheesePool(Texture2D texture, Visual box, Visual rat, Parameters parameters, CheeseScore scoreBoard) 
            : base(texture, parameters.scale, rat, box, null, box.Rectangle)
        {
            this.reduceFactorX = parameters.reduceCollisionX;
            this.reduceFactorY = parameters.reduceCollisionY;
            this.speed = parameters.fallingSpeed;
            this.topFrames = parameters.topFramesPerCheese;
            this.pool = new List<Cheese>();
            this.framesUntilNewCheese = 0;
            this.scoreBoard = scoreBoard;
            this.uiScale = parameters.uiScale;
        }

        public override void Update()
        {
            if(framesUntilNewCheese == 0)
            {
                Random random = new Random();
                framesUntilNewCheese = random.Next(topFrames);
                pool.Add(new Cheese(new Vector2(0, 0), this.actualTexture, this.container, this.scoreBoard.tinyCheese, reduceFactorX, reduceFactorY, speed, scale, uiScale));
            }
            else
            {
                framesUntilNewCheese--;
            }
            scoreCheeses(this.collider);
            deleteCheeses();
            updateCheeses();
            scoreBoard.score = eatenCheese;
        }

        public override void Draw(SpriteBatch spritebatch, bool flip = false, Vector2? position = null, float? layer = null)
        {
            foreach (var cheese in pool)
            {
                cheese.Draw(spritebatch);
            }
        }

        private void updateCheeses()
        {
            foreach(var cheese in pool)
            {
                cheese.Update();
            }
        }

        private void scoreCheeses(Visual collider)
        {
            foreach (var cheese in pool.Where(cheese => cheese.state == CheeseStateID.chilling).ToList())
            {
                if (cheese.Collision(collider).HasValue)
                {
                    eatenCheese++;
                    cheese.SetState(CheeseStateID.going);
                }
            }
        }

        private void deleteCheeses()
        {
            pool.RemoveAll(cheese => cheese.state == CheeseStateID.deleted);
        }
    }
}
