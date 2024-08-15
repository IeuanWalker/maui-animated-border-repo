namespace App.Routes;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

	async void BtnInputRepo_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new InputRepoPage());
	}

	async void BtnButtonRepo_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ButtonRepoPage());
	}
}