using System.ComponentModel;
using System.Windows.Input;
using BadgeMaker.Commands;
using BadgeMaker.Services;
using BadgeMaker.Stores;
using BadgeMaker.ViewModels;

namespace BadgeMaker.Controls
{
    class MainControlsViewModel : BaseViewModel, IMainControlsViewModel
    {
        private readonly ITemplateErrorStore _templateErrorStore;

        public MainControlsViewModel(
            IWordService wordService,
            ISettingsService settings,
            ITemplateErrorStore templateErrorStore
        )
        {
            LoadTemplateCommand = new LoadTemplateCommand(
                settings,
                wordService,
                templateErrorStore
            );
            _templateErrorStore = templateErrorStore;
            _templateErrorStore.PropertyChanged += OnTemplateErrorsChange;
        }

        public ITemplateErrorStore TemplateErrors => _templateErrorStore;

        private void OnTemplateErrorsChange(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TemplateErrors));
        }

        public ICommand LoadTemplateCommand { get; init; }
        public ICommand NavigateCommand => new NavigateCommand();
        public ICommand ThemeChangeCommand => new ThemeChangeCommand();
    }
}
