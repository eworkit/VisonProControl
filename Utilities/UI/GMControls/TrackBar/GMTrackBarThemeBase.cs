using System;
using System.Drawing;

namespace Utilities.UI
{
    public class GMTrackBarThemeBase
    {
        #region 尺寸及间距调节

        /// <summary>
        /// 在可移动方向上的长度
        /// </summary>
        public int ButtonLength1 { get; set; }

        /// <summary>
        /// 在不可移动方向上的长度
        /// </summary>
        public int ButtonLength2 { get; set; }

        /// <summary>
        /// 在非可移动方向上的长度
        /// </summary>
        public int MainLineLength { get; set; }

        /// <summary>
        /// Button在可移动方向上与内边界的间距
        /// </summary>
        public int ButtonOutterSpace1 { get; set; }

        /// <summary>
        /// Button在非可移动方向上与内边界或TickLine的间距
        /// </summary>
        public int ButtonOutterSpace2 { get; set; }

        public int TickLineLength { get; set; }

        /// <summary>
        /// TickLine与Button之间的间距
        /// </summary>
        public int TickLineSpaceWithButton { get; set; }

        /// <summary>
        /// TickLine与内边界之间的间距
        /// </summary>
        public int TickLineSpaceWithBorder { get; set; }

        #endregion

        #region 边框及颜色设置

        public int BorderWidth { get; set; }
        public bool DrawBackground { get; set; }
        public bool DrawBorder { get; set; }
        public bool DrawInnerBorder { get; set; }
        public Color BackColor { get; set; }
        public Color BorderColor { get; set; }
        public Color InnerBorderColor { get; set; }        
        public Color TickLineColor { get; set; }

        #endregion 

        #region extra

        // thumb button
        //public ButtonColorTable ThumbButtonColorTable { get; set; }
        public ButtonBorderType ThumbButtonBorderType { get; set; }
        public GMButtonThemeBase ThumbButtonTheme { get; set; }

        // main line
        public bool MainLineDrawBorder { get; set; }
        public Color MainLineBorderColor1 { get; set; }
        public Color MainLineBorderColor2 { get; set; }
        public int MainLineRadius { get; set; }
        public Color MainLineRange1BackColor { get; set; }
        public Color MainLineRange2BackColor { get; set; }

        #endregion

        public GMTrackBarThemeBase()
        {
            ButtonLength1 = 8;
            ButtonLength2 = 18;
            MainLineLength = 4;
            ButtonOutterSpace1 = 4;
            ButtonOutterSpace2 = 2;
            TickLineLength = 3;
            TickLineSpaceWithButton = 2;
            TickLineSpaceWithBorder = 6;

            BorderWidth = 1;
            DrawBackground = true;
            DrawBorder = false;
            DrawInnerBorder = false;
            BackColor = Color.Transparent;
            TickLineColor = Color.FromArgb(185, 185, 185);

            MainLineDrawBorder = true;
            MainLineBorderColor1 = Color.FromArgb(0, 114, 198);
            MainLineRadius = 0;
            MainLineRange1BackColor = MainLineRange2BackColor = Color.White;

            ThumbButtonTheme = new GMButtonThemeBase();
            ThumbButtonTheme.RoundedRadius = 0;
        }
    }
}
