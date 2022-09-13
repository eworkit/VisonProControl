using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Utilities.Gradients
{
	/// <summary>
	/// Container control which paints a gradient transparent border around its interior. By setting
	/// <see cref="BorderWidth"/> the width of this border as well as the <see cref="ScrollableControl.DockPadding"/>
	/// is set. Thus a contained <see cref="Control"/> which is docked won't overlap the transparent border. 
	/// </summary>
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	public class GradientBorder : System.Windows.Forms.UserControl
	{
		#region Fields

		private const int DEFAULT_BORDER_WIDTH = 20;
		private const bool DEFAULT_AUTO_UPDATE_INTERIOR_COLORS = true;

		private Color _innerColor;
		private bool _bitmapInvalid = true;
		private Bitmap _bufferBitmap;
		private int _borderWidth = DEFAULT_BORDER_WIDTH;
		private bool _autoUpdateInteriorColors = DEFAULT_AUTO_UPDATE_INTERIOR_COLORS;

		#endregion

		#region Events

		/// <summary>
		/// Event which gets fired when <see cref="InnerColor"/> has changed.
		/// </summary>
		public event EventHandler InnerColorChanged;

		/// <summary>
		/// Event which gets fired when <see cref="BorderWidth"/> has changed.
		/// </summary>
		public event EventHandler BorderWidthChanged;

		/// <summary>
		/// Event which gets fired when <see cref="AutoUpdateInteriorColors"/> has changed.
		/// </summary>
		public event EventHandler AutoUpdateInteriorColorsChanged;

		/// <summary>
		/// Event which gets fired when a click occured in the inner area
		/// (meaning not on the border) of this instance. When a contained control
		/// is clicked this event will not fire.
		/// </summary>
		public event EventHandler InnerAreaClick;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public GradientBorder()
		{			
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			base.DockPadding.All = 20;	
			base.BackColor = Color.Transparent;

			_innerColor = DefaultInnerColor;
		}

		#endregion

		#region Public interface

		/// <summary>
		/// Gets or sets whether the <see cref="Control.BackColor"/> of contained controls
		/// should automatically be updated when the <see cref="InnerColor"/> changes.
		/// </summary>
		[Category("Appearance"), Browsable(true), DefaultValue(DEFAULT_AUTO_UPDATE_INTERIOR_COLORS)]
		[Description("Gets or sets whether the backcolor of contained controls should automatically be updated when the inner color changes.")]
		public bool AutoUpdateInteriorColors
		{
			get { return _autoUpdateInteriorColors; }
			set 
			{
				if (_autoUpdateInteriorColors == value)
					return;

				_autoUpdateInteriorColors = value;
				if (_autoUpdateInteriorColors)
					UpdateInteriorColors();
				OnAutoUpdateInteriorColorsChanged(EventArgs.Empty);
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the color of the interior and the border.
		/// </summary>
		[Category("Appearance"), Browsable(true)]
		[Description("Gets or sets the color of the interior and the border.")]
		public virtual Color InnerColor
		{
			get { return _innerColor; }
			set 
			{
				if (_innerColor == value)
					return;

				_innerColor = value;
				OnInnerColorChanged(EventArgs.Empty);
				if (_autoUpdateInteriorColors)
					UpdateInteriorColors();
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the width of the border.
		/// </summary>
		[Category("Appearance"), Browsable(true), DefaultValue(DEFAULT_BORDER_WIDTH)]
		[Description("Gets or sets the width of the border.")]
		public virtual int BorderWidth 
		{
			get { return _borderWidth; }
			set 
			{
				if (_borderWidth == value)
					return;

				_borderWidth = value;
				base.DockPadding.All = _borderWidth;
				OnBorderWidthChanged(EventArgs.Empty);
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets a rectangle containing the area of this instance which
		/// is not covered by the border.
		/// </summary>
		[Browsable(false)]
		public Rectangle InnerBounds 
		{
			get 
			{
				int width = base.Width - 2 * _borderWidth;
				int height = base.Height - 2 * _borderWidth;
				if (width > 0 && height > 0)
					return new Rectangle(_borderWidth, _borderWidth, width, height); 
				else
					return Rectangle.Empty;
			}
		}

		#endregion

		#region Protected Interface

		/// <summary>
		/// Raises the <see cref="InnerColorChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected void OnInnerColorChanged(EventArgs eventArgs)
		{
			if (InnerColorChanged != null)
				InnerColorChanged(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="BorderWidthChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected void OnBorderWidthChanged(EventArgs eventArgs)
		{
			if (BorderWidthChanged != null)
				BorderWidthChanged(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="AutoUpdateInteriorColorsChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected void OnAutoUpdateInteriorColorsChanged(EventArgs eventArgs)
		{
			if (AutoUpdateInteriorColorsChanged != null)
				AutoUpdateInteriorColorsChanged(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="InnerAreaClick"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected void OnInnerAreaClick(EventArgs eventArgs)
		{
			if (InnerAreaClick != null)
				InnerAreaClick(this, eventArgs);
		}

		/// <summary>
		/// Gets the default value of the <see cref="InnerColor"/> property.
		/// </summary>
		protected virtual Color DefaultInnerColor 
		{
			get { return Color.Red; }
		}

		/// <summary>
		/// Indicates the designer whether <see cref="InnerColor"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeDefaultInnerColor()
		{
			return _innerColor != DefaultInnerColor;
		}

		/// <summary>
		/// Indicates the designer whether <see cref="BackColor"/> needs
		/// to be serialized. Always return false.
		/// </summary>
		protected virtual bool ShouldSerializeBackColor()
		{
			return false;
		}

		#endregion

		#region Privates

		private void UpdateInteriorColors()
		{
			foreach (Control control in base.Controls)
				control.BackColor = _innerColor;
		}

		private unsafe void PaintTo(Bitmap bitmap)
		{
			int height = base.Height;
			int width = base.Width;

			Color innerColor = _innerColor;
			int gradientWidth = _borderWidth;
			if (_borderWidth > height / 2)
			{
				int alpha = ((gradientWidth - height / 2) * -_innerColor.A / gradientWidth + _innerColor.A);
				innerColor = Color.FromArgb(alpha, innerColor.R, innerColor.G, innerColor.B);
				gradientWidth = height / 2;
			}
//			using (Graphics g = Graphics.FromImage(bitmap))
//			{
//				g.Clear(innerColor);
//			}

			BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			int scan0 = data.Scan0.ToInt32();

			byte * colPixel;
			byte * rowPixel;

			byte innerColorB = innerColor.B;
			byte innerColorG = innerColor.G;
			byte innerColorR = innerColor.R;
			byte innerColorA = innerColor.A;

			int stride = data.Stride;

//			for (int y = 0; y < height; y++)
//			{
//				rowPixel = (byte *)(scan0 + y * stride);
//				for (int x = 0; x < width; x++)
//				{
//					colPixel = rowPixel + 4 * x;
//					*colPixel = innerColorB;
//					*(colPixel + 1) = innerColorG;
//					*(colPixel + 2) = innerColorR;
//					*(colPixel + 3) = innerColorA;
//				}
//			}
			
//			byte * p = (byte *)(void *)data.Scan0;
//			int nOffset = data.Stride - bitmap.Width * 4; 
//			for (int y = 0; y < height; ++y)
//			{
//				for (int x = 0; x < width; ++x)
//				{
//					p[0] = innerColorB;
//					p[1] = innerColorG;
//					p[2] = innerColorR;
//					p[3] = innerColorA;
//					p += 4;
//				}
//				p += nOffset;
//			}
			

			long * pl = (long *)(void *)data.Scan0;
			int * pi;
			byte[] bytes = new byte[] { innerColorB, innerColorG, innerColorR, innerColorA, innerColorB, innerColorG, innerColorR, innerColorA };
			long lval = BitConverter.ToInt64(bytes, 0);
			bytes = new byte[] { innerColorB, innerColorG, innerColorR, innerColorA };
			int ival = BitConverter.ToInt32(bytes, 0);
			int halfWidth = width / 2;
			int loopWidth = halfWidth;
			if (width % 2 != 0)
				loopWidth = (width - 1) / 2;
			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < loopWidth; ++x)
				{
					pl[0] = lval;
					++pl;
				}
				if (width % 2 == 0)
				{
					pi = (int *)pl;
					--pi;
					pi[0] = ival;
				}
			}

			//top
			for (int y = 0; y < gradientWidth; y ++)
			{
				rowPixel = (byte *)(scan0 + y * stride);
				byte rowA = (byte)((gradientWidth - y) * -innerColorA / gradientWidth + innerColorA);
				
				for (int x = gradientWidth; x < width - gradientWidth; x++)
				{
					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = rowA;
				}
			}

			//bottom
			for (int y = height - gradientWidth; y < height; y ++)
			{
				rowPixel = (byte *)(scan0 + y * stride);
				byte rowA = (byte)((y - height + gradientWidth) * -innerColorA / gradientWidth + innerColorA);
				
				for (int x = gradientWidth; x < width - gradientWidth; x++)
				{
					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = rowA;
				}
			}

			//left
			for (int x = 0; x < gradientWidth; x++)
			{
				byte colA = (byte)((gradientWidth - x) * -innerColorA / gradientWidth + innerColorA);

				for (int y = gradientWidth; y < height - gradientWidth; y++)
				{
					rowPixel = (byte *)(scan0 + y * stride);
				
					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = colA;
				}
			}

			//right
			for (int x = width - gradientWidth; x < width; x++)
			{
				byte colA = (byte)((x - width + gradientWidth) * -innerColorA / gradientWidth + innerColorA);

				for (int y = gradientWidth; y < height - gradientWidth; y ++)
				{
					rowPixel = (byte *)(scan0 + y * stride);
				
					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = colA;
				}
			}

			//top left
			for (int y = 0; y < gradientWidth; y ++)
			{
				rowPixel = (byte *)(scan0 + y * stride);
				
				for (int x = 0; x < gradientWidth; x++)
				{
					double pixelDistance = Math.Sqrt(Math.Pow(gradientWidth - x, 2) + Math.Pow(gradientWidth - y, 2));

					byte pixelA = (byte)(pixelDistance >= gradientWidth ? 0 : ((pixelDistance) * -innerColorA / gradientWidth + innerColorA));

					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = pixelA;
				}
			}

			//top right
			for (int y = 0; y < gradientWidth; y ++)
			{
				rowPixel = (byte *)(scan0 + y * stride);
				
				for (int x = width - gradientWidth; x < width; x++)
				{
					double pixelDistance = Math.Sqrt(Math.Pow(gradientWidth - width + x, 2) + Math.Pow(gradientWidth - y, 2));

					byte pixelA = (byte)(pixelDistance >= gradientWidth ? 0 : ((pixelDistance) * -innerColorA / gradientWidth + innerColorA));

					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = pixelA;
				}
			}

			//bottom left
			for (int y = height - gradientWidth; y < height; y ++)
			{
				rowPixel = (byte *)(scan0 + y * stride);
				
				for (int x = 0; x < gradientWidth; x++)
				{
					double pixelDistance = Math.Sqrt(Math.Pow(gradientWidth - x, 2) + Math.Pow(gradientWidth - height + y, 2));

					byte pixelA = (byte)(pixelDistance >= gradientWidth ? 0 : ((pixelDistance) * -innerColorA / gradientWidth + innerColorA));

					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = pixelA;
				}
			}

			//bottom right
			for (int y = height - gradientWidth; y < height; y ++)
			{
				rowPixel = (byte *)(scan0 + y * stride);
				
				for (int x = width - gradientWidth; x < width; x++)
				{
					double pixelDistance = Math.Sqrt(Math.Pow(gradientWidth - width + x, 2) + Math.Pow(gradientWidth - height + y, 2));

					byte pixelA = (byte)(pixelDistance >= gradientWidth ? 0 : ((pixelDistance) * -innerColorA / gradientWidth + innerColorA));

					colPixel = rowPixel + 4 * x;
					*(colPixel + 3) = pixelA;
				}
			}

			bitmap.UnlockBits(data);
		}

		/// <summary>
		/// Invalidates the control and causes a paint message being sent to it.
		/// </summary>
		public new void Invalidate()
		{
			_bitmapInvalid = true;
			base.Invalidate();
		}

		#endregion

		#region Overridden from UserControl

		/// <summary>
		/// Overriden to hide the property from the designer and to
		/// ignore changes.
		/// </summary>
		[Browsable(false)]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set {}
		}

		/// <summary>
		/// Raises the <see cref="InnerColorChanged"/> event and optionally
		/// set the <see cref="Control.BackColor"/> of the added <see cref="Control"/>.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if (_autoUpdateInteriorColors)
				e.Control.BackColor = _innerColor;
		}

		/// <summary>
		/// Raises the <see cref="Control.Click"/> event and the
		/// <see cref="InnerAreaClick"/> event if the click didn't
		/// occur within the border.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Point point = base.PointToClient(Cursor.Position);
			if (point.X >= _borderWidth && point.X <= base.Width - _borderWidth
				&& point.Y >= _borderWidth && point.Y <= base.Height - _borderWidth)
			{
				OnInnerAreaClick(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Paints the border.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			if (_bitmapInvalid)
			{
				if (_bufferBitmap == null || _bufferBitmap.Size != this.Size)
					_bufferBitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
				PaintTo(_bufferBitmap);
				_bitmapInvalid = false;
			}

			e.Graphics.DrawImageUnscaled(_bufferBitmap, 0, 0);
		}

		/// <summary>
		/// Raises the <see cref="Control.Layout"/> event and invalidates
		/// this instance.
		/// </summary>
		/// <param name="levent">Event data.</param>
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			this.Invalidate();
		}

		#endregion
	}
}
