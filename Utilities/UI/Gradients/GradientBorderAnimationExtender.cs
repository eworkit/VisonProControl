using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace Utilities.Gradients
{
	/// <summary>
	/// Helper class for easy runtime animation of <see cref="GradientBorder"/>s.
	/// Internalyl it uses <see cref="Utilities.Gradients.GradientBorderInnerColorAnimator"/>,
	/// <see cref="Utilities.Animations.ControlForeColorAnimator"/> and
	/// <see cref="Utilities.Gradients.GradientBorderWidthAnimator"/> to accomplish
	/// the animation.
	/// </summary>
	public class GradientBorderAnimationExtender : System.ComponentModel.Component
	{
		#region Events

		/// <summary>
		/// Event which gets fired when animation has been started.
		/// </summary>
		public event EventHandler AnimationStarted;
		
		/// <summary>
		/// Event which gets fired when animation has finished running.
		/// </summary>
		public event EventHandler AnimationFinished;

		#endregion

		#region Fields

		private const bool DEFAULT_IS_ANIMATED = true;

		private Utilities.Gradients.GradientBorderInnerColorAnimator _animatorInnerColor;
		private Utilities.Animations.ControlForeColorAnimator _animatorForeColor;
		private Utilities.Gradients.GradientBorderWidthAnimator _animatorBorderWidth;
		private System.ComponentModel.IContainer components;
		private bool _isAnimated = DEFAULT_IS_ANIMATED;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="container">Container the new instance should be added to.</param>
		public GradientBorderAnimationExtender(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public GradientBorderAnimationExtender()
		{
			InitializeComponent();
		}

		#endregion

		#region Overridden from Component

		/// <summary>
		/// Frees used resources.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Vom Komponenten-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._animatorInnerColor = new Utilities.Gradients.GradientBorderInnerColorAnimator(this.components);
			this._animatorForeColor = new Utilities.Animations.ControlForeColorAnimator(this.components);
			this._animatorBorderWidth = new Utilities.Gradients.GradientBorderWidthAnimator(this.components);
			((System.ComponentModel.ISupportInitialize)(this._animatorInnerColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._animatorForeColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._animatorBorderWidth)).BeginInit();
			// 
			// _animatorInnerColor
			// 
			this._animatorInnerColor.NeverEndingTimer = true;
			this._animatorInnerColor.ParentAnimator = this._animatorForeColor;
			this._animatorInnerColor.StepSize = 5;
			// 
			// _animatorForeColor
			// 
			this._animatorForeColor.NeverEndingTimer = true;
			this._animatorForeColor.StepSize = 5;
			this._animatorForeColor.AnimationStarted += new System.EventHandler(this.OnAnimationStarted);
			this._animatorForeColor.AnimationFinished += new System.EventHandler(this.OnAnimationFinished);
			// 
			// _animatorBorderWidth
			// 
			this._animatorBorderWidth.NeverEndingTimer = true;
			this._animatorBorderWidth.ParentAnimator = this._animatorForeColor;
			this._animatorBorderWidth.StepSize = 5;
			((System.ComponentModel.ISupportInitialize)(this._animatorInnerColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._animatorForeColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._animatorBorderWidth)).EndInit();

		}
		#endregion

		#region Public interface

		/// <summary>
		/// Gets or sets the <see cref="GradientBorder"/> which sould be animated.
		/// </summary>
		[Description("Gets or sets the GradientBorder which sould be animated.")]
		[Browsable(true), DefaultValue(null), Category("Behavior")]
		public GradientBorder GradientBorder
		{
			get { return _animatorBorderWidth.GradientBorder; }
			set 
			{
				_animatorBorderWidth.GradientBorder = value;
				_animatorForeColor.Control = value;
				_animatorInnerColor.GradientBorder = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of each step (in %) when updating the animation.
		/// </summary>
		[Description("Gets or sets the size of each step (in %) when updating the animation.")]
		[Browsable(true), DefaultValue(Utilities.Animations.AnimatorBase.DEFAULT_STEP_SIZE), Category("Behavior")]
		public double StepSize 
		{
			get { return _animatorForeColor.StepSize; }
			set { _animatorForeColor.StepSize = value; }
		}

		/// <summary>
		/// Gets or sets the intervall (in milliseconds) between updates to the animation.
		/// </summary>
		[Description("Gets or sets the intervall (in milliseconds) between updates to the animation.")]
		[Browsable(true), DefaultValue(Utilities.Animations.AnimatorBase.DEFAULT_INTERVALL), Category("Behavior")]
		public int Intervall 
		{
			get { return _animatorForeColor.Intervall; }
			set { _animatorForeColor.Intervall = value; }
		}

		/// <summary>
		/// Gets or sets whether <see cref="Change"/> calls are done immediatly
		/// or animated.
		/// </summary>
		[Description("Gets or sets whether changes are done immediatly or animated.")]
		[Browsable(true), DefaultValue(DEFAULT_IS_ANIMATED), Category("Behavior")]
		public bool IsAnimated
		{
			get { return _isAnimated; }
			set { _isAnimated = value; }
		}

		/// <summary>
		/// Gets whether an animation is currently running.
		/// </summary>
		[Browsable(false)]
		public bool IsRunning
		{
			get { return _animatorForeColor.IsRunning; }
		}

		/// <summary>
		/// Changes the properties of the set <see cref="GradientBorder"/> to the new values.
		/// </summary>
		/// <param name="innerColor">The new <see cref="Utilities.Gradients.GradientBorder.InnerColor"/> value.</param>
		/// <param name="borderWidth">The new <see cref="Utilities.Gradients.GradientBorder.BorderWidth"/> value.</param>
		/// <param name="foreColor">The new <see cref="System.Windows.Forms.Control.ForeColor"/> value.</param>
		public void Change(Color innerColor, int borderWidth, Color foreColor)
		{
			if (this.GradientBorder == null)
				return;

			if (_isAnimated) 
			{
				_animatorInnerColor.StartColor = this.GradientBorder.InnerColor;
				_animatorInnerColor.EndColor = innerColor;
				_animatorBorderWidth.StartWidth = this.GradientBorder.BorderWidth;
				_animatorBorderWidth.EndWidth = borderWidth;
				_animatorForeColor.StartColor = this.GradientBorder.ForeColor;
				_animatorForeColor.EndColor = foreColor;
				_animatorForeColor.Start();
			} 
			else 
			{
				this.GradientBorder.InnerColor = innerColor;
				this.GradientBorder.BorderWidth = borderWidth;
				this.GradientBorder.ForeColor = foreColor;
			}
		}

		#endregion

		#region Privates

		private void OnAnimationStarted(object sender, System.EventArgs e)
		{
			OnAnimationStarted(e);
		}

		private void OnAnimationFinished(object sender, System.EventArgs e)
		{
			OnAnimationFinished(e);
		}

		private void OnAnimationStarted(System.EventArgs e)
		{
			if (AnimationStarted != null)
				AnimationStarted(this, e);
		}

		private void OnAnimationFinished(System.EventArgs e)
		{
			if (AnimationFinished != null)
				AnimationFinished(this, e);
		}

		#endregion
	}
}
