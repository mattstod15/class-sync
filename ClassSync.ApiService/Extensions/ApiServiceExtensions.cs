using System;
using ClassSync.ApiService.Service;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace ClassSync.ApiService.Extensions;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddSyllabusServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register business services
        services.AddScoped<PdfExtractionService>();
        services.AddScoped<SyllabusExtractionService>();

        // Centralized Kernel registration
        services.AddScoped<Kernel>(sp =>
        {
            var builder = Kernel.CreateBuilder();
            builder.AddOllamaChatCompletion(
                modelId: configuration["Ollama:ModelId"] ?? "mistral-nemo",
                endpoint: new Uri(configuration["Ollama:Endpoint"] ?? "http://localhost:11434")
            );
            return builder.Build();
        });

        return services;
    }
}
