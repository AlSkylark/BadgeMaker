using BadgeMaker.Services;
using BadgeMaker.Stores;
using BadgeMaker.ViewModels;

namespace BadgeMaker.Commands;

public class PrintAllCommand(
    IEnumerable<BadgeViewModel> badges,
    IWordService wordService,
    ITemplateErrorStore store
) : BaseCommand
{
    public override async void Execute(object? parameter)
    {
        var dispatcher = Dispatcher.GetForCurrentThread();

        foreach (var badge in badges)
        {
            dispatcher?.Dispatch(() => badge.Printing = true);
            var result = await Task.Run(() => wordService.PrintBadge(badge.Badge));
            dispatcher?.Dispatch(() =>
            {
                badge.Printing = false;
                badge.IsPrinted = result;
            });
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return badges.Any() && !store.TemplateErrors.Any(te => te.IsCritical);
    }
}
