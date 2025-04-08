using System.Diagnostics;
using BadgeMaker.Models;
using BadgeMaker.Services;
using BadgeMaker.Stores;

namespace BadgeMaker.Commands;

public class PrintBadgeCommand(Badge badge, IWordService wordService, ITemplateErrorStore store)
    : BaseCommand
{
    public override void Execute(object? parameter)
    {
        //call word service
        Debug.WriteLine($"Printing! {badge.FullName}, {badge.Company}, {badge.Type}");
        wordService.PrintBadge(badge);
    }

    public override bool CanExecute(object? parameter)
    {
        var test =
            !string.IsNullOrWhiteSpace(badge.FullName)
            && !store.TemplateErrors.Any(te => te.IsCritical);
        Debug.WriteLine(test);
        return test;
    }
}
