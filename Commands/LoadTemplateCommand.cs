using BadgeMaker.Services;
using BadgeMaker.Stores;

namespace BadgeMaker.Commands;

public class LoadTemplateCommand : BaseCommand
{
    private readonly ISettingsService settings;
    private readonly IWordService wordService;
    private readonly ITemplateErrorStore store;

    public LoadTemplateCommand(
        ISettingsService settings,
        IWordService wordService,
        ITemplateErrorStore store
    )
    {
        this.settings = settings;
        this.wordService = wordService;
        this.store = store;

        SetUpTemplate(settings.DefaultTemplatePath);
    }

    public override void Execute(object? parameter)
    {
        Dispatcher
            .GetForCurrentThread()
            ?.Dispatch(async () =>
            {
                var result = await PickFile();
                SetUpTemplate(result);
            });
    }

    private void SetUpTemplate(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        store.TemplateErrors.Clear();
        var errors = wordService.CheckTemplate(path);
        foreach (var error in errors)
        {
            store.TemplateErrors.Add(error);
        }

        if (path == settings.DefaultTemplatePath)
        {
            store.IsDefault = true;
        }
        else
        {
            store.IsDefault = false;
        }

        settings.TemplatePath = path;
    }

    private static async Task<string> PickFile()
    {
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Select a Word template",
                FileTypes = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, [".dotx"] },
                    }
                ),
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result is not null)
            {
                return result.FullPath;
            }

            return string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
