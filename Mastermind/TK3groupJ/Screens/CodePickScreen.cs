using System;
using Microsoft.SPOT;

using Mastermind.Widgets;
using Mastermind.Modules;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Screens
{
    // -TODO- Implement. In 2 player mode, this will be the screen where user picks a code
    class CodePickScreen
    {
        Controller mController;

        public CodePickScreen(Controller mController)
        {
            this.mController = mController;
        }

        public void Init()
        {
            RectangleView sampleRect = new RectangleView(0, 0,
                mController.GetDisplay().SimpleGraphics.Width,
                mController.GetDisplay().SimpleGraphics.Height, 
                GT.Color.Orange, mController.GetDisplay());

            TextView sampleText = new TextView(
                mController.GetDisplay(),
                "Code Picker",
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
