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
    public delegate void Printing();
    public event Printing? PrintStarting;
    public event Printing? PrintFinishing;

    public bool PrintCancelled { get; set; }

    public override async void Execute(object? parameter)
    {
        PrintStarting?.Invoke();

        var dispatcher = Dispatcher.GetForCurrentThread();

        await Task.Run(() =>
        {
            foreach (var badge in badges)
            {
                if (PrintCancelled)
                {
                    CancelPrint(dispatcher!, badge);
                    break;
                }

                PrintBadge(dispatcher!, badge);
            }
        });

        PrintFinishing?.Invoke();
    }

    private void PrintBadge(IDispatcher dispatcher, BadgeViewModel badge)
    {
        dispatcher?.Dispatch(() => badge.Printing = true);
        var result = wordService.PrintBadge(badge.Badge);
        dispatcher?.Dispatch(() =>
        {
            badge.Printing = false;
            badge.IsPrinted = result;
        });
    }

    private void CancelPrint(IDispatcher dispatcher, BadgeViewModel badge)
    {
        dispatcher?.Dispatch(() =>
        {
            badge.Printing = false;
            badge.IsPrinted = false;
        });

        PrintCancelled = false;
    }

    public override bool CanExecute(object? parameter)
    {
        return badges.Any() && !store.TemplateErrors.Any(te => te.IsCritical);
    }
}
