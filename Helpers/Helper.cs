using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RatPet.Helpers
{
    public static class Helper
    {
        public static float GetScale(float y, float scale, Rectangle container)
        {
            if(new Parameters().perspective)
            {
                float totalPosition = y - container.Location.Y;
                return 0.6f * (scale / container.Height) * totalPosition + 0.4f * scale;
            } 
            else
            {
                return scale;
            }
        }
    }
}
