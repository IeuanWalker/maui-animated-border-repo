using System.Runtime.CompilerServices;

namespace App.Controls.Inputs;

public partial class Entry : ContentView
{
	#region Properties

	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Entry), null, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(Entry), string.Empty, BindingMode.OneTime);

	public string Placeholder
	{
		get => (string)GetValue(PlaceholderProperty);
		set => SetValue(PlaceholderProperty, value);
	}

	public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(Entry), ReturnType.Default, BindingMode.OneTime);

	public ReturnType ReturnType
	{
		get => (ReturnType)GetValue(ReturnTypeProperty);
		set => SetValue(ReturnTypeProperty, value);
	}

	public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(Entry), default(bool), BindingMode.OneTime);

	public bool IsPassword
	{
		get => (bool)GetValue(IsPasswordProperty);
		set => SetValue(IsPasswordProperty, value);
	}

	public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(Entry), TextTransform.Default, BindingMode.OneTime);

	public TextTransform TextTransform
	{
		get => (TextTransform)GetValue(TextTransformProperty);
		set => SetValue(TextTransformProperty, value);
	}

	public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(Entry), Keyboard.Default, BindingMode.OneTime, coerceValue: (_, v) => (Keyboard)v ?? Keyboard.Default);

	public Keyboard Keyboard
	{
		get => (Keyboard)GetValue(KeyboardProperty);
		set => SetValue(KeyboardProperty, value);
	}

	public static readonly BindableProperty ErrorProperty = BindableProperty.Create(nameof(Error), typeof(bool), typeof(Entry), false, BindingMode.TwoWay, null);

	public bool Error
	{
		get => (bool)GetValue(ErrorProperty);
		set => SetValue(ErrorProperty, value);
	}

	public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(Entry), int.MaxValue, BindingMode.OneTime);

	public int MaxLength
	{
		get => (int)GetValue(MaxLengthProperty);
		set => SetValue(MaxLengthProperty, value);
	}

	public static readonly BindableProperty ErrorsProperty = BindableProperty.Create(nameof(Errors), typeof(ICollection<string>), typeof(Entry), new List<string>(), BindingMode.TwoWay);

	public ICollection<string> Errors
	{
		get => (ICollection<string>)GetValue(ErrorsProperty);
		set => SetValue(ErrorsProperty, value);
	}

	#endregion Properties

	#region Events

	public event EventHandler? Completed;

	public event EventHandler? TextChanged;

	#endregion Events

	public Entry()
	{
		InitializeComponent();
	}

	void Handle_Focused(object sender, FocusEventArgs e)
	{
		if(string.IsNullOrEmpty(Text))
		{
			TransitionToTitle();
		}

		EntryFrame.SetDynamicResource(Border.StrokeProperty, Errors.Count > 0 ? "WarningColor" : "EntryBorderColour");
		HiddenLabel.SetDynamicResource(Label.TextColorProperty, "TextSecondaryColour");
	}

	void Handle_Unfocused(object sender, FocusEventArgs e)
	{
		if(string.IsNullOrEmpty(Text))
		{
			TransitionToPlaceholder();
		}

		EntryFrame.SetDynamicResource(Border.StrokeProperty, Errors.Count > 0 ? "WarningColor" : "UnfocusedColour");
		HiddenLabel.SetDynamicResource(Label.TextColorProperty, "TextPrimaryColour");
	}

	void Handle_Completed(object sender, EventArgs e)
	{
		Completed?.Invoke(this, e);
	}

	void EntryField_TextChanged(object sender, TextChangedEventArgs e)
	{
		TextChanged?.Invoke(this, e);
	}

	void TransitionToTitle()
	{
		HiddenLabel.Opacity = 0;
		HiddenLabel.IsVisible = true;

		Animation toTitleAnimation = new()
		{
			{ 0.5, 1, new Animation(_ => HiddenLabel.Opacity = _, 0, 1, Easing.Linear) },
			{ 0, 1, new Animation(_ => HiddenLabel.TranslationY = _, 75, 0, Easing.Linear) },
		};
		toTitleAnimation.Commit(this, nameof(toTitleAnimation), 16, 200, finished: (_, _) => EntryField.Placeholder = null);
	}

	void TransitionToPlaceholder()
	{
		Animation toPlaceholderAnimation = new()
		{
			{ 0, 0.7, new Animation(_ => HiddenLabel.Opacity = _, 1, 0, Easing.Linear) },
			{ 0, 1, new Animation(_ => HiddenLabel.TranslationY = _, 0, 75, Easing.Linear) },
		};
		toPlaceholderAnimation.Commit(this, nameof(toPlaceholderAnimation), 16, 200, finished: (_, _) =>
		{
			HiddenLabel.IsVisible = false;
			EntryField.Placeholder = Placeholder;
		});
	}

	static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
	{
		if(bindable is not Entry control)
		{
			return;
		}

		if(control.EntryField.IsFocused)
		{
			return;
		}

		if(!string.IsNullOrEmpty((string)newValue))
		{
			control.TransitionToTitle();
		}
		else
		{
			control.TransitionToPlaceholder();
		}
	}

	protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		base.OnPropertyChanged(propertyName);

		if(propertyName == nameof(IsEnabled) && EntryFrame is not null)
		{
			EntryFrame.IsEnabled = IsEnabled;
		}

		if(EntryFrame is null)
		{
			return;
		}

		if(Errors.Count > 0)
		{
			EntryFrame.SetDynamicResource(Border.StrokeProperty, "WarningColor");
		}
		else if(EntryField.IsFocused)
		{
			EntryFrame.SetDynamicResource(Border.StrokeProperty, "EntryBorderColour");
		}
		else
		{
			EntryFrame.SetDynamicResource(Border.StrokeProperty, "UnfocusedColour");
		}
	}
}