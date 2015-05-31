using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
    class EllipseView
    {
        // Widget params
        private int posX = 0;
        private int posY = 0;
        private int radiusX = 0;
        private int radiusY = 0;
        private Gadgeteer.Color fillColor = Gadgeteer.Color.Black;
        private Gadgeteer.Color outlineColor = Gadgeteer.Color.Black;
        private int outlineThickness = 0;

        private DisplayTE35 mDisplay;
        
        public EllipseView(DisplayTE35 display){
            this.mDisplay = display;    
        }
        
        public EllipseView(int posX, int posY,  int radiusX, int radiusY,
            Gadgeteer.Color fillColor, DisplayTE35 display)
        {
            this.posX = posX;
            this.posY = posY;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.fillColor = fillColor;
            this.mDisplay = display;
        }

        public EllipseView(int posX, int posY, int radiusX, int radiusY, 
            Gadgeteer.Color fillColor, Gadgeteer.Color outlineColor,
            int outlineThickness, DisplayTE35 display)
        {
            this.posX = posX;
            this.posY = posY;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.fillColor = fillColor;
            this.outlineColor = outlineColor;
            this.outlineThickness = outlineThickness;
            this.mDisplay = display;
        }

        public void Draw()
        {
            mDisplay.SimpleGraphics.DisplayEllipse(
                this.outlineColor,
                this.outlineThickness,
                this.fillColor, 
                this.posX, this.posY, 
                this.radiusX, this.radiusY);
        }

    }
}
