using maui_project.Pages;

namespace maui_project;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Homepage), typeof(Homepage));
	}
}
