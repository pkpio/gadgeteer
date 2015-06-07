using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
    /**
     * Represents the 4 bubbles of the code.
     */ 
    class CodeView
    {
        private static GT.Color COLOR_0 = GT.Color.Blue;
        private static GT.Color COLOR_1 = GT.Color.Magenta;
        private static GT.Color COLOR_2 = GT.Color.FromRGB(0, 204, 0); //Green
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
        private int selected = 0; //the bubble that is currently selected
        private int[] values  = new int[4]; //colors of the bubbles represented by int values

        // Widgets
        EllipseView[] bubble = new EllipseView[4];
       
        private DisplayTE35 mDisplay;
        
        public CodeView(int posX, int posY,DisplayTE35 display)
        {
           this.posX = posX;
            this.posY = posY;
            this.mDisplay = display;
            values = new int[]{0, 0, 0, 0}; // all bubbles have the same color/value at the beginning
            
            //draw the 4 bubbles
            bubble[0] = new EllipseView(posX, posY, radius, radius,
                                defaultFillColor, outlineSelectColor, outlineThickness, mDisplay);
            bubble[1] = new EllipseView(posX + 15, posY, radius, radius,
                                defaultFillColor, outlineUnselectColor, outlineThickness, mDisplay);
            bubble[2] = new EllipseView(posX + 30, posY, radius, radius,
                                defaultFillColor, outlineUnselectColor, outlineThickness, mDisplay);
            bubble[3] = new EllipseView(posX + 45, posY, radius, radius,
                                defaultFillColor, outlineUnselectColor, outlineThickness, mDisplay);
        }

        /**
         * Selects the bubble which is left from the current one.
         * If there is no bubble at the left, the bubble at the right end is selected.
         */
        public  void MoveSelectionLeft()
        {
            bubble[selected].SetOutlineColor(outlineUnselectColor);
            selected = (selected == 0) ? 3 : selected - 1;
            bubble[selected].SetOutlineColor(outlineSelectColor);
        }

        /**
         * Selects the bubble which is right from the current one.
         * If there is no bubble at the right, the bubble at the left end is selected.
         */
        public void MoveSelectionRight()
        {
            bubble[selected].SetOutlineColor(outlineUnselectColor);
            selected = (selected == 3) ? 0 : selected + 1;
            bubble[selected].SetOutlineColor(outlineSelectColor);
        }

        /**
         * Enable or disable the showing of a selector on one of the bubbles.
         */
        public void ShowSelector(Boolean state)
        {
            if(state)
                bubble[selected].SetOutlineColor(outlineSelectColor);
            else
                bubble[selected].SetOutlineColor(outlineUnselectColor);
        }

        /**
         * Moves upwards through the possible colors for the selected bubble.
         */
        public void IncreaseValue()
        {
            values[selected] = (values[selected] >= 5) ? 0 : values[selected] + 1;
            bubble[selected].SetFillColor(MapValueToColor(values[selected]));
        }

        /**
        * Moves downwards through the possible colors for the selected bubble.
        */
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
         * Compares the values of this CodeView to an array 
         * Returns a pair with 1. how many colors are at the correct spot and 
         * 2. how many colors are correct but at the wrong spot
         */
        public int[] CompareGuess(int[] solution)
        {
            //first entry: number of bubbles with correct color and spot, 
            //second entry: number of bubbles with correct color but wrong spot
            int[] returnVal = new int[]{0,0};
            int[] guess = this.GetValues();
            //is changed to true if bubble has correct color and spot, don't look at in second for-loop
            Boolean[] matched = new Boolean[4]{false, false, false, false};
            //is changed to true if bubble correct color but wrong spot, can only be used once, than done
            Boolean[] done = new Boolean[4] { false, false, false, false };
            int size = guess.Length;

            //get number of bubbles with correct color and spot
            for (int i = 0; i < size; i++)
            {
                if (guess[i] == solution[i])
                {
                    matched[i] = true;
                    returnVal[0]++;
                }
            }
            //get number of bubbles with correct color but wrong spot. Bubbles that are done, cannot be used
            for (int j = 0; j < size; j++)
            {
                if (!matched[j])
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (j!=k && guess[j]==solution[k] && !done[k] && !matched[k])
                        {
                            done[k] = true;
                            returnVal[1]++;
                        }
                    }
                }
            }
            return returnVal;
        }

        /**
         * Get the current bubble pattern values.
         */
        public int[] GetValues()
        {
            return this.values;
        }

        /**
         * Updates the current bubble values and refresh the colors accordingly.
         */
        public void SetValues(int[] newValues)
        {
            // Checks for values size
            if (newValues == null || newValues.Length != 4)
                throw new ArgumentException("Argument to SetValues MUST be an array of size 4");

            this.values = newValues;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] < 0 || values[i] >= 6)
                {
                    throw new ArgumentException("Argument to SetValues MUST only contain values between 0 and 5");
                }
                bubble[i].SetFillColor(MapValueToColor(values[i]));
            }
        }

    }
}
