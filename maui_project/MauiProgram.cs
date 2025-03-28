﻿using maui_project.Pages;
using maui_project.ViewModel;
using Microsoft.Extensions.Logging;

namespace maui_project;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<LoginViewModel>();
		
		return builder.Build();
	}
}
