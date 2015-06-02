using System;
using Microsoft.SPOT;

using Mastermind.Widgets;
using Mastermind.Modules;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Screens
{
    /**
     * In 2 player mode, this will be the screen where user picks a code
     */
    class CodePickScreen
    {
        // Widget positions
        private const int titlePosX = 10;
        private const int titlePosY = 10;
        private const int codeViewPosX = 20;
        private const int codeViewPosY = 50;

        // Widget values
        private const String titleString = "Please generate a code";

        Controller mController;
        CodeView mCodeView;

        public CodePickScreen(Controller mController)
        {
            this.mController = mController;
        }

        public void Init()
        {
            TextView Text = new TextView(mController.GetDisplay(), titleString, titlePosX, titlePosY);
            mCodeView = new CodeView(codeViewPosX, codeViewPosY, mController.GetDisplay());
            // Init joystick
            mController.SetJoystickCallback(JoystickEvent);
            // Init button
            mController.SetButtonCallback(ButtonEvent);
        }

        void JoystickEvent(int action)
        {
            
            switch (action)
            {
                case JoystickHandler.JS_MOVE_DOWN:
                    mCodeView.DecreaseValue();
                    break;

                case JoystickHandler.JS_MOVE_UP:
                    mCodeView.IncreaseValue();
                    break;

                case JoystickHandler.JS_MOVE_LEFT:
                    mCodeView.MoveSelectionLeft();
                    break;

                case JoystickHandler.JS_MOVE_RIGHT:
                    mCodeView.MoveSelectionRight();
                    break;
            }
        }

        void ButtonEvent(int action)
        {
            if(action == ButtonHandler.BTN_RELEASE)
            {
                mController.GameCode = this.mCodeView.GetValues();
                mController.ChangeScreen(Controller.SCREEN_GAME_PLAY);
            }
        }
    }
}
