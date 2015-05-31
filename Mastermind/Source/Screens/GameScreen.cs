using System;
using Microsoft.SPOT;

using Mastermind.Widgets;
using Mastermind.Modules;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Screens
{
    class GameScreen
    {
        /**
         * -TODO- Implement. 
         * This is the screen where the actual guessing game happens
         * This is same for both 1 & 2 player cases
         */
        
        Controller mController;

        public GameScreen(Controller mController)
        {
            this.mController = mController;
        }

        public void Init()
        {
            RectangleView sampleRect = new RectangleView(0, 0,
                mController.GetDisplay().SimpleGraphics.Width,
                mController.GetDisplay().SimpleGraphics.Height, 
                GT.Color.Green, mController.GetDisplay());

            TextView sampleText = new TextView(
                mController.GetDisplay(),
                "Game Screen",
                mController.GetDisplay().SimpleGraphics.Width/2 - 50,
                mController.GetDisplay().SimpleGraphics.Height/2 - 10);

            mController.SetJoystickCallback(JoystickEvent);
        }

        void JoystickEvent(int action)
        {
            mController.ChangeScreen(Controller.SCREEN_START);
        }
    }
}
