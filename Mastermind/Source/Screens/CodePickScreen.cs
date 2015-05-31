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
        CodeView code;

        public CodePickScreen(Controller mController)
        {
            this.mController = mController;
        }

        public void Init()
        {
            /* RectangleView sampleRect = new RectangleView(0, 0,
                 mController.GetDisplay().SimpleGraphics.Width,
                 mController.GetDisplay().SimpleGraphics.Height, 
                 GT.Color.Orange, mController.GetDisplay());
             */
            TextView Text = new TextView(
                mController.GetDisplay(),
                "Please Generate a Code",
                10,
                10);
             code = new CodeView(20, 50, mController.GetDisplay());
            

            mController.SetJoystickCallback(JoystickEvent);
        }

        void JoystickEvent(int action)
        {
            
            switch (action)
            {
                case JoystickHandler.JS_MOVE_DOWN:
                    code.DecreaseValue();
                    break;

                case JoystickHandler.JS_MOVE_UP:
                    code.IncreaseValue();
                    break;

                case JoystickHandler.JS_MOVE_LEFT:
                    code.MoveSelectionLeft();
                    break;

                case JoystickHandler.JS_MOVE_RIGHT:
                    code.MoveSelectionRight();
                    break;

              /*  case JoystickHandler.JS_RELEASE:
                    if (modeChoice == MODE_ONEPLAY)
                        // -TODO- Generate a random code
                        mController.ChangeScreen(Controller.SCREEN_GAME_PLAY);
                    else if (modeChoice == MODE_TWOPLAY)
                        mController.ChangeScreen(Controller.SCREEN_CODE_PICKER);
                    break;*/
            }
        }
    }
}
