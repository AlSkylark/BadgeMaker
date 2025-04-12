using BadgeMaker.ViewModels;

namespace BadgeMaker.Views;

public partial class BulkMode : ContentPage
{
    public BulkMode(IBulkModeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
