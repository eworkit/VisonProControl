using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Globalization; 
using System.Collections;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using System.Net.Sockets;
using Utilities.Properties; 

namespace Utilities.UI
{
    [Designer(typeof(IPAddressControlDesigner))]
    public class IPAddressControl : ContainerControl
    {
        // Fields
        private bool _autoHeight = true;
        private bool _backColorChanged;
        private BorderStyle _borderStyle = BorderStyle.Fixed3D;
        private DotControl[] _dotControls = new DotControl[3];
        private FieldControl[] _fieldControls = new FieldControl[4];
        private bool _focused;
        private bool _hasMouse;
        private bool _readOnly;
        private TextBox _referenceTextBox = new TextBox();
        public const int FieldCount = 4;
        private Size Fixed3DOffset = new Size(3, 3);
        private Size FixedSingleOffset = new Size(2, 2);

        // Events
        public event EventHandler<FieldChangedEventArgs> FieldChangedEvent;

        // Methods
        public IPAddressControl()
        {
            this.BackColor = SystemColors.Window;
            this.ResetBackColorChanged();
            for (int i = 0; i < this._fieldControls.Length; i++)
            {
                this._fieldControls[i] = new FieldControl();
                this._fieldControls[i].CreateControl();
                this._fieldControls[i].FieldIndex = i;
                this._fieldControls[i].Name = "FieldControl" + i.ToString(CultureInfo.InvariantCulture);
                this._fieldControls[i].Parent = this;
                this._fieldControls[i].CedeFocusEvent += new EventHandler<CedeFocusEventArgs>(this.OnCedeFocus);
                this._fieldControls[i].Click += new EventHandler(this.OnSubControlClicked);
                this._fieldControls[i].DoubleClick += new EventHandler(this.OnSubControlDoubleClicked);
                this._fieldControls[i].GotFocus += new EventHandler(this.OnFieldGotFocus);
                this._fieldControls[i].KeyDown += new KeyEventHandler(this.OnFieldKeyDown);
                this._fieldControls[i].KeyPress += new KeyPressEventHandler(this.OnFieldKeyPressed);
                this._fieldControls[i].KeyUp += new KeyEventHandler(this.OnFieldKeyUp);
                this._fieldControls[i].LostFocus += new EventHandler(this.OnFieldLostFocus);
                this._fieldControls[i].MouseClick += new MouseEventHandler(this.OnSubControlMouseClicked);
                this._fieldControls[i].MouseDoubleClick += new MouseEventHandler(this.OnSubControlMouseDoubleClicked);
                this._fieldControls[i].MouseEnter += new EventHandler(this.OnSubControlMouseEntered);
                this._fieldControls[i].MouseHover += new EventHandler(this.OnSubControlMouseHovered);
                this._fieldControls[i].MouseLeave += new EventHandler(this.OnSubControlMouseLeft);
                this._fieldControls[i].MouseMove += new MouseEventHandler(this.OnSubControlMouseMoved);
                this._fieldControls[i].PreviewKeyDown += new PreviewKeyDownEventHandler(this.OnFieldPreviewKeyDown);
                this._fieldControls[i].TextChangedEvent += new EventHandler<TextChangedEventArgs>(this.OnFieldTextChanged);
                base.Controls.Add(this._fieldControls[i]);
                if (i < 3)
                {
                    this._dotControls[i] = new DotControl();
                    this._dotControls[i].CreateControl();
                    this._dotControls[i].Name = "DotControl" + i.ToString(CultureInfo.InvariantCulture);
                    this._dotControls[i].Parent = this;
                    this._dotControls[i].Click += new EventHandler(this.OnSubControlClicked);
                    this._dotControls[i].DoubleClick += new EventHandler(this.OnSubControlDoubleClicked);
                    this._dotControls[i].MouseClick += new MouseEventHandler(this.OnSubControlMouseClicked);
                    this._dotControls[i].MouseDoubleClick += new MouseEventHandler(this.OnSubControlMouseDoubleClicked);
                    this._dotControls[i].MouseEnter += new EventHandler(this.OnSubControlMouseEntered);
                    this._dotControls[i].MouseHover += new EventHandler(this.OnSubControlMouseHovered);
                    this._dotControls[i].MouseLeave += new EventHandler(this.OnSubControlMouseLeft);
                    this._dotControls[i].MouseMove += new MouseEventHandler(this.OnSubControlMouseMoved);
                    base.Controls.Add(this._dotControls[i]);
                }
            }
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.FixedWidth, true);
            base.SetStyle(ControlStyles.FixedHeight, true);
            this._referenceTextBox.AutoSize = true;
            this.Cursor = Cursors.IBeam;
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            base.AutoScaleMode = AutoScaleMode.Dpi;
            base.Size = this.MinimumSize;
            base.DragEnter += new DragEventHandler(this.IPAddressControl_DragEnter);
            base.DragDrop += new DragEventHandler(this.IPAddressControl_DragDrop);
        }

