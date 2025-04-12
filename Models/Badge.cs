using CsvHelper.Configuration.Attributes;

namespace BadgeMaker.Models;

public class Badge
{
    [Index(0)]
    public string? FullName { get; set; }

    [Index(1)]
    public string? Company { get; set; }

    [Index(2)]
    public string? Type { get; set; }

    [Ignore]
    public bool IsPrinted { get; set; }

    [Ignore]
    public bool Printing { get; set; }
}
