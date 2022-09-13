using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Utilities.UI
{
    public delegate GraphicsPath ButtonForePathGetter(Rectangle rect);

    /// <summary>
    /// 定义三态按钮的各个状态, 因为系统也有一个ButtonState类型，
    /// 为避免冲突，加了GM前缀
    /// </summary>
    public enum GMButtonState
    {
        Normal,
        Hover,
        Pressed,
        PressLeave,
    }

    /// <summary>
    /// 定义按钮的边框是方形的还是圆形的
    /// </summary>
    public enum ButtonBorderType
    {
        Rectangle,
        Ellipse,
    }

    public enum BackgroundStyle
    {
        Flat,
        LinearGradient
    }

    public enum MouseOperationType
    {
        Move,
        Down,
        Up,
        Hover,
        Leave,
        Wheel
    }

    public enum KeyOperationType
    {
        KeyDown,
        KeyUp
    }

    public enum ForePathRatoteDirection
    {
        Down,
        Left,
        Up,
        Right,
    }

    public enum ForePathRenderMode
    {
        Draw,
        Fill,
    }

    public enum ScrollBarShowMode
    {
        Never,
        Always,
        Auto
    }

    public enum ListSelectionMode
    {
        None,
        One,
        Multiple
    }

    public enum ListDrawMode
    {
        AutoDraw,
        UserDraw
    }
}