        private void AdjustSize()
        {
            Size minimumSize = this.MinimumSize;
            if (base.Width > minimumSize.Width)
            {
                minimumSize.Width = base.Width;
            }
            if (base.Height > minimumSize.Height)
            {
                minimumSize.Height = base.Height;
            }
            if (this.AutoHeight)
            {
                base.Size = new Size(minimumSize.Width, this.MinimumSize.Height);
            }
            else
            {
                base.Size = minimumSize;
            }
            this.LayoutControls();
        }

        private Size CalculateMinimumSize()
        {
            Size size = new Size(0, 0);
            foreach (FieldControl control in this._fieldControls)
            {
                size.Width += control.Width;
                size.Height = Math.Max(size.Height, control.Height);
            }
            foreach (DotControl control2 in this._dotControls)
            {
                size.Width += control2.Width;
                size.Height = Math.Max(size.Height, control2.Height);
            }
            switch (this.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    size.Width += 4;
                    size.Height += this.GetSuggestedHeight() - size.Height;
                    return size;

                case BorderStyle.Fixed3D:
                    size.Width += 6;
                    size.Height += this.GetSuggestedHeight() - size.Height;
                    return size;
            }
            return size;
        }

        private void Cleanup()
        {
            foreach (DotControl control in this._dotControls)
            {
                base.Controls.Remove(control);
                control.Dispose();
            }
            foreach (FieldControl control2 in this._fieldControls)
            {
                base.Controls.Remove(control2);
                control2.Dispose();
            }
            this._dotControls = null;
            this._fieldControls = null;
        }

        public void Clear()
        {
            foreach (FieldControl control in this._fieldControls)
            {
                control.Clear();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Cleanup();
            }
            base.Dispose(disposing);
        }

