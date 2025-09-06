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
        public char direction;
        public Keys keyToActivate;
        private Texture2D texture1;
        private Texture2D texture2;
        private bool needToFlip;
        private bool canMove;
        private int topFramesAnimation;

        private bool moving;
        private int framesAnimation;
        private Texture2D actualTexture;


        public RatState(XElement state, ContentManager content, int topFramesAnimation)
        {
            this.numState = (State)int.Parse(state.Elements(nameof(RatState.numState)).First().Attribute("value").Value);
            this.direction = state.Elements(nameof(RatState.direction)).First().Attribute("value").Value[0];
            this.keyToActivate = (Keys)int.Parse(state.Elements(nameof(RatState.keyToActivate)).First().Attribute("value").Value);
            this.texture1 = content.Load<Texture2D>(state.Elements(nameof(RatState.texture1)).First().Attribute("value").Value);
            this.texture2 = content.Load<Texture2D>(state.Elements(nameof(RatState.texture2)).First().Attribute("value").Value);
            this.needToFlip = bool.Parse(state.Elements(nameof(RatState.needToFlip)).First().Attribute("value").Value);
            this.canMove = bool.Parse(state.Elements(nameof(RatState.canMove)).First().Attribute("value").Value);
            this.topFramesAnimation = topFramesAnimation;
            this.framesAnimation = 0;
            this.moving = false;
            this.topFramesAnimation = topFramesAnimation;
            this.actualTexture = this.texture1;
        }

        public Texture2D GetNextTexture(RatState lastState)
        {
            if (this.direction != lastState.direction || this.framesAnimation == this.topFramesAnimation)
            {
                this.framesAnimation = 0;
                this.actualTexture = changeTextureDirection();
            }
            return this.actualTexture;
        }

        private Texture2D changeTextureDirection()
        {
            return this.actualTexture == this.texture1 ? this.texture2 : this.texture1;
        }
    }
}
