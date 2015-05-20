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
    class CodegeneratorScreen
    {
        DisplayTE35 dis;
        Joystick jstick;
        int highlight = 0;
        Codebubble bubble;

        public CodegeneratorScreen(DisplayTE35 dis, Joystick jstick)
        {
            this.dis = dis;
            this.jstick = jstick;

            dis.SimpleGraphics.DisplayText("Please generate a code", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 10, 10);
            bubble = new Codebubble(20, 50, highlight, 0,0,0,0, dis);
            bubble.draw();

            Thread JstickThread = new Thread(JstickLoop);
            JstickThread.Start();
        }

        void JstickLoop()
        {
            double posX;
            double posY;

            while (true)
            {
                posX = jstick.GetPosition().X;
                posY = jstick.GetPosition().Y;

                if (posX > 0.5)
                {
                    highlight = System.Math.Min(highlight + 1, 3);
                    bubble.changeHighlight(highlight);

                }
                else if (posX < -0.5)
                {
                    highlight = System.Math.Max(highlight - 1, 0);
                    bubble.changeHighlight(highlight);
                }

                if (posY > 0.5)
                {
                    bubble.changeColor(highlight, false);
                }
                else if (posY < -0.5)
                {
                    bubble.changeColor(highlight, true);
                }

                Thread.Sleep(80);
            }
        }
    }
}
