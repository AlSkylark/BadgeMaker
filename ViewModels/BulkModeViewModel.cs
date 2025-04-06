using BadgeMaker.Commands;
using BadgeMaker.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BadgeMaker.ViewModels;

public partial class BulkModeViewModel : BaseViewModel
{
    private ObservableCollection<Badge> _badges =
    [
        new() { Company = "Test" },
        new() { Company = "Erino" },
    ];

    public ObservableCollection<Badge> Badges
    {
        get { return _badges; }
        set
        {
            _badges = value;
            OnPropertyChanged(nameof(Badges));
        }
    }

    public ICommand NavigateCommand { get; } = new NavigateCommand();
}
