using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities.ExMethod;

namespace Utilities.UI
{
    public enum TextInputType
    {
        Any, Int, Decimal, Letter, LetterOrDigit
    }
    public class TextBoxEx : TextBox
    {
        public TextInputType TextType { get; set; }
        public bool Nonnegative { get; set; }
        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
   
        public TextBoxEx()
        {
            TextType = TextInputType.Any;
            KeyPress += new KeyPressEventHandler(TextBoxEx_KeyPress); 
        }
        public TextBoxEx(TextInputType type)
            : this()
        {
            TextType = type;
        }
        void TextBoxEx_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (TextType)
            {
                case TextInputType.Any:
                    break;
                case TextInputType.Int:
                    if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case TextInputType.Decimal:
                    if (e.KeyChar == InputText.DecimalSeparator)
                    {
                        TextBox tb = sender as TextBox;
                        if (tb.SelectionStart == 0)
                            e.Handled = true;
                        else if (tb.Text.Contains(InputText.DecimalSeparator))
                            e.Handled = true;
                    }
                    else if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case TextInputType.Letter:
                    if (e.KeyChar != '\b' && !char.IsNumber(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case TextInputType.LetterOrDigit:
                    if (e.KeyChar != '\b' && !char.IsLetterOrDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }
    }
    public class InputStyle
    {
        public TextInputType TextType;
        public bool IsNonnegative { get; set; }
        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
        public char[] IgnoredChars { get; set; }
        /// <summary>
        /// 返回值：true--执行后返回；false--执行后继续
        /// </summary>
        public Func<Control, KeyPressEventArgs,bool> Handled { get; set; }
        public InputStyle()
       {
           IsNonnegative = false;
       }
    }
    public static class InputText
    {
        public static  char DecimalSeparator = MyApp.DecimalSeparator[0];
        static Dictionary<Control, InputStyle> InputStyles = new Dictionary<Control, InputStyle>();
        public static void SetType(this Control tb, InputStyle style)
        {
            InputStyle style0;
            if (InputStyles.TryGetValue(tb, out style0))
            {
                InputStyles[tb] = style;
            }
            else
            {
                style0 = style;
                InputStyles.Add(tb, style0);
                tb.Disposed += (s1, e1) =>
                {
                    if (InputStyles.ContainsKey(tb))
                        InputStyles.Remove(tb);
                };
            }
            tb.KeyPress -= TextBoxEx_KeyPress;
            tb.KeyPress += TextBoxEx_KeyPress;
            tb.LostFocus -= tb_LostFocus;
            tb.LostFocus += tb_LostFocus;
        }
        public static void SetType(this Control tb, TextInputType type, bool Nonnegative, Func<Control, KeyPressEventArgs, bool> handled)
        {
            InputStyle newstyle = new InputStyle
            {
                TextType = type, 
                Handled = handled
            };
            SetType(tb,newstyle);
        }
        public static void SetType(this Control tb, TextInputType type, bool Nonnegative = false, double? MinV = null, double? MaxV = null, char[] IgnoredChars = null)
        {
            InputStyle newstyle = new InputStyle
            {
                TextType = type,
                IsNonnegative = Nonnegative,
                MaxValue = MaxV,
                MinValue = MinV,
                IgnoredChars = IgnoredChars,
            };
            InputStyle style0;
            if (InputStyles.TryGetValue(tb, out style0))
            {
                if (style0 == null)
                    style0 = newstyle;
                else
                    style0.TextType = type;
                InputStyles[tb] = style0;
            }
            else
            {
                style0 = newstyle;
                InputStyles.Add(tb, style0);
                tb.Disposed += (s1, e1) =>
                {
                    if (InputStyles.ContainsKey(tb))
                        InputStyles.Remove(tb);
                };
            }
            tb.KeyPress -= TextBoxEx_KeyPress;
            tb.KeyPress += TextBoxEx_KeyPress;
            tb.LostFocus -= tb_LostFocus;
            tb.LostFocus += tb_LostFocus;
        }
        static void tb_LostFocus(object  sender, EventArgs e)
        {
            Control tb = sender as Control;
            if (tb.Text.Length == 0)
            {
                // return;
            }
            InputStyle style;
            if (!InputStyles.TryGetValue(tb, out style))
                return;
            Action a1 = () =>
            {
                if (tb.Text.Length == 0)
                {
                    if (style.MinValue.HasValue)
                        tb.Text = style.MinValue.Value.ToString();
                }
                else if (tb.Text[0] == '-')
                {
                    if (tb.Text.Length > 1)
                    {
                        if (tb.Text[1] == '0')
                        {
                            tb.Text = "-" + tb.Text.TrimStart('-').TrimStart('0');
                            if (tb.Text == "-")
                                tb.Text = "0";
                        }

                    }
                    else tb.Text = "";
                }
                else if (tb.Text.Length > 1)
                {
                    tb.Text = tb.Text.TrimStart('0');
                    if (tb.Text.Length == 0)
                        tb.Text = "0";
                }
            };
           
            if (style == null)
                return;
            if (style.TextType == TextInputType.Any)
                return;
            string s = tb.Text;
            bool isNegative = false;
            if (!style.IsNonnegative)
                isNegative = s.StartsWith("-");
            if (isNegative && tb.Text == "-")
            {
                tb.Text = "";
                return;
            }
            if (style.TextType == TextInputType.Int || style.TextType == TextInputType.Decimal)
            {
                if(style.IgnoredChars==null || style.IgnoredChars.Length==0)
                {
                    tb.Text = s.TrimEnd(new[] { DecimalSeparator, '-' });
                    if (style.TextType == TextInputType.Int)
                    {
                        a1();
                    }
                    else
                    {
                        int n = s.IndexOf(DecimalSeparator);
                        if (n < 0)
                            a1();
                        else if (n == 0)
                            tb.Text = "0" + s;
                        else
                        {
                            if (double.Parse(tb.Text) == 0)
                            {
                                tb.Text = "0";
                                return;
                            }
                            if (n == s.Length - 1)
                                tb.Text = s.Remove(n);
                            int j = 0;
                            for (int i = isNegative ? 1 : 0; i < n - 1; i++)
                            {
                                if (s[i] == '0')
                                    j++;
                                else
                                    break;
                            }
                            if (j > 0)
                                tb.Text = s.Remove(isNegative ? 1 : 0, j);
                        }
                    }
                }
            }
        }

        public static bool checkMaxMin(InputStyle style, ref string text)
        {
            switch (style.TextType)
            {
                case TextInputType.Int:
                    long a = 0;
                    if (long.TryParse(text, out a))
                    {
                        if (style.MinValue.HasValue)
                            if (a < style.MinValue)
                            {
                                text = style.MinValue.ToString();
                                return false;
                            }
                        if (style.MaxValue.HasValue)
                            if (a > style.MaxValue)
                            {
                                text = style.MaxValue.ToString();
                                return false;
                            }

                    }
                    break;
                case TextInputType.Decimal:
                    double b = 0;
                    if (double.TryParse(text, out b))
                    {
                        if (style.MinValue.HasValue)
                            if (b < style.MinValue)
                            {
                                text = style.MinValue.ToString();
                                return false;
                            }
                        if (style.MaxValue.HasValue)
                        {
                            if (b > style.MaxValue)
                            {
                                text = style.MaxValue.ToString();
                                return false;
                            }
                        }

                    }
                    break;
            }
            return true;
        }

        static void TextBoxEx_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as Control;
            // var inputStyle = InputStyles[tb];
            string s = tb.Text;

            InputStyle inputStyle;
            if (!InputStyles.TryGetValue(tb, out inputStyle))
                return;
            if (inputStyle == null)
                return;
            if (inputStyle.Handled != null)
                if (inputStyle.Handled(tb, e))
                    return;
            if (inputStyle.TextType == TextInputType.Any)
            {
                return;
            }
            if (e.KeyChar == '\b')
                return;
            if (inputStyle.IgnoredChars != null && inputStyle.IgnoredChars.Contains(e.KeyChar))
                return;
            int SelectionStart = 0,selectionLen=0;
            var tb1 = tb as TextBox;
            if (tb1 != null)
            {
                SelectionStart = tb1.SelectionStart;
                selectionLen = tb1.SelectionLength;
            }
            else
            {
                var cmb = tb as ComboBox;
                if (cmb != null)
                {
                    SelectionStart = cmb.SelectionStart;
                    selectionLen = cmb.SelectionLength;
                }
            }
            switch (inputStyle.TextType)
            {
                case TextInputType.Int:
                    {
                        if (e.KeyChar == '-')
                        {
                            if (inputStyle.IsNonnegative)
                            {
                                e.Handled = true;
                                return;
                            }
                            else
                            {
                                if (SelectionStart == 0)
                                {
                                    if (tb.Text.Length > 0 && tb.Text[0] == '-')
                                    {
                                        e.Handled = true;
                                        return;
                                    }
                                }
                                else if (SelectionStart > 0)
                                {
                                    e.Handled = true;
                                    return;
                                }
                            }

                        }
                        else if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                        else if (e.KeyChar == '0' && SelectionStart == 0 && tb.Text.Length > 0)
                            e.Handled = true;
                        string s2 = s.Remove(SelectionStart, selectionLen).Insert(SelectionStart, e.KeyChar.ToString());
                        if (!checkMaxMin(inputStyle, ref s2))
                        {
                            tb.Text = s2;
                            e.Handled = true;
                        }
                        int ntmp;
                        if (e.KeyChar != '\b')
                        {
                            if (inputStyle.IgnoredChars != null && inputStyle.IgnoredChars.Length > 0)
                            {
                            }
                            else if (!int.TryParse(s2, out ntmp))
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                    }
                case TextInputType.Decimal:
                    {
                        if (e.KeyChar == DecimalSeparator)
                        {
                            if (SelectionStart == 0)
                                e.Handled = true;
                            else if (SelectionStart == 1 && !char.IsDigit(tb.Text[0]))
                                e.Handled = true;
                            else if (tb.Text.Contains(DecimalSeparator))
                                e.Handled = true;
                        }
                        else if (e.KeyChar == '-')
                        {
                            if (inputStyle.IsNonnegative)
                                e.Handled = true;
                            else
                            {
                                if (SelectionStart == 0)
                                {
                                    if (tb.Text.Length > 0 && tb.Text[0] == '-')
                                        e.Handled = true;
                                }
                                else if (SelectionStart > 0)
                                    e.Handled = true;
                            }
                        }
                        else if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                        string s2 = s.Remove(SelectionStart, selectionLen).Insert(SelectionStart, e.KeyChar.ToString());
                        if (!checkMaxMin(inputStyle, ref s2))
                        {
                            tb.Text = s2;
                            e.Handled = true;
                        }
                        if (inputStyle.IgnoredChars != null && inputStyle.IgnoredChars.Length > 0)
                        {
                        }
                       else if (!s2.LocalStringToDoubleX().HasValue)
                        {
                            e.Handled = true;
                        }
                        break;
                    }
                case TextInputType.Letter:
                    if (e.KeyChar != '\b' && !char.IsNumber(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case TextInputType.LetterOrDigit:
                    if (e.KeyChar != '\b' && !char.IsLetterOrDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }
    }
}
