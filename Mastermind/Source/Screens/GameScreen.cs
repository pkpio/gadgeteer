using System;
using System.Threading;
using Microsoft.SPOT;

using Mastermind.Widgets;
using Mastermind.Modules;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Screens
{
    /**
     *In this screen the player enters a code to guess the correct one. When he guesses right, he wins the game.
     *If he doesn't guess the code after the max number of rounds he loses the game.
     */
    class GameScreen
    {
        //Screen values
        private static int NUM_ROUNDS = 10;
        private int currentRound = 1;
        private Boolean gameOn = true; // is set to false once the game is over

        //Widget sizes

        //Widget positions
        private const int titlePosX = 10;
        private const int titlePosY = 10;
        private const int codeViewPosX = 20;
        private const int codeViewPosY = 30;
        private const int keyPosX = 90;
        private const int codeWasPosX = 180;
        private const int codeWasPosY = 100;

        private int codeSpacer = 18;

        //Widget strings
        private String guessString = "Please enter a guess. ";
        private String roundString1 = "(Round ";
        private String roundString2 = "/" + NUM_ROUNDS + ")";
        private String lostString = "You lost ";
        private String wonString = "You won! ";
        private String codeWasString = "The code was";

        //Widgets
        private TextView screenTitle;
        Controller mController;
        CodeView mCodeView;

        Thread flashThread;

        public GameScreen(Controller mController)
        {
            this.mController = mController;
        }

        public void Init()
        {
            // Init widgets
            screenTitle = new TextView(mController.GetDisplay(), ComposeTitle(), titlePosX, titlePosY);
            mCodeView = new CodeView(codeViewPosX, currentRound * codeSpacer + codeViewPosY, mController.GetDisplay());

            // Init joystick
            mController.SetJoystickCallback(JoystickEvent);
            // Init button
            mController.SetButtonCallback(ButtonEvent);
        }

        /**
         * When the last round was played, the player did not guess right, so the player loses and the game is ended.
         * Otherwise the round number gets incremented and new code bubbles are displayed.
         */
        void IncreaseRound()
        {
            if(currentRound<NUM_ROUNDS)
            {
                currentRound++;
                ClearTitle();
                screenTitle.SetText(ComposeTitle());
                mCodeView.ShowSelector(false);
                int[] lastGuess = mCodeView.GetValues();
                mCodeView = new CodeView(codeViewPosX, currentRound * codeSpacer + codeViewPosY, mController.GetDisplay());
                mCodeView.SetValues(lastGuess);
            }
            else
            {
                EndGame(false);
            }
        }

        /**
         * Displays if player won or lost the game, and what the correct code was
         * ends the game
         */
        private void EndGame(Boolean won)
        {
            gameOn = false;
            ClearTitle();
            mCodeView.ShowSelector(false);
            mController.SetJoystickCallback(null);
            if(won)
            {
                screenTitle.SetText(wonString + roundString1 + currentRound + roundString2);
                screenTitle.SetColor(GT.Color.FromRGB(0, 204, 0));
                flashThread = new Thread(() => mController.FlashLEDLoop(0));
                flashThread.Start();
            }
            else
            {
                screenTitle.SetText(lostString + roundString1 + currentRound + roundString2);
                screenTitle.SetColor(GT.Color.Red);
                flashThread = new Thread(() => mController.FlashLEDLoop(6));
                flashThread.Start();
            }
            new TextView(mController.GetDisplay(), codeWasString, codeWasPosX, codeWasPosY);
            CodeView sol = new CodeView(codeWasPosX + 10, codeWasPosY + 30, mController.GetDisplay());
            sol.SetValues(mController.GetCode());
            sol.ShowSelector(false);
        }

        /**
         * returns a composition of the title, current round number and right punctuation
         */
        private String ComposeTitle()
        {
            return guessString + roundString1 + currentRound + roundString2;
        }

        /**
         * covers the title with a black rectangle, so that a new title can be displayed
         */
        private void ClearTitle()
        {
            new RectangleView(0, 0, mController.GetDisplay().Width, codeViewPosY, GT.Color.Black, mController.GetDisplay());
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

        /**
         * When button is pressed while game is running (gameOn), a guess is submitted. This guess gets compared to solution.
         * The result of the comparison is displayed. If gues is completely correct the game ends.
         * If game is over (gameOn is false) and the button is pressed, we change to the startscreen.
         */
        void ButtonEvent(int action)
        {
            if (action == ButtonHandler.BTN_RELEASE)
            {
                if (gameOn)
                {
                    int[] compResult = mCodeView.CompareGuess(mController.GetCode());
                    new KeyView(keyPosX, currentRound * codeSpacer + codeViewPosY, compResult[0], compResult[1], mController.GetDisplay());
                    if (compResult[0] == 4)
                    {
                        EndGame(true);
                    }
                    else
                    {
                        IncreaseRound();
                    }                    
                }
                else
                {
                    flashThread.Abort();
                    mController.ChangeScreen(Controller.SCREEN_START);
                }
            }
        }
    }
}
