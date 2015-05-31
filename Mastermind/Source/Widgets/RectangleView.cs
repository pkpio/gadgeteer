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
        private int width = 0;
        private int height = 0;
        private int posX = 0;
        private int posY = 0;
        private Gadgeteer.Color fillColor = Gadgeteer.Color.Black;
        private Gadgeteer.Color outlineColor = Gadgeteer.Color.Black;
        private int outlineThickness = 0;

        private DisplayTE35 mDisplay;

        public RectangleView(DisplayTE35 mDisplay)
        {
            this.mDisplay = mDisplay;
            Draw();
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
            Draw();
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
            Draw();
        }

        public void SetFillColor(Gadgeteer.Color color)
        {
            this.fillColor = color;
            Draw();
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
