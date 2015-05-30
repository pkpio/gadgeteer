using System;
using System.Threading;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind
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
        private const int SENSITIVITY = 200; // Lower values imply high sensitivity

        /**
         * Joystick event handler delegate / type
         */
        public delegate void JsEventHandler(int jsEvent);
        event JsEventHandler jsEventCallback;

        Joystick mJoystick;
        Thread mJsThread;

        public JoystickHandler(Joystick joystick)
        {
            this.mJoystick = joystick;
        }

        /**
         * This callback will be called when ever a joystick event occurs
         */
        public void SetCallback(JsEventHandler jsEventHandle)
        {
            this.jsEventCallback = jsEventHandle;
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

                if (jsEventCallback != null)
                {
                    // X events
                    if (posX < -0.5)
                        jsEventCallback(JS_MOVE_LEFT);
                    else if (posX > 0.5)
                        jsEventCallback(JS_MOVE_RIGHT);

                    // Y events
                    if (posY < -0.5)
                        jsEventCallback(JS_MOVE_DOWN);
                    else if (posY > 0.5)
                        jsEventCallback(JS_MOVE_UP);
                }

                Thread.Sleep(SENSITIVITY);
            }
        }

        public void Start()
        {
            mJsThread = new Thread(StartJoystick);
            mJsThread.Start();
        }

        public void Stop()
        {
            mJsThread.Suspend();
        }

    }
}
