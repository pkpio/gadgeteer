using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Mastermind.Modules;
using Mastermind.Widgets;
using Mastermind.Screens;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind
{
    public partial class Program
    {

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            Debug.Print("Program Started");

            // Init Joystick
            JoystickHandler jsHandler = new JoystickHandler(joystick);
            jsHandler.Start();

            // Init Button
            ButtonHandler btnHandler = new ButtonHandler(button);
            btnHandler.Start();
            
            // Setup a controller
            Controller mController = new Controller(jsHandler, btnHandler, displayTE35);

            // Start from startscreen
            mController.ChangeScreen(Controller.SCREEN_START);
        }

    }
}
