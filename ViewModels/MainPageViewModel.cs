using System.ComponentModel;
using System.Windows.Input;
using BadgeMaker.Commands;
using BadgeMaker.Services;
using BadgeMaker.Stores;
using BadgeMaker.Views;

namespace BadgeMaker.ViewModels;

public partial class MainPageViewModel : BaseViewModel, IMainPageViewModel
{
    public MainPageViewModel(IWordService wordService, ITemplateErrorStore templateErrorStore)
    {
        BadgeViewModel = new() { Type = "SPEAKER" };
        BadgeViewModel.PropertyChanged += OnBadgeChange;
        PrintBadgeCommand = new PrintBadgeCommand(BadgeViewModel, wordService, templateErrorStore);
    }

    private void OnBadgeChange(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(BadgeViewModel));
        PrintBadgeCommand.ChangeCanExecute();
    }

    private void OnTemplateErrorsChange(object? sender, PropertyChangedEventArgs e)
    {
        PrintBadgeCommand.ChangeCanExecute();
    }

    public BadgeViewModel BadgeViewModel { get; set; }

    public BaseCommand PrintBadgeCommand { get; init; }

    public ICommand OpenPrinterInfoCommand =>
        new Command(() =>
        {
            var window = new PrintingInstructions();
            Application.Current.OpenWindow(window);
        });
}
