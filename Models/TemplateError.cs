namespace BadgeMaker.Models;

public class TemplateError
{
    public required string Error { get; set; }
    public bool IsCritical { get; set; }
}
