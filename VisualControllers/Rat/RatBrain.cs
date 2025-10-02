using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers.Rat
{
    public class RatBrain
    {
        public List<RatState> States { get; set; }
        private RatTask actualTask = null;

        public RatBrain(List<RatState> states)
        {
            this.States = states;
        }

        public RatState GetNewState(Vector2 position, Keys keyPressed, Rectangle? mousePositionPressed, RatState actualState)
        {
            var wasMoving = actualState.moving;
            var previousHorizontalDirection = actualState.direction == Direction.right || actualState.direction == Direction.left ?
                actualState.direction : actualState.previousHorizontalDirection;
            if (keyPressed != Keys.None) 
            {
                this.actualTask = null;
                actualState = getKeyState(keyPressed, actualState, previousHorizontalDirection);
            }
            else
            {
                actualState = getTaskState(position, actualState, mousePositionPressed);
            }
            actualState.wasMoving = wasMoving;
            actualState.previousHorizontalDirection = previousHorizontalDirection;
            return actualState;
        }

        private RatState getKeyState(Keys keyPressed, RatState actualState, Direction previousHorizontalDirection)
        {
            var posibleStates = States.Where(state => state.keyToActivate == keyPressed).ToList();
            if (posibleStates.Count == 1)
            {
                actualState = posibleStates[0];
            }
            else if (posibleStates.Count > 1)
            {
                actualState = posibleStates.Where(s => s.direction == previousHorizontalDirection).First();
            }
            actualState.moving = true;
            return actualState;
        }

        public RatState getTaskState(Vector2 actualPosition, RatState actualState, Rectangle? rectPressed = null)
        {
            if (rectPressed.HasValue)
            {
                this.actualTask = new RatTask(rectPressed.Value, actualState, actualPosition);
            }
            else if (this.actualTask == null && !rectPressed.HasValue) 
            {
                actualState.moving = false;
                return actualState;
            }
            var stateID = this.actualTask.GetNextIDRatState(actualPosition);
            if (stateID.HasValue)
            {
                var state = States.Where(state => state.numState == stateID).First();
                state.moving = true;
                return state;
            }
            else
            {
                this.actualTask = null;
                actualState.moving = false;
                return actualState;
            }
        }

        public void Collision()
        {
            if (this.actualTask != null)
            {
                this.actualTask.collision();
            }
        }
    }
}
