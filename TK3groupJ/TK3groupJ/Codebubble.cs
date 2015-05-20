using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace TK3groupJ
{
    public class Codebubble
    {
        int x;
        int y;
        int highlighted;
        int[] colors;
        GT.Color[] colorRange = new GT.Color[] { GT.Color.Blue, GT.Color.Magenta, GT.Color.Green, GT.Color.Red, GT.Color.Cyan, GT.Color.Yellow };
        DisplayTE35 dis;

        public Codebubble(int x, int y, int highlighted, int color1, int color2, int color3, int color4, DisplayTE35 dis)
        {
            this.x = x;
            this.y = y;
            this.highlighted = highlighted;
            colors = new int[] { color1, color2, color3, color4 };
            this.dis = dis;
        }

        public void changeHighlight(int pos)
        {
            highlighted = pos;
            draw();
        }

        public void changeColor(int pos, Boolean increase)
        {
            if (increase)
            {
                colors[pos] = System.Math.Min(colors[pos]+1, 5);
            }
            else
            {
                colors[pos] = System.Math.Max(colors[pos] - 1, 0);
            }
            draw();
        }

        public void draw()
        {
            if(highlighted == 0)
            {
                dis.SimpleGraphics.DisplayEllipse(GT.Color.Red, 1, colorRange[colors[0]], x, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[1]], x + 15, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[2]], x + 30, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[3]], x + 45, y, 5, 5);
            }
            else if(highlighted == 1)
            {
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[0]], x, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.Red, 1, colorRange[colors[1]], x + 15, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[2]], x + 30, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[3]], x + 45, y, 5, 5);
            } 
            else if(highlighted == 2)
            {
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[0]], x, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[1]], x + 15, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.Red, 1, colorRange[colors[2]], x + 30, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[3]], x + 45, y, 5, 5);
            }
            else if (highlighted == 3)
            {
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[0]], x, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[1]], x + 15, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[2]], x + 30, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.Red, 1, colorRange[colors[3]], x + 45, y, 5, 5);
            }
            else
            {
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[0]], x, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[1]], x + 15, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[2]], x + 30, y, 5, 5);
                dis.SimpleGraphics.DisplayEllipse(GT.Color.White, 1, colorRange[colors[3]], x + 45, y, 5, 5);
            }
        }
    }
}
