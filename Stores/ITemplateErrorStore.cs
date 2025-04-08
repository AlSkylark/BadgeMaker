using System.Collections.ObjectModel;
using System.ComponentModel;
using BadgeMaker.Models;

namespace BadgeMaker.Stores;

public interface ITemplateErrorStore : INotifyPropertyChanged
{
    public ObservableCollection<TemplateError> TemplateErrors { get; set; }
    public bool IsDefault { get; set; }
}
