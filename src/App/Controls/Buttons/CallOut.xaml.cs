using System.Windows.Input;
using App.Resources.Styles;

namespace App.Controls.Buttons;

public partial class CallOut : ContentView
{
	#region properties

	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CallOut), string.Empty, BindingMode.OneTime);

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(CallOut), IconFont.ChevronRight, BindingMode.OneTime);

	public string Icon
	{
		get => (string)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}

	public static readonly BindableProperty IconFontSizeProperty = BindableProperty.Create(nameof(IconFontSize), typeof(double), typeof(CallOut), 26d, BindingMode.OneTime);

	public double IconFontSize
	{
		get => (double)GetValue(IconFontSizeProperty);
		set => SetValue(IconFontSizeProperty, value);
	}

	public static readonly BindableProperty IconStartProperty = BindableProperty.Create(nameof(IconStart), typeof(string), typeof(CallOut), string.Empty, BindingMode.OneTime);

	public string IconStart
	{
		get => (string)GetValue(IconStartProperty);
		set => SetValue(IconStartProperty, value);
	}

	public static readonly BindableProperty IconStartFontSizeProperty = BindableProperty.Create(nameof(IconStartFontSize), typeof(double), typeof(CallOut), 26d, BindingMode.OneTime);

	public double IconStartFontSize
	{
		get => (double)GetValue(IconStartFontSizeProperty);
		set => SetValue(IconStartFontSizeProperty, value);
	}

	public static readonly BindableProperty TextPositionProperty = BindableProperty.Create(nameof(TextPosition), typeof(TextPositionEnum), typeof(CallOut), TextPositionEnum.Default, BindingMode.OneTime);

	public TextPositionEnum TextPosition
	{
		get => (TextPositionEnum)GetValue(TextPositionProperty);
		set => SetValue(TextPositionProperty, value);
	}

	public static readonly BindableProperty ColourModeProperty = BindableProperty.Create(nameof(ColourMode), typeof(ColourModeEnum), typeof(CallOut), ColourModeEnum.Gradient, BindingMode.OneTime);

	public ColourModeEnum ColourMode
	{
		get => (ColourModeEnum)GetValue(ColourModeProperty);
		set => SetValue(ColourModeProperty, value);
	}

	public static readonly BindableProperty AccessibilityNameProperty = BindableProperty.Create(nameof(AccessibilityName), typeof(string), typeof(CallOut), null, BindingMode.OneTime);

	public string AccessibilityName
	{
		get => (string)GetValue(AccessibilityNameProperty);
		set => SetValue(AccessibilityNameProperty, value);
	}

	#endregion properties

	#region Events

	public event EventHandler? Clicked;

	#endregion Events

	#region Commands

	public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(CallOut), null, BindingMode.OneTime);

	public ICommand ClickedCommand
	{
		get => (ICommand)GetValue(ClickedCommandProperty);
		set => SetValue(ClickedCommandProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="ClickedCommandParameter"/> property.
	/// </summary>
	public static readonly BindableProperty ClickedCommandParameterProperty = BindableProperty.Create(nameof(ClickedCommandParameter), typeof(object), typeof(CallOut));

	/// <summary>
	/// Property that gets returned when  <see cref="ClickedCommand" /> is executed. This is a bindable property.
	/// </summary>
	public object ClickedCommandParameter
	{
		get => GetValue(ClickedCommandParameterProperty);
		set => SetValue(ClickedCommandParameterProperty, value);
	}

	#endregion Commands

	public CallOut()
	{
		InitializeComponent();
	}

	void StateButton_Clicked(object sender, EventArgs e)
	{
		Clicked?.Invoke(this, EventArgs.Empty);
		ClickedCommand?.Execute(ClickedCommandParameter);
	}
}

public enum TextPositionEnum
{
	Centre,
	Default
}

public enum ColourModeEnum
{
	Solid,
	Gradient
}