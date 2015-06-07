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
     * This screen allows the player to select the player mode (1 player or 2 player) at the beginning of the game.
     */ 
    class StartScreen
    {
        Controller mController;

        // Widget sizes
        private int boxWidth = 300;
        private int boxHeight = 50;

        // Widget positions
        private int titleX = 10;
        private int titleY = 10;
        private int onePlayX = 10;
        private int onePlayY = 40;
        private int twoPlayX = 10;
        private int twoPlayY = 110;
        private int onePlayTextX = 20;
        private int onePlayTextY = 60;
        private int twoPlayTextX = 20;
        private int twoPlayTextY = 130;

        // Widget strings
        private String titleString = "Select player mode";
        private String onePlayString = "1 Player";
        private String twoPlayString = "2 Players";

        // Widgets
        private TextView screenTitle;
        private RectangleView onePlayerBox;
        private RectangleView twoPlayerBox;
        private TextView onePlayText;
        private TextView twoPlayText;

        // Screen values
        private int MODE_ONEPLAY = 0;
        private int MODE_TWOPLAY = 1;
        private int modeChoice = 0;

        public StartScreen(Controller mController)
        {
            this.mController = mController;
        }

        /**
         * This method will be called when a screen is launced.
         */
        public void Init()
        {
            // Init widgets
            screenTitle = new TextView(mController.GetDisplay(), titleString, titleX, titleY);
            onePlayerBox = new RectangleView(onePlayX, onePlayY, boxWidth, boxHeight, 
                                    GT.Color.Red, mController.GetDisplay());
            twoPlayerBox = new RectangleView(twoPlayX, twoPlayY, boxWidth, boxHeight,
                                    GT.Color.Gray, mController.GetDisplay());
            onePlayText = new TextView(mController.GetDisplay(), onePlayString, onePlayTextX, onePlayTextY);
            twoPlayText = new TextView(mController.GetDisplay(), twoPlayString, twoPlayTextX, twoPlayTextY);

            // Init joystick
            mController.SetJoystickCallback(JoystickEvent);
        }

        /**
         * Using the joystick, the user can change the player mode and start the game. 
         * Moving the joystick up or down selects the player mode (1 player or 2 player). 
         * Releasing the joystick starts the game with the chosen game mode.
         */ 
        void JoystickEvent(int action)
        {
            switch (action)
            {
                case JoystickHandler.JS_MOVE_DOWN:
                    if (modeChoice == MODE_ONEPLAY)
                    {
                        modeChoice = MODE_TWOPLAY;
                        RefreshSelection();
                    } 
                    break;

                case JoystickHandler.JS_MOVE_UP:
                    if (modeChoice == MODE_TWOPLAY)
                    {
                        modeChoice = MODE_ONEPLAY;
                        RefreshSelection();
                    }
                    break;
                
                case JoystickHandler.JS_RELEASE:
                    if (modeChoice == MODE_ONEPLAY)
                    {
                        GenerateCodeForPlayer();
                        mController.ChangeScreen(Controller.SCREEN_GAME_PLAY);
                    }
                    else if (modeChoice == MODE_TWOPLAY)
                        mController.ChangeScreen(Controller.SCREEN_CODE_PICKER);
                    break;
            }
        }

        /**
         * The selected player mode is shown in red, 
         * the other one in gray.
         */ 
        void RefreshSelection()
        {
            if (modeChoice == MODE_ONEPLAY)
            {
                onePlayerBox.SetFillColor(GT.Color.Red);
                twoPlayerBox.SetFillColor(GT.Color.Gray);
            }
            else if (modeChoice == MODE_TWOPLAY)
            {
                onePlayerBox.SetFillColor(GT.Color.Gray);
                twoPlayerBox.SetFillColor(GT.Color.Red);
            }

            // Redraw text
            onePlayText.Draw();
            twoPlayText.Draw();
        }

        /**
         * Generates a random code and sets it as game code.
         */ 
        void GenerateCodeForPlayer()
        {
            Random rnd = new Random();
            int[] code = new int[4];
            for (int i = 0; i < 4; i++)
                code[i] = rnd.Next(6);

            // Set this as game code
            mController.GameCode = code;
        }
    }
}
