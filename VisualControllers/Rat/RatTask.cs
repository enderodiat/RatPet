using Microsoft.Xna.Framework;
using RatPet.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static RatPet.Helpers.Enums;

namespace RatPet.VisualControllers.Rat
{
    public class RatTask
    {
        private Rectangle destination;
        private List<Direction> directionPriority;

        public RatTask(Rectangle destination, RatState actualState, Vector2 origin)
        {
            this.destination = destination;
            this.directionPriority = new List<Direction>();
            if(Math.Abs(destination.X - origin.X) > Math.Abs(destination.Y - origin.Y))
            {
                loadHorizontalDirection(destination, origin);
                loadVerticalDirection(destination, origin);
            }
            else
            {
                loadVerticalDirection(destination, origin);
                loadHorizontalDirection(destination, origin);
            }
        }

        private void loadVerticalDirection(Rectangle destination, Vector2 origin)
        {
            if (origin.Y < destination.Y)
            {
                directionPriority.Add(Direction.down);
            }
            else if (origin.Y > destination.Y)
            {
                directionPriority.Add(Direction.up);
            }
        }

        private void loadHorizontalDirection(Rectangle destination, Vector2 origin)
        {
            if (origin.X < destination.X)
            {
                directionPriority.Add(Direction.right);
            }
            else if (origin.X > destination.X)
            {
                directionPriority.Add(Direction.left);
            }
        }

        public void collision()
        {
            if (directionPriority.Count > 0)
            {
                directionPriority.RemoveAt(0);
            }
        }

        public RatStateID? GetNextIDRatState(Vector2 position)
        {
            Direction? actualDirection = directionPriority.Count > 0 ? directionPriority.First() : null;
            if (!actualDirection.HasValue || destination.Contains(position))
            {
                return null;
            }
            else
            {
                if(actualDirection.Value == Direction.left && position.X > destination.Center.X)
                {
                    return RatStateID.goingLeft;
                }
                else if(actualDirection.Value == Direction.right && position.X < destination.Center.X)
                {
                    return RatStateID.goingRight;
                }
                else if(actualDirection.Value == Direction.up && position.Y > destination.Center.Y)
                {
                    return RatStateID.goingUp;
                }
                else if(actualDirection.Value == Direction.down && position.Y < destination.Center.Y)
                {
                    return RatStateID.goingDown;
                }
                directionPriority.RemoveAt(0);
                return GetNextIDRatState(position);
            }
        }
    }
}
