namespace RatPet.Helpers
{
    public static class Enums
    {
        public enum RatStateID
        {
            goingRight,
            goingLeft,
            goingUp,
            goingDown,
            idleRight,
            idleLeft
        }
        public enum Direction
        {
            right,
            up,
            left,
            down
        }

        public enum CheeseStateID
        {
            falling,
            chilling,
            going,
            deleted
        }
    }
}
