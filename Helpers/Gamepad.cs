using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RatPet.VisualControllers;
using RatPet.VisualControllers.Rat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatPet.Helpers
{
    public class Gamepad
    {
        private int clickPadding;
        private ButtonState previousMouseState;
        private Dictionary<Keys, int> simultaneousKeys;

        public Gamepad(Parameters parameters)
        {
            this.clickPadding = parameters.clickMousePadding;
            this.previousMouseState = ButtonState.Released;
            this.simultaneousKeys = new Dictionary<Keys, int>();
        }

        public Rectangle? GetValidMousePositionPressed(Visual container)
        {
            var mouseState = Mouse.GetState().LeftButton;
            if (mouseState == ButtonState.Pressed && this.previousMouseState == ButtonState.Released)
            {
                this.previousMouseState = mouseState;
                Vector2 mousePositionPressed = Mouse.GetState().Position.ToVector2();
                if (container.Rectangle.Contains(mousePositionPressed))
                {
                    return new Rectangle((int)(mousePositionPressed.X - this.clickPadding / 2),
                        (int)(mousePositionPressed.Y - this.clickPadding / 2),
                        this.clickPadding,
                        this.clickPadding);
                }
                else
                {
                    return null;
                }
            }
            this.previousMouseState = mouseState;
            return null;
        }

        public Keys GetAllowedKeyPressed(List<RatState> states)
        {
            var allowedKeys = states.Select(state => state.keyToActivate).ToList();
            var keysPressed = allowedKeys.Intersect(Keyboard.GetState().GetPressedKeys().ToList()).ToList();
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
