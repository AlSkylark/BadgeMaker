namespace BadgeMaker.Views;

public partial class TemplateInstructionsWindow : Window
{
    public TemplateInstructionsWindow()
    {
        InitializeComponent();
    }

    public void OnClose(object sender, EventArgs e)
    {
        Application.Current.CloseWindow(this);
    }
}
