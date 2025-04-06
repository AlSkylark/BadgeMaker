using BadgeMaker.Commands;
using BadgeMaker.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace BadgeMaker.ViewModels;

public partial class MainPageViewModel : BaseViewModel, IMainPageViewModel
{
    public MainPageViewModel(IWordService wordService)
    {
        BadgeViewModel = new() { Type = "Speaker" };
        BadgeViewModel.PropertyChanged += OnBadgeChanged;
        PrintBadgeCommand = new PrintBadgeCommand(BadgeViewModel.Badge, wordService);
    }

    private void OnBadgeChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(BadgeViewModel));
        PrintBadgeCommand.ChangeCanExecute();
    }

    public BadgeViewModel BadgeViewModel { get; set; }

    public ICommand NavigateCommand { get; } = new NavigateCommand();
    public BaseCommand PrintBadgeCommand { get; init; }
}
