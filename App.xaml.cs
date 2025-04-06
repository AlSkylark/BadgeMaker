using BadgeMaker.Services;

namespace BadgeMaker;

public partial class App : Application
{
    private readonly IServiceProvider serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        this.serviceProvider = serviceProvider;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
        window.Destroying += CleanUpWord;
        return window;
    }

    protected override void OnStart()
    {
        base.OnStart();
        AppDomain.CurrentDomain.ProcessExit += CleanUpWord;
        AppDomain.CurrentDomain.UnhandledException += CleanUpWord;
    }

    private void CleanUpWord(object? sender, EventArgs e)
    {
        serviceProvider.GetService<IWordService>()?.Dispose();
    }
}
