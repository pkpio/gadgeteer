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
    public class Startscreen
    {
        DisplayTE35 dis;
        Joystick jstick;
        Boolean twoPlayers = false;

        public Startscreen(DisplayTE35 dis, Joystick jstick)
        {
            this.dis = dis;
            this.jstick = jstick;
            dis.SimpleGraphics.DisplayText("Select player mode", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 10, 10);
            dis.SimpleGraphics.DisplayRectangle(GT.Color.Red, 2, GT.Color.Gray, 10, 40, 300, 50);
            dis.SimpleGraphics.DisplayRectangle(GT.Color.White, 2, GT.Color.Gray, 10, 110, 300, 50);
            dis.SimpleGraphics.DisplayText("1 Player", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 60);
            dis.SimpleGraphics.DisplayText("2 Players", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 130);

            jstick.JoystickReleased += jstick_JoystickReleased;

            Thread JstickThread = new Thread(JstickLoop);
            JstickThread.Start();
        }

        void jstick_JoystickReleased(Joystick sender, Joystick.ButtonState state)
        {
            
        }

        void JstickLoop()
        {
            double posY;

            while(true)
            {
                posY = jstick.GetPosition().Y;

                if(twoPlayers && posY > 0.5)
                {
                    twoPlayers = !twoPlayers;
                    dis.SimpleGraphics.DisplayRectangle(GT.Color.Red, 2, GT.Color.Gray, 10, 40, 300, 50);
                    dis.SimpleGraphics.DisplayRectangle(GT.Color.White, 2, GT.Color.Gray, 10, 110, 300, 50);
                    dis.SimpleGraphics.DisplayText("1 Player", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 60);
                    dis.SimpleGraphics.DisplayText("2 Players", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 130);

                } 
                else if(!twoPlayers && posY < -0.5)
                {
                    twoPlayers = !twoPlayers;
                    dis.SimpleGraphics.DisplayRectangle(GT.Color.White, 2, GT.Color.Gray, 10, 40, 300, 50);
                    dis.SimpleGraphics.DisplayRectangle(GT.Color.Red, 2, GT.Color.Gray, 10, 110, 300, 50);
                    dis.SimpleGraphics.DisplayText("1 Player", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 60);
                    dis.SimpleGraphics.DisplayText("2 Players", Resources.GetFont(Resources.FontResources.NinaB), GT.Color.White, 20, 130);
                }

                Thread.Sleep(80);
            }
        }
    }
}
