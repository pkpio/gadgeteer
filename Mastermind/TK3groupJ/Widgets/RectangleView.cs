using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
    class RectangleView
    {
        // Widget params
        public int width = 0;
        public int height = 0;
        public int posX = 0;
        public int posY = 0;
        public Gadgeteer.Color fillColor = Gadgeteer.Color.Black;
        public Gadgeteer.Color outlineColor = Gadgeteer.Color.Black;
        public int outlineThickness = 0;
        
        DisplayTE35 mDisplay;

        public RectangleView(DisplayTE35 mDisplay)
        {
            this.mDisplay = mDisplay;
        }

        public RectangleView(int posX, int posY, int width, int height,
            Gadgeteer.Color fillColor, DisplayTE35 display)
        {
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
            this.fillColor = fillColor;
            this.mDisplay = display;
        }

        public RectangleView(int posX, int posY, int width, int height, 
            Gadgeteer.Color fillColor, Gadgeteer.Color outlineColor,
            int outlineThickness, DisplayTE35 display)
        {
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
            this.fillColor = fillColor;
            this.outlineColor = outlineColor;
            this.outlineThickness = outlineThickness;
            this.mDisplay = display;
        }

        public void Draw()
        {
            mDisplay.SimpleGraphics.DisplayRectangle(
                this.outlineColor,
                this.outlineThickness,
                this.fillColor, 
                this.posX, this.posY, 
                this.width, this.height);
        }

    }


}
