using BadgeMaker.Views;

namespace BadgeMaker;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("singlemode", typeof(MainPage));
        Routing.RegisterRoute("bulkmode", typeof(BulkMode));
    }
}
