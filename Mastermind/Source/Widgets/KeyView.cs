using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
    /**
     * Represents the feedback pins of the game.
     * Used to inform the player on for how many code bubble he chose
     * 1. the right color and position and
     * 2. the right color but not the right position.
     */ 
    class KeyView
    {
        private static GT.Color COLOR_0 = GT.Color.Red; 
        private static GT.Color COLOR_1 = GT.Color.White;
        private static GT.Color COLOR_2 = GT.Color.Gray;

        private int posX = 0;
        private int posY = 0;
        private int radius = 3;
        private int outlineThickness = 1;
        private GT.Color outlineColor = GT.Color.DarkGray;
        private GT.Color[] values = new GT.Color[4];

        // Widgets
        EllipseView[] pins = new EllipseView[4];
       
        private DisplayTE35 mDisplay;
        
        public KeyView(int posX, int posY, int perfect, int correct, DisplayTE35 display)
        {
            //set color for each pin
            for (int i=0; i < 4; i++)
            {
                //pins that represent amount of code bubbles with correct color and position
                if(i<perfect) 
                {
                    values[i] = COLOR_0;
                }
                //pins that represent amount of code bubbles with correct color only
                else if(i<(perfect+correct))
                {
                    values[i] = COLOR_1; 
                }
                //the pins not needed are not shown
                else
                {
                    values[i] = COLOR_2;
                }
            }
            this.posX = posX;
            this.posY = posY;
            this.mDisplay = display;
            pins[0] = new EllipseView(posX, posY, radius, radius,
                                values[0], outlineColor, outlineThickness, mDisplay);
            pins[1] = new EllipseView(posX + 13, posY, radius, radius,
                                values[1], outlineColor, outlineThickness, mDisplay);
            pins[2] = new EllipseView(posX + 26, posY, radius, radius,
                                values[2], outlineColor, outlineThickness, mDisplay);
            pins[3] = new EllipseView(posX + 39, posY, radius, radius,
                                values[3], outlineColor, outlineThickness, mDisplay);
        }
    }
}
