using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatPet.Helpers
{
    public class Parameters
    {
        public int padding;
        public int topFramesAnimation;
        public int mediaFramesPerCheese;
        public int defaultSpeed;
        public int fallingSpeed;
        public float scale;
        public float reduceCollisionY;
        public float reduceCollisionX;
        public string statesFileName;
        public bool perspective;
        public bool boxBorders;
        public Parameters()
        {
            padding = int.Parse(ConfigurationManager.AppSettings["areaRatPadding"]);
            topFramesAnimation = int.Parse(ConfigurationManager.AppSettings["topFramesPerSpriteAnimation"]);
            mediaFramesPerCheese = int.Parse(ConfigurationManager.AppSettings["mediaFramesPerNewCheese"]);
            defaultSpeed = int.Parse(ConfigurationManager.AppSettings["defaultSpeed"]);
            fallingSpeed = int.Parse(ConfigurationManager.AppSettings["fallingSpeed"]);
            scale = float.Parse(ConfigurationManager.AppSettings["defaultScale"]);
            reduceCollisionY = float.Parse(ConfigurationManager.AppSettings["collisionCheeseReduceFactorY"]);
            reduceCollisionX = float.Parse(ConfigurationManager.AppSettings["collisionCheeseReduceFactorX"]);
            statesFileName = ConfigurationManager.AppSettings["statesFileName"];
            perspective = bool.Parse(ConfigurationManager.AppSettings["perspective"]);
            boxBorders = bool.Parse(ConfigurationManager.AppSettings["boxBorders"]);
        }
    }
}
