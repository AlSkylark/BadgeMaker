using BadgeMaker.ViewModels;

namespace BadgeMaker.Views;

public partial class MainPage : ContentPage
{
    public MainPage(IMainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
