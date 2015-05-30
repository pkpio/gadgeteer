using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
    class TextView
    {
        public String text;
        public int posX = 0;
        public int posY = 0;
        public Gadgeteer.Color textColor = Gadgeteer.Color.White;
        public Font textFont = Resources.GetFont(Resources.FontResources.NinaB);

        DisplayTE35 mDisplay;

        public TextView(DisplayTE35 display)
        {
            this.mDisplay = display;   
        }

        public TextView(DisplayTE35 display, String text, int posX, int posY)
        {
            this.mDisplay = display;
            this.text = text;
            this.posX = posX;
            this.posY = posY;
        }

        public TextView(DisplayTE35 display, String text, int posX, int posY,
            Gadgeteer.Color textColor, Font textFont)
        {
            this.mDisplay = display;
            this.text = text;
            this.posX = posX;
            this.posY = posY;
            this.textColor = textColor;
            this.textFont = textFont;
        }

        public void Draw()
        {
            mDisplay.SimpleGraphics.DisplayText(
                this.text,
                this.textFont,
                this.textColor,
                this.posX, this.posY);
        }
    }
}
