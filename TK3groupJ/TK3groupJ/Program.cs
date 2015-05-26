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
    public partial class Program
    {
        Boolean twoPlayers;
        Startscreen startscreen;
        CodegeneratorScreen codegeneratorscreen;
        Thread currentThread;
        int[] correctCode = new int[4];


        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
            /*Codebubble c = new Codebubble(10, 30, GT.Color.Green, GT.Color.Magenta, GT.Color.Red, GT.Color.Blue, displayTE35);
            c.draw();*/

            startscreen = new Startscreen(displayTE35);
            currentThread = new Thread(startscreenLoop);
            currentThread.Start();
            //ModusScreen getModus = new ModusScreen(displayTE35, joystick);
            // CodegeneratorScreen genScreen = new CodegeneratorScreen(displayTE35, joystick);

        }

        void startscreenLoop()
        {
            double posY;
            joystick.JoystickReleased += startscreen_JoystickReleased;

            while (true)
            {
                posY = joystick.GetPosition().Y;

                if ((twoPlayers && posY > 0.5) || (!twoPlayers && posY < -0.5))
                {
                    twoPlayers = !twoPlayers;
                    startscreen.changeSelectedMode(twoPlayers);
                }
                Thread.Sleep(80);
            }
        }

        void codegeneratorLoop()
        {
            double posX;
            double posY;


            joystick.JoystickReleased += codeGeneratorScreen_JoystickReleased;

            while (true)
            {
                posX = joystick.GetPosition().X;
                posY = joystick.GetPosition().Y;

                if (posX > 0.5)
                {
                    codegeneratorscreen.moveRight();

                }
                else if (posX < -0.5)
                {
                    codegeneratorscreen.moveLeft();
                }

                if (posY > 0.5)
                {
                    codegeneratorscreen.moveUp();
                }
                else if (posY < -0.5)
                {
                    codegeneratorscreen.moveDown();
                }

                Thread.Sleep(80);
            }
        }

        void startscreen_JoystickReleased(Joystick sender, Joystick.ButtonState state)
        {
            if (twoPlayers)
            {
                displayTE35.SimpleGraphics.Clear();
                currentThread.Abort();
                codegeneratorscreen = new CodegeneratorScreen(displayTE35);
                currentThread = new Thread(codegeneratorLoop);
                currentThread.Start();
            }
            else
            {
                correctCode = generateCode();
                //TODO: create gaming screen
            }
            joystick.JoystickReleased -= startscreen_JoystickReleased;
        }

        void codeGeneratorScreen_JoystickReleased(Joystick sender, Joystick.ButtonState state)
        {
            correctCode = codegeneratorscreen.getCode();
            displayTE35.SimpleGraphics.Clear();
            currentThread.Abort();
            //TODO: create gaming screen
            joystick.JoystickReleased -= codeGeneratorScreen_JoystickReleased;
        }

        int[] generateCode()
        {
            Random rnd = new Random();
            int[] code = new int[4];
            for (int i = 0; i < 4; i++)
            {
                code[i] = rnd.Next(6);
            }
            return code;
        }


    }
}
