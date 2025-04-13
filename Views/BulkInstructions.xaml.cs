namespace BadgeMaker.Views;

public partial class BulkInstructions : Window
{
    public BulkInstructions()
    {
        InitializeComponent();
    }

    public void OnClose(object sender, EventArgs e)
    {
        Application.Current.CloseWindow(this);
    }
}
