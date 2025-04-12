using System.ComponentModel;
using System.Windows.Input;
using BadgeMaker.Stores;

namespace BadgeMaker.Controls;

public interface IMainControlsViewModel : INotifyPropertyChanged
{
    ITemplateErrorStore TemplateErrors { get; }
    ICommand NavigateCommand { get; }
    ICommand LoadTemplateCommand { get; init; }
    ICommand ThemeChangeCommand { get; }
}
