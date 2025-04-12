using BadgeMaker.Models;

namespace BadgeMaker.ViewModels;

public partial class BadgeViewModel : BaseViewModel
{
    private readonly Badge _badge = new();
    public Badge Badge => _badge;

    public BadgeViewModel() { }

    public BadgeViewModel(Badge badge)
    {
        FullName = badge.FullName;
        Company = badge.Company;
        Type = badge.Type;
    }

    private string? _fullName;
    public string? FullName
    {
        get { return _fullName; }
        set
        {
            _fullName = value;
            _badge.FullName = _fullName;
            OnPropertyChanged(nameof(FullName));
        }
    }

    private string? _company;
    public string? Company
    {
        get { return _company; }
        set
        {
            _company = value;
            _badge.Company = _company;
            OnPropertyChanged(nameof(Company));
        }
    }

    private string? _type;
    public string? Type
    {
        get { return _type; }
        set
        {
            _type = value;
            _badge.Type = _type;
            OnPropertyChanged(nameof(Type));
        }
    }

    private bool _isPrinted;
    public bool IsPrinted
    {
        get { return _isPrinted; }
        set
        {
            _isPrinted = value;
            _badge.IsPrinted = _isPrinted;
            OnPropertyChanged(nameof(IsPrinted));
        }
    }

    private bool _printing;
    public bool Printing
    {
        get { return _printing; }
        set
        {
            _printing = value;
            _badge.Printing = _printing;
            OnPropertyChanged(nameof(Printing));
        }
    }
}
