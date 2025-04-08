namespace BadgeMaker.Services;

public interface ISettingsService
{
    string DefaultTemplatePath { get; }
    string TemplatePath { get; set; }
}
