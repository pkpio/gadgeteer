using System;
using Microsoft.SPOT;

using Mastermind.Modules;
using Mastermind.Screens;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind
{
    class Controller
    {
        public const int SCREEN_START = 0;
        public const int SCREEN_CODE_PICKER = 1;
        public const int SCREEN_GAME_PLAY = 2;

        private JoystickHandler mJoystickHandler;
        private ButtonHandler mButtonHandler;
        private DisplayTE35 mDisplay;

        public int[] GameCode = new int[4]; // Solution to the game

        public Controller(JoystickHandler jsHandler, ButtonHandler btnHandler, DisplayTE35 display)
        {
            this.mJoystickHandler = jsHandler;
            this.mButtonHandler = btnHandler;
            this.mDisplay = display;
        }

        /**
         * Returns a handle to display module
         */
        public DisplayTE35 GetDisplay()
        {
            return this.mDisplay;
        }

        /**
         * Transfer control to a different screen
         */
        public void ChangeScreen(int screenChoice)
        {
            // Erase display
            mDisplay.SimpleGraphics.Clear();

            // Reset callbacks
            mJoystickHandler.SetCallback(null);
            mButtonHandler.SetCallback(null);

            // Change screen
            switch (screenChoice)
            {
                case SCREEN_START:
                    new StartScreen(this).Init();
                    break;
                case SCREEN_CODE_PICKER:
                    new CodePickScreen(this).Init();
                    break;
                case SCREEN_GAME_PLAY:
                    new GameScreen(this).Init();
                    break;
            }
        }

        /**
         * Set a callback for joystick events
         */
        public void SetJoystickCallback(JoystickHandler.JsEventCallback callback)
        {
            mJoystickHandler.SetCallback(callback);
        }

        /**
         * Set a callback for Button events
         */
        public void SetButtonCallback(ButtonHandler.BtnEventCallback callback)
        {
            mButtonHandler.SetCallback(callback);
        }
    }
}
