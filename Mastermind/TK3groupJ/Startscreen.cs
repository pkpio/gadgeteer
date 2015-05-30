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

using Mastermind.Widgets;

namespace Mastermind
{
    public class Startscreen
    {
        DisplayTE35 dis;

        public Startscreen(DisplayTE35 dis)
        {
            this.dis = dis;
            //dis.SimpleGraphics.DisplayText("Select player mode", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 10, 10);
            TextView titleView = new TextView(dis, "Select player mode", 10, 10);
            titleView.Draw();
            dis.SimpleGraphics.DisplayRectangle(GT.Color.Red, 2, GT.Color.Gray, 10, 40, 300, 50);
            dis.SimpleGraphics.DisplayRectangle(GT.Color.White, 2, GT.Color.Gray, 10, 110, 300, 50);
            dis.SimpleGraphics.DisplayText("1 Player", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 60);
            dis.SimpleGraphics.DisplayText("2 Players", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 130);

        }

        public void changeSelectedMode(Boolean twoPlayers)
        {
            if (twoPlayers)
            {
                dis.SimpleGraphics.DisplayRectangle(GT.Color.White, 2, GT.Color.Gray, 10, 40, 300, 50);
                dis.SimpleGraphics.DisplayRectangle(GT.Color.Red, 2, GT.Color.Gray, 10, 110, 300, 50);
                dis.SimpleGraphics.DisplayText("1 Player", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 60);
                dis.SimpleGraphics.DisplayText("2 Players", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 130);
            }
            else
            {
                dis.SimpleGraphics.DisplayRectangle(GT.Color.Red, 2, GT.Color.Gray, 10, 40, 300, 50);
                dis.SimpleGraphics.DisplayRectangle(GT.Color.White, 2, GT.Color.Gray, 10, 110, 300, 50);
                dis.SimpleGraphics.DisplayText("1 Player", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 60);
                dis.SimpleGraphics.DisplayText("2 Players", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 130);

            }
        }
    }
}
