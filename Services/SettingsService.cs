namespace BadgeMaker.Services;

public class SettingsService : ISettingsService
{
    private readonly string _defaultTemplatePath = Path.Combine(
        Environment.CurrentDirectory,
        "BadgeTemplate.dotx"
    );
    public string DefaultTemplatePath => _defaultTemplatePath;

    public string TemplatePath
    {
        get => Preferences.Default.Get(nameof(TemplatePath), _defaultTemplatePath);
        set => Preferences.Default.Set(nameof(TemplatePath), value);
    }
}
