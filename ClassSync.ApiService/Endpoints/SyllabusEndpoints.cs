using System;
using ClassSync.ApiService.Service;

namespace ClassSync.ApiService.Endpoints;

public static class SyllabusEndpoints
{
    public static void MapSyllabusEndpoints(this IEndpointRouteBuilder app)
    {
        // Define a route group for all syllabus-related operations
        var group = app.MapGroup("/api/syllabus");

        group.MapPost("/upload", async (
            IFormFile file,
            PdfExtractionService pdfSvc,
            SyllabusExtractionService syllabusSvc) =>
        {
            if (file.Length == 0) return Results.BadRequest("File is empty.");

            // 1. Extract text from PDF
            using var stream = file.OpenReadStream();
            var rawText = pdfSvc.ExtractText(stream);

            // 2. Extract structured data via AI
            var jsonResult = await syllabusSvc.ExtractAsync(rawText);

            return Results.Ok(jsonResult);
        });
    }
}
