#if IOS
using App.Controls.HandleOverrides;
#endif

using CommunityToolkit.Maui;
using IeuanWalker.Maui.StateButton;
using IeuanWalker.Maui.Switch;
using Microsoft.Extensions.Logging;

namespace App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		MauiAppBuilder builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("fa-solid-900.ttf", "FontAwesomeRegular");
				fonts.AddFont("fa-light-300.ttf", "FontAwesomeLight");
				fonts.AddFont("cardiff-logo.ttf", "CardiffLogo");
				fonts.AddFont("Inter-Regular.ttf", "InterRegular");
			})
			.UseMauiCommunityToolkit()
			.UseSwitch()
			.UseStateButton();

#if IOS
		//BorderHandler.Override(builder);
#endif

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}