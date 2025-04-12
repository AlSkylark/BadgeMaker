using BadgeMaker.Controls;
using BadgeMaker.Services;
using BadgeMaker.Stores;
using BadgeMaker.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BadgeMaker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IWordService, WordService>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddSingleton<ITemplateErrorStore, TemplateErrorStore>();
        builder.Services.AddSingleton<IMainControlsViewModel, MainControlsViewModel>();
        builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();
        builder.Services.AddSingleton<IBulkModeViewModel, BulkModeViewModel>();

        return builder.Build();
    }
}
