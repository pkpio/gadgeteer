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
            selected = (selected == 0) ? 3 : selected - 1;
            bubble[selected].SetOutlineColor(outlineSelectColor);
        }

        public void MoveSelectionRight()
        {
            bubble[selected].SetOutlineColor(outlineUnselectColor);
            selected = (selected == 3) ? 0 : selected + 1;
            bubble[selected].SetOutlineColor(outlineSelectColor);
        }

        /**
         * Enable or disable the showing of a selector on one of the bubbles
         */
        public void ShowSelector(Boolean state)
        {
            if(state)
                bubble[selected].SetOutlineColor(outlineSelectColor);
            else
                bubble[selected].SetOutlineColor(outlineUnselectColor);
        }

        public void IncreaseValue()
        {
            values[selected] = (values[selected] >= 5) ? 0 : values[selected] + 1;
            bubble[selected].SetFillColor(MapValueToColor(values[selected]));
        }

        public void DecreaseValue()
        {
            values[selected] = (values[selected] <= 0) ? 5 : values[selected] - 1;
            bubble[selected].SetFillColor(MapValueToColor(values[selected]));
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

        /**
         * Get the current bubble pattern values
         */
        public int[] GetValues()
        {
            return this.values;
        }

        /**
         * Updates the current bubble values and refresh the colors accordingly
         */
        public void SetValues(int[] newValues)
        {
            // Checks for values size
            if (newValues == null || newValues.Length != 4)
                throw new ArgumentException("Argument to SetValues MUST be an array of size 4");

            // -TODO- Value Max. and Min. checks

            this.values = newValues;
            for (int i = 0; i < values.Length; i++)
                bubble[i].SetFillColor(MapValueToColor(values[i]));
        }

    }
}
