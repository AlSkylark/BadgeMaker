using System.ComponentModel;
using System.Windows.Input;
using BadgeMaker.Commands;

namespace BadgeMaker.ViewModels;

public interface IMainPageViewModel : INotifyPropertyChanged
{
    BadgeViewModel BadgeViewModel { get; set; }
    ICommand NavigateCommand { get; }
    BaseCommand PrintBadgeCommand { get; init; }
    ICommand LoadTemplateCommand { get; init; }
    ICommand ThemeChangeCommand { get; }
}
