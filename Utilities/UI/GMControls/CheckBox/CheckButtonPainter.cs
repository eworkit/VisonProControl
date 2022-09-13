using System;
using System.Drawing;
using System.Windows.Forms;

namespace Utilities.UI
{
    public class CheckButtonPainter
    {
        public static void RenderCheckButton(Graphics g, Rectangle rect, GMCheckBoxThemeBase xtheme,
            bool enable, CheckState checkState, GMButtonState state, string text,
            CheckMarkAlignment checkAlign, bool drawFocus)
        {

            Rectangle rectCheck, rectText;

            RadioButtonPainter.GetCheckRectAndTextRect(
                rect,
                xtheme.InnerPaddingWidth,
                xtheme.CheckRectWidth,
                xtheme.SpaceBetweenCheckMarkAndText,
                checkAlign,
                text,
                xtheme.TextFont,
                out rectCheck,
                out rectText);

            CheckRectPainter.RenderCheckRect(
                g,
                rectCheck,
                xtheme,
                enable,
                checkState,
                state);

            rectText.Offset(0, -1);
            TextRenderer.DrawText(
                g,
                text,
                xtheme.TextFont,
                rectText,
                enable ? xtheme.TextColor : xtheme.TextColorDisabled,
                TextFormatFlags.Left);

            if (drawFocus)
                BasicBlockPainter.RenderFocusRect(g, rectText, 0);
        }
    }
}
