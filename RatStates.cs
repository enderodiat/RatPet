using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using static Project1.Enums;

namespace Project1
{
    public class RatStates
    {
        public List<RatState> States { get; set; }
        private int topFramesAnimation;
        public RatStates() { }
        public void Load(string fileName, ContentManager content)
        {
            States = new List<RatState>();
            this.topFramesAnimation = int.Parse(ConfigurationManager.AppSettings["topFramesPerSpriteAnimation"]);
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                XDocument doc = XDocument.Load(stream);
                var states = doc.Descendants(typeof(RatState).Name);
                foreach (var element in states)
                {
                    States.Add(new RatState(element, content, this.topFramesAnimation));
                }
            }
        }

        public RatState GetNewState(Keys keyPressed, RatState actualState)
        {
            var wasMoving = actualState.moving;
            var previousHorizontalDirection = actualState.direction == Direction.right || actualState.direction == Direction.left ?
                actualState.direction : actualState.previousHorizontalDirection;
            var posibleStates = this.States.Where(state => state.keyToActivate == keyPressed).ToList();
            if (posibleStates.Count == 1)
            {
                actualState = posibleStates[0];
            }
            else if(posibleStates.Count > 1)
            {
                actualState = posibleStates.Where(s => s.direction == previousHorizontalDirection).First();
            }
            actualState.moving = keyPressed != Keys.None;
            actualState.wasMoving = wasMoving;
            actualState.previousHorizontalDirection = previousHorizontalDirection;
            return actualState;
        }
    }
}
