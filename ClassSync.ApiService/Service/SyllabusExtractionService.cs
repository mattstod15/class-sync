using System;
using Microsoft.SemanticKernel;

namespace ClassSync.ApiService.Service;

public class SyllabusExtractionService(Kernel kernel)
{
    private readonly Kernel _kernel = kernel;

    public async Task<string> ExtractAsync(string rawSyllabusText)
    {
        // We will define our Prompt Template here in the next step
        string prompt = $@"
            You are an expert academic assistant. 
            Extract the following course details from the text provided.
            Output ONLY valid JSON matching the schema: 
            {{ 
                ""CourseName"": string, 
                ""Assignments"": [{{ ""Title"": string, ""Date"": string }}] 
            }}
            
            Text to process:
            {rawSyllabusText}";
            
        var result = await _kernel.InvokePromptAsync(prompt, new() { ["input"] = rawSyllabusText });
        return result.ToString();
    }
}
