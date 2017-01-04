using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WeaveSilk
{
    public static class Line
    {
        private static Texture2D t;
        public static void DrawLine(GraphicsDevice device, SpriteBatch sb, Vector2 v1, Vector2 v2, int bold = 1) 
            => DrawLine(device, sb, v1, v2, Color.Black, bold);
        public static void DrawLine(GraphicsDevice device, SpriteBatch sb, Vector2 v1, Vector2 v2, Color color, int bold = 1)
        {
            if (t == null)
            {
                t = new Texture2D(device, 1, 1);
                t.SetData<Color>(
                    new Color[] { Color.White });// fill the texture with white
            }
            Vector2 edge = v2 - v1;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)v1.X,
                    (int)v1.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    bold), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }
    }
}
