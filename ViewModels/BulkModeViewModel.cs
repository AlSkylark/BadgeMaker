using System.Collections.ObjectModel;
using System.Windows.Input;
using BadgeMaker.Commands;
using BadgeMaker.Services;
using BadgeMaker.Stores;

namespace BadgeMaker.ViewModels;

public partial class BulkModeViewModel : BaseViewModel, IBulkModeViewModel
{
    public BulkModeViewModel(IWordService wordService, ITemplateErrorStore store)
    {
        PrintAllCommand = new PrintAllCommand(Badges, wordService, store);
        PrintBadgeCommand = new PrintBadgeCommand(() => SelectedBadge, wordService, store);

        Badges.CollectionChanged += OnCollectionChange;
    }

    private void OnCollectionChange(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e
    )
    {
        OnPropertyChanged(nameof(HasNoBadges));
        PrintAllCommand.ChangeCanExecute();
    }

    private BadgeViewModel _selectedBadge = new();
    public BadgeViewModel SelectedBadge
    {
        get { return _selectedBadge; }
        set
        {
            _selectedBadge = value;
            OnPropertyChanged(nameof(SelectedBadge));
            PrintBadgeCommand.ChangeCanExecute();
        }
    }

    public bool HasNoBadges => Badges.Count == 0;

    public ObservableCollection<BadgeViewModel> Badges { get; set; } = [];

    public BaseCommand PrintBadgeCommand { get; init; }
    public BaseCommand PrintAllCommand { get; init; }

    public ICommand LoadListCommand => new LoadListCommand(Badges);
    public ICommand DeleteCommand =>
        new Command(() =>
        {
            if (SelectedBadge is null)
            {
                return;
            }

            var badge = SelectedBadge;

            var next =
                Badges.ElementAtOrDefault(Badges.IndexOf(badge) + 1)
                ?? Badges.ElementAtOrDefault(Badges.IndexOf(badge) - 1);

            SelectedBadge = next;

            Badges.Remove(badge);
        });
}
