using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Project1.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project1
{
    public class RatState
    {
        public State numState;
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
            this.numState = (State)int.Parse(state.Elements(nameof(RatState.numState)).First().Attribute("value").Value);
            this.direction = (Direction)int.Parse(state.Elements(nameof(RatState.direction)).First().Attribute("value").Value);
            this.keyToActivate = (Keys)int.Parse(state.Elements(nameof(RatState.keyToActivate)).First().Attribute("value").Value);
            this.texture1 = content.Load<Texture2D>(state.Elements(nameof(RatState.texture1)).First().Attribute("value").Value);
            this.texture2 = content.Load<Texture2D>(state.Elements(nameof(RatState.texture2)).First().Attribute("value").Value);
            this.needToFlip = bool.Parse(state.Elements(nameof(RatState.needToFlip)).First().Attribute("value").Value);
            this.canMove = bool.Parse(state.Elements(nameof(RatState.canMove)).First().Attribute("value").Value);
            this.topFramesAnimation = topFramesAnimation;
            this.framesAnimation = 0;
            this.moving = false;
            this.actualTexture = this.texture1;
        }

        public Texture2D GetTexture()
        {
            if (!this.wasMoving || this.framesAnimation == this.topFramesAnimation)
            {
                this.framesAnimation = 0;
                this.actualTexture = changeTextureAnimation();
            } 
            else
            {
                this.framesAnimation++;
            }
            return this.actualTexture;
        }

        private Texture2D changeTextureAnimation()
        {
            return this.actualTexture == this.texture1 ? this.texture2 : this.texture1;
        }
    }
}
