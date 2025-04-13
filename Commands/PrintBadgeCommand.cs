using BadgeMaker.Services;
using BadgeMaker.Stores;
using BadgeMaker.ViewModels;

namespace BadgeMaker.Commands;

public class PrintBadgeCommand : BaseCommand
{
    private readonly BadgeViewModel? _badge;
    private readonly Func<BadgeViewModel>? getBadge;
    private readonly IWordService wordService;
    private readonly ITemplateErrorStore store;

    public PrintBadgeCommand(
        BadgeViewModel badge,
        IWordService wordService,
        ITemplateErrorStore store
    )
    {
        _badge = badge;
        this.wordService = wordService;
        this.store = store;
    }

    public PrintBadgeCommand(
        Func<BadgeViewModel> getBadge,
        IWordService wordService,
        ITemplateErrorStore store
    )
    {
        this.getBadge = getBadge;
        this.wordService = wordService;
        this.store = store;
    }

    public override async void Execute(object? parameter)
    {
        var badge = _badge ?? getBadge!();

        var dispatcher = Dispatcher.GetForCurrentThread();
        dispatcher?.Dispatch(() => badge.Printing = true);
        var result = await Task.Run(() => wordService.PrintBadge(badge.Badge));
        dispatcher?.Dispatch(() =>
        {
            badge.Printing = false;
            badge.IsPrinted = result;
        });
    }

    public override bool CanExecute(object? parameter)
    {
        var badge = _badge ?? getBadge!();

        return badge is not null
            && NoControlIsNull(badge)
            && !store.TemplateErrors.Any(te => te.IsCritical);
    }

    private static bool NoControlIsNull(BadgeViewModel badge)
    {
        return !string.IsNullOrWhiteSpace(badge.FullName)
            || !string.IsNullOrWhiteSpace(badge.Company)
            || !string.IsNullOrWhiteSpace(badge.Type);
    }
}
