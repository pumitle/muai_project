using maui_project.ViewModel;

namespace maui_project.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel(); 
	}
	
}