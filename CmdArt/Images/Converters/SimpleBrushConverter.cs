﻿using System;
using System.Drawing;

namespace CmdArt.Images.Converters
{
    public class SimpleBrushConverter : IBrushConverter
    {
        public ImageBrush CreateBrush(Color c)
        {
            int rBright = c.R / 43;
            int gBright = c.G / 43;
            int bBright = c.B / 43;

            int rBgComp = (rBright < 1 ? 0 : 4) | (rBright >= 4 ? 8 : 0);
            int gBgComp = (gBright < 1 ? 0 : 2) | (gBright >= 4 ? 8 : 0);
            int bBgComp = (bBright < 1 ? 0 : 1) | (bBright >= 4 ? 8 : 0);

            var bgColor = (ConsoleColor)(rBgComp | gBgComp | bBgComp);

            int rFgComp = (rBright < 2 ? 0 : 4) | (rBright >= 5 ? 8 : 0);
            int gFgComp = (gBright < 2 ? 0 : 2) | (gBright >= 5 ? 8 : 0);
            int bFgComp = (bBright < 2 ? 0 : 1) | (bBright >= 5 ? 8 : 0);

            var fgColor = (ConsoleColor)(rFgComp | gFgComp | bFgComp);
            char ch = ' ';

            return new ImageBrush(new Palette(bgColor, fgColor), ch);
        }
    }
}