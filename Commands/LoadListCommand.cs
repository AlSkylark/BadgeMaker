using System.Collections.ObjectModel;
using System.Globalization;
using BadgeMaker.Models;
using BadgeMaker.ViewModels;
using CsvHelper;

namespace BadgeMaker.Commands;

public class LoadListCommand(ObservableCollection<BadgeViewModel> badges) : BaseCommand
{
    public override void Execute(object? parameter)
    {
        Dispatcher
            .GetForCurrentThread()
            ?.Dispatch(async () =>
            {
                var result = await PickFile();
                if (result is not null)
                {
                    badges.Clear();
                    LoadList(result);
                }
            });
    }

    private void LoadList(FileResult result)
    {
        if (Path.GetExtension(result.FileName) == ".csv")
        {
            using var reader = new StreamReader(result.FullPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Badge>().ToList();
            foreach (var record in records)
            {
                badges.Add(new BadgeViewModel(record));
            }
        }
        else { }
    }

    private static async Task<FileResult?> PickFile()
    {
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Select an CSV or Excel file",
                FileTypes = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, [".csv", ".xlsx", ".xls"] },
                    }
                ),
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result is not null)
            {
                return result;
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
