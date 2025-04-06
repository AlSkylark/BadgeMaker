using BadgeMaker.Services;
using BadgeMaker.ViewModels;
using Microsoft.Extensions.Logging;

namespace BadgeMaker;

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
        builder.Services.AddSingleton<IWordService, WordService>();
        builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();

        return builder.Build();
    }
}
