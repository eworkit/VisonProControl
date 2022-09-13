using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Utilities.Animations;

namespace Utilities.Gradients
{
	/// <summary>
	/// Class inheriting <see cref="AnimatorBase"/> to animated the
	/// <see cref="Utilities.Gradients.GradientBorder.BorderWidth"/> of a <see cref="GradientBorder"/>.
	/// </summary>
	public class GradientBorderWidthAnimator : AnimatorBase
	{
		#region Fields

		private const int DEFAULT_WIDTH = 0;
		private GradientBorder _gradientBorder;
		private int _startWidth = DEFAULT_WIDTH;
		private int _endWidth = DEFAULT_WIDTH;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="container">Container the new instance should be added to.</param>
		public GradientBorderWidthAnimator(IContainer container) : base(container) {}

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public GradientBorderWidthAnimator() {}

		#endregion

		#region Public interface

		/// <summary>
		/// Gets or sets the starting border width for the animation.
		/// </summary>
		[Description("Gets or sets the starting color for the animation.")]
		[Browsable(true), DefaultValue(DEFAULT_WIDTH), Category("Appearance")]
		public int StartWidth 
		{
			get { return _startWidth; }
			set 
			{
				if (_startWidth == value)
					return;

				_startWidth = value;

				OnStartValueChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the ending border width for the animation.
		/// </summary>
		[Description("Gets or sets the ending int for the animation.")]
		[Browsable(true), DefaultValue(DEFAULT_WIDTH), Category("Appearance")]
		public int EndWidth 
		{
			get { return _endWidth; }
			set 
			{
				if (_endWidth == value)
					return;

				_endWidth = value;

				OnEndValueChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="GradientBorder"/> which 
		/// <see cref="Utilities.Gradients.GradientBorder.BorderWidth"/> should be animated.
		/// </summary>
		[Description("Gets or sets which GradientBorder should be animated.")]
		[Browsable(true), Category("Behavior")]
		[DefaultValue(null), RefreshProperties(RefreshProperties.Repaint)]
		public GradientBorder GradientBorder
		{
			get { return _gradientBorder; }
			set
			{
				if (_gradientBorder == value)
					return;

				if (_gradientBorder != null)
					_gradientBorder.BorderWidthChanged -= new EventHandler(OnBorderWidthChanged);

				_gradientBorder = value;

				if (_gradientBorder != null)
					_gradientBorder.BorderWidthChanged += new EventHandler(OnBorderWidthChanged);

				base.ResetValues();
			}
		}

		#endregion

		#region Overridden from AnimatorBase

		/// <summary>
		/// Gets or sets the currently shown value.
		/// </summary>
		protected override object CurrentValueInternal
		{
			get { return _gradientBorder == null ? 0 : _gradientBorder.BorderWidth; }
			set 
			{
				if (_gradientBorder != null)
					_gradientBorder.BorderWidth = (int)value; 
			}
		}

		/// <summary>
		/// Gets or sets the starting value for the animation.
		/// </summary>
		public override object StartValue
		{
			get { return StartWidth; }
			set { StartWidth = (int)value; }
		}

		/// <summary>
		/// Gets or sets the ending value for the animation.
		/// </summary>
		public override object EndValue
		{
			get { return EndWidth; }
			set { EndWidth = (int)value; }
		}

		/// <summary>
		/// Calculates an interpolated value between <see cref="StartValue"/> and
		/// <see cref="EndValue"/> for a given step in %.
		/// Giving 0 will return the <see cref="StartValue"/>.
		/// Giving 100 will return the <see cref="EndValue"/>.
		/// </summary>
		/// <param name="step">Animation step in %</param>
		/// <returns>Interpolated value for the given step.</returns>
		protected override object GetValueForStep(double step)
		{
			return InterpolateIntegerValues(_startWidth, _endWidth, step);
		}

		#endregion

		#region Protected
		
		/// <summary>
		/// Indicates the designer whether <see cref="StartWidth"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeStartColor()
		{
			return _startWidth != 0;
		}

		/// <summary>
		/// Indicates the designer whether <see cref="EndWidth"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeEndColor()
		{
			return _endWidth != 0;
		}

		#endregion

		#region Privates

		private void OnBorderWidthChanged(object sender, EventArgs e)
		{
			base.SynchronizeFromSource();
		}

		#endregion
	}
}
