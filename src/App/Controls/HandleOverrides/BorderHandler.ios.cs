using IeuanWalker.Maui.StateButton;
using IeuanWalker.Maui.StateButton.Handler;
using IeuanWalker.Maui.StateButton.Platform;
using Microsoft.Maui.Platform;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace App.Controls.HandleOverrides;

/// <summary>
/// TODO: Future - Remove once issue is fixed
/// <para>https://github.com/dotnet/maui/issues/24104</para>
/// <para>https://cardiffcouncilict.visualstudio.com/Cardiff%20App/_sprints/taskboard/Cardiff%20App%20Team/Cardiff%20App/Sprint%20123?workitem=39871</para>
/// </summary>
static class BorderHandler
{
	public static void Override(MauiAppBuilder builder)
	{
		builder.ConfigureMauiHandlers(handlers =>
		{
			handlers.AddHandler<Border, NotAnimatedBorderHandler>();
			handlers.AddHandler<StateButton, NotAnimatedStateButtonBorderHandler>();
		});
	}
}

public class NotAnimatedBorderHandler : Microsoft.Maui.Handlers.BorderHandler
{
	class BorderContentView : ContentView
	{
		public override void LayoutSubviews()
		{
			// This is the only workaround I found to avoid the animation when the border size is updated.
			// https://github.com/dotnet/maui/issues/15363
			// https://github.com/dotnet/maui/issues/18204

			if(Layer.Sublayers?.FirstOrDefault(layer => layer is MauiCALayer) is { AnimationKeys: not null } caLayer)
			{
				caLayer.RemoveAnimation("bounds");
				caLayer.RemoveAnimation("position");
			}

			base.LayoutSubviews();
		}
	}

	protected override ContentView CreatePlatformView()
	{
		_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a {nameof(ContentView)}");
		_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} cannot be null");

		return new BorderContentView
		{
			CrossPlatformLayout = VirtualView
		};
	}
}

public class NotAnimatedStateButtonBorderHandler : StateButtonHandler
{
	class BorderContentView : CustomContentView
	{
		public BorderContentView(IBorderView virtualView) : base(virtualView)
		{
		}

		public override void LayoutSubviews()
		{
			// This is the only workaround I found to avoid the animation when the border size is updated.
			// https://github.com/dotnet/maui/issues/15363
			// https://github.com/dotnet/maui/issues/18204

			if(Layer.Sublayers?.FirstOrDefault(layer => layer is MauiCALayer) is { AnimationKeys: not null } caLayer)
			{
				caLayer.RemoveAnimation("bounds");
				caLayer.RemoveAnimation("position");
			}

			base.LayoutSubviews();
		}
	}

	protected override CustomContentView CreatePlatformView()
	{
		_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a {nameof(ContentView)}");
		_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} cannot be null");

		return new BorderContentView(VirtualView)
		{
			CrossPlatformLayout = VirtualView
		};
	}
}