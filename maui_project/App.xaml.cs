using maui_project.Pages;

namespace maui_project;

public partial class App : Application
{
	public static string CurrentUserId { get; set; }
	public App()
	{
		InitializeComponent();
		  System.Diagnostics.Debug.Print("App.xaml.cs - CurrentUserId: " + CurrentUserId);
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
	  public void SetCurrentUser(string userId)
    {
        CurrentUserId = userId; // ตั้งค่า userId
    }
}