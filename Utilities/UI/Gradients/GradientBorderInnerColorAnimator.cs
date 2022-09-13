using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Utilities.Animations;

namespace Utilities.Gradients
{
	/// <summary>
	/// Class inheriting <see cref="AnimatorBase"/> to animated the
	/// <see cref="Utilities.Gradients.GradientBorder.InnerColor"/> of a <see cref="GradientBorder"/>.
	/// </summary>
	public class GradientBorderInnerColorAnimator : AnimatorBase
	{
		#region Fields

		private GradientBorder _gradientBorder;
		private Color _startColor = Color.Empty;
		private Color _endColor = Color.Empty;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="container">Container the new instance should be added to.</param>
		public GradientBorderInnerColorAnimator(IContainer container) : base(container) 
		{
			Initialize();
		}

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public GradientBorderInnerColorAnimator() 
		{
			Initialize();
		}

		private void Initialize()
		{
			_startColor = DefaultStartColor;
			_endColor = DefaultEndColor;
		}

		#endregion

		#region Public interface

		/// <summary>
		/// Gets or sets the starting color for the animation.
		/// </summary>
		[Browsable(true), Category("Appearance")]
		[Description("Gets or sets the starting color for the animation.")]
		public Color StartColor 
		{
			get { return _startColor; }
			set 
			{
				if (_startColor == value)
					return;

				_startColor = value;

				OnStartValueChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the ending Color for the animation.
		/// </summary>
		[Browsable(true), Category("Appearance")]
		[Description("Gets or sets the starting color for the animation.")]
		public Color EndColor 
		{
			get { return _endColor; }
			set 
			{
				if (_endColor == value)
					return;

				_endColor = value;

				OnEndValueChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="GradientBorder"/> which 
		/// <see cref="Utilities.Gradients.GradientBorder.InnerColor"/> should be animated.
		/// </summary>
		[Description("Gets or sets which GradientBorder should be animated.")]
		[DefaultValue(null), RefreshProperties(RefreshProperties.Repaint), Category("Behavior")]
		public GradientBorder GradientBorder
		{
			get { return _gradientBorder; }
			set
			{
				if (_gradientBorder == value)
					return;

				if (_gradientBorder != null)
					_gradientBorder.InnerColorChanged -= new EventHandler(OnControlColorChanged);

				_gradientBorder = value;

				if (_gradientBorder != null)
					_gradientBorder.InnerColorChanged += new EventHandler(OnControlColorChanged);

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
			get { return _gradientBorder == null ? Color.Empty : _gradientBorder.InnerColor; }
			set 
			{
				if (_gradientBorder != null)
					_gradientBorder.InnerColor = (Color)value; 
			}
		}

		/// <summary>
		/// Gets or sets the starting value for the animation.
		/// </summary>
		public override object StartValue
		{
			get { return StartColor; }
			set { StartColor = (Color)value; }
		}

		/// <summary>
		/// Gets or sets the ending value for the animation.
		/// </summary>
		public override object EndValue
		{
			get { return EndColor; }
			set { EndColor = (Color)value; }
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
			if (_startColor == Color.Empty || _endColor == Color.Empty)
				return CurrentValue;

			return InterpolateColors(_startColor, _endColor, step);
		}

		#endregion

		#region Protected

		/// <summary>
		/// Gets the default value of the <see cref="StartColor"/> property.
		/// </summary>
		protected virtual Color DefaultStartColor
		{
			get { return Color.Empty; }
		}

		/// <summary>
		/// Gets the default value of the <see cref="EndColor"/> property.
		/// </summary>
		protected virtual Color DefaultEndColor
		{
			get { return Color.Empty; }
		}
		
		/// <summary>
		/// Indicates the designer whether <see cref="StartColor"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeStartColor()
		{
			return _startColor != DefaultStartColor;
		}

		/// <summary>
		/// Indicates the designer whether <see cref="EndColor"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeEndColor()
		{
			return _endColor != DefaultEndColor;
		}

		#endregion

		#region Privates

		private void OnControlColorChanged(object sender, EventArgs e)
		{
			base.SynchronizeFromSource();
		}

		#endregion
	}
}
