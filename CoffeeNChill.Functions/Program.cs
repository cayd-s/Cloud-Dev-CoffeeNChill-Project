using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoffeeNChill.Functions.Interfaces;
using CoffeeNChill.Functions.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();


builder.Services.AddSingleton<ITableStorageService, TableStorageService>();
builder.Services.AddSingleton<IFileStorageService, FileStorageService>();

builder.Build().Run();
