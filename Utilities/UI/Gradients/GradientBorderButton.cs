using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

using Utilities.Animations;

namespace Utilities.Gradients
{
	/// <summary>
	/// Class deriving from <see cref="GradientBorder"/> which shows a text in it's centre
	/// and is able to animate it's <see cref="GradientBorder.BorderWidth"/> and
	/// <see cref="GradientBorder.InnerColor"/> when the mouse is moved above it.
	/// When the contained <see cref="Label"/> is clicked the <see cref="GradientBorder.InnerAreaClick"/>
	/// event is raised.
	/// </summary>
	public class GradientBorderButton : Utilities.Gradients.GradientBorderLabel
	{
		#region Fields

		private const int DEFAULT_BORDER_WIDTH_UNFOCUSED = 30;
		private const int DEFAULT_BORDER_WIDTH_FOCUSED = 10;
		private const bool DEFAULT_IS_ANIMATION_ENABLED = true;
		private System.ComponentModel.IContainer components = null;
		private int _borderWidthUnfocused = DEFAULT_BORDER_WIDTH_UNFOCUSED;
		private int _borderWidthFocused = DEFAULT_BORDER_WIDTH_FOCUSED;
		private Color _innerColorFocused;
		private Color _innerColorUnfocused;
		private Color _foreColorFocused;
		private Color _foreColorUnfocused;
		private bool _isFocused;
		private bool _isAnimationEnabled = DEFAULT_IS_ANIMATION_ENABLED;
		private Utilities.Gradients.GradientBorderInnerColorAnimator _innerColorAnimator;
		private Utilities.Gradients.GradientBorderWidthAnimator _borderWidthAnimator;
		private ControlForeColorAnimator _foreColorAnimator;

		#endregion

		#region Events

		/// <summary>
		/// Event which gets fired when animation has been started.
		/// </summary>
		public event EventHandler AnimationStarted;
		
		/// <summary>
		/// Event which gets fired when animation has finished running.
		/// </summary>
		public event EventHandler AnimationFinished;

		/// <summary>
		/// Event which gets fired when <see cref="IsFocused"/> has changed.
		/// </summary>
		public event EventHandler IsFocusedChanged;

		/// <summary>
		/// Event which gets fired when <see cref="IsAnimationEnabled"/> has changed.
		/// </summary>
		public event EventHandler IsAnimationEnabledChanged;

		/// <summary>
		/// Event which gets fired when any of the focused/unfocused properties has changed.
		/// </summary>
		public event EventHandler AnimationSettingsChanged;

		/// <summary>
		/// Event which gets fired when <see cref="AnimationStepSize"/> has changed.
		/// </summary>
		public event EventHandler AnimationStepSizeChanged;

		/// <summary>
		/// Event which gets fired when <see cref="AnimationIntervall"/> has changed.
		/// </summary>
		public event EventHandler AnimationIntervallChanged;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public GradientBorderButton() : base()
		{
			InitializeComponent();

			base.BorderWidth = DEFAULT_BORDER_WIDTH_UNFOCUSED;
			base.InnerColor = DefaultInnerColorUnfocused;
			_innerColorFocused = DefaultInnerColorFocused;
			_innerColorUnfocused = DefaultInnerColorUnfocused;
			_foreColorFocused = DefaultForeColorFocused;
			_foreColorUnfocused = DefaultForeColorUnfocused;
		}

		#endregion

