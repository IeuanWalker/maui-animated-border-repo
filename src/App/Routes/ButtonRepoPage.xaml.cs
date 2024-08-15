namespace App.Routes;

public partial class ButtonRepoPage : ContentPage
{
	public ButtonRepoPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await Task.Delay(1000);

		repoButton.IsVisible = true;
	}
}