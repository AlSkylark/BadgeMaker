using System.Collections.ObjectModel;
using BadgeMaker.Models;
using BadgeMaker.ViewModels;

namespace BadgeMaker.Stores;

public class TemplateErrorStore : BaseViewModel, ITemplateErrorStore
{
    public ObservableCollection<TemplateError> TemplateErrors { get; set; } = [];

    private bool _isDefault;
    public bool IsDefault
    {
        get { return _isDefault; }
        set
        {
            _isDefault = value;
            OnPropertyChanged(nameof(IsDefault));
        }
    }
}
