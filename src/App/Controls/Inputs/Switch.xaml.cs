using App.Resources.Styles;
using IeuanWalker.Maui.Switch;
using IeuanWalker.Maui.Switch.Helpers;

namespace App.Controls.Inputs;

public partial class Switch : CustomSwitch
{
	public Switch()
	{
		InitializeComponent();

		SwitchPanUpdate += (_, e) =>
		{
			Color trueColor = Color.FromArgb("#086c6d");
			Color falseColor = Application.Current?.Resources.GetResource<Color>("SwitchFalseColor") ?? Colors.Transparent;

			//Color Animation
			Color fromColor = IsToggled ? trueColor : falseColor;
			Color toColor = IsToggled ? falseColor : trueColor;

			if((int)e.Percentage == 100 || e.Percentage == 0)
			{
				if(IsToggled)
				{
					BackgroundColor = trueColor;
				}
				else
				{
					SetDynamicResource(BackgroundColorProperty, "SwitchFalseColor");
				}
			}
			else
			{
				double t = e.Percentage * 0.01;
				BackgroundColor = ColorAnimationUtil.ColorAnimation(fromColor, toColor, t);
			}
		};
	}
}