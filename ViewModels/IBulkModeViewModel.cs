using System.Collections.ObjectModel;
using System.ComponentModel;
using BadgeMaker.Commands;

namespace BadgeMaker.ViewModels;

public interface IBulkModeViewModel : INotifyPropertyChanged
{
    ObservableCollection<BadgeViewModel> Badges { get; set; }
    BaseCommand PrintAllCommand { get; init; }
    BaseCommand PrintBadgeCommand { get; init; }
}
