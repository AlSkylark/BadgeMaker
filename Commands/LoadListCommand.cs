using System.Collections.ObjectModel;
using System.Globalization;
using BadgeMaker.Models;
using BadgeMaker.ViewModels;
using CsvHelper;
using MiniExcelLibs;

namespace BadgeMaker.Commands;

public class LoadListCommand(ObservableCollection<BadgeViewModel> badges) : BaseCommand
{
    public override async void Execute(object? parameter)
    {
        var result = await PickFile();
        if (result is not null)
        {
            badges.Clear();
            await LoadList(result);
        }
    }

    private async Task LoadList(FileResult result)
    {
        List<Badge> results = [];
        var extension = Path.GetExtension(result.FileName);

        if (extension == ".csv")
        {
            results = await Task.Run(() => LoadCsv(result));
        }
        else if (extension == ".xlsx")
        {
            results = await Task.Run(() => LoadExcel(result));
        }

        Dispatcher
            .GetForCurrentThread()
            ?.Dispatch(() =>
            {
                foreach (var record in results)
                {
                    badges.Add(new BadgeViewModel(record));
                }
            });
    }

    private static List<Badge> LoadCsv(FileResult result)
    {
        using var reader = new StreamReader(result.FullPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Badge>().ToList();
        return records;
    }

    private static List<Badge> LoadExcel(FileResult result)
    {
        var rows = MiniExcel.Query<Badge>(result.FullPath).ToList();
        return rows;
    }

    private static async Task<FileResult?> PickFile()
    {
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Select an CSV or Excel (.xlsx) file",
                FileTypes = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, [".csv", ".xlsx"] },
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
