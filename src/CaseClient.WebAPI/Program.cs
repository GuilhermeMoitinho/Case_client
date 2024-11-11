using CaseClient.WebAPI.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using CaseClient.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); 

builder.AddOpenApi().AddDependency();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", op =>
    {
        op.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Production");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddInitialData();

app.Run();
