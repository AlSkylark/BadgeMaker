namespace BadgeMaker.Controls;

public partial class MainControls : ContentView
{
    public static readonly BindableProperty NavigateToProperty = BindableProperty.Create(
        nameof(NavigateTo),
        typeof(string),
        typeof(string),
        null
    );

    public string NavigateTo
    {
        get => (string)GetValue(MainControls.NavigateToProperty);
        set => SetValue(MainControls.NavigateToProperty, value);
    }

    public static readonly BindableProperty NavigateButtonTextProperty = BindableProperty.Create(
        nameof(NavigateButtonText),
        typeof(string),
        typeof(string),
        null
    );

    public string NavigateButtonText
    {
        get => (string)GetValue(MainControls.NavigateButtonTextProperty);
        set => SetValue(MainControls.NavigateButtonTextProperty, value);
    }

    public MainControls()
    {
        InitializeComponent();

        var serviceProvider = IPlatformApplication.Current!.Services;
        var viewModel = serviceProvider.GetRequiredService<IMainControlsViewModel>();
        BindingContext = viewModel;
    }
}
