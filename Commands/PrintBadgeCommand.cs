using BadgeMaker.Models;
using BadgeMaker.Services;
using System.Diagnostics;

namespace BadgeMaker.Commands;

public class PrintBadgeCommand(Badge badge, IWordService wordService) : BaseCommand
{
    public override void Execute(object? parameter)
    {
        //call word service
        Debug.WriteLine($"Printing! {badge.FullName}, {badge.Company}, {badge.Type}");
        wordService.PrintBadge(badge);
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(badge.FullName);
    }
}