        public byte[] GetAddressBytes()
        {
            byte[] buffer = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                buffer[i] = this._fieldControls[i].Value;
            }
            return buffer;
        }

        private int GetSuggestedHeight()
        {
            this._referenceTextBox.BorderStyle = this.BorderStyle;
            this._referenceTextBox.Font = this.Font;
            return this._referenceTextBox.Height;
        }

        [SuppressMessage("Microsoft.Usage", "CA1806", Justification = "What should be done if ReleaseDC() doesn't work?")]
        private static NativeMethods.TEXTMETRIC GetTextMetrics(IntPtr hwnd, Font font)
        {
            NativeMethods.TEXTMETRIC textmetric;
            IntPtr windowDC = NativeMethods.GetWindowDC(hwnd);
            IntPtr hgdiobj = font.ToHfont();
            try
            {
                IntPtr ptr3 = NativeMethods.SelectObject(windowDC, hgdiobj);
                NativeMethods.GetTextMetrics(windowDC, out textmetric);
                NativeMethods.SelectObject(windowDC, ptr3);
            }
            finally
            {
                NativeMethods.ReleaseDC(hwnd, windowDC);
                NativeMethods.DeleteObject(hgdiobj);
            }
            return textmetric;
        }

        private void IPAddressControl_DragDrop(object sender, DragEventArgs e)
        {
            this.Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void IPAddressControl_DragEnter(object sender, DragEventArgs e)////////////////                                                                                                                dw                                                                                               .rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr444444444444444444444444444444444444zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz                                                                                                           ///
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void LayoutControls()
        {
            base.SuspendLayout();
            int num = base.Width - this.MinimumSize.Width;
            int num2 = (this._fieldControls.Length + this._dotControls.Length) + 1;
            int num3 = num / num2;
            int num4 = num % num2;
            int[] numArray = new int[num2];
            for (int i = 0; i < num2; i++)
            {
                numArray[i] = num3;
                if (i < num4)
                {
                    numArray[i]++;
                }
            }
            int x = 0;
            int y = 0;
            switch (this.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    x = this.FixedSingleOffset.Width;
                    y = this.FixedSingleOffset.Height;
                    break;

                case BorderStyle.Fixed3D:
                    x = this.Fixed3DOffset.Width;
                    y = this.Fixed3DOffset.Height;
                    break;
            }
            int num8 = 0;
            x += numArray[num8++];
            for (int j = 0; j < this._fieldControls.Length; j++)
            {
                this._fieldControls[j].Location = new Point(x, y);
                x += this._fieldControls[j].Width;
                if (j < this._dotControls.Length)
                {
                    x += numArray[num8++];
                    this._dotControls[j].Location = new Point(x, y);
                    x += this._dotControls[j].Width;
                    x += numArray[num8++];
                }
            }
            base.ResumeLayout(false);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            this._backColorChanged = true;
        }

        private void OnCedeFocus(object sender, CedeFocusEventArgs e)
        {
            switch (e.Action)
            {
                case MyAction.Trim:
                    if (e.FieldIndex != 0)
                    {
                        this._fieldControls[e.FieldIndex - 1].TakeFocus(MyAction.Trim);
                        return;
                    }
                    return;

                case MyAction.Home:
                    this._fieldControls[0].TakeFocus(MyAction.Home);
                    return;

                case MyAction.End:
                    this._fieldControls[3].TakeFocus(MyAction.End);
                    return;
            }
            if (((e.Direction != Direction.Reverse) || (e.FieldIndex != 0)) && ((e.Direction != Direction.Forward) || (e.FieldIndex != 3)))
            {
                int fieldIndex = e.FieldIndex;
                if (e.Direction == Direction.Forward)
                {
                    fieldIndex++;
                }
                else
                {
                    fieldIndex--;
                }
                this._fieldControls[fieldIndex].TakeFocus(e.Direction, e.Selection);
            }
        }

        private void OnFieldGotFocus(object sender, EventArgs e)
        {
            if (!this._focused)
            {
                this._focused = true;
                base.OnGotFocus(EventArgs.Empty);
            }
        }

        private void OnFieldKeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        private void OnFieldKeyPressed(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void OnFieldKeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        private void OnFieldLostFocus(object sender, EventArgs e)
        {
            if (!this.Focused)
            {
                this._focused = false;
                base.OnLostFocus(EventArgs.Empty);
            }
        }

        private void OnFieldPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.OnPreviewKeyDown(e);
        }

        private void OnFieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.FieldChangedEvent != null)
            {
                FieldChangedEventArgs args = new FieldChangedEventArgs
                {
                    FieldIndex = e.FieldIndex,
                    Text = e.Text
                };
                this.FieldChangedEvent(this, args);
            }
            this.OnTextChanged(EventArgs.Empty);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.AdjustSize();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this._focused = true;
            this._fieldControls[0].TakeFocus(Direction.Forward, Selection.All);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!this.Focused)
            {
                this._focused = false;
                base.OnLostFocus(e);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!this._hasMouse)
            {
                this._hasMouse = true;
                base.OnMouseEnter(e);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!this.HasMouse)
            {
                base.OnMouseLeave(e);
                this._hasMouse = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            base.OnPaint(e);
            Color backColor = this.BackColor;
            if (!this._backColorChanged && (!base.Enabled || this.ReadOnly))
            {
                backColor = SystemColors.Control;
            }
            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, base.ClientRectangle);
            }
            Rectangle rect = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
            switch (this.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
                    return;

                case BorderStyle.Fixed3D:
                    if (!Application.RenderWithVisualStyles)
                    {
                        ControlPaint.DrawBorder3D(e.Graphics, base.ClientRectangle, Border3DStyle.Sunken);
                        return;
                    }
                    using (Pen pen = new Pen(System.Windows.Forms.VisualStyles.VisualStyleInformation.TextControlBorder))
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                    rect.Inflate(-1, -1);
                    e.Graphics.DrawRectangle(SystemPens.Window, rect);
                    return;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.AdjustSize();
        }

        private void OnSubControlClicked(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void OnSubControlDoubleClicked(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }

        private void OnSubControlMouseClicked(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void OnSubControlMouseDoubleClicked(object sender, MouseEventArgs e)
        {
            this.OnMouseDoubleClick(e);
        }

        private void OnSubControlMouseEntered(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void OnSubControlMouseHovered(object sender, EventArgs e)
        {
            this.OnMouseHover(e);
        }

        private void OnSubControlMouseLeft(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void OnSubControlMouseMoved(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        private void Parse(string text)
        {
            this.Clear();
            if (text != null)
            {
                int startIndex = 0;
                int index = 0;
                index = 0;
                while (index < this._dotControls.Length)
                {
                    int num3 = text.IndexOf(this._dotControls[index].Text, startIndex, StringComparison.Ordinal);
                    if (num3 < 0)
                    {
                        break;
                    }
                    this._fieldControls[index].Text = text.Substring(startIndex, num3 - startIndex);
                    startIndex = num3 + this._dotControls[index].Text.Length;
                    index++;
                }
                this._fieldControls[index].Text = text.Substring(startIndex);
            }
        }

        private void ResetBackColorChanged()
        {
            this._backColorChanged = false;
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Justification = "Using Bytes seems more informative than SetAddressValues.")]
        public void SetAddressBytes(byte[] bytes)
        {
            this.Clear();
            if (bytes != null)
            {
                int num = Math.Min(4, bytes.Length);
                for (int i = 0; i < num; i++)
                {
                    this._fieldControls[i].Text = bytes[i].ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public void SetFieldFocus(int fieldIndex)
        {
            if ((fieldIndex >= 0) && (fieldIndex < 4))
            {
                this._fieldControls[fieldIndex].TakeFocus(Direction.Forward, Selection.All);
            }
        }

        public void SetFieldRange(int fieldIndex, byte rangeLower, byte rangeUpper)
        {
            if ((fieldIndex >= 0) && (fieldIndex < 4))
            {
                this._fieldControls[fieldIndex].RangeLower = rangeLower;
                this._fieldControls[fieldIndex].RangeUpper = rangeUpper;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                builder.Append(this._fieldControls[i].ToString());
                if (i < this._dotControls.Length)
                {
                    builder.Append(this._dotControls[i].ToString());
                }
            }
            return builder.ToString();
        }

        // Properties
        [Browsable(true)]
        public bool AllowInternalTab
        {
            get
            {
                FieldControl[] controlArray = this._fieldControls;
                int index = 0;
                while (index < controlArray.Length)
                {
                    FieldControl control = controlArray[index];
                    return control.TabStop;
                }
                return false;
            }
            set
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    control.TabStop = value;
                }
            }
        }

        [Browsable(true)]
        public bool AnyBlank
        {
            get
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    if (control.Blank)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [Browsable(true)]
        public bool AutoHeight
        {
            get
            {
                return this._autoHeight;
            }
            set
            {
                this._autoHeight = value;
                if (this._autoHeight)
                {
                    this.AdjustSize();
                }
            }
        }

        [Browsable(false)]
        public int Baseline
        {
            get
            {
                int num = GetTextMetrics(base.Handle, this.Font).tmAscent + 1;
                switch (this.BorderStyle)
                {
                    case BorderStyle.FixedSingle:
                        return (num + this.FixedSingleOffset.Height);

                    case BorderStyle.Fixed3D:
                        return (num + this.Fixed3DOffset.Height);
                }
                return num;
            }
        }

        [Browsable(true)]
        public bool Blank
        {
            get
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    if (!control.Blank)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        [Browsable(true)]
        public BorderStyle BorderStyle
        {
            get
            {
                return this._borderStyle;
            }
            set
            {
                this._borderStyle = value;
                this.AdjustSize();
                base.Invalidate();
            }
        }

        [Browsable(false)]
        public override bool Focused
        {
            get
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    if (control.Focused)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private bool HasMouse
        {
            get
            {
                return this.DisplayRectangle.Contains(base.PointToClient(Control.MousePosition));
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IPAddress IPAddress
        {
            get
            {
                return new IPAddress(this.GetAddressBytes());
            }
            set
            {
                this.Clear();
                if ((value != null) && (value.AddressFamily == AddressFamily.InterNetwork))
                {
                    this.SetAddressBytes(value.GetAddressBytes());
                }
            }
        }

        [Browsable(true)]
        public override Size MinimumSize
        {
            get
            {
                return this.CalculateMinimumSize();
            }
        }

        [Browsable(true)]
        public bool ReadOnly
        {
            get
            {
                return this._readOnly;
            }
            set
            {
                this._readOnly = value;
                foreach (FieldControl control in this._fieldControls)
                {
                    control.ReadOnly = this._readOnly;
                }
                foreach (DotControl control2 in this._dotControls)
                {
                    control2.ReadOnly = this._readOnly;
                }
                base.Invalidate();
            }
        }

        [Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        public override string Text
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < this._fieldControls.Length; i++)
                {
                    builder.Append(this._fieldControls[i].Text);
                    if (i < this._dotControls.Length)
                    {
                        builder.Append(this._dotControls[i].Text);
                    }
                }
                return builder.ToString();
            }
            set
            {
                this.Parse(value);
            }
        }
    }
    internal enum MyAction
    {
        None,
        Trim,
        Home,
        End
    }

    internal class FieldControl : TextBox
    {
        // Fields
        private int _fieldIndex = -1;
        private byte _rangeLower;
        private byte _rangeUpper = 0xff;
        private TextFormatFlags _textFormatFlags = (TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.HorizontalCenter);
        public const byte MaximumValue = 0xff;
        public const byte MinimumValue = 0;

        // Events
        public event EventHandler<CedeFocusEventArgs> CedeFocusEvent;

        public event EventHandler<TextChangedEventArgs> TextChangedEvent;

        // Methods
        public FieldControl()
        {
            base.BorderStyle = BorderStyle.None;
            this.MaxLength = 3;
            base.Size = this.MinimumSize;
            base.TabStop = false;
            base.TextAlign = HorizontalAlignment.Center;
        }

        private void HandleBackspaceKey(KeyEventArgs e)
        {
            if (!base.ReadOnly && ((this.TextLength == 0) || ((base.SelectionStart == 0) && (this.SelectionLength == 0))))
            {
                this.SendCedeFocusEvent(MyAction.Trim);
                e.SuppressKeyPress = true;
            }
        }

        private static bool IsBackspaceKey(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.Back);
        }

        private bool IsCedeFocusKey(KeyEventArgs e)
        {
            return ((((e.KeyCode == Keys.OemPeriod) || (e.KeyCode == Keys.Decimal)) || (e.KeyCode == Keys.Space)) && (((this.TextLength != 0) && (this.SelectionLength == 0)) && (base.SelectionStart != 0)));
        }

        private static bool IsEditKey(KeyEventArgs e)
        {
            if (((e.KeyCode != Keys.Back) && (e.KeyCode != Keys.Delete)) && ((e.Modifiers != Keys.Control) || (((e.KeyCode != Keys.C) && (e.KeyCode != Keys.V)) && (e.KeyCode != Keys.X))))
            {
                return false;
            }
            return true;
        }

        private static bool IsEnterKey(KeyEventArgs e)
        {
            if ((e.KeyCode != Keys.Enter) && (e.KeyCode != Keys.Enter))
            {
                return false;
            }
            return true;
        }

        private static bool IsForwardKey(KeyEventArgs e)
        {
            if ((e.KeyCode != Keys.Right) && (e.KeyCode != Keys.Down))
            {
                return false;
            }
            return true;
        }

        private static bool IsNumericKey(KeyEventArgs e)
        {
            if (((e.KeyCode < Keys.NumPad0) || (e.KeyCode > Keys.NumPad9)) && ((e.KeyCode < Keys.D0) || (e.KeyCode > Keys.D9)))
            {
                return false;
            }
            return true;
        }

        private static bool IsReverseKey(KeyEventArgs e)
        {
            if ((e.KeyCode != Keys.Left) && (e.KeyCode != Keys.Up))
            {
                return false;
            }
            return true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.End:
                    this.SendCedeFocusEvent(MyAction.End);
                    return;

                case Keys.Home:
                    this.SendCedeFocusEvent(MyAction.Home);
                    return;
            }
            if (this.IsCedeFocusKey(e))
            {
                this.SendCedeFocusEvent(Direction.Forward, Selection.All);
                e.SuppressKeyPress = true;
            }
            else if (IsForwardKey(e))
            {
                if (e.Control)
                {
                    this.SendCedeFocusEvent(Direction.Forward, Selection.All);
                }
                else if ((this.SelectionLength == 0) && (base.SelectionStart == this.TextLength))
                {
                    this.SendCedeFocusEvent(Direction.Forward, Selection.None);
                }
            }
            else if (IsReverseKey(e))
            {
                if (e.Control)
                {
                    this.SendCedeFocusEvent(Direction.Reverse, Selection.All);
                }
                else if ((this.SelectionLength == 0) && (base.SelectionStart == 0))
                {
                    this.SendCedeFocusEvent(Direction.Reverse, Selection.None);
                }
            }
            else if (IsBackspaceKey(e))
            {
                this.HandleBackspaceKey(e);
            }
            else if ((!IsNumericKey(e) && !IsEditKey(e)) && !IsEnterKey(e))
            {
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            this.BackColor = base.Parent.BackColor;
        }

        protected override void OnParentForeColorChanged(EventArgs e)
        {
            base.OnParentForeColorChanged(e);
            this.ForeColor = base.Parent.ForeColor;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Size = this.MinimumSize;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (!this.Blank)
            {
                int num;
                if (!int.TryParse(this.Text, out num))
                {
                    base.Text = string.Empty;
                }
                else if (num > this.RangeUpper)
                {
                    base.Text = this.RangeUpper.ToString(CultureInfo.InvariantCulture);
                    base.SelectionStart = 0;
                }
                else if ((this.TextLength == this.MaxLength) && (num < this.RangeLower))
                {
                    base.Text = this.RangeLower.ToString(CultureInfo.InvariantCulture);
                    base.SelectionStart = 0;
                }
                else
                {
                    int textLength = this.TextLength;
                    int selectionStart = base.SelectionStart;
                    base.Text = num.ToString(CultureInfo.InvariantCulture);
                    if (this.TextLength < textLength)
                    {
                        selectionStart -= textLength - this.TextLength;
                        base.SelectionStart = Math.Max(0, selectionStart);
                    }
                }
            }
            if (this.TextChangedEvent != null)
            {
                TextChangedEventArgs args = new TextChangedEventArgs
                {
                    FieldIndex = this.FieldIndex,
                    Text = this.Text
                };
                this.TextChangedEvent(this, args);
            }
            if (((this.TextLength == this.MaxLength) && this.Focused) && (base.SelectionStart == this.TextLength))
            {
                this.SendCedeFocusEvent(Direction.Forward, Selection.All);
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            if (!this.Blank && (this.Value < this.RangeLower))
            {
                this.Text = this.RangeLower.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void SendCedeFocusEvent(MyAction action)
        {
            if (this.CedeFocusEvent != null)
            {
                CedeFocusEventArgs e = new CedeFocusEventArgs
                {
                    FieldIndex = this.FieldIndex,
                    Action = action
                };
                this.CedeFocusEvent(this, e);
            }
        }

        private void SendCedeFocusEvent(Direction direction, Selection selection)
        {
            if (this.CedeFocusEvent != null)
            {
                CedeFocusEventArgs e = new CedeFocusEventArgs
                {
                    FieldIndex = this.FieldIndex,
                    Action = MyAction.None,
                    Direction = direction,
                    Selection = selection
                };
                this.CedeFocusEvent(this, e);
            }
        }

        public void TakeFocus(MyAction action)
        {
            base.Focus();
            switch (action)
            {
                case MyAction.Trim:
                    if (this.TextLength > 0)
                    {
                        int length = this.TextLength - 1;
                        base.Text = this.Text.Substring(0, length);
                    }
                    base.SelectionStart = this.TextLength;
                    return;

                case MyAction.Home:
                    base.SelectionStart = 0;
                    this.SelectionLength = 0;
                    return;

                case MyAction.End:
                    base.SelectionStart = this.TextLength;
                    return;
            }
        }

        public void TakeFocus(Direction direction, Selection selection)
        {
            base.Focus();
            if (selection == Selection.All)
            {
                base.SelectionStart = 0;
                this.SelectionLength = this.TextLength;
            }
            else
            {
                base.SelectionStart = (direction == Direction.Forward) ? 0 : this.TextLength;
            }
        }

        public override string ToString()
        {
            return this.Value.ToString(CultureInfo.InvariantCulture);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0x7b)
            {
                base.WndProc(ref m);
            }
        }

        // Properties
        public bool Blank
        {
            get
            {
                return (this.TextLength == 0);
            }
        }

        public int FieldIndex
        {
            get
            {
                return this._fieldIndex;
            }
            set
            {
                this._fieldIndex = value;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                Graphics dc = Graphics.FromHwnd(base.Handle);
                Size size = TextRenderer.MeasureText(dc, Resources.FieldMeasureText, this.Font, base.Size, this._textFormatFlags);
                dc.Dispose();

                return size;
            }
        }

        public byte RangeLower
        {
            get
            {
                return this._rangeLower;
            }
            set
            {
                if (value < 0)
                {
                    this._rangeLower = 0;
                }
                else if (value > this._rangeUpper)
                {
                    this._rangeLower = this._rangeUpper;
                }
                else
                {
                    this._rangeLower = value;
                }
                if (this.Value < this._rangeLower)
                {
                    this.Text = this._rangeLower.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public byte RangeUpper
        {
            get
            {
                return this._rangeUpper;
            }
            set
            {
                if (value < this._rangeLower)
                {
                    this._rangeUpper = this._rangeLower;
                }
                else if (value > 0xff)
                {
                    this._rangeUpper = 0xff;
                }
                else
                {
                    this._rangeUpper = value;
                }
                if (this.Value > this._rangeUpper)
                {
                    this.Text = this._rangeUpper.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public byte Value
        {
            get
            {
                byte rangeLower;
                if (!byte.TryParse(this.Text, out rangeLower))
                {
                    rangeLower = this.RangeLower;
                }
                return rangeLower;
            }
        }
    }
    internal class DotControl : Control
    {
        // Fields
        private bool _backColorChanged;
        private bool _readOnly;
        private SizeF _sizeText;
        private StringFormat _stringFormat;

        // Methods
        public DotControl()
        {
            this.Text = Resources.FieldSeparator;
            this._stringFormat = StringFormat.GenericTypographic;
            this._stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            this.BackColor = SystemColors.Window;
            base.Size = this.MinimumSize;
            base.TabStop = false;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.FixedHeight, true);
            base.SetStyle(ControlStyles.FixedWidth, true);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            base.Size = this.MinimumSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            base.OnPaint(e);
            Color backColor = this.BackColor;
            if (!this._backColorChanged && (!base.Enabled || this.ReadOnly))
            {
                backColor = SystemColors.Control;
            }
            Color foreColor = this.ForeColor;
            if (!base.Enabled)
            {
                foreColor = SystemColors.GrayText;
            }
            else if (this.ReadOnly && !this._backColorChanged)
            {
                foreColor = SystemColors.WindowText;
            }
            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, base.ClientRectangle);
            }
            using (SolidBrush brush2 = new SolidBrush(foreColor))
            {
                float x = (((float)base.ClientRectangle.Width) / 2f) - (this._sizeText.Width / 2f);
                e.Graphics.DrawString(this.Text, this.Font, brush2, new RectangleF(x, 0f, this._sizeText.Width, this._sizeText.Height), this._stringFormat);
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            this.BackColor = base.Parent.BackColor;
            this._backColorChanged = true;
        }

        protected override void OnParentForeColorChanged(EventArgs e)
        {
            base.OnParentForeColorChanged(e);
            this.ForeColor = base.Parent.ForeColor;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Size = this.MinimumSize;
        }

        public override string ToString()
        {
            return this.Text;
        }

        // Properties
        public override Size MinimumSize
        {
            get
            {
                using (Graphics graphics = Graphics.FromHwnd(base.Handle))
                {
                    this._sizeText = graphics.MeasureString(this.Text, this.Font, -1, this._stringFormat);
                }
                this._sizeText.Height++;
                return this._sizeText.ToSize();
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this._readOnly;
            }
            set
            {
                this._readOnly = value;
                base.Invalidate();
            }
        }
    }
    internal class NativeMethods
    {
        // Methods
        private NativeMethods()
        {
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern bool DeleteObject(IntPtr hdc);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRIC lptm);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        // Nested Types
        [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }
    }



    public class FieldChangedEventArgs : EventArgs
    {
        // Fields
        private int _fieldIndex;
        private string _text;

        // Properties
        public int FieldIndex
        {
            get
            {
                return this._fieldIndex;
            }
            set
            {
                this._fieldIndex = value;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
    }
    internal class TextChangedEventArgs : EventArgs
    {
        // Fields
        private int _fieldIndex;
        private string _text;

        // Properties
        public int FieldIndex
        {
            get
            {
                return this._fieldIndex;
            }
            set
            {
                this._fieldIndex = value;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
    }

    internal enum Direction
    {
        Forward,
        Reverse
    }

    internal enum Selection
    {
        None,
        All
    }
    internal class CedeFocusEventArgs : EventArgs
    {
        // Fields
        private MyAction _action;
        private Direction _direction;
        private int _fieldIndex;
        private Selection _selection;

        // Properties
        public MyAction Action
        {
            get
            {
                return this._action;
            }
            set
            {
                this._action = value;
            }
        }

        public Direction Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                this._direction = value;
            }
        }

        public int FieldIndex
        {
            get
            {
                return this._fieldIndex;
            }
            set
            {
                this._fieldIndex = value;
            }
        }

        public Selection Selection
        {
            get
            {
                return this._selection;
            }
            set
            {
                this._selection = value;
            }
        }
    }



    internal class IPAddressControlDesigner : ControlDesigner
    {
        // Properties
        public override SelectionRules SelectionRules
        {
            get
            {
                IPAddressControl control = (IPAddressControl)this.Control;
                if (control.AutoHeight)
                {
                    return (SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.RightSizeable | SelectionRules.LeftSizeable);
                }
                return (SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.AllSizeable);
            }
        }

        public override IList SnapLines
        {
            get
            {
                IPAddressControl control = (IPAddressControl)this.Control;
                IList snapLines = base.SnapLines;
                snapLines.Add(new SnapLine(SnapLineType.Baseline, control.Baseline));
                return snapLines;
            }
        }
    }

}
