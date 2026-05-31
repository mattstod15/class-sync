using System;
using UglyToad.PdfPig;

namespace ClassSync.ApiService.Service;

public class PdfExtractionService
{
   public string ExtractText(Stream pdfStream)
    {
        using PdfDocument document = PdfDocument.Open(pdfStream);
        var text = string.Empty;

        foreach (var page in document.GetPages())
        {
            text += page.Text + " ";
        }

        return text.Trim();
    }
}
