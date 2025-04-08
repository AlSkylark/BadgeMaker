using System.ComponentModel;
using System.Windows.Input;
using BadgeMaker.Commands;
using BadgeMaker.Services;
using BadgeMaker.Stores;

namespace BadgeMaker.ViewModels;

public partial class MainPageViewModel : BaseViewModel, IMainPageViewModel
{
    private readonly ITemplateErrorStore _templateErrorStore;

    public MainPageViewModel(
        IWordService wordService,
        ISettingsService settings,
        ITemplateErrorStore templateErrorStore
    )
    {
        BadgeViewModel = new() { Type = "SPEAKER" };
        BadgeViewModel.PropertyChanged += OnBadgeChange;
        PrintBadgeCommand = new PrintBadgeCommand(
            BadgeViewModel.Badge,
            wordService,
            templateErrorStore
        );
        LoadTemplateCommand = new LoadTemplateCommand(settings, wordService, templateErrorStore);
        _templateErrorStore = templateErrorStore;
        _templateErrorStore.PropertyChanged += OnTemplateErrorsChange;
    }

    public ITemplateErrorStore TemplateErrors => _templateErrorStore;

    private void OnBadgeChange(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(BadgeViewModel));
        PrintBadgeCommand.ChangeCanExecute();
    }

    private void OnTemplateErrorsChange(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(TemplateErrors));
        PrintBadgeCommand.ChangeCanExecute();
    }

    public BadgeViewModel BadgeViewModel { get; set; }

    public ICommand NavigateCommand { get; } = new NavigateCommand();
    public BaseCommand PrintBadgeCommand { get; init; }
    public ICommand LoadTemplateCommand { get; init; }

    public ICommand ThemeChangeCommand =>
        new Command(execute: () =>
        {
            Application.Current!.UserAppTheme =
                Application.Current.RequestedTheme == AppTheme.Light
                || Application.Current.RequestedTheme == AppTheme.Unspecified
                    ? AppTheme.Dark
                    : AppTheme.Light;
        });
}
