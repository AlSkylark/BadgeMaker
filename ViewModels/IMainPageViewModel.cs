using System.ComponentModel;
using BadgeMaker.Commands;

namespace BadgeMaker.ViewModels;

public interface IMainPageViewModel : INotifyPropertyChanged
{
    BadgeViewModel BadgeViewModel { get; set; }
    BaseCommand PrintBadgeCommand { get; init; }
}
