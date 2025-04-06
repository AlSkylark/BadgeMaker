using BadgeMaker.Models;
using Word = Microsoft.Office.Interop.Word;

namespace BadgeMaker.Services;

public class WordService : IWordService
{
    private readonly Word.Application _wordApp;

    public WordService()
    {
        _wordApp = new Word.Application();
    }

    public bool PrintBadge(Badge badge)
    {
        //do the printing
        string filePath = Path.Combine(Environment.CurrentDirectory, "BadgeTemplate.dotx");
        try
        {
            Word.Document doc = _wordApp.Documents.Add(filePath);

            PopulateControls("FullName", doc, $"{badge.FullName}");
            PopulateControls("CompanyName", doc, $"{badge.Company}");
            PopulateControls("BadgeType", doc, $"{badge.Type}");

            doc.PrintOut();
            doc.Close(false);
            return true;
        }
        catch (Exception)
        {
            return false;
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
