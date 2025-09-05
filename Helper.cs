using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Project1
{
    public static class Helper
    {
        public static float GetScale(float y, Viewport window, float scale, float boxPadding)
        {
            float totalHeight = window.Height - 2 * boxPadding;
            float totalPosition = y - boxPadding;
            return 0.6f * (scale/totalHeight) * totalPosition + 0.4f*scale;
        }

        public static float GetSpeed(float y, Viewport window, float speed, float boxPadding)
        {
            float totalHeight = window.Height - 2 * boxPadding;
            float totalPosition = y - boxPadding;
            return 0.6f * (speed / totalHeight) * totalPosition + 0.4f * speed;
        }
    }
}
