using System;
using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mastermind.Widgets
{
    class TextView
    {
        private String text;
        private int posX = 0;
        private int posY = 0;
        private Gadgeteer.Color textColor = Gadgeteer.Color.White;
        private Font textFont = Resources.GetFont(Resources.FontResources.NinaB);

        private DisplayTE35 mDisplay;

        public TextView(DisplayTE35 display)
        {
            this.mDisplay = display;
            Draw();
        }

        public TextView(DisplayTE35 display, String text, int posX, int posY)
        {
            this.mDisplay = display;
            this.text = text;
            this.posX = posX;
            this.posY = posY;
            Draw();
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
            Draw();
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
