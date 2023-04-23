using Cff.Extensions.AspNetCore.TestServer.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<SendMailDto>, SendMailDtoValidator>();
builder.Host.UseClearModelValidatorMVC();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.MapPost("/mail", SendMail)
   .Accepts<SendMailDto>("application/json")
   .WithOpenApi();

await app.RunAsync();


public partial class Program { }
