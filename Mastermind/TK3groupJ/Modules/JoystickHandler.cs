using System;
using System.Threading;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Modules
{
    class JoystickHandler
    {
        /**
         * Possible actions with joystick. We are limiting the choices here.
         */
        public const int JS_MOVE_LEFT = 1;
        public const int JS_MOVE_RIGHT = 2;
        public const int JS_MOVE_UP = 3;
        public const int JS_MOVE_DOWN = 4;
        public const int JS_RELEASE = 5;

        private const int SENSITIVITY = 200; // Lower values imply high sensitivity

        /**
         * Joystick event handler delegate / type
         */
        public delegate void JsEventCallback(int jsEvent);
        event JsEventCallback eventCallback;

        Joystick mJoystick;
        Thread mJsThread;

        public JoystickHandler(Joystick joystick)
        {
            this.mJoystick = joystick;
        }

        /**
         * This callback will be called when ever a joystick event occurs
         */
        public void SetCallback(JsEventCallback jsEventHandle)
        {
            this.eventCallback = jsEventHandle;
        }

        /**
         * Start handling joystick. This should be done from a new thread.
         */
        void StartJoystick()
        {
            while (true)
            {
                double posX = mJoystick.GetPosition().X;
                double posY = mJoystick.GetPosition().Y;

                // X events
                if (posX < -0.5)
                    SendEventToCallback(JS_MOVE_LEFT);
                else if (posX > 0.5)
                    SendEventToCallback(JS_MOVE_RIGHT);

                // Y events
                if (posY < -0.5)
                    SendEventToCallback(JS_MOVE_DOWN);
                else if (posY > 0.5)
                    SendEventToCallback(JS_MOVE_UP);

                Thread.Sleep(SENSITIVITY);
            }
        }

        void JsRelease(Joystick sender, Joystick.ButtonState state)
        {
            SendEventToCallback(JS_RELEASE);
        }

        private void SendEventToCallback(int action)
        {
            if(eventCallback != null)
                eventCallback(action);
        }

        public void Start()
        {
            mJoystick.JoystickReleased += JsRelease;
            mJsThread = new Thread(StartJoystick);
            mJsThread.Start();
        }

        public void Stop()
        {
            mJsThread.Suspend();
        }

    }
}
