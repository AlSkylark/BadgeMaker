using BadgeMaker.Commands;
using System.ComponentModel;
using System.Windows.Input;

namespace BadgeMaker.ViewModels;

public interface IMainPageViewModel : INotifyPropertyChanged
{
    BadgeViewModel BadgeViewModel { get; set; }
    ICommand NavigateCommand { get; }
    BaseCommand PrintBadgeCommand { get; init; }
}
