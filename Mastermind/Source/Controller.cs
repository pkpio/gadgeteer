using System;
using System.Threading;
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
        private LEDStrip mLEDStrip;
        private DisplayTE35 mDisplay;

        public int[] GameCode = new int[4]; // Solution to the game

        public Controller(JoystickHandler jsHandler, ButtonHandler btnHandler, LEDStrip ledStrip, DisplayTE35 display)
        {
            this.mJoystickHandler = jsHandler;
            this.mButtonHandler = btnHandler;
            this.mLEDStrip = ledStrip;
            this.mDisplay = display;
        }

        /**
         * Returns the LED strip.
         */
        public LEDStrip GetLEDStrip()
        {
            return this.mLEDStrip;
        }

        /**
         * Returns a handle to display module.
         */
        public DisplayTE35 GetDisplay()
        {
            return this.mDisplay;
        }

        /**
         * Returns the solution code.
         */
        public int[] GetCode()
        {
            return this.GameCode;
        }

        /**
         * Flashes the LED with the given number.
         */
        public void FlashLEDLoop(int led)
        {
            while(true)
            {
                mLEDStrip.TurnLedOn(led);
                Thread.Sleep(200);
                mLEDStrip.TurnAllLedsOff();
                Thread.Sleep(200);
            }            
        }

        /**
         * Transfer control to a different screen.
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
         * Set a callback for joystick events.
         */
        public void SetJoystickCallback(JoystickHandler.JsEventCallback callback)
        {
            mJoystickHandler.SetCallback(callback);
        }

        /**
         * Set a callback for Button events.
         */
        public void SetButtonCallback(ButtonHandler.BtnEventCallback callback)
        {
            mButtonHandler.SetCallback(callback);
        }
    }
}