		#region Vom Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._innerColorAnimator = new Utilities.Gradients.GradientBorderInnerColorAnimator(this.components);
			this._foreColorAnimator = new Utilities.Animations.ControlForeColorAnimator(this.components);
			this._borderWidthAnimator = new Utilities.Gradients.GradientBorderWidthAnimator(this.components);
			((System.ComponentModel.ISupportInitialize)(this._innerColorAnimator)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._foreColorAnimator)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._borderWidthAnimator)).BeginInit();
			// 
			// _innerColorAnimator
			// 
			this._innerColorAnimator.GradientBorder = this;
			// 
			// _foreColorAnimator
			// 
			this._foreColorAnimator.Control = this;
			this._foreColorAnimator.EndColor = System.Drawing.SystemColors.ControlText;
			this._foreColorAnimator.ParentAnimator = this._innerColorAnimator;
			this._foreColorAnimator.StartColor = System.Drawing.SystemColors.ControlText;
			// 
			// _borderWidthAnimator
			// 
			this._borderWidthAnimator.GradientBorder = this;
			this._borderWidthAnimator.ParentAnimator = this._innerColorAnimator;
			// 
			// GradientBorderButton
			// 
			this.BorderWidth = 30;
			this.DockPadding.All = 30;
			this.Name = "GradientBorderButton";
			((System.ComponentModel.ISupportInitialize)(this._innerColorAnimator)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._foreColorAnimator)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._borderWidthAnimator)).EndInit();

		}
		#endregion

		#region Overridden from GradientBorder

		/// <summary>
		/// Frees used resources.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseEnter"/> event and starts (if
		/// necessary) an animation.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);			
			
			_borderWidthAnimator.EndWidth = _borderWidthFocused;
			_innerColorAnimator.EndColor = _innerColorFocused;
			_foreColorAnimator.EndColor = _foreColorFocused;

			if (_isAnimationEnabled)
				_innerColorAnimator.Start();

			_isFocused = true;
			OnIsFocusedChanged(EventArgs.Empty);
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseLeave"/> event and starts (if
		/// necessary) an animation.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);			
			
			_borderWidthAnimator.EndWidth = _borderWidthUnfocused;
			_innerColorAnimator.EndColor = _innerColorUnfocused;
			_foreColorAnimator.EndColor = _foreColorUnfocused;

			if (_isAnimationEnabled)
				_innerColorAnimator.Start();

			_isFocused = false;
			OnIsFocusedChanged(EventArgs.Empty);
		}
		
		/// <summary>
		/// Overridden property to disable designer support.
		/// </summary>
		[Browsable(false)]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set 
			{
				base.ForeColor = value;
				base.Invalidate();
			}
		}

		/// <summary>
		/// Overridden property to disable designer support.
		/// </summary>
		[Browsable(false)]
		public override Color InnerColor
		{
			get { return base.InnerColor; }
			set { base.InnerColor = value; }
		}

		/// <summary>
		/// Overridden property to disable designer support.
		/// </summary>
		[Browsable(false)]
		public override int BorderWidth
		{
			get { return base.BorderWidth; }
			set { base.BorderWidth = value; }
		}

		#endregion

		#region Public interface

		/// <summary>
		/// Gets or sets the desired <see cref="GradientBorder.BorderWidth"/> when
		/// the cursor is located above the button.
		/// </summary>
		[Description("Gets or sets the desired border width when the cursor is located above the button.")]
		[Browsable(true), DefaultValue(DEFAULT_BORDER_WIDTH_FOCUSED), Category("Appearance")]
		public int BorderWidthFocused 
		{
			get { return _borderWidthFocused; }
			set 
			{
				if (_borderWidthFocused != value)
				{
					_borderWidthFocused = value;
					OnAnimationSettingsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets or sets the desired <see cref="GradientBorder.BorderWidth"/> when
		/// the cursor is not located above the button.
		/// </summary>
		[Description("Gets or sets the desired border width when the cursor is not located above the button.")]
		[Browsable(true), DefaultValue(DEFAULT_BORDER_WIDTH_UNFOCUSED), Category("Appearance")]
		public int BorderWidthUnfocused 
		{
			get { return _borderWidthUnfocused; }
			set 
			{
				if (_borderWidthUnfocused != value)
				{
					_borderWidthUnfocused = value;
					this.BorderWidth = _borderWidthUnfocused;
					OnAnimationSettingsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets or sets the desired <see cref="GradientBorder.InnerColor"/> when
		/// the cursor is located above the button.
		/// </summary>
		[Description("Gets or sets the desired inner color when the cursor is located above the button.")]
		[Browsable(true), Category("Appearance")]
		public Color InnerColorFocused
		{
			get { return _innerColorFocused; }
			set 
			{
				if (_innerColorFocused != value)
				{
					_innerColorFocused = value;
					OnAnimationSettingsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets or sets the desired <see cref="GradientBorder.InnerColor"/> when
		/// the cursor is not located above the button.
		/// </summary>
		[Description("Gets or sets the desired inner color when the cursor is not located above the button.")]
		[Browsable(true), Category("Appearance")]
		public Color InnerColorUnfocused
		{
			get { return _innerColorUnfocused; }
			set 
			{
				if (_innerColorUnfocused != value)
				{
					_innerColorUnfocused = value;
					this.InnerColor = _innerColorUnfocused;
					OnAnimationSettingsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets or sets the desired <see cref="Control.ForeColor"/> when
		/// the cursor is not located above the button.
		/// </summary>
		[Description("Gets or sets the desired forecolor when the cursor is located above the button.")]
		[Browsable(true), Category("Appearance")]
		public Color ForeColorFocused
		{
			get { return _foreColorFocused; }
			set 
			{
				if (_foreColorFocused != value)
				{
					_foreColorFocused = value;
					OnAnimationSettingsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets or sets the desired <see cref="Control.ForeColor"/> when
		/// the cursor is located above the button.
		/// </summary>
		[Description("Gets or sets the desired forecolor when the cursor is not located above the button.")]
		[Browsable(true), Category("Appearance")]
		public Color ForeColorUnfocused
		{
			get { return _foreColorUnfocused; }
			set 
			{
				if (_foreColorUnfocused != value)
				{
					_foreColorUnfocused = value;
					this.ForeColor = _foreColorUnfocused;
					OnAnimationSettingsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets whether the button is currently focused. In this context it means
		/// if the cursor is located above the button.
		/// </summary>
		[Browsable(false)]
		public bool IsFocused
		{
			get { return _isFocused; }
		}

		/// <summary>
		/// Gets or sets whether the button anmates when the cursor is
		/// moved above it. Any animation in process will be continued.
		/// </summary>
		[Description("Gets or sets whether the button anmates when the cursor is moved above it. Any animation in process will be continued.")]
		[Browsable(true), DefaultValue(DEFAULT_IS_ANIMATION_ENABLED), Category("Behavior")]
		public bool IsAnimationEnabled 
		{
			get { return _isAnimationEnabled; }
			set 
			{
				if (_isAnimationEnabled != value)
				{
					_isAnimationEnabled = value;
					OnIsAnimationEnabledChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Gets whether the button is currently animating.
		/// </summary>
		[Browsable(false)]
		public bool IsAnimationRunning 
		{
			get { return _innerColorAnimator.IsRunning; }
		}

		/// <summary>
		/// Gets or sets the intervall (in milliseconds) between updates to the animation.
		/// </summary>
		[Description("Gets or sets the intervall (in milliseconds) between updates to the animation.")]
		[Browsable(true), DefaultValue(Utilities.Animations.AnimatorBase.DEFAULT_INTERVALL), Category("Behavior")]
		public int AnimationIntervall 
		{
			get { return _innerColorAnimator.Intervall; }
			set { _innerColorAnimator.Intervall = value; }
		}

		/// <summary>
		/// Gets or sets the size of each step (in %) when updating the animation.
		/// </summary>
		[Description("Gets or sets the size of each step (in %) when updating the animation.")]
		[Browsable(true), DefaultValue(Utilities.Animations.AnimatorBase.DEFAULT_STEP_SIZE), Category("Behavior")]
		public double AnimationStepSize
		{
			get { return _innerColorAnimator.StepSize; }
			set { _innerColorAnimator.StepSize = value; }
		}

		#endregion

		#region Protected

		#region Eventhandling

		/// <summary>
		/// Raises the <see cref="AnimationStarted"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected virtual void OnAnimationStarted(EventArgs eventArgs)
		{
			if (AnimationStarted != null)
				AnimationStarted(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="AnimationFinished"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected virtual void OnAnimationFinished(EventArgs eventArgs)
		{
			if (AnimationFinished != null)
				AnimationFinished(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="IsFocusedChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected virtual void OnIsFocusedChanged(EventArgs eventArgs)
		{
			if (IsFocusedChanged != null)
				IsFocusedChanged(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="IsAnimationEnabledChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected virtual void OnIsAnimationEnabledChanged(EventArgs eventArgs)
		{
			if (IsAnimationEnabledChanged != null)
				IsAnimationEnabledChanged(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="AnimationSettingsChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected virtual void OnAnimationSettingsChanged(EventArgs eventArgs)
		{
			if (AnimationSettingsChanged != null)
				AnimationSettingsChanged(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="AnimationStepSizeChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected virtual void OnAnimationStepSizeChanged(EventArgs eventArgs)
		{
			if (AnimationStepSizeChanged != null)
				AnimationStepSizeChanged(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="AnimationIntervallChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">Event data.</param>
		protected virtual void OnAnimationIntervallChanged(EventArgs eventArgs)
		{
			if (AnimationIntervallChanged != null)
				AnimationIntervallChanged(this, eventArgs);
		}

		#endregion

		#region Defaults

		/// <summary>
		/// Gets the default value of the <see cref="InnerColorFocused"/> property.
		/// </summary>
		protected virtual Color DefaultInnerColorFocused
		{
			get { return Color.Green; }
		}

		/// <summary>
		/// Gets the default value of the <see cref="InnerColorUnfocused"/> property.
		/// </summary>
		protected virtual Color DefaultInnerColorUnfocused
		{
			get { return Color.Red; }
		}

		/// <summary>
		/// Gets the default value of the <see cref="InnerColorFocused"/> property.
		/// </summary>
		protected virtual Color DefaultForeColorFocused
		{
			get { return Color.White; }
		}

		/// <summary>
		/// Gets the default value of the <see cref="InnerColorUnfocused"/> property.
		/// </summary>
		protected virtual Color DefaultForeColorUnfocused
		{
			get { return Color.Black; }
		}

		/// <summary>
		/// Indicates the designer whether <see cref="InnerColorFocused"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeInnerColorFocused()
		{
			return _innerColorFocused != DefaultInnerColorFocused;
		}

		/// <summary>
		/// Indicates the designer whether <see cref="InnerColorUnfocused"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeInnerColorUnfocused()
		{
			return _innerColorUnfocused != DefaultInnerColorUnfocused;
		}

		/// <summary>
		/// Indicates the designer whether <see cref="ForeColorFocused"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeForeColorFocused()
		{
			return _foreColorFocused != DefaultForeColorFocused;
		}

		/// <summary>
		/// Indicates the designer whether <see cref="ForeColorUnfocused"/> needs
		/// to be serialized.
		/// </summary>
		protected virtual bool ShouldSerializeForeColorUnfocused()
		{
			return _foreColorUnfocused != DefaultForeColorUnfocused;
		}

		#endregion

		#endregion

		#region Privates

		private void OnAnimationIntervallChanged(object sender, System.EventArgs e)
		{
			OnAnimationIntervallChanged(e);
		}

		private void OnAnimationStepSizeChanged(object sender, System.EventArgs e)
		{
			OnAnimationStepSizeChanged(e);
		}

		private void OnAnimationStarted(object sender, System.EventArgs e)
		{
			OnAnimationStarted(e);
		}

		private void OnAnimationFinished(object sender, System.EventArgs e)
		{
			OnAnimationFinished(e);
		}

//		private void CheckCursorLocation()
//		{
//			Point cursorPos = base.PointToClient(Cursor.Position);
//			bool isMouseOver = !(cursorPos.X < 0 || cursorPos.Y < 0
//				|| cursorPos.X >= base.Width || cursorPos.Y >= base.Height);
//
//			if (isMouseOver == _isFocused)
//				return;
//
//			if (isMouseOver)
//			{
//				_borderWidthAnimator.EndWidth = _borderWidthFocused;
//				_innerColorAnimator.EndColor = _innerColorFocused;
//				_foreColorAnimator.EndColor = _foreColorFocused;
//			} 
//			else 
//			{
//				_borderWidthAnimator.EndWidth = _borderWidthUnfocused;
//				_innerColorAnimator.EndColor = _innerColorUnfocused;
//				_foreColorAnimator.EndColor = _foreColorUnfocused;
//			}
//			if (_isAnimationEnabled)
//				_innerColorAnimator.Start();
//
//			_isFocused = isMouseOver;
//			OnIsFocusedChanged(EventArgs.Empty);
//		}

		#endregion
	}
}

