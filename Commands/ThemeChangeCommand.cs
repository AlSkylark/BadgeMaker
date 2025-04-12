namespace BadgeMaker.Commands;

public class ThemeChangeCommand : BaseCommand
{
    public override void Execute(object? parameter)
    {
        Application.Current!.UserAppTheme =
            Application.Current.RequestedTheme == AppTheme.Light
            || Application.Current.RequestedTheme == AppTheme.Unspecified
                ? AppTheme.Dark
                : AppTheme.Light;
    }
}
