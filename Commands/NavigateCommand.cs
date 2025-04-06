namespace BadgeMaker.Commands;

public class NavigateCommand : BaseCommand
{
    public override async void Execute(object? parameter)
    {
        if (parameter is string page)
        {
            await Shell.Current.GoToAsync($"//{page}");
        }
    }
}
