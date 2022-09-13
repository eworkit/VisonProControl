using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Utilities.Gradients
{
	/// <summary>
	/// Class inheriting from <see cref="GradientBorder"/> which adds a text.
	/// </summary>
	public class GradientBorderLabel : GradientBorder
	{
		#region Events

		/// <summary>
		/// Event which gets fired when <see cref="TextAlign"/> has changed.
		/// </summary>
		public event EventHandler TextAlignChanged;

		#endregion

		#region Fields

		private const ContentAlignment DEFAULT_TEXT_ALIGNMENT = ContentAlignment.MiddleCenter;
		private System.ComponentModel.Container components = null;
		private ContentAlignment _textAlign = DEFAULT_TEXT_ALIGNMENT;
		private string _text;
		private StringFormat _stringFormat;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public GradientBorderLabel() : base()
		{
			InitializeComponent();
		}
		#endregion

		#region Vom Komponenten-Designer generierter Code
		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// GradientBorderLabel
			// 
			this.Name = "GradientBorderLabel";
			this.Size = new System.Drawing.Size(176, 88);

		}
		#endregion

		#region Overridden from GradientBorder

		/// <summary>
		/// Frees used resources.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
			ClearStringFormat();

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
	
		/// <summary>
		/// Adds the painting of the text to the base implementation.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (base.InnerBounds.IsEmpty)
				return;

			PaintText(e.Graphics);
		}

		/// <summary>
		/// Reinitializes the internal string format of the text.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			ClearStringFormat();
		}

		/// <summary>
		/// Redirects the given text to the contained <see cref="Label"/>.
		/// </summary>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get { return base.Text; }
			set 
			{
				base.Text = value;
				_text = value;
				base.Invalidate();
			}
		}

		#endregion

		#region Public Interface

		/// <summary>
		/// Gets or sets the alignment of the text.
		/// </summary>
		[Description("Gets or sets the alignment of the text.")]
		[Browsable(true), DefaultValue(DEFAULT_TEXT_ALIGNMENT), Category("Appearance")]
		public ContentAlignment TextAlign 
		{
			get { return _textAlign; }
			set 
			{
				if (_textAlign == value)
					return;

				_textAlign = value;
				ClearStringFormat();
				base.Invalidate();
				OnTextAlignChanged(EventArgs.Empty);
			}
		}

		#endregion

		#region Protected Interface

		/// <summary>
		/// Raises the <see cref="TextAlignChanged"/> event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnTextAlignChanged(EventArgs eventArgs)
		{
			if (TextAlignChanged != null)
				TextAlignChanged(this, eventArgs);
		}

		#endregion

		#region Privates

		private void PaintText(Graphics g)
		{
			if (_text == null || _text.Length == 0)
				return;

			Color color = g.GetNearestColor(base.Enabled ? this.ForeColor : this.InnerColor);

			if (base.Enabled)
			{
				using (Brush brush = new SolidBrush(color))
				{
					g.DrawString(_text, this.Font, brush, base.InnerBounds, GetStringFormat());
				}
			} 
			else 
			{
				ControlPaint.DrawStringDisabled(g, _text, this.Font, color, base.ClientRectangle, GetStringFormat());
			}
		}
		
		private void ClearStringFormat()
		{
			if (_stringFormat != null)
			{
				_stringFormat.Dispose();
				_stringFormat = null;
			}
		}
		
		private StringFormat GetStringFormat()
		{
			if (_stringFormat == null)
				_stringFormat = CreateStringFormat();
			return _stringFormat;
		}
		
		private StringFormat CreateStringFormat()
		{
			StringFormat stringFormat = new StringFormat();

			if ((this.TextAlign & (ContentAlignment.BottomRight | 
				ContentAlignment.MiddleRight | ContentAlignment.TopRight))
				!= (ContentAlignment)0)
				stringFormat.Alignment = StringAlignment.Far;
			else if ((this.TextAlign & (ContentAlignment.BottomCenter | 
				ContentAlignment.MiddleCenter | ContentAlignment.TopCenter))
				!= (ContentAlignment)0)
				stringFormat.Alignment = StringAlignment.Center;
			else
				stringFormat.Alignment = StringAlignment.Near;

			if ((this.TextAlign & (ContentAlignment.BottomRight | 
				ContentAlignment.BottomCenter | ContentAlignment.BottomLeft))
				!= (ContentAlignment)0)
				stringFormat.LineAlignment = StringAlignment.Far;
			else if ((this.TextAlign & (ContentAlignment.MiddleCenter | 
				ContentAlignment.MiddleLeft | ContentAlignment.MiddleRight))
				!= (ContentAlignment)0)
				stringFormat.LineAlignment = StringAlignment.Center;
			else
				stringFormat.LineAlignment = StringAlignment.Near;

			if (this.RightToLeft == RightToLeft.Yes)
				stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

			return stringFormat;
		}

		#endregion
	}
}
