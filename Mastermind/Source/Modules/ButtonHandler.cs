using System;
using System.Threading;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Modules
{
    class ButtonHandler
    {
        /**
         * Possible actions for button
         */
        public const int BTN_PRESS = 1;
        public const int BTN_RELEASE = 2;
        public const int BTN_LONGPRESS = 3;

        private const int LONGPRESS_TIME = 1000; // Press 'at least' this long to interpret as long press

        /**
         * Button event handler delegate / type
         */
        public delegate void BtnEventCallback(int btnEvent);
        event BtnEventCallback eventCallback;

        Button mButton;
        Thread mBtnThread;
        Boolean longPressing = false;

        public ButtonHandler(Button mBtn)
        {
            this.mButton = mBtn;
        }

        /**
         * This callback will be called when ever a button event occurs
         */
        public void SetCallback(BtnEventCallback btnEventHandle)
        {
            this.eventCallback = btnEventHandle;
        }

        /**
         * Called by gadgeteer when Button state changes
         */
        void ButtonStateChanged(Button sender, Button.ButtonState state)
        {
            if (state == Button.ButtonState.Pressed)
            {
                SendEventToCallback(BTN_PRESS);
            }
            else if (state == Button.ButtonState.Released)
            {
                this.longPressing = false;
                SendEventToCallback(BTN_RELEASE);
            }
        }

        private void SendEventToCallback(int action)
        {
            if (eventCallback != null)
                eventCallback(action);
        }

        /**
         * Start handling Button long press events.
         */
        void StartButton()
        {
            while (true)
            {
                longPressing = mButton.Pressed;
                Thread.Sleep(LONGPRESS_TIME);
                if (longPressing && mButton.Pressed)
                {
                    SendEventToCallback(BTN_LONGPRESS);
                    longPressing = false;
                }
            }
        }

        public void Start()
        {
            mButton.ButtonReleased += ButtonStateChanged;
            mButton.ButtonPressed += ButtonStateChanged;
            mBtnThread = new Thread(StartButton);
            mBtnThread.Start();
        }

        public void Stop()
        {
            mBtnThread.Suspend();
        }
    }
}
