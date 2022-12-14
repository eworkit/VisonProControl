using System;
using System.Drawing;

using Utilities.UI.MyGraphics;

namespace Utilities.UI
{
    public class ButtonColorTable
    {
        public Color BorderColorNormal { get; set; }
        public Color BorderColorHover { get; set; }
        public Color BorderColorPressed { get; set; }
        public Color BorderColorDisabled { get; set; }

        public Color BackColorNormal { get; set; }
        public Color BackColorHover { get; set; }
        public Color BackColorPressed { get; set; }
        public Color BackColorDisabled { get; set; }

        public Color ForeColorNormal { get; set; }
        public Color ForeColorHover { get; set; }
        public Color ForeColorPressed { get; set; }
        public Color ForeColorDisabled { get; set; }

        public ButtonColorTable()
        {

        }

        public static ButtonColorTable DefaultTable()
        {
            ButtonColorTable table = new ButtonColorTable();

            table.ForeColorNormal = table.ForeColorHover = table.ForeColorPressed = Color.Black;

            table.BackColorNormal = Color.LightPink;
            table.BackColorHover = Color.HotPink;
            table.BackColorPressed = Color.DeepPink;

            return table;
        }

        public static ButtonColorTable GetDefaultCloseBtnColor()
        {
            ButtonColorTable table = new ButtonColorTable();

            table.ForeColorNormal = Color.FromArgb(0,0,0);
            table.ForeColorHover = table.ForeColorPressed = Color.White;
            table.BackColorNormal = Color.Transparent;
            table.BackColorHover = Color.FromArgb(241,13,13);
            table.BackColorPressed = Color.FromArgb(141,5,5);

            return table;
        }

        public static ButtonColorTable GetColorTableVs2013Theme()
        {
            ButtonColorTable table = new ButtonColorTable();

            table.ForeColorNormal = table.ForeColorHover = table.ForeColorPressed = Color.Black;
            table.BackColorHover = Color.FromArgb(255, 252, 244);
            table.BackColorPressed = Color.FromArgb(255, 232, 166);
            table.BorderColorHover = table.BorderColorPressed = Color.FromArgb(229, 195, 101);

            return table;
        }

        public static ButtonColorTable GetDevWhiteThemeMinMaxBtnColor()
        {
            ButtonColorTable maxTable = new ButtonColorTable();
            maxTable.ForeColorNormal = Color.Black;
            maxTable.ForeColorHover = maxTable.ForeColorPressed = Color.White;
            maxTable.BackColorNormal = Color.Transparent;
            maxTable.BackColorHover = Color.FromArgb(60, 255-Color.Transparent.R,255-Color.Transparent.G,255-Color.Transparent.B);
            maxTable.BackColorPressed = Color.FromArgb(120, 255 - Color.Transparent.R, 255 - Color.Transparent.G, 255 - Color.Transparent.B);
            return maxTable;
        }

        public static ButtonColorTable GetDevWhiteThemeCloseBtnColor()
        {
            ButtonColorTable closeTable = new ButtonColorTable();
            closeTable.ForeColorNormal = closeTable.ForeColorHover = closeTable.ForeColorPressed
                = ColorHelper.GetDarkerColor(Color.White, 5);
            closeTable.BackColorNormal = Color.FromArgb(199, 80, 80);
            closeTable.BackColorHover = Color.FromArgb(224, 67, 67);
            closeTable.BackColorPressed = Color.FromArgb(153, 61, 61);
            return closeTable;
        }
        public static ButtonColorTable GetDevWhiteThemeOptoinBtnColor()
        {
            ButtonColorTable maxTable = new ButtonColorTable();
            maxTable.ForeColorNormal = Color.Black;
            maxTable.ForeColorHover = maxTable.ForeColorPressed = Color.White;
            maxTable.BackColorNormal = Color.Transparent;
            maxTable.BackColorHover = Color.FromArgb(60, 255 - Color.Transparent.R, 255 - Color.Transparent.G, 255 - Color.Transparent.B);
            maxTable.BackColorPressed = Color.FromArgb(120, 255 - Color.Transparent.R, 255 - Color.Transparent.G, 255 - Color.Transparent.B);
            return maxTable;
        }
    }
}
