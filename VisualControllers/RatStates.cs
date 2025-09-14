using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers
{
    public class RatStates
    {
        public List<RatState> States { get; set; }

        private int topFramesAnimation;
        private List<Keys> allowedKeys;
        private Dictionary<Keys, int> simultaneousKeys;

        public RatStates(string fileName, ContentManager content, int topFrames) { 
            simultaneousKeys = new Dictionary<Keys, int>();
            States = new List<RatState>();
            topFramesAnimation = topFrames;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                XDocument doc = XDocument.Load(stream);
                var states = doc.Descendants(typeof(RatState).Name);
                foreach (var element in states)
                {
                    States.Add(new RatState(element, content, topFramesAnimation));
                }
            }
            allowedKeys = new List<Keys>();
            allowedKeys.AddRange(States.Select(state => state.keyToActivate).ToList());
        }

        public RatState GetNewState(Keys keyPressed, RatState actualState)
        {
            var wasMoving = actualState.moving;
            var previousHorizontalDirection = actualState.direction == Direction.right || actualState.direction == Direction.left ?
                actualState.direction : actualState.previousHorizontalDirection;
            var previousKeyPressed = actualState.keyToActivate;
            var posibleStates = States.Where(state => state.keyToActivate == keyPressed).ToList();
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

        public Keys GetAllowedKeyPressed(List<Keys> keysPressed)
        {
            keysPressed = allowedKeys.Intersect(keysPressed).ToList();
            var keysUnpressed = allowedKeys.Except(keysPressed).ToList();
            foreach (var key in keysUnpressed)
            {
                simultaneousKeys.Remove(key);
            }
            if (keysPressed.Count == 0) 
            { 
                return Keys.None;
            }
            else
            {
                return getNewerKey(keysPressed);
            }
        }

        private Keys getNewerKey(List<Keys> keysPressed)
        {
            foreach (var key in keysPressed)
            {
                if (simultaneousKeys.ContainsKey(key))
                {
                    simultaneousKeys[key]++;
                }
                else
                {
                    simultaneousKeys.Add(key, 1);
                }
            }
            return simultaneousKeys.OrderBy(k => k.Value).First().Key;
        }
    }
}
