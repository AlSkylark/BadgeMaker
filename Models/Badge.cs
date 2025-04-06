namespace BadgeMaker.Models;

public class Badge
{
    public string? FullName { get; set; }
    public string? Company { get; set; }
    public string? Type { get; set; }
    public bool IsPrinted { get; set; }
    public bool Printing { get; set; }
}
