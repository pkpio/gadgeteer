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

namespace Mastermind
{
    class CodegeneratorScreen
    {
        DisplayTE35 dis;
        int highlight = 0;
        Codebubble bubble;

        public CodegeneratorScreen(DisplayTE35 dis)
        {
            this.dis = dis;

            dis.SimpleGraphics.DisplayText("Please generate a code", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 10, 10);
            bubble = new Codebubble(20, 50, highlight, 0, 0, 0, 0, dis);
            bubble.draw();
        }

        public int[] getCode()
        {
            return bubble.getColors();
        }

        public void moveRight()
        {
            highlight = System.Math.Min(highlight + 1, 3);
            bubble.changeHighlight(highlight);
        }

        public void moveLeft()
        {
            highlight = System.Math.Max(highlight - 1, 0);
            bubble.changeHighlight(highlight);
        }

        public void moveUp()
        {
            bubble.changeColor(highlight, false);
        }

        public void moveDown()
        {
            bubble.changeColor(highlight, true);
        }
    }
}
