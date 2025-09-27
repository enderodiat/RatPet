using System.Configuration;

namespace RatPet.Helpers
{
    public class Parameters
    {
        public int ratAreaPaddingTop;
        public int ratAreaPaddingRight;
        public int ratAreaPaddingLeft;
        public int ratAreaPaddingBottom;
        public int scorePaddingTop;
        public int scorePaddingRight;
        public int tinyCheesePaddingLeft;
        public int scoreCheeseTextPaddingRight;
        public int scoreCheeseTextPaddingTop;
        public int topFramesAnimation;
        public int topFramesPerCheese;
        public int defaultSpeed;
        public int fallingSpeed;
        public int cheeseGoingSpeed;
        public float scale;
        public float uiScale;
        public float reduceCollisionY;
        public float reduceCollisionX;
        public string statesFileName;
        public bool perspective;
        public bool boxBorders;
        public Parameters()
        {
            ratAreaPaddingTop = int.Parse(ConfigurationManager.AppSettings["areaRatPaddingTop"]);
            ratAreaPaddingRight = int.Parse(ConfigurationManager.AppSettings["areaRatPaddingRight"]);
            ratAreaPaddingLeft = int.Parse(ConfigurationManager.AppSettings["areaRatPaddingLeft"]);
            ratAreaPaddingBottom = int.Parse(ConfigurationManager.AppSettings["areaRatPaddingBottom"]);
            scorePaddingTop = int.Parse(ConfigurationManager.AppSettings["scorePaddingTop"]);
            scorePaddingRight = int.Parse(ConfigurationManager.AppSettings["scorePaddingRight"]);
            tinyCheesePaddingLeft = int.Parse(ConfigurationManager.AppSettings["tinyCheesePaddingLeft"]);
            scoreCheeseTextPaddingRight = int.Parse(ConfigurationManager.AppSettings["scoreCheeseTextPaddingRight"]);
            scoreCheeseTextPaddingTop = int.Parse(ConfigurationManager.AppSettings["scoreCheeseTextPaddingTop"]);
            uiScale = float.Parse(ConfigurationManager.AppSettings["uiScale"]);
            topFramesAnimation = int.Parse(ConfigurationManager.AppSettings["topFramesPerSpriteAnimation"]);
            topFramesPerCheese = int.Parse(ConfigurationManager.AppSettings["topFramesPerNewCheese"]);
            defaultSpeed = int.Parse(ConfigurationManager.AppSettings["defaultSpeed"]);
            fallingSpeed = int.Parse(ConfigurationManager.AppSettings["fallingSpeed"]);
            cheeseGoingSpeed = int.Parse(ConfigurationManager.AppSettings["cheeseGoingSpeed"]);
            scale = float.Parse(ConfigurationManager.AppSettings["defaultScale"]);
            reduceCollisionY = float.Parse(ConfigurationManager.AppSettings["collisionCheeseReduceFactorY"]);
            reduceCollisionX = float.Parse(ConfigurationManager.AppSettings["collisionCheeseReduceFactorX"]);
            statesFileName = ConfigurationManager.AppSettings["statesFileName"];
            perspective = bool.Parse(ConfigurationManager.AppSettings["perspective"]);
            boxBorders = bool.Parse(ConfigurationManager.AppSettings["boxBorders"]);
        }
    }
}
