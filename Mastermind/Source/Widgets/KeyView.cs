using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
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
            for (int i=0; i < 4; i++)
            {
                if(i<perfect)
                {
                    values[i] = COLOR_0;
                }
                else if(i<(perfect+correct))
                {
                    values[i] = COLOR_1;
                } else
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
