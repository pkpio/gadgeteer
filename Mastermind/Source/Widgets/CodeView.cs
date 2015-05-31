using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
    class CodeView
    {
        private static GT.Color COLOR_0 = GT.Color.Blue;
        private static GT.Color COLOR_1 = GT.Color.Magenta;
        private static GT.Color COLOR_2 = GT.Color.Green;
        private static GT.Color COLOR_3 = GT.Color.Red;
        private static GT.Color COLOR_4 = GT.Color.Yellow;
        private static GT.Color COLOR_5 = GT.Color.Gray;

        private int posX = 0;
        private int posY = 0;
        private int radius = 5;
        private int outlineThickness = 1;
        private GT.Color outlineSelectColor = GT.Color.White;
        private GT.Color outlineUnselectColor = GT.Color.Black;
        private GT.Color defaultFillColor = COLOR_0;
        private int selected = 0;
        private int[] values  = new int[4];

        // Widgets
        EllipseView[] bubble = new EllipseView[4];
       
        private DisplayTE35 mDisplay;
        
        public CodeView(int posX, int posY,DisplayTE35 display)
        {
           
            this.posX = posX;
            this.posY = posY;
            this.mDisplay = display;
            values = new int[]{0, 0, 0, 0};
            bubble[0] = new EllipseView(posX, posY, radius, radius,
                                defaultFillColor, outlineSelectColor, outlineThickness, mDisplay);
            bubble[1] = new EllipseView(posX + 15, posY, radius, radius,
                                defaultFillColor, outlineUnselectColor, outlineThickness, mDisplay);
            bubble[2] = new EllipseView(posX + 30, posY, radius, radius,
                                defaultFillColor, outlineUnselectColor, outlineThickness, mDisplay);
            bubble[3] = new EllipseView(posX + 45, posY, radius, radius,
                                defaultFillColor, outlineUnselectColor, outlineThickness, mDisplay);
        }

        public  void MoveSelectionLeft()
        {
            bubble[selected].SetOutlineColor(outlineUnselectColor);
            if (selected == 0){
                selected = 3;
            }
            else selected --;
            bubble[selected].SetOutlineColor(outlineSelectColor);
        }
        public void MoveSelectionRight()
        {
            bubble[selected].SetOutlineColor(outlineUnselectColor);
            if (selected == 3)
            {
                selected = 0;
            }
            else selected++;
            bubble[selected].SetOutlineColor(outlineSelectColor);
        }
        public void IncreaseValue()
        {
            if (values[selected] == 5)
            {
                values[selected] = 0;
            }
            else values[selected] = values[selected] + 1;

            bubble[selected].SetFillColor(MapValueToColor(values[selected]));

        }
        public void DecreaseValue()
        {
            if (values[selected] == 0)
            {
                values[selected] = 5;
            }
            else values[selected] = values[selected] - 1;

            bubble[selected].SetFillColor(MapValueToColor(values[selected]));

        }
        public void NoSelect()
        {

        }

        private GT.Color MapValueToColor(int value)
        {
            switch (value)
            {
                case 0:
                    return COLOR_0;
                case 1:
                    return COLOR_1;
                case 2:
                    return COLOR_2;
                case 3:
                    return COLOR_3;
                case 4:
                    return COLOR_4;
                case 5:
                    return COLOR_5;
            }
            return GT.Color.Blue;
        }
    }
}
