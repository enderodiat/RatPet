using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Xml.Linq;
using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers
{
    public class RatState
    {
        public RatStateID numState;
        public Direction direction;
        public Direction previousHorizontalDirection;
        public Keys keyToActivate;
        public bool needToFlip;
        public bool moving;
        public bool canMove;
        public bool wasMoving;
        private Texture2D actualTexture;
        private Texture2D texture1;
        private Texture2D texture2;
        private int topFramesAnimation;
        private int framesAnimation;

        public RatState(XElement state, ContentManager content, int topFramesAnimation)
        {
            numState = (RatStateID)int.Parse(state.Elements(nameof(numState)).First().Attribute("value").Value);
            direction = (Direction)int.Parse(state.Elements(nameof(direction)).First().Attribute("value").Value);
            keyToActivate = (Keys)int.Parse(state.Elements(nameof(keyToActivate)).First().Attribute("value").Value);
            texture1 = content.Load<Texture2D>(state.Elements(nameof(texture1)).First().Attribute("value").Value);
            texture2 = content.Load<Texture2D>(state.Elements(nameof(texture2)).First().Attribute("value").Value);
            needToFlip = bool.Parse(state.Elements(nameof(needToFlip)).First().Attribute("value").Value);
            canMove = bool.Parse(state.Elements(nameof(canMove)).First().Attribute("value").Value);
            this.topFramesAnimation = topFramesAnimation;
            framesAnimation = 0;
            moving = false;
            actualTexture = texture1;
            previousHorizontalDirection = Direction.right;
        }

        public Texture2D GetTexture()
        {
            if (moving)
            {
                if (!wasMoving || framesAnimation == topFramesAnimation)
                {
                    framesAnimation = 0;
                    actualTexture = changeTextureAnimation();
                }
                else
                {
                    framesAnimation++;
                }
            }
            return actualTexture;
        }

        private Texture2D changeTextureAnimation()
        {
            return actualTexture == texture1 ? texture2 : texture1;
        }
    }
}
