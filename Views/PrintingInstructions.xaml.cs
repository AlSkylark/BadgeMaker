namespace BadgeMaker.Views;

public partial class PrintingInstructions : Window
{
    public PrintingInstructions()
    {
        InitializeComponent();
    }

    public void OnClose(object sender, EventArgs e)
    {
        Application.Current.CloseWindow(this);
    }
}
