using BadgeMaker.Models;
using Word = Microsoft.Office.Interop.Word;

namespace BadgeMaker.Services;

public class WordService : IWordService
{
    private readonly Word.Application _wordApp;
    private readonly ISettingsService _settings;

    private const string TAG_FULLNAME = "FullName";
    private const string TAG_COMPANYNAME = "CompanyName";
    private const string TAG_BADGETYPE = "BadgeType";

    public WordService(ISettingsService settings)
    {
        _wordApp = new Word.Application();
        _settings = settings;
    }

    public bool PrintBadge(Badge badge)
    {
        //do the printing
        string filePath = _settings.TemplatePath;

        try
        {
            Word.Document doc = _wordApp.Documents.Add(filePath);

            PopulateControls(TAG_FULLNAME, doc, $"{badge.FullName}");
            PopulateControls(TAG_COMPANYNAME, doc, $"{badge.Company}");
            PopulateControls(TAG_BADGETYPE, doc, $"{badge.Type}");

            doc.PrintOut();
            doc.Close(false);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public IEnumerable<TemplateError> CheckTemplate(string path)
    {
        List<TemplateError> errors = [];

        try
        {
            Word.Document doc = _wordApp.Documents.Add(path);
            if (doc is null)
            {
                errors.Add(new() { Error = "Template could not be loaded", IsCritical = true });
                return errors;
            }
            CheckControls(TAG_FULLNAME, doc, errors);
            CheckControls(TAG_COMPANYNAME, doc, errors);
            CheckControls(TAG_BADGETYPE, doc, errors);

            if (errors.Count == 3)
            {
                errors.Clear();
                errors.Add(
                    new()
                    {
                        Error = "Template has none of the required ContentControls",
                        IsCritical = true,
                    }
                );
            }

            doc.Close(false);
            return errors;
        }
        catch (Exception)
        {
            errors.Add(
                new() { Error = "Something went wrong loading template", IsCritical = true }
            );
            return errors;
        }
    }

    private static void CheckControls(string tag, Word.Document doc, List<TemplateError> errors)
    {
        var controls = doc.SelectContentControlsByTag(tag);
        if (controls is null || controls?.Count == 0)
        {
            errors.Add(
                new()
                {
                    Error = $"{tag} ContentControl tag was not found in template",
                    IsCritical = false,
                }
            );
        }
    }

    private static void PopulateControls(string tag, Word.Document doc, string text)
    {
        foreach (Word.ContentControl control in doc.SelectContentControlsByTag(tag))
        {
            control.Range.Text = text;
        }
    }

    public void Dispose()
    {
        _wordApp.Quit();
    }
}
